using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Walle.Annotations;

namespace Walle.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IReadOnlyCollection<CommandViewModel> _commands;
        private string _coordinates;
        private CanvasHostViewModel _canvasHost;
        public event Action RequestClose;

        public CanvasHostViewModel CanvasHost
        {
            get { return _canvasHost; }
            set
            {
                if (Equals(value, _canvasHost)) return;
                _canvasHost = value;
                OnPropertyChanged();
            }
        }


        protected virtual void OnRequestClose()
        {
            var handler = RequestClose;
            if (handler != null) handler();
        }

        [UsedImplicitly]
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
                new CommandViewModel("Open...", "Resources/folder_Open_32xLG.png",
                    new RelayCommand(param => this.OpenFile())),
                new CommandViewModel("Close...", new RelayCommand(param => this.CloseFile(), param => this.CanClose)),
                new CommandViewModel("Exit...", "Resources/Close_16xLG.png", new RelayCommand(param => this.CloseApp())),
            };
        }

        private void CloseApp()
        {
            this.OnRequestClose();
        }

        private void CloseFile()
        {
            throw new NotImplementedException();
        }

        public bool CanClose { get; set; }

        private void OpenFile()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Images|*.jpg;*.png;*.gif";
            var result = dialog.ShowDialog();
            if (result == true)
            {
                CanvasHost = new CanvasHostViewModel(new BitmapImage(new Uri(dialog.FileName)));
            }
        }

        public void UpdateCoordinates(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(sender as IInputElement);
            this.Coordinates = String.Format("{0:F0},{1:F0}", point.X, point.Y);
        }

        public string Coordinates
        {
            get { return _coordinates; }
            set
            {
                if (value == _coordinates) return;
                _coordinates = value;
                OnPropertyChanged();
            }
        }
    }
}