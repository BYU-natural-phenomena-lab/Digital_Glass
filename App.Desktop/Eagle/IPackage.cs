using System.Collections.Generic;

namespace Walle.Eagle
{
    public interface IPackage
    {
        string PackageName { get; }
        IList<string> Pads { get; }
        string WiresXml();
    }
}