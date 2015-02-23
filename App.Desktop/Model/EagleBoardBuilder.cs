using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace Walle.Model
{
    public class EagleBoardBuilder
    {
        private readonly EagleBoard _board = new EagleBoard();
        public EagleBoard.Element Pinhead { get; private set; }
        private readonly IList<EagleBoard.Element> _leds = new List<EagleBoard.Element>();

        /// <summary>
        /// Pin 1 = GND
        /// Pin 2 = VCC
        /// Pin 3 = Data IN
        /// </summary>
        public EagleBoardBuilder()
        {
            _board.Signals.Add("GND", new EagleBoard.Signal());
            _board.Signals.Add("VCC", new EagleBoard.Signal());
            Pinhead = new EagleBoard.Element
            {
                Name = "LP1",
                Package = new Pinhead3Rot90(),
                X = 10,
                Y = 10,
                Rotation = 0
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

        public IReadOnlyList<EagleBoard.Element> Leds
        {
            get { return new ReadOnlyCollection<EagleBoard.Element>(_leds); }
        }


        public void SetPinheadLocation(double x, double y)
        {
            Pinhead.X = x;
            Pinhead.Y = y;
        }

        public void AddLedAtPoint(double x, double y)
        {
            var newLed = new EagleBoard.Element
            {
                Name = "LED" + (_leds.Count),
                Package = new WS2812B(),
                X = x,
                Y = y
            };

            var ledSignal = new EagleBoard.Signal();
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