using System.Linq;
using System.Xml.Linq;

namespace Walle.Eagle
{
    public partial class EagleBoard
    {
        public XDocument ToXml()
        {
            var doc= new XDocument()
            {
                Declaration = new       XDeclaration("1.0","utf-8",null)
            };
            var rootNode = new XElement("eagle",
                new XAttribute("version", this.Version),
                new XElement("drawing",
                    GetLayers(),
                    new XElement("board",
                        GetDimension(),
                        new XElement("libraries",
                            new XElement("library", new XAttribute("name", "default"),
                                GetPackages()
                                )
                            ),
                        new DefaultDesignRules().RulesXml,
                        GetElements(),
                        GetSignals())));
            doc.AddFirst(new XDocumentType("eagle", null, "eagle.dtd", null));
            doc.Add(rootNode);
            var x = new XElement("test");
            return doc;
        }

        private XElement GetSignals()
        {
            return new XElement("signals",
                Signals.Select(s =>
                    new XElement("signal",
                        new XAttribute("name", s.Key),
                        s.Value.ContactRefs.Select(cr => new XElement("contactref",
                            new XAttribute("element", cr.Element.Name),
                            new XAttribute("pad", cr.Element.Package.Pads[cr.PadIndex -1])))))
                );
        }

        private XElement GetElements()
        {
            return new XElement("elements", Elements.Select(e =>
                new XElement("element",
                    new XAttribute("name", e.Name),
                    new XAttribute("library", "default"),
                    new XAttribute("package", e.Package.PackageName),
                    new XAttribute("value", ""),
                    new XAttribute("x", e.X),
                    new XAttribute("y", e.Y),
                    new XAttribute("rot", e.Rot)
                    )));
        }

        private XElement GetPackages()
        {
            return new XElement("packages",
                Packages.Select(
                    package =>
                        package.ToXml()
                ));
        }

        private XElement GetLayers()
        {
            return new XElement("layers",
                _layers.Select(pair => new XElement("layer",
                    new XAttribute("number", pair.Key),
                    new XAttribute("name", pair.Value),
                    new XAttribute("color", "1")
                    ))
                )
                ;
        }

        private XElement GetDimension()
        {
            return new XElement("plain",
                new XElement("wire",
                    new XAttribute("x1", 0),
                    new XAttribute("y1", 0),
                    new XAttribute("x2", Width),
                    new XAttribute("y2", 0),
                    new XAttribute("width", 0.1),
                    new XAttribute("layer", 20)
                    ),
                new XElement("wire",
                    new XAttribute("x1", Width),
                    new XAttribute("y1", 0),
                    new XAttribute("x2", Width),
                    new XAttribute("y2", Height),
                    new XAttribute("width", 0.1),
                    new XAttribute("layer", 20)
                    ),
                new XElement("wire",
                    new XAttribute("x1", Width),
                    new XAttribute("y1", Height),
                    new XAttribute("x2", 0),
                    new XAttribute("y2", Height),
                    new XAttribute("width", 0.1),
                    new XAttribute("layer", 20)
                    ),
                new XElement("wire",
                    new XAttribute("x1", 0),
                    new XAttribute("y1", Height),
                    new XAttribute("x2", 0),
                    new XAttribute("y2", 0),
                    new XAttribute("width", 0.1),
                    new XAttribute("layer", 20)
                    )
                );
        }
    }
}