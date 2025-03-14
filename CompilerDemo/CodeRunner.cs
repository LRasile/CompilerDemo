using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

public class CodeRunner
{
    private const string OutputDir = "GeneratedProgram";

    public async Task Run(string code, string fileName)
    {
        Directory.CreateDirectory(OutputDir);

        var exeFileName = fileName + ".exe";
        var csFileName = fileName + ".cs";

        string csFilePath = Path.Combine(OutputDir, csFileName);
        string exeFilePath = Path.Combine(OutputDir, exeFileName);

        await File.WriteAllTextAsync(csFilePath, code);
        Console.WriteLine($"Generated C# file: {csFilePath}");

        Console.WriteLine("Compiling...");

        // need to debug this here really
        var processInfo = new ProcessStartInfo
        {
            Arguments = $"\"{csFilePath}\" -out:\"{exeFilePath}\"",
            FileName = "csc.exe",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        };

        using (var process = Process.Start(processInfo))
        {
            string output = await process!.StandardOutput.ReadToEndAsync();
            string errors = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                Console.WriteLine("Compilation failed.");
                Console.WriteLine(errors);
                return;
            }
        }

        Console.WriteLine("Compilation successful! Running program...\n");

        // Run the compiled executable
        var runProcess = Process.Start(new ProcessStartInfo
        {
            FileName = Path.Combine(OutputDir, exeFileName),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        });

        if (runProcess != null)
        {
            string output = await runProcess.StandardOutput.ReadToEndAsync();
            string error = await runProcess.StandardError.ReadToEndAsync();
            await runProcess.WaitForExitAsync();

            if (!string.IsNullOrWhiteSpace(output))
                Console.WriteLine(output);

            if (!string.IsNullOrWhiteSpace(error))
                Console.WriteLine("Runtime errors:\n" + error);
        }
        else
        {
            Console.WriteLine("Failed to run the compiled program.");
        }
    }
}
