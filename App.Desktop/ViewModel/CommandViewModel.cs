using System;
using System.Windows.Input;

namespace Walle.ViewModel
{
    public class CommandViewModel : ViewModelBase
    {
        public CommandViewModel(string displayName,string imageUri, ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            base.DisplayName = displayName;
            this.ImageUri = imageUri;
            this.Command = command;
        }

        public string ImageUri { get; private set; }

        public ICommand Command { get; private set; }
    }
}