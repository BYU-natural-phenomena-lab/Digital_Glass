using System;
using Walle.ViewModel;

namespace Walle.Commands
{
    public class CanvasHostCommandFactory
    {
        public static ICanvasHostCommand Create(CanvasHostViewModel viewModel,  CanvasHostMode mode)
        {
            switch (mode)
            {
                case CanvasHostMode.FindCell:
                    return new FindCellCommand(viewModel);

                case CanvasHostMode.PlaceLED:
                    return new PlaceLedCommand(viewModel);

                case CanvasHostMode.None:
                default:
                    return null;
            }
        }
    }
}