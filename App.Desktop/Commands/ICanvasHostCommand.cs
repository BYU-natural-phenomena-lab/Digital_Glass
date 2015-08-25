using System.Windows;

namespace DigitalGlass.Commands
{
    public interface ICanvasHostCommand
    {
        void Execute(Point startClick, Point endClick);
    }
}