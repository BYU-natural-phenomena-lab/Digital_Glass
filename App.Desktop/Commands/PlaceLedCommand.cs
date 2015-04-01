using System.Windows;
using Walle.Model;
using Walle.ViewModel;

namespace Walle.Commands
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
            _viewModel.Leds.Add(new Led
            {
                X = endClick.X,
                Y = endClick.Y
            });
        }
    }
}