using System.Collections.Generic;
using System.Xml.Linq;

namespace Walle.Eagle
{
    /// <summary>
    /// A 2 pin header
    /// </summary>
    public class PinheadSingle : IPackage
    {
        private readonly IList<string> _pads = new[]
        {
            "1"
        };

        public string PackageName
        {
            get { return "1X01"; }
        }

        public IList<string> Pads
        {
            get { return _pads; }
        }

        public XElement ToXml()
        {
            return new XElement("package",
                new XAttribute("name", this.PackageName),
                new XElement("wire",
                    new XAttribute("x1", "-.635"),
                    new XAttribute("y1", "1.27"),
                    new XAttribute("x2", "0.635"),
                    new XAttribute("y2", "1.27"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "0.635"),
                    new XAttribute("y1", "1.27"),
                    new XAttribute("x2", "1.27"),
                    new XAttribute("y2", "0.635"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "1.27"),
                    new XAttribute("y1", "0.635"),
                    new XAttribute("x2", "1.27"),
                    new XAttribute("y2", "-0.635"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "1.27"),
                    new XAttribute("y1", "-0.635"),
                    new XAttribute("x2", "0.635"),
                    new XAttribute("y2", "-1.27"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "-1.27"),
                    new XAttribute("y1", "0.635"),
                    new XAttribute("x2", "-1.27"),
                    new XAttribute("y2", "-0.635"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "-0.635"),
                    new XAttribute("y1", "1.27"),
                    new XAttribute("x2", "-1.27"),
                    new XAttribute("y2", "0.635"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "-1.27"),
                    new XAttribute("y1", "-0.635"),
                    new XAttribute("x2", "-.635"),
                    new XAttribute("y2", "-1.27"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "0.635"),
                    new XAttribute("y1", "-1.27"),
                    new XAttribute("x2", "-0.635"),
                    new XAttribute("y2", "-1.27"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("pad",
                    new XAttribute("name", "1"),
                    new XAttribute("x", "0"),
                    new XAttribute("y", "0"),
                    new XAttribute("drill", "1.016"),
                    new XAttribute("shape", "long"),
                    new XAttribute("rot", "R90")
                    ),
                new XElement("text",
                    new XAttribute("x", "-0.5"),
                    new XAttribute("y", "1.8288"),
                    new XAttribute("size", "1.27"),
                    new XAttribute("layer", "25"),
                    new XAttribute("ratio", "10"),
                    new XText(">NAME")
                    ),
                new XElement("text",
                    new XAttribute("x", "-2.54"),
                    new XAttribute("y", "-3.175"),
                    new XAttribute("size", "1.27"),
                    new XAttribute("layer", "27"),
                    new XText(">VALUE")
                    ),
                new XElement("rectangle",
                    new XAttribute("x1", "-0.254"),
                    new XAttribute("y1", "-0.254"),
                    new XAttribute("x2", "0.254"),
                    new XAttribute("y2", "0.254"),
                    new XAttribute("layer", "51")
                    ));
        }
    }
}