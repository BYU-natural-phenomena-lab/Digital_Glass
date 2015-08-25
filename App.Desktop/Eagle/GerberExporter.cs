using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using DigitalGlass.Properties;

namespace DigitalGlass.Eagle
{
    /// <summary>
    /// Generates the files for GERBER PCB assembly.
    /// This launches a separate process (eaglecon.exe). This is configured in app.config <seealso cref="Settings"/>
    /// This will first create a temporary Eagle board file and then execute the SeeedStudioCamJob <seealso cref="SeeedStudioCamJob"/>
    /// </summary>
    public class GerberExporter : EagleExporter
    {
        private string _zip;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destZipFile">The location to save a zip file of the GERBER files</param>
        /// <param name="board">The model from which to generate the board</param>
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
                    FileName = Settings.Default.EagleConsoleExe,
                    Arguments = String.Format(@" -X {0}", step)
                };
                Console.WriteLine(pri.Arguments);
                Process.Start(pri).WaitForExit();
            }
            ZipFile.CreateFromDirectory(gerberDir, destZipFile);
        }
    }
}