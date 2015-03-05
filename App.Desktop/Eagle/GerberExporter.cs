using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace Walle.Eagle
{
    public class GerberExporter
    {
        private LedBoardBuilder _board;
        private string _zip;

        public GerberExporter(string destZipFile, LedBoardBuilder board)
        {
            _zip = destZipFile;
            _board = board;
        }

        private string CreateBoardFile()
        {
            var boardFile = Path.GetTempFileName() + ".brd";
            //  var boardFile = "test.brd";
            using (var file = new StreamWriter(boardFile))
            {
                _board.ToXml().Save(file);
            }
            return boardFile;
        }

        private Process Autoroute(string boardFile)
        {
            var pri = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = @"C:\EAGLE-7.2.0\bin\eagle.exe",
                Arguments = String.Format(@" -C 'ripup *; auto *; write; quit;' {0}", boardFile)
            };
            return Process.Start(pri);
        }

        public void Export()
        {
            var boardFile = CreateBoardFile();
            Autoroute(boardFile).WaitForExit();
            Gerberify(boardFile);
        }

        private void Gerberify(string boardFile)
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
            ZipFile.CreateFromDirectory(gerberDir, _zip);
        }
    }
}