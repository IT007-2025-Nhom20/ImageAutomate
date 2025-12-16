using ImageAutomate.Core;
using ImageAutomate.StandardBlocks;

namespace Scratch;
public class Program
{
    public static void Main()
    {
        LoadBlock loadBlock = new LoadBlock();
        loadBlock.SourcePath = "C:\\Users\\phanm\\OneDrive\\Pictures";
        
        foreach (var item in loadBlock.Execute(new Dictionary<Socket, IReadOnlyList<IBasicWorkItem>>()))
        {
            if (item.Value.Count > 0)
            {
                Console.WriteLine(item.Key.Id);
                foreach (var data in item.Value)
                {
                    if (data is WorkItem workItem)
                        Console.WriteLine($"{workItem.Metadata["FileName"]}\n");
                }    
            }
        }    
    }
}