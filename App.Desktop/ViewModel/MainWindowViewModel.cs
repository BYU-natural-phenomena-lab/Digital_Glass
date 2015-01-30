using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Walle.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IReadOnlyCollection<CommandViewModel> _commands;
        public event Action RequestClose;

        protected virtual void OnRequestClose()
        {
            var handler = RequestClose;
            if (handler != null) handler();
        }

        public IReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_commands == null)
                {
                    _commands = new ReadOnlyCollection<CommandViewModel>(CreateCommands());
                }
                return _commands;
            }
        }

        private IList<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>()
            {
                new CommandViewModel("Open File","Resources/folder_Open_32xLG.png",new RelayCommand(param=>this.OpenFile())),
            };
        }

        private void OpenFile()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Images|*.jpg;*.png;*.gif";
            var result = dialog.ShowDialog();
            if (result == true)
            {
               // MainCanvas.Source = new BitmapImage(new Uri(dialog.FileName));
                //TODO load files
            }
        }
    }
}