using System.Collections.Generic;

namespace Walle.Eagle
{
    public partial class EagleBoard
    {
        public EagleBoard()
        {
            _layers = new Dictionary<int, string>
            {
                {1, "Top"},
                {16, "Bottom"},
                {17, "Pads"},
                {18, "Vias"},
                {20, "Dimension"},
                {21, "tPlace"},
                {22, "bPlace"},
                {25, "tNames"},
                {26, "bNames"},
                {27, "tValues"},
                {28, "bValues"},
            };
            Signals = new Dictionary<string, Signal>();
            Packages = new List<IPackage>();
            Elements = new List<Element>();
            DesignRules = new DefaultDesignRules();
        }

        public IDesignRules DesignRules { get; set; }

        public uint Height { get; set; }

        public uint Width { get; set; }

        public string Version
        {
            get { return "7.2.0"; }
        }

        public IList<Element> Elements { get; private set; }

        public IList<IPackage> Packages { get; private set; }

        public IDictionary<string, Signal> Signals { get; private set; }

        private readonly IDictionary<int, string> _layers;
    }
}