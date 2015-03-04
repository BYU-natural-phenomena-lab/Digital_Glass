using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;
using Walle.Eagle;
using Walle.Model;

namespace ConsoleApplication1
{
    class Program
    {
        static string CreateBoard()
        {
            var boardFile = Path.GetTempFileName() + ".brd";
          //  var boardFile = "test.brd";
            using (var file = new StreamWriter(boardFile))
            {
                var board = new LedBoardBuilder(100, 100);
                board.AddLedAtPoint(90, 90);
                board.AddLedAtPoint(90, 30);
                board.AddLedAtPoint(90, 20);
                board.AddLedAtPoint(61, 32);
                board.ToXml().Save(file);
            }
            return boardFile;
        }

        static Process Autoroute(string boardFile)
        {
            var pri = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = @"C:\EAGLE-7.2.0\bin\eagle.exe",
                Arguments = String.Format(@" -C 'ripup *; auto *; write; quit;' {0}", boardFile)
            };
            return Process.Start(pri);
        }
        static void Main(string[] args)
        {
            var boardFile = CreateBoard();
            Console.WriteLine("Generating circuit board at \n\t{0}", boardFile);
            Autoroute(boardFile).WaitForExit();
            Gerberify(boardFile);
            Console.WriteLine("Finished. Press any key to exit ...");
            Console.ReadKey();
        }

        private static void Gerberify(string boardFile)
        {
            var gerberDir = Path.Combine(Path.GetTempPath(),Path.GetRandomFileName());
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
            var zip = Path.GetTempFileName() + ".zip";
            Console.WriteLine("Making zip : {0}",zip);
            ZipFile.CreateFromDirectory(gerberDir,zip);
        }
    }
}
