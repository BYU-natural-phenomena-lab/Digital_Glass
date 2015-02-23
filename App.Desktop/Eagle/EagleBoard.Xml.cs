using System.Linq;
using System.Xml.Linq;

namespace Walle.Eagle
{
    public partial class EagleBoard
    {
        public XDocument ToXml()
        {
            var docNode = new XElement("eagle",
                new XAttribute("version", this.Version),
                new XElement("drawing",
                    new XElement("settings",
                        GetLayers()),
                    new XElement("board",
                        GetDimension(),
                        new XElement("libraries",
                            new XElement("library", new XAttribute("name", "default"),
                                GetPackages()
                                )
                            ),
                        new XElement("designrules", new XAttribute("name", "default"), new XText(DesignRules.RulesXml)),
                        GetElements(),
                        GetSignals())));
            return new XDocument(docNode);
        }

        private XElement GetSignals()
        {
            return new XElement("signals",
                Signals.Select(s =>
                    new XElement("signal",
                        new XAttribute("name", s.Key),
                        s.Value.ContactRefs.Select(cr => new XElement("contactref",
                            new XAttribute("element", cr.Element.Name),
                            new XAttribute("pad", cr.Element.Package.Pads[cr.PadIndex])))))
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
                        new XElement("package", new XAttribute("name", package.PackageName),
                            new XText(package.WiresXml())))
                );
        }

        private XElement GetLayers()
        {
            return new XElement("layers",
                _layers.Select(pair => new XElement("layer",
                    new XAttribute("number", pair.Key),
                    new XAttribute("name", pair.Value)
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
                    new XAttribute("x1", Width),
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