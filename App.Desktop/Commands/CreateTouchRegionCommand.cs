using C5;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
//using System.Windows;
using DigitalGlass.Model;
using DigitalGlass.ViewModel;

namespace DigitalGlass.Commands
{
    /// <summary>
    /// Used to select the color a region should be colored
    /// </summary>
    public class CreateTouchRegionCommand : ICanvasHostCommand
    {
        private readonly CanvasHostViewModel _viewModel;
        
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="viewModel"></param>
        public CreateTouchRegionCommand(CanvasHostViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Execute(System.Windows.Point startClick, System.Windows.Point endClick)
        {
            Animation a = Animation.getInstance();

           // if (a.touchRegions.Count <= 12)
          //  {
                TouchRegion t = new TouchRegion
                {
                    X = endClick.X,
                    Y = endClick.Y,
                    number = a.touchRegions.Count + 1
                };


                _viewModel.TouchRegions.Add(t);
                a.addTouchRegion(t);
         //   }
       
        }

    }
}