namespace Walle.Eagle
{
    /// <summary>
    /// Represents one point a signal. <seealso cref="Signal"/>
    /// A signal is a collection of ContactRef
    /// </summary>
    public class ContactRef
    {
        public Element Element { get; set; }
        public int PadIndex { get; set; }
    }
}