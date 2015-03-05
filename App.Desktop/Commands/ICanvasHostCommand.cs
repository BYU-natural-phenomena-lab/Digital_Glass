using System.Windows;

namespace Walle.Commands
{
    public interface ICanvasHostCommand
    {
        void Execute(Point startClick, Point endClick);
    }
}