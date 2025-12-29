import subprocess
import xml.etree.ElementTree as ET
import os
import sys
import argparse
import signal
import shutil
import time
import threading
import csv

# Optional psutil for memory monitoring
try:
    import psutil
except ImportError:
    psutil = None

# Configuration
TRX_FILE = os.path.abspath("temp_test_results.trx")
FAIL_LOG_FILE = os.path.abspath("failed_tests.log")

# Global State
results = {}  
run_count = 0
failed = False
monitor_active = False
memory_samples = []

def log_status(*args, **kwargs):
    """Helper to print to STDERR (console) instead of STDOUT (file pipe)"""
    print(*args, file=sys.stderr, **kwargs)

def signal_handler(sig, frame):
    log_status("\n\nStopping... Generating report.")
    print_report()
    cleanup()
    sys.exit(0)

def cleanup():
    if os.path.exists(TRX_FILE):
        try:
            os.remove(TRX_FILE)
        except OSError:
            pass

def monitor_memory_usage(pid, interval=0.1):
    global monitor_active, memory_samples
    memory_samples = []
    
    if not psutil:
        return

    try:
        process = psutil.Process(pid)
        while monitor_active:
            try:
                # Get RSS memory in MB
                mem = process.memory_info().rss / (1024 * 1024)
                
                # Check children (e.g., testhost.exe) recursively
                children = process.children(recursive=True)
                for child in children:
                    try:
                        mem += child.memory_info().rss / (1024 * 1024)
                    except (psutil.NoSuchProcess, psutil.AccessDenied):
                        pass

                memory_samples.append(mem)
            except (psutil.NoSuchProcess, psutil.AccessDenied):
                break
            time.sleep(interval)
    except psutil.NoSuchProcess:
        pass

def get_all_tests(project_path):
    log_status("Discovering tests...")
    if not project_path or not os.path.exists(project_path):
        log_status("Error: --project path is required and must exist for test discovery.")
        sys.exit(1)

    cmd = ["dotnet", "test", project_path, "--list-tests"]
    result = subprocess.run(cmd, capture_output=True, text=True)

    tests = []
    capture = False
    for line in result.stdout.splitlines():
        if "The following Tests are available:" in line:
            capture = True
            continue
        if capture and line.strip():
            tests.append(line.strip())

    log_status(f"Found {len(tests)} tests.")
    return tests

def parse_trx(file_path, record_failures):
    global failed
    current_run_stats = {} # { (class, test): outcome }

    try:
        tree = ET.parse(file_path)
        root = tree.getroot()
        
        # Map testId to ClassName
        test_id_map = {} 
        for elem in root.iter():
            if elem.tag.endswith('UnitTest'):
                test_id = elem.get('id')
                class_name = "Unknown"
                for child in elem:
                    if child.tag.endswith('TestMethod'):
                        class_name = child.get('className')
                        break
                if test_id:
                    test_id_map[test_id] = class_name

        # Parse Results
        for elem in root.iter():
            if elem.tag.endswith('UnitTestResult'):
                test_id = elem.get('testId')
                test_name = elem.get('testName')
                outcome = elem.get('outcome')

                class_name = test_id_map.get(test_id, "UnknownContainer")

                if class_name not in results:
                    results[class_name] = {}
                
                if test_name not in results[class_name]:
                    results[class_name][test_name] = {'pass': 0, 'fail': 0, 'memory': []}

                if outcome == 'Passed':
                    results[class_name][test_name]['pass'] += 1
                elif outcome == 'Failed':
                    results[class_name][test_name]['fail'] += 1
                    failed = True
                    if record_failures:
                        record_failure_details(elem, class_name, test_name)
                
                current_run_stats[(class_name, test_name)] = outcome

    except ET.ParseError:
        log_status(f"[!] XML Parse Error on run {run_count}")
    
    return current_run_stats

def record_failure_details(result_elem, class_name, test_name):
    error_msg = ""
    stack_trace = ""
    for output in result_elem:
        if output.tag.endswith('Output'):
            for error_info in output:
                if error_info.tag.endswith('ErrorInfo'):
                    for child in error_info:
                        if child.tag.endswith('Message'):
                            error_msg = child.text
                        elif child.tag.endswith('StackTrace'):
                            stack_trace = child.text

    try:
        with open(FAIL_LOG_FILE, "a", encoding="utf-8") as f:
            f.write(f"--- Run {run_count} Failure ---\n")
            f.write(f"Test: {class_name}.{test_name}\n")
            f.write(f"Timestamp: {time.strftime('%Y-%m-%d %H:%M:%S')}\n")
            f.write(f"Message:\n{error_msg}\n")
            f.write(f"Stack Trace:\n{stack_trace}\n")
            f.write("-" * 50 + "\n\n")
    except Exception as e:
        log_status(f"[!] Failed to record failure details: {e}")

def print_report(csv_file=None):
    # This remains on STDOUT so it can be piped
    print(f"\n--- Test Report (Total Runs: {run_count}) ---")
    if not results:
        print("No results collected.")
        return

    sorted_containers = sorted(results.keys())
    
    # Prepare CSV data
    csv_rows = []

    for container in sorted_containers:
        print(f"\nContainer: {container}")
        header = f"{'Test Case':<60} | {'Pass':<5} | {'Fail':<5} | {'Fail %':<7} | {'Max MB':<8} | {'Avg MB':<8}"
        print(header)
        print("-" * len(header))

        container_results = results[container]
        sorted_tests = sorted(
            container_results.items(), 
            key=lambda item: (item[1]['fail'], item[0]), 
            reverse=True
        )

        for name, stats in sorted_tests:
            total = stats['pass'] + stats['fail']
            fail_rate = (stats['fail'] / total) * 100 if total > 0 else 0.0
            
            # Memory stats calculation
            mem_max = 0
            mem_avg = 0
            if stats['memory']:
                all_maxes = [m['max'] for m in stats['memory']]
                all_avgs = [m['avg'] for m in stats['memory']]
                mem_max = max(all_maxes) if all_maxes else 0
                mem_avg = sum(all_avgs) / len(all_avgs) if all_avgs else 0

            display_name = (name[:57] + '..') if len(name) > 57 else name
            row = f"{display_name:<60} | {stats['pass']:<5} | {stats['fail']:<5} | {fail_rate:.1f}%   | {mem_max:<8.2f} | {mem_avg:<8.2f}"
            
            # Use sys.stdout check to see if we should colorize (don't colorize files)
            if stats['fail'] > 0 and sys.stdout.isatty():
                print(f"\033[91m{row}\033[0m") 
            else:
                print(row)
            
            if csv_file:
                csv_rows.append({
                    'Container': container,
                    'Test': name,
                    'Pass': stats['pass'],
                    'Fail': stats['fail'],
                    'FailPercent': f"{fail_rate:.2f}",
                    'MaxMB': f"{mem_max:.2f}",
                    'AvgMB': f"{mem_avg:.2f}",
                    'Samples': len(stats['memory'])
                })

    if csv_file and csv_rows:
        try:
            with open(csv_file, 'w', newline='') as f:
                fieldnames = ['Container', 'Test', 'Pass', 'Fail', 'FailPercent', 'MaxMB', 'AvgMB', 'Samples']
                writer = csv.DictWriter(f, fieldnames=fieldnames)
                writer.writeheader()
                writer.writerows(csv_rows)
            log_status(f"\nDetailed statistics saved to {csv_file}")
        except Exception as e:
            log_status(f"Error saving CSV: {e}")

def main():
    global run_count, failed, monitor_active, memory_samples

    parser = argparse.ArgumentParser(description="Unified Test Runner & Memory Monitor")
    parser.add_argument('--runs', type=int, default=1, help='Number of times to run the tests')
    parser.add_argument('--run-until-fail', action='store_true', help='Run tests repeatedly until a failure occurs')
    parser.add_argument('--filter', type=str, default='', help='Filter for tests')
    parser.add_argument('--project', type=str, default='', help='Path to the test project file')
    parser.add_argument('-c', '--configuration', type=str, default='Debug', help='Build configuration')
    parser.add_argument('--record-failed-results', action='store_true', help='Log failed test output')
    parser.add_argument('--monitor-memory', action='store_true', help='Enable memory monitoring (requires psutil)')
    parser.add_argument('--csv', type=str, default='', help='Path to save results CSV')
    parser.add_argument('--discover', action='store_true', help='Discover all tests in project and run them individually')

    args = parser.parse_args()

    signal.signal(signal.SIGINT, signal_handler)

    if args.monitor_memory and not psutil:
        log_status("Warning: --monitor-memory requested but 'psutil' module is not installed. Memory tracking disabled.")
        args.monitor_memory = False

    if args.discover and not args.project:
        log_status("Error: --discover requires --project")
        sys.exit(1)

    if args.record_failed_results and os.path.exists(FAIL_LOG_FILE):
        try:
            os.remove(FAIL_LOG_FILE)
        except:
            pass

    # Determine execution list
    test_queue = []
    if args.discover:
        test_queue = get_all_tests(args.project)
    else:
        test_queue = [None] # Single batch run

    log_status(f"Mode: {'Run Until Fail' if args.run_until_fail else f'Run {args.runs} times'}")
    log_status(f"Monitoring Memory: {'Yes' if args.monitor_memory else 'No'}")
    log_status("Starting execution...")

    total_queue_items = len(test_queue)
    
    for i, test_item in enumerate(test_queue):
        # Stop if we are in run-until-fail mode and a failure has occurred (global flag)
        if args.run_until_fail and failed:
            break

        current_run = 0
        while True:
            if not args.run_until_fail and args.runs > 0 and current_run >= args.runs:
                break
            if args.run_until_fail and failed:
                break

            current_run += 1
            run_count += 1
            
            # Build Command
            cmd = [
                "dotnet", "test",
                "--configuration", args.configuration,
                "--logger", f"trx;LogFileName={TRX_FILE}"
            ]
            
            if args.project:
                cmd.insert(2, args.project)

            # Filter Logic
            active_filter = ""
            if test_item:
                active_filter = f"FullyQualifiedName={test_item}"
            elif args.filter:
                active_filter = f"FullyQualifiedName={args.filter}" if '=' not in args.filter else args.filter
            
            if active_filter:
                cmd.extend(["--filter", active_filter])

            # Display Status (TO STDERR)
            status_msg = f"Run {current_run}"
            if args.discover:
                status_msg = f"[{i+1}/{total_queue_items}] {test_item[-40:] if test_item else 'Batch'} - {status_msg}"
            
            log_status(f"{status_msg:<80}", end='\r')

            # Execution with optional monitoring
            monitor_thread = None
            proc = None
            
            monitor_active = args.monitor_memory
            memory_samples = []

            try:
                # Use Popen to allow background monitoring
                proc = subprocess.Popen(cmd, stdout=subprocess.DEVNULL, stderr=subprocess.PIPE)
                
                if args.monitor_memory:
                    monitor_thread = threading.Thread(target=monitor_memory_usage, args=(proc.pid,))
                    monitor_thread.start()
                
                proc.wait()
            finally:
                monitor_active = False
                if monitor_thread:
                    monitor_thread.join()

            # Process Results
            mem_stat = {'max': 0, 'avg': 0}
            if args.monitor_memory and memory_samples:
                mem_stat['max'] = max(memory_samples)
                mem_stat['avg'] = sum(memory_samples) / len(memory_samples)

            if os.path.exists(TRX_FILE):
                parsed_tests = parse_trx(TRX_FILE, args.record_failed_results)
                os.remove(TRX_FILE)
                
                for (c_name, t_name) in parsed_tests.keys():
                    if results[c_name][t_name]['memory'] is not None:
                         results[c_name][t_name]['memory'].append(mem_stat)
            else:
                log_status(f"\n[!] Run {current_run} failed to produce results (crashed?).")
                break

    # Clean up the stderr status line
    log_status("")
    print_report(args.csv)
    cleanup()

if __name__ == "__main__":
    main()