import subprocess
import xml.etree.ElementTree as ET
import os
import sys
import argparse
import signal
import shutil

# Configuration
# Force absolute path to ensure we know exactly where the file is
TRX_FILE = os.path.abspath("stress_test_temp.trx")

results = {}  # { "TestName": { "pass": 0, "fail": 0 } }
run_count = 0
failed = False

def signal_handler(sig, frame):
    print("\n\nStopping... Generating report.")
    print_report()
    cleanup()
    sys.exit(0)

def cleanup():
    if os.path.exists(TRX_FILE):
        try:
            os.remove(TRX_FILE)
        except:
            pass

def print_report():
    print(f"\n--- Stress Test Report (Runs: {run_count}) ---")
    if not results:
        print("No results collected. (Did the build fail?)")
        return

    print(f"{'Test Case':<60} | {'Pass':<6} | {'Fail':<6} | {'Fail %':<8}")
    print("-" * 90)

    # Sort by failure count descending
    sorted_tests = sorted(
        results.items(), 
        key=lambda item: (item[1]['fail'], item[0]), 
        reverse=True
    )

    for name, stats in sorted_tests:
        total = stats['pass'] + stats['fail']
        fail_rate = (stats['fail'] / total) * 100 if total > 0 else 0.0
        
        display_name = (name[:57] + '..') if len(name) > 57 else name
        row = f"{display_name:<60} | {stats['pass']:<6} | {stats['fail']:<6} | {fail_rate:.1f}%"
        
        if stats['fail'] > 0:
            print(f"\033[91m{row}\033[0m") 
        else:
            print(row)

def parse_trx(file_path):
    global failed
    try:
        tree = ET.parse(file_path)
        root = tree.getroot()

        # Namespace agnostic search
        # We iterate everything and check the tag name string ending
        for elem in root.iter():
            if elem.tag.endswith('UnitTestResult'):
                test_name = elem.get('testName')
                outcome = elem.get('outcome') # 'Passed', 'Failed'

                if test_name not in results:
                    results[test_name] = {'pass': 0, 'fail': 0}

                if outcome == 'Passed':
                    results[test_name]['pass'] += 1
                elif outcome == 'Failed':
                    results[test_name]['fail'] += 1
                    failed = True

    except ET.ParseError:
        print(f"[!] XML Parse Error on run {run_count}")

def main():
    global run_count, failed

    parser = argparse.ArgumentParser()
    parser.add_argument('--runs', type=int, default=10,
                        help='Number of times to run the tests (ignored if --run-until-fail is used)')
    parser.add_argument('--run-until-fail', action='store_true',
                        help='Run tests repeatedly until a failure occurs')
    parser.add_argument('--filter', type=str, default='',
                        help='Filter for tests (e.g., "FullyQualifiedName=MyTestMethod" or just "MyTestMethod")')
    parser.add_argument('--project', type=str, default='',
                        help='Path to the test project file (required when using --filter with just a method name)')
    parser.add_argument('-c', '--configuration', type=str, default='Debug',
                        help='Build configuration (Debug or Release)')

    args = parser.parse_args()

    signal.signal(signal.SIGINT, signal_handler)

    # Note the usage of LogFileName with an absolute path
    cmd = [
        "dotnet", "test",
        "--configuration", args.configuration,
        "--logger", f"trx;LogFileName={TRX_FILE}"
    ]

    if args.project:
        cmd.insert(2, args.project)

    if args.filter:
        # If the filter doesn't contain '=', treat it as a method name only
        if '=' not in args.filter:
            # Use FullyQualifiedName filter with the provided method name
            filter_value = f"FullyQualifiedName={args.filter}"
            cmd.extend(["--filter", filter_value])
        else:
            cmd.extend(["--filter", args.filter])

    print(f"Targeting Log File: {TRX_FILE}")
    if args.run_until_fail:
        print("Mode: Run until failure detected")
    else:
        print(f"Mode: Run {args.runs} times")
    print("Starting...")

    while True:
        if not args.run_until_fail and args.runs > 0 and run_count >= args.runs:
            break

        if args.run_until_fail and failed:
            break
            
        run_count += 1
        print(f"Run {run_count}...", end='\r')
        
        # We capture stderr to debug build failures
        proc = subprocess.run(cmd, stdout=subprocess.DEVNULL, stderr=subprocess.PIPE)
        
        if os.path.exists(TRX_FILE):
            parse_trx(TRX_FILE)
            os.remove(TRX_FILE)
        else:
            # If TRX is missing, dotnet test failed to run tests (Build error? Bad filter?)
            print(f"\n[!] Run {run_count} failed to produce results.")
            print("STDERR Output:")
            print(proc.stderr.decode())
            # Don't loop infinitely if the build is broken
            break

    print_report()
    cleanup()

if __name__ == "__main__":
    main()