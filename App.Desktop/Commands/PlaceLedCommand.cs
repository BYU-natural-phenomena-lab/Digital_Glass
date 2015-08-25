using System.Media;
using System.Windows;
using DigitalGlass.Model;
using DigitalGlass.ViewModel;

namespace DigitalGlass.Commands
{
    /// <summary>
    /// Adds a new LED to the model based on the mouseup event
    /// </summary>
    public class PlaceLedCommand : ICanvasHostCommand
    {
        private CanvasHostViewModel _viewModel;

        public PlaceLedCommand(CanvasHostViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Execute(Point startClick, Point endClick)
        {
            Animation a = Animation.getInstance();
            if (!a.PointInCell(startClick))
            {
                //Create Cell to Place LED in
                SystemSounds.Beep.Play();
                var command = CanvasHostCommandFactory.Create(_viewModel, CanvasHostMode.PlaceLEDWithoutCell);
                command.Execute(startClick, endClick);

                a.ToString();
            }

            Cell c = a.findCell(startClick);

            Led l = new Led
            {
                 X = endClick.X,
                 Y = endClick.Y
            };

            _viewModel.Leds.Add(l);
            c.addLed(l);

        }
    }
}