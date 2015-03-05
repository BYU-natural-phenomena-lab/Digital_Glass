using System.Linq;
using System.Windows;
using Walle.Model;
using Walle.ViewModel;

namespace Walle.Commands
{
    public class FindCellCommand : ICanvasHostCommand
    {
        private readonly CanvasHostViewModel _viewModel;

        public FindCellCommand(CanvasHostViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Execute(Point startClick, Point endClick)
        {
            var pt = new System.Drawing.Point((int) startClick.X, (int) startClick.Y);
            var finder = new RegionFinder(_viewModel.Image, pt, _viewModel.Tolerance);
            finder.OnLineFound +=
                line => _viewModel.Cells.Add(new CellBoundaries {Points = line.Select(p => new Point(p.X, p.Y)).ToArray()});
            finder.Process();
        }
    }
}