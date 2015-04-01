using System.Collections.Generic;

namespace Walle.Eagle
{
    /// <summary>
    /// A CAM processing job to be executed. Contains a list of steps to run via eaglecon.exe
    /// </summary>
    internal interface ICamJob
    {
        /// <summary>
        /// For each step, provides the command line arguments that are passed to eaglecon.exe
        /// </summary>
        IList<string> StepArguments { get; }
    }
}
