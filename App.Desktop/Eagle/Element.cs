namespace Walle.Eagle
{
    /// <summary>
    /// Represents an element on the PCB. This includes settings about rotation, location, and which kind of package it is.
    /// Currently all elements are part of the "default" library.
    /// </summary>
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
            get { return "R" + Rotation; }
        }

        public int Rotation { get; set; }
    }
}