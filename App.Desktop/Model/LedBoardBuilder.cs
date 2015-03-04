using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Walle.Eagle;

namespace Walle.Model
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
            _board.Packages.Add(new Pinhead3Rot90());

            Pinhead = new Element
            {
                Name = "LP1",
                Package = new Pinhead3Rot90(),
                X = 5,
                Y = 5,
                Rotation = 90
            };
            _board.Elements.Add(Pinhead);
            _board.Signals["GND"].AddContact(
                element: Pinhead,
                padIndex: 1 // GND
                );
            _board.Signals["VCC"].AddContact(
                element: Pinhead,
                padIndex: 2 // VCC
                );
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
                    padIndex: 3 // Data
                    );
            }
            else
            {
                ledSignal.AddContact(
                    element: _leds.Last(),
                    padIndex: 2 // DOUT
                    );
            }
            ledSignal.AddContact(
                element: newLed,
                padIndex: 4 // DIN
                );

            _leds.Add(newLed);
            _board.Elements.Add(newLed);

            _board.Signals.Add("N$" + newLed.Name, ledSignal);
            _board.Signals["VCC"].AddContact(
                element: newLed,
                padIndex: 1 // VCC
                );
            _board.Signals["GND"].AddContact(
                element: newLed,
                padIndex: 3 // GND
                );
        }
    }
}