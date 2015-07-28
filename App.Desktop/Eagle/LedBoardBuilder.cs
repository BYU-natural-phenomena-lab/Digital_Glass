using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace Walle.Eagle
{
    /// <summary>
    /// Provides a simple API for creating an EAGLE board from a set of LED locations.
    /// This board uses an X/Y coordinate system that is measured from the bottom left corner of the board. 
    ///  ^
    ///  |   (+x, +y)
    ///  |  /
    ///  | /
    ///  |/___________>
    /// (0,0)
    /// 
    /// Positive x is to the right of the left edge. 
    /// Positive y is up from the bottom edge. 
    /// </summary>
    public class LedBoardBuilder
    {
        private readonly EagleBoard _board = new EagleBoard();
        public Element Pinhead { get; private set; }
        public Element PinheadOut { get; private set; }
        private readonly IList<Element> _leds = new List<Element>();
        private readonly IList<Element> _touchPads = new List<Element>();

        /// <summary>
        /// Create a new board
        /// Pin 1 = GND
        /// Pin 2 = VCC
        /// Pin 3 = Data IN
        /// </summary>
        /// <param name="width">The width in millimeters</param>
        /// <param name="height">The height in millimeters</param>
        public LedBoardBuilder(uint width, uint height)
        {
            _board.Width = width;
            _board.Height = height;
            _board.Signals.Add("GND", new Signal());
            _board.Signals.Add("VCC", new Signal());
            _board.Packages.Add(new WS2812B());
            _board.Packages.Add(new Pinhead3Rot90());
            _board.Packages.Add(new PinheadSingle());

            Pinhead = new Element
            {
                Name = "LP1",
                Package = new Pinhead3Rot90(),
                X = 5,
                Y = 15,
                Rotation = 90
            };
            _board.Elements.Add(Pinhead);
            _board.Signals["GND"].AddContact(
                element: Pinhead,
                padIndex: 0 // GND
                );
            _board.Signals["VCC"].AddContact(
                element: Pinhead,
                padIndex: 1 // VCC
                );

            //Board Out
            PinheadOut = new Element
            {
                Name = "LP2",
                Package = new Pinhead3Rot90(),
                X = width - 5,
                Y = 15,
                Rotation = 270
            };

            _board.Elements.Add(PinheadOut);
            _board.Signals["GND"].AddContact(
            element: PinheadOut,
            padIndex: 2 // GND
            );
            _board.Signals["VCC"].AddContact(
                element: PinheadOut,
                padIndex: 1 // VCC
                );

        }
        /// <summary>
        /// The LED elements on the board.
        /// </summary>
        public IReadOnlyList<Element> Leds
        {
            get { return new ReadOnlyCollection<Element>(_leds); }
        }
        /// <summary>
        /// Produces an XML representation
        /// </summary>
        /// <returns>XDocument</returns>
        public XDocument ToXml()
        {
            return _board.ToXml();
        }
        /// <summary>
        /// Sets the location of the header to which the circuits are connected
        /// </summary>
        /// <param name="x">Millimeters</param>
        /// <param name="y">Millimeters</param>
        public void SetPinheadLocation(double x, double y)
        {
            Pinhead.X = x;
            Pinhead.Y = y;
        }
        /// <summary>
        /// Add a new LED at these coordinates
        /// </summary>
        /// <param name="x">Millimeters</param>
        /// <param name="y">Millimeters</param>
        public void AddLedAtPoint(double x, double y)
        {
            var newLed = new Element
            {
                Name = "LED" + (_leds.Count),
                Package = new WS2812B(),
                X = x,
                Y = y
            };

            var ledSignal = new Signal();
            if (_leds.Count == 0)
            {
                //Connect to input pad
                ledSignal.AddContact(
                    element: Pinhead,
                    padIndex: 2 // Data
                    );
            }
            else
            {
                ledSignal.AddContact(
                    element: _leds.Last(),
                    padIndex: 1 // Data OUT
                    );
            }
            ledSignal.AddContact(
                element: newLed,
                padIndex: 3 // Data IN
                );

            _leds.Add(newLed);
            _board.Elements.Add(newLed);

            _board.Signals.Add("N$" + newLed.Name, ledSignal);
            _board.Signals["VCC"].AddContact(
                element: newLed,
                padIndex: 0 // VCC
                );
            _board.Signals["GND"].AddContact(
                element: newLed,
                padIndex: 2 // GND
                );
        }

        /// <summary>
        /// Add a new Touch Pad at these coordinates
        /// </summary>
        /// <param name="x">Millimeters</param>
        /// <param name="y">Millimeters</param>
        public void AddTouchPadAtPoint(double x, double y)
        {
            var newTouchPad = new Element
            {
                Name = "T" + (_touchPads.Count),
                Package = new PinheadSingle(),
                X = x,
                Y = y,
                Rotation = 90
            };

            var newTouchPadOut = new Element
            {
                Name = _touchPads.Count + "",
                Package = new PinheadSingle(),
                X = 5 + 3.81,
                Y = 15 + 5.08 + (_touchPads.Count * 2.54), //Places output above the 
                Rotation = 90
            };

            _touchPads.Add(newTouchPad);

            var padSignal = new Signal();


            padSignal.AddContact(
                   element: newTouchPad,
                   padIndex: 0 
                   );
            padSignal.AddContact(
                   element: newTouchPadOut,
                   padIndex: 0
                   );

            _board.Elements.Add(newTouchPad);
            _board.Elements.Add(newTouchPadOut);

            _board.Signals.Add("N$" + newTouchPad.Name, padSignal);


        }
        /// <summary>
        /// Ataches the data pin of the last LED to PinheadOut
        /// </summary>
        public void attachToOutputPin()
        {
            var ledSignal = new Signal();

            ledSignal.AddContact(
                    element: _leds.Last(),
                    padIndex: 1 // Data OUT
                    );

            ledSignal.AddContact(
                  element: PinheadOut,
                  padIndex: 0 // Data
                  );

            _board.Signals.Add("LP2", ledSignal);
        }
    }
}