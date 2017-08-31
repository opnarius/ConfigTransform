using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.Publishing.Tasks;

namespace TransformerCli
{
    class Program
    {
        static void Main(string[] args)
        {
            Options options = new Options();
            var optionParseResults = CommandLine.Parser.Default.ParseArguments(args, options);
            if (!optionParseResults)
            {
                return;
            }

            var baseFileInfo = new FileInfo(options.BaseConfig);
            var transformFileInfo = new FileInfo(options.TransformConfig);

            var myBasePath = new FileInfo($".\\workspace\\{baseFileInfo.Name}");
            var myTransformPath = new FileInfo($".\\workspace\\{transformFileInfo.Name}");
            var myResultPath = new FileInfo($".\\workspace\\{baseFileInfo.Name.Replace(baseFileInfo.Extension, "")}_Result{baseFileInfo.Extension}");


            CopyFile(baseFileInfo, myBasePath);
            CopyFile(transformFileInfo, myTransformPath);

            var diffCommand = ConfigurationManager.AppSettings["diffCommand"];
            var diffCommandArgs = ConfigurationManager.AppSettings["diffCommand:Args"];
            var diffCommandWait = bool.Parse(ConfigurationManager.AppSettings["diffCommand:Wait"]);

            var transformer = new TransformXml
            {
                BuildEngine = new TransformBuildEngine(),
                SourceRootPath = @"./workspace/",
                Source = myBasePath.Name,
                Transform = myTransformPath.Name,
                Destination = myResultPath.FullName
            };

            var transformationSucceeded = transformer.Execute();

            if (transformationSucceeded)
            {
                var processInfo = new ProcessStartInfo(diffCommand);
                processInfo.Arguments = diffCommandArgs.Replace("${Source}", myBasePath.FullName)
                    .Replace("${Result}", myResultPath.FullName);

                Console.WriteLine($"\"{processInfo.FileName}\" {processInfo.Arguments}");

                var process = Process.Start(processInfo);

                if (diffCommandWait)
                {
                    process?.WaitForExit();
                }
            }

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        static void CopyFile(FileInfo sourceInfo, FileInfo destinationInfo)
        {
            destinationInfo.Directory.Create();

            File.Copy(sourceInfo.FullName, destinationInfo.FullName, true);
        }
    }
}
