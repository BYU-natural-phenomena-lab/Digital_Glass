using System.Collections.Generic;

namespace DigitalGlass.Eagle
{
    /// <summary>
    /// A representation of PCB using Eagle's model
    /// </summary>
    public partial class EagleBoard
    {
        public EagleBoard()
        {
            _layers = new Dictionary<int, string>
            {
                {1, "Top"}, // elements on top of the board
                {16, "Bottom"}, // elements on the bottom of the board
                {17, "Pads"}, // defineds the pads for all SMD
                {18, "Vias"}, // holes through the board, to connect traces on the bottom/top
                {20, "Dimension"}, // size of the board
                {21, "tPlace"},
                {22, "bPlace"},
                {25, "tNames"}, 
                {26, "bNames"},
                {27, "tValues"},
                {28, "bValues"},
                {29, "tStop"},
                {30, "bStop"},
                {31, "tCream"},
                {32, "bCream"},
                {44, "Drills"},
                {45, "Holes"},
                {46, "Milling"},
                {47, "Measures"},
            };
            Signals = new Dictionary<string, Signal>();
            Packages = new List<IPackage>();
            Elements = new List<Element>();
            DesignRules = new DefaultDesignRules();
        }
        /// <summary>
        /// Allows eagle to run a design rule check (DRC)
        /// </summary>
        public IDesignRules DesignRules { get; set; }
        /// <summary>
        /// In millimeters
        /// </summary>
        public uint Height { get; set; }
        /// <summary>
        /// In millimeters
        /// </summary>
        public uint Width { get; set; }
        /// <summary>
        /// Which version of EAGLE its using
        /// </summary>
        public string Version
        {
            get { return "7.2.0"; }
        }
        /// <summary>
        /// Instances of devices. The package must be in <seealso cref="Packages"/>
        /// </summary>
        public IList<Element> Elements { get; private set; }

        /// <summary>
        /// A library of device types
        /// </summary>
        public IList<IPackage> Packages { get; private set; }
        /// <summary>
        /// Electrical connections between <seealso cref="Elements"/>
        /// </summary>
        public IDictionary<string, Signal> Signals { get; private set; }

        private readonly IDictionary<int, string> _layers;
    }
}