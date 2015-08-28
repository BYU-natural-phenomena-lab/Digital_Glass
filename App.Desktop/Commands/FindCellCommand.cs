using System.Linq;
using System.Windows;
using DigitalGlass.Model;
using DigitalGlass.ViewModel;

namespace DigitalGlass.Commands
{
    /// <summary>
    /// Finds the path around the region of color that matches the starting point. Adds the cell to the model.
    /// </summary>
    public class FindCellCommand : ICanvasHostCommand
    {
        private readonly CanvasHostViewModel _viewModel;
        System.Windows.Forms.ColorDialog colorDlg = new System.Windows.Forms.ColorDialog();
        bool askForColor = true;
        System.Drawing.Color fillColor;

        public FindCellCommand(CanvasHostViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public FindCellCommand(CanvasHostViewModel viewModel, System.Drawing.Color fillColor) : this(viewModel)
        {
            //Will fill the cell without asking for a color from the color dialog
            askForColor = false;
            this.fillColor = fillColor;
        }

        public void Execute(Point startClick, Point endClick)
        {
            var pt = new System.Drawing.Point((int) startClick.X, (int) startClick.Y);

            if (askForColor)
            {
                colorDlg.ShowDialog();
                fillColor = colorDlg.Color;
            }
  
            Animation animation = Animation.getInstance();
            if (animation.PointInCell(startClick))
            {
                //Already Exisiting Cell
                Cell c = animation.findCell(startClick);
                int frame = _viewModel.currentFrame;
                c.color[frame] = fillColor;

                //Trigers and update on the View
                _viewModel.Cells.Remove(c);
                _viewModel.Cells.Add(c);
            }
            else
            {

                var finder = new RegionFinder(_viewModel.Image, pt, _viewModel.Tolerance);
                finder.LineFound +=
                    line => _viewModel.Cells.Add(new Cell(line.Select(p => new Point(p.X, p.Y)).ToArray(), animation.numFrames(), _viewModel.currentFrame, fillColor));
                finder.Process();

                //Add to the Model
                animation.addCell(_viewModel.Cells.Last());
               
            }
        }
    }
}