using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace Walle.Eagle
{
    public class LedBoardBuilder
    {
        private readonly EagleBoard _board = new EagleBoard();
        public Element Pinhead { get; private set; }
        private readonly IList<Element> _leds = new List<Element>();

        /// <summary>
        /// Pin 1 = GND
        /// Pin 2 = VCC
        /// Pin 3 = Data IN
        /// </summary>
        public LedBoardBuilder(uint width, uint height)
        {
            _board.Width = width;
            _board.Height = height;
            _board.Signals.Add("GND", new Signal());
            _board.Signals.Add("VCC", new Signal());
            _board.Packages.Add(new WS2812B());
            _board.Packages.Add(new Pinhead5Rot90());

            Pinhead = new Element
            {
                Name = "LP1",
                Package = new Pinhead5Rot90(),
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

            // custom part
            _board.Signals.Add("TOUCH1",new Signal());
            _board.Signals.Add("TOUCH2",new Signal());
            _board.Packages.Add(new Pinhead2());
            var touchPin = new Element()
            {
                Name = "LP2",
                Package = new Pinhead2(),
                X = 130,
                Y = 20,
                Rotation = 0
            };
            _board.Elements.Add(touchPin);
            _board.Signals["TOUCH1"].AddContact(touchPin,0);
            _board.Signals["TOUCH1"].AddContact(Pinhead,3);
            _board.Signals["TOUCH2"].AddContact(touchPin,1);
            _board.Signals["TOUCH2"].AddContact(Pinhead,4);
        }

        public IReadOnlyList<Element> Leds
        {
            get { return new ReadOnlyCollection<Element>(_leds); }
        }

        public XDocument ToXml()
        {
            return _board.ToXml();
        }

        public void SetPinheadLocation(double x, double y)
        {
            Pinhead.X = x;
            Pinhead.Y = y;
        }

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
                ledSignal.AddContact(
                    element: Pinhead,
                    padIndex: 2 // Data
                    );
            }
            else
            {
                ledSignal.AddContact(
                    element: _leds.Last(),
                    padIndex: 1 // DOUT
                    );
            }
            ledSignal.AddContact(
                element: newLed,
                padIndex: 3 // DIN
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
    }
}