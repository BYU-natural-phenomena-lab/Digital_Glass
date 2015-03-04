namespace Walle.Eagle
{
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