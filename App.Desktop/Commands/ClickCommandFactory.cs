using System.Drawing;
using Walle.ViewModel;

namespace Walle.Commands
{
    /// <summary>
    /// Maps the canvashost mode into which command to execute on click
    /// </summary>
    public class CanvasHostCommandFactory
    {
        public static ICanvasHostCommand Create(CanvasHostViewModel viewModel,  CanvasHostMode mode)
        {
            switch (mode)
            {
                case CanvasHostMode.ColorFill:
                    return new FindCellCommand(viewModel);

                case CanvasHostMode.PlaceLED:
                    return new PlaceLedCommand(viewModel);

                case CanvasHostMode.CreateTouchRegion:
                    return new CreateTouchRegionCommand(viewModel);

                case CanvasHostMode.PlaceLEDWithoutCell:
                    return new FindCellCommand(viewModel, Color.Black);

                case CanvasHostMode.None:
                default:
                    return null;
            }


        }
    }
}