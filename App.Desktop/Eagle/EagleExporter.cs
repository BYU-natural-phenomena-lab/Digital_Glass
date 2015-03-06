using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace Walle.Eagle
{
    public class EagleExporter
    {
        private LedBoardBuilder _board;
        protected string BoardFile;

        public EagleExporter(string destBoardFile, LedBoardBuilder board)
        {
            if (!destBoardFile.EndsWith(".brd"))
                destBoardFile += ".brd";
            BoardFile = destBoardFile;
            _board = board;
        }

        protected static bool CreateBoardFile(string boardFile, LedBoardBuilder board)
        {
            using (var file = new StreamWriter(boardFile))
            {
                board.ToXml().Save(file);
            }
            return true;
        }

        protected static Process Autoroute(string boardFile)
        {
            var pri = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = @"C:\EAGLE-7.2.0\bin\eagle.exe",
                Arguments = String.Format(@" -C 'ripup *; auto *; write; quit;' {0}", boardFile)
            };
            return Process.Start(pri);
        }

        public virtual bool Export()
        {
            if (!CreateBoardFile(BoardFile, _board))
            {
                return false;
            }
            Autoroute(BoardFile).WaitForExit();
            return true;
        }

    }
}