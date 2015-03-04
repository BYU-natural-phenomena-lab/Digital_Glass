using System.Collections.Generic;
using System.Xml.Linq;

namespace Walle.Eagle
{
    public interface IPackage
    {
        string PackageName { get; }
        IList<string> Pads { get; }
        XElement ToXml();
    }
}