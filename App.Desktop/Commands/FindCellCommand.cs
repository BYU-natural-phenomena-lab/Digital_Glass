using System.Linq;
using System.Windows;
using Walle.Model;
using Walle.ViewModel;

namespace Walle.Commands
{
    /// <summary>
    /// Finds the path around the region of color that matches the starting point. Adds the cell to the model.
    /// </summary>
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
            finder.LineFound +=
                line => _viewModel.Cells.Add(new CellBoundaries {Points = line.Select(p => new Point(p.X, p.Y)).ToArray()});
            finder.Process();
        }
    }
}