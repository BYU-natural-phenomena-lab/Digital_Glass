using System.Collections.Generic;
using System.Xml.Linq;

namespace DigitalGlass.Eagle
{
    /// <summary>
    /// A pacakge is from a library of devices. Example: A surface mount LED 
    /// </summary>
    public interface IPackage
    {
        string PackageName { get; }
        /// <summary>
        /// Where a surface mounted device comes into contact with the PCB
        /// </summary>
        IList<string> Pads { get; }
        XElement ToXml();
    }
}