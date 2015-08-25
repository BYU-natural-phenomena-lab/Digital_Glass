using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DigitalGlass.Eagle
{
    /// <summary>
    /// Produces the files SeeedStudio needs to assemble a PCB
    /// http://www.seeedstudio.com/service/index.php?r=pcb
    /// http://support.seeedstudio.com/knowledgebase/articles/422482-fusion-pcb-order-submission-guidelines
    /// </summary>
    public class SeeedStudioCamJob : ICamJob
    {
        public SeeedStudioCamJob(string boardPath, string outDir)
        {
            _boardPath = boardPath;
            _outfileRootPath = Path.Combine(outDir,Path.GetFileNameWithoutExtension(boardPath));
        }

        private static readonly List<string> Args = new List<string>
        {
            "-dGERBER_RS274X -o\"{0}\".gml \"{1}\" 20 46", // outline
            "-dEXCELLON -o\"{0}\"txt \"{1}\" 20 44 45", // drilling

            "-dGERBER_RS274X -o\"{0}\".gtl \"{1}\" 1 17 18 20", // top layer
            "-dGERBER_RS274X -o\"{0}\".gtp \"{1}\" 20 31", // top pad
            "-dGERBER_RS274X -o\"{0}\".gts \"{1}\" 20 29", //top solder
            "-dGERBER_RS274X -o\"{0}\".gto \"{1}\" 20 21 25", // top silkscreen
                                             
            "-dGERBER_RS274X -o\"{0}\".gbl \"{1}\" 16 17 18 20", // bottom layer
            "-dGERBER_RS274X -o\"{0}\".gbp \"{1}\" 20 32", // bottom pad
            "-dGERBER_RS274X -o\"{0}\".gbs \"{1}\" 20 30", // bottom solder
            "-dGERBER_RS274X -o\"{0}\".gbo \"{1}\" 20 22 26", // bottom silkscreen
        };

        private readonly string _outfileRootPath;
        private readonly string _boardPath;

        public IList<string> StepArguments
        {
            get { return Args.Select(tmpl =>String.Format(tmpl, _outfileRootPath, _boardPath)).ToList(); }
        }
    }
}