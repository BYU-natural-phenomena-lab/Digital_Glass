using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace Walle.Eagle
{
    public class GerberExporter : EagleExporter
    {
        private string _zip;

        public GerberExporter(string destZipFile, LedBoardBuilder board)
            :base(GetTempBoardFile(), board)
        {
            _zip = destZipFile;
        }

        private static string GetTempBoardFile()
        {
            return Path.GetTempFileName() + ".brd";
        }

        public override bool Export()
        {
            if (!base.Export())
            {
                return false;
            }
            Gerberify(BoardFile, _zip);
            return true;
        }

        private static void Gerberify(string boardFile, string destZipFile)
        {
            var gerberDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(gerberDir);
            var job = new SeeedStudioCamJob(boardFile, gerberDir);
            foreach (var step in job.StepArguments)
            {
                var pri = new ProcessStartInfo
                {
                    CreateNoWindow = true,
                    FileName = @"C:\EAGLE-7.2.0\bin\eaglecon.exe",
                    Arguments = String.Format(@" -X {0}", step)
                };
                Console.WriteLine(pri.Arguments);
                Process.Start(pri).WaitForExit();
            }
            ZipFile.CreateFromDirectory(gerberDir, destZipFile);
        }
    }
}