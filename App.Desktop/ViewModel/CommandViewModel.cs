using System;
using System.Windows.Input;

namespace DigitalGlass.ViewModel
{
    /// <summary>
    /// Representation of a command in the "File" menu.
    /// </summary>
    public class CommandViewModel : ViewModelBase
    {
        public CommandViewModel(string displayName, string imageUri, ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            base.DisplayName = displayName;
            this.ImageUri = imageUri;
            this.Command = command;
        }

        public CommandViewModel(string displayName, ICommand command)
            : this(displayName, null, command)
        {
        }

        public string ImageUri { get; private set; }

        public ICommand Command { get; private set; }
    }
}