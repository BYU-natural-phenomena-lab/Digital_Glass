using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Walle.Model
{
    public class EagleBoard
    {
        public IDictionary<string, Signal> Signals { get; set; }
        public IList<Element> Elements { get; set; }

        public class Element
        {
            public string Name { get; set; }

            public string Library
            {
                get { return "default"; }
            }

            public IPackage Package { get; set; }


            public double X { get; set; }
            public double Y { get; set; }

            public string Rot
            {
                get { return "ROT" + Rotation; }
            }

            public int Rotation { get; set; }
        }

        public interface IPackage
        {
            string PackageName { get; }
            IList<string> Pads { get; }
            string WiresXml();
        }

        public class Signal
        {
            private readonly IList<ContactRef> _contactRefs = new List<ContactRef>();

            public IReadOnlyList<ContactRef> ContactRefs
            {
                get { return new ReadOnlyCollection<ContactRef>(_contactRefs); }
            }

            public void AddContact(Element element, uint padIndex)
            {
                _contactRefs.Add(new ContactRef
                {
                    Element = element,
                    PadIndex = padIndex
                });
            }
        }

        public class ContactRef
        {
            public Element Element { get; set; }
            public uint PadIndex { get; set; }
        }
    }
}