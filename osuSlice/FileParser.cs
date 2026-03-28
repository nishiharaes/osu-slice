using System.Diagnostics;
using System.IO;

namespace WpfApp1;

public class FileParser
{
    
    public static string GetOsuDir()
    {
        string dir = null;

        while (dir == null)
        {
            try
            {
                var processes = Process.GetProcessesByName("osu!");

                if (processes.Length > 0)
                {
                    dir = processes[0].Modules[0].FileName;
                    dir = dir.Remove(dir.LastIndexOf('\\'));
                    return dir;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            Thread.Sleep(1000);
        }
        return string.Empty;
    }

    public static void DebugMessage(string msg, string type)
    {
        bool debug = string.Equals(type, "debug", StringComparison.OrdinalIgnoreCase);
        bool info =  string.Equals(type, "info", StringComparison.OrdinalIgnoreCase);
        bool warning = string.Equals(type, "warning", StringComparison.OrdinalIgnoreCase);
        bool error = string.Equals(type, "error", StringComparison.OrdinalIgnoreCase);
        
        if (debug)
        {
            Console.WriteLine("DEBUG " + msg);
        }

        else if (info)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("INFO: " + msg);
        }

        else if (warning)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("WARNING: " + msg);
        }

        else if (error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: " + msg);
        }

        else
        {
            Console.WriteLine("Incorrect or no debug type specified.");
            Console.WriteLine(msg);
        }
    }
    
    
}