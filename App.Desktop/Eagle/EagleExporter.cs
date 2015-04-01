using System;
using System.Diagnostics;
using System.IO;
using Walle.Properties;

namespace Walle.Eagle
{
    /// <summary>
    /// Creates an Eagle file that represents the board.
    /// This exports using eagle.exe <seealso cref="Settings"/>
    /// </summary>
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
        /// <summary>
        /// Runs the autorouter through eagle.exe
        /// <see cref="http://web.mit.edu/xavid/arch/i386_rhel4/help/24.htm"/>
        /// </summary>
        /// <param name="boardFile"></param>
        /// <returns></returns>
        protected static Process Autoroute(string boardFile)
        {
            var pri = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName =  Settings.Default.EagleExe,
                Arguments = String.Format(@" -C 'ripup *; auto *; write; quit;' {0}", boardFile) 
                // -C executes these eagle commands
                // ripup destroys an previous autorouting
                // auto runs the autorouter
                // write saves the file
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