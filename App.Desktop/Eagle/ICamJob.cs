using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walle.Eagle
{
    internal interface ICamJob
    {
        IList<string> StepArguments { get; }
    }
}
