using System;
using System.IO;
using System.Linq;

namespace NoxVela.Protocol.Generator;

class Program
{
    static int Main(string[] args)
    {
        try
        {
            string rootProtocolDir = string.Empty;
            bool generateHtml = args.Any(a => a.Equals("--html", StringComparison.OrdinalIgnoreCase) || a.Equals("-h", StringComparison.OrdinalIgnoreCase))
                || Environment.GetEnvironmentVariable("GENERATE_ASYNCAPI_HTML") == "true";

            for (int i = 0; i < args.Length; i++)
            {
                if ((args[i].Equals("--version", StringComparison.OrdinalIgnoreCase) || args[i].Equals("-v", StringComparison.OrdinalIgnoreCase)) && i + 1 < args.Length)
                {
                    Environment.SetEnvironmentVariable("PROTOCOL_RELEASE_VERSION", args[i + 1]);
                }
            }

            var dirArg = args.FirstOrDefault(a => !a.StartsWith("-"));
            if (!string.IsNullOrWhiteSpace(dirArg))
            {
                rootProtocolDir = Path.GetFullPath(dirArg);
            }
            else
            {
                var current = new DirectoryInfo(AppContext.BaseDirectory);
                while (current != null && !File.Exists(Path.Combine(current.FullName, "NoxVela.sln")))
                {
                    current = current.Parent;
                }

                if (current == null)
                {
                    Console.Error.WriteLine("[AsyncApiGenerator] Could not locate protocol root directory containing NoxVela.sln.");
                    return 1;
                }

                rootProtocolDir = current.FullName;
            }

            AsyncApiDocumentBuilder.Generate(rootProtocolDir, generateHtml);
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[AsyncApiGenerator] Fatal error: {ex.Message}\n{ex.StackTrace}");
            return 1;
        }
    }
}
