using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Walle.Eagle
{
    /// <summary>
    /// Electrical connection in a board. Represents a collection of elements that should all be connected in a 
    /// </summary>
    public class Signal
    {
        private readonly IList<ContactRef> _contactRefs = new List<ContactRef>();

        public IReadOnlyList<ContactRef> ContactRefs
        {
            get { return new ReadOnlyCollection<ContactRef>(_contactRefs); }
        }

        /// <summary>
        /// Add a new contact to the signa. Specify exactly which pad to connect to.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="padIndex"></param>
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