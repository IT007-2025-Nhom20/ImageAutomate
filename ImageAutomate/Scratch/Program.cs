using ImageAutomate.Core;
using ImageAutomate.StandardBlocks;
using System.Runtime.CompilerServices;

namespace Scratch;
public class Program
{
    public static void Main()
    {
        LoadBlock loadBlock = new LoadBlock();
        loadBlock.SourcePath = "C:\\Users\\Admin\\Pictures\\Test";
        
        foreach (var item in loadBlock.LoadFromFolderInternal())
        {
            if (item != null)
            {
                Console.WriteLine(item.Id);
                foreach (var data in item.Metadata)
                {
                    Console.WriteLine($"{data.Key}: {data.Value}\n");
                }    
            }
        }    
    }
}