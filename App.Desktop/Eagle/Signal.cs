using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Walle.Eagle
{
    public class Signal
    {
        private readonly IList<ContactRef> _contactRefs = new List<ContactRef>();

        public IReadOnlyList<ContactRef> ContactRefs
        {
            get { return new ReadOnlyCollection<ContactRef>(_contactRefs); }
        }

        public void AddContact(Element element, int padIndex)
        {
            _contactRefs.Add(new ContactRef
            {
                Element = element,
                PadIndex = padIndex
            });
        }
    }
}