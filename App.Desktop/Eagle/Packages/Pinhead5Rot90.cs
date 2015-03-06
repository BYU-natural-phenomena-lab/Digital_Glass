using System.Collections.Generic;
using System.Xml.Linq;

namespace Walle.Eagle
{
    public class Pinhead5Rot90 : IPackage
    {
        private readonly IList<string> _pads = new[]
        {
            "1",
            "2",
            "3",
            "4",
            "5"
        };

        public string PackageName
        {
            get { return "1X05/90"; }
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
                    new XAttribute("x1", "-6.35"),
                    new XAttribute("y1", "-1.905"),
                    new XAttribute("x2", "-3.81"),
                    new XAttribute("y2", "-1.905"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "-3.81"),
                    new XAttribute("y1", "-1.905"),
                    new XAttribute("x2", "-3.81"),
                    new XAttribute("y2", "0.635"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "-3.81"),
                    new XAttribute("y1", "0.635"),
                    new XAttribute("x2", "-6.35"),
                    new XAttribute("y2", "0.635"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "-6.35"),
                    new XAttribute("y1", "0.635"),
                    new XAttribute("x2", "-6.35"),
                    new XAttribute("y2", "-1.905"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "-5.08"),
                    new XAttribute("y1", "6.985"),
                    new XAttribute("x2", "-5.08"),
                    new XAttribute("y2", "1.27"),
                    new XAttribute("width", "0.762"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "-3.81"),
                    new XAttribute("y1", "-1.905"),
                    new XAttribute("x2", "-1.27"),
                    new XAttribute("y2", "-1.905"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "-1.27"),
                    new XAttribute("y1", "-1.905"),
                    new XAttribute("x2", "-1.27"),
                    new XAttribute("y2", "0.635"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "-1.27"),
                    new XAttribute("y1", "0.635"),
                    new XAttribute("x2", "-3.81"),
                    new XAttribute("y2", "0.635"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "-2.54"),
                    new XAttribute("y1", "6.985"),
                    new XAttribute("x2", "-2.54"),
                    new XAttribute("y2", "1.27"),
                    new XAttribute("width", "0.762"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "-1.27"),
                    new XAttribute("y1", "-1.905"),
                    new XAttribute("x2", "1.27"),
                    new XAttribute("y2", "-1.905"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "1.27"),
                    new XAttribute("y1", "-1.905"),
                    new XAttribute("x2", "1.27"),
                    new XAttribute("y2", "0.635"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "1.27"),
                    new XAttribute("y1", "0.635"),
                    new XAttribute("x2", "-1.27"),
                    new XAttribute("y2", "0.635"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "0"),
                    new XAttribute("y1", "6.985"),
                    new XAttribute("x2", "0"),
                    new XAttribute("y2", "1.27"),
                    new XAttribute("width", "0.762"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "1.27"),
                    new XAttribute("y1", "-1.905"),
                    new XAttribute("x2", "3.81"),
                    new XAttribute("y2", "-1.905"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "3.81"),
                    new XAttribute("y1", "-1.905"),
                    new XAttribute("x2", "3.81"),
                    new XAttribute("y2", "0.635"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "3.81"),
                    new XAttribute("y1", "0.635"),
                    new XAttribute("x2", "1.27"),
                    new XAttribute("y2", "0.635"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "2.54"),
                    new XAttribute("y1", "6.985"),
                    new XAttribute("x2", "2.54"),
                    new XAttribute("y2", "1.27"),
                    new XAttribute("width", "0.762"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "3.81"),
                    new XAttribute("y1", "-1.905"),
                    new XAttribute("x2", "6.35"),
                    new XAttribute("y2", "-1.905"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "6.35"),
                    new XAttribute("y1", "-1.905"),
                    new XAttribute("x2", "6.35"),
                    new XAttribute("y2", "0.635"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "6.35"),
                    new XAttribute("y1", "0.635"),
                    new XAttribute("x2", "3.81"),
                    new XAttribute("y2", "0.635"),
                    new XAttribute("width", "0.1524"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("wire",
                    new XAttribute("x1", "5.08"),
                    new XAttribute("y1", "6.985"),
                    new XAttribute("x2", "5.08"),
                    new XAttribute("y2", "1.27"),
                    new XAttribute("width", "0.762"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("pad",
                    new XAttribute("name", "1"),
                    new XAttribute("x", "-5.08"),
                    new XAttribute("y", "-3.81"),
                    new XAttribute("drill", "1.016"),
                    new XAttribute("shape", "long"),
                    new XAttribute("rot", "R90")
                    ),
                new XElement("pad",
                    new XAttribute("name", "2"),
                    new XAttribute("x", "-2.54"),
                    new XAttribute("y", "-3.81"),
                    new XAttribute("drill", "1.016"),
                    new XAttribute("shape", "long"),
                    new XAttribute("rot", "R90")
                    ),
                new XElement("pad",
                    new XAttribute("name", "3"),
                    new XAttribute("x", "0"),
                    new XAttribute("y", "-3.81"),
                    new XAttribute("drill", "1.016"),
                    new XAttribute("shape", "long"),
                    new XAttribute("rot", "R90")
                    ),
                new XElement("pad",
                    new XAttribute("name", "4"),
                    new XAttribute("x", "2.54"),
                    new XAttribute("y", "-3.81"),
                    new XAttribute("drill", "1.016"),
                    new XAttribute("shape", "long"),
                    new XAttribute("rot", "R90")
                    ),
                new XElement("pad",
                    new XAttribute("name", "5"),
                    new XAttribute("x", "5.08"),
                    new XAttribute("y", "-3.81"),
                    new XAttribute("drill", "1.016"),
                    new XAttribute("shape", "long"),
                    new XAttribute("rot", "R90")
                    ),
                new XElement("text",
                    new XAttribute("x", "-6.985"),
                    new XAttribute("y", "-3.81"),
                    new XAttribute("size", "1.27"),
                    new XAttribute("layer", "25"),
                    new XAttribute("ratio", "10"),
                    new XAttribute("rot", "R90"),
                    new XText(">NAME")
                    ),
                new XElement("text",
                    new XAttribute("x", "8.255"),
                    new XAttribute("y", "-3.81"),
                    new XAttribute("size", "1.27"),
                    new XAttribute("layer", "27"),
                    new XAttribute("rot", "R90"),
                    new XText(">VALUE")
                    ),
                new XElement("rectangle",
                    new XAttribute("x1", "-5.461"),
                    new XAttribute("y1", "0.635"),
                    new XAttribute("x2", "-4.699"),
                    new XAttribute("y2", "1.143"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("rectangle",
                    new XAttribute("x1", "-2.921"),
                    new XAttribute("y1", "0.635"),
                    new XAttribute("x2", "-2.159"),
                    new XAttribute("y2", "1.143"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("rectangle",
                    new XAttribute("x1", "-0.381"),
                    new XAttribute("y1", "0.635"),
                    new XAttribute("x2", "0.381"),
                    new XAttribute("y2", "1.143"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("rectangle",
                    new XAttribute("x1", "2.159"),
                    new XAttribute("y1", "0.635"),
                    new XAttribute("x2", "2.921"),
                    new XAttribute("y2", "1.143"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("rectangle",
                    new XAttribute("x1", "4.699"),
                    new XAttribute("y1", "0.635"),
                    new XAttribute("x2", "5.461"),
                    new XAttribute("y2", "1.143"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("rectangle",
                    new XAttribute("x1", "-5.461"),
                    new XAttribute("y1", "-2.921"),
                    new XAttribute("x2", "-4.699"),
                    new XAttribute("y2", "-1.905"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("rectangle",
                    new XAttribute("x1", "-2.921"),
                    new XAttribute("y1", "-2.921"),
                    new XAttribute("x2", "-2.159"),
                    new XAttribute("y2", "-1.905"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("rectangle",
                    new XAttribute("x1", "-0.381"),
                    new XAttribute("y1", "-2.921"),
                    new XAttribute("x2", "0.381"),
                    new XAttribute("y2", "-1.905"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("rectangle",
                    new XAttribute("x1", "2.159"),
                    new XAttribute("y1", "-2.921"),
                    new XAttribute("x2", "2.921"),
                    new XAttribute("y2", "-1.905"),
                    new XAttribute("layer", "21")
                    ),
                new XElement("rectangle",
                    new XAttribute("x1", "4.699"),
                    new XAttribute("y1", "-2.921"),
                    new XAttribute("x2", "5.461"),
                    new XAttribute("y2", "-1.905"),
                    new XAttribute("layer", "21")
                    )
                );
        }
    }
}