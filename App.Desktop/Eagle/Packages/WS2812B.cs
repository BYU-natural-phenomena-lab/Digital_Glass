using System.Collections.Generic;
using System.Xml.Linq;

namespace DigitalGlass.Eagle
{
    /// <summary>
    /// http://www.adafruit.com/datasheets/WS2812B.pdf
    /// A RGB led unit that has a very specific protocol. 
    /// Chainable via DIN and DOUT.
    /// </summary>
    public class WS2812B : IPackage
    {
        private readonly IList<string> _pads = new[]
        {
            "1-VDD",
            "2-DOUT",
            "3-GND",
            "4-DIN"
        };

        public string PackageName
        {
            get { return "WS2812B"; }
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
                    new XAttribute("x1", "2.5"),
                    new XAttribute("y1", "-2.5"),
                    new XAttribute("x2", "-2.5"),
                    new XAttribute("y2", "-2.5"),
                    new XAttribute("width", "0.127"),
                    new XAttribute("layer", "21")),
                new XElement("wire",
                    new XAttribute("x1", "-2.5"),
                    new XAttribute("y1", "-2.5"),
                    new XAttribute("x2", "-2.5"),
                    new XAttribute("y2", "1.6"),
                    new XAttribute("width", "0.127"),
                    new XAttribute("layer", "21")),
                new XElement("wire",
                    new XAttribute("x1", "-2.5"),
                    new XAttribute("y1", "1.6"),
                    new XAttribute("x2", "-2.5"),
                    new XAttribute("y2", "2.5"),
                    new XAttribute("width", "0.127"),
                    new XAttribute("layer", "21")),
                new XElement("wire",
                    new XAttribute("x1", "-2.5"),
                    new XAttribute("y1", "2.5"),
                    new XAttribute("x2", "-1.6"),
                    new XAttribute("y2", "2.5"),
                    new XAttribute("width", "0.127"),
                    new XAttribute("layer", "21")),
                new XElement("wire",
                    new XAttribute("x1", "-1.6"),
                    new XAttribute("y1", "2.5"),
                    new XAttribute("x2", "2.5"),
                    new XAttribute("y2", "2.5"),
                    new XAttribute("width", "0.127"),
                    new XAttribute("layer", "21")),
                new XElement("wire",
                    new XAttribute("x1", "2.5"),
                    new XAttribute("y1", "2.5"),
                    new XAttribute("x2", "2.5"),
                    new XAttribute("y2", "-2.5"),
                    new XAttribute("width", "0.127"),
                    new XAttribute("layer", "21")),
                new XElement("wire",
                    new XAttribute("x1", "-2.5"),
                    new XAttribute("y1", "1.6"),
                    new XAttribute("x2", "-1.6"),
                    new XAttribute("y2", "2.5"),
                    new XAttribute("width", "0.127"),
                    new XAttribute("layer", "21")),
                new XElement("smd",
                    new XAttribute("name", "1-VDD"),
                    new XAttribute("x", "2.45"),
                    new XAttribute("y", "-1.65"),
                    new XAttribute("dx", "1.5"),
                    new XAttribute("dy", "0.9"),
                    new XAttribute("layer", "1"),
                    new XAttribute("rot", "R180")),
                new XElement("smd",
                    new XAttribute("name", "2-DOUT"),
                    new XAttribute("x", "2.45"),
                    new XAttribute("y", "1.65"),
                    new XAttribute("dx", "1.5"),
                    new XAttribute("dy", "0.9"),
                    new XAttribute("layer", "1"),
                    new XAttribute("rot", "R180")),
                new XElement("smd",
                    new XAttribute("name", "3-GND"),
                    new XAttribute("x", "-2.45"),
                    new XAttribute("y", "1.65"),
                    new XAttribute("dx", "1.5"),
                    new XAttribute("dy", "0.9"),
                    new XAttribute("layer", "1"),
                    new XAttribute("rot", "R180")),
                new XElement("smd",
                    new XAttribute("name", "4-DIN"),
                    new XAttribute("x", "-2.45"),
                    new XAttribute("y", "-1.65"),
                    new XAttribute("dx", "1.5"),
                    new XAttribute("dy", "0.9"),
                    new XAttribute("layer", "1"),
                    new XAttribute("rot", "R180")),
                new XElement("circle",
                    new XAttribute("x", "0"),
                    new XAttribute("y", "0"),
                    new XAttribute("radius", "1.7204625"),
                    new XAttribute("width", "0.127"),
                    new XAttribute("layer", "21")),
                new XElement("text",
                    new XAttribute("x", "3.4925"),
                    new XAttribute("y", "1.5875"),
                    new XAttribute("size", "0.8128"),
                    new XAttribute("layer", "25"),
                    new XAttribute("ratio", "10"),
                    new XAttribute("rot", "R270"),
                    new XText(">NAME")),
                new XElement("wire",
                    new XAttribute("x1", "-1.6"),
                    new XAttribute("y1", "2.5"),
                    new XAttribute("x2", "-1.3"),
                    new XAttribute("y2", "2.8"),
                    new XAttribute("width", "0.127"),
                    new XAttribute("layer", "21")),
                new XElement("wire",
                    new XAttribute("x1", "-1.3"),
                    new XAttribute("y1", "2.8"),
                    new XAttribute("x2", "-1.7"),
                    new XAttribute("y2", "3.2"),
                    new XAttribute("width", "0.127"),
                    new XAttribute("layer", "21")),
                new XElement("wire",
                    new XAttribute("x1", "-1.7"),
                    new XAttribute("y1", "3.2"),
                    new XAttribute("x2", "-2.5"),
                    new XAttribute("y2", "2.5"),
                    new XAttribute("width", "0.127"),
                    new XAttribute("layer", "21"))
                );
        }
    }
}