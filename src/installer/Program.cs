using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

class Program
{
    static void Main()
    {
        var appDir = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
        var extractDir = Path.Combine(appDir, "MicrosipTelegramApp");
        
        var embeddedZip = Path.Combine(AppContext.BaseDirectory, "app.zip");
        
        if (!File.Exists(embeddedZip))
        {
            Console.WriteLine("Error: app.zip not found inside the installer!");
            return;
        }
        
        Console.WriteLine("Installing to: " + extractDir);
        
        if (Directory.Exists(extractDir))
        {
            Directory.Delete(extractDir, true);
        }
        
        Console.WriteLine("Extracting...");
        ZipFile.ExtractToDirectory(embeddedZip, extractDir);
        
        Console.WriteLine("Starting application...");
        
        var startInfo = new ProcessStartInfo
        {
            WorkingDirectory = extractDir,
            FileName = "node",
            Arguments = ".next/server.js",
            UseShellExecute = true
        };
        
        Process.Start(startInfo);
        
        Console.WriteLine("Done! Application started!");
    }
}