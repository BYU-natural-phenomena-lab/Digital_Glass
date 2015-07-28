using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Walle.Annotations;
using Walle.Eagle;
using Walle.Model;

namespace Walle.ViewModel
{
    /// <summary>
    /// Controls the main window of the application.
    /// <seealso cref="MainWindow.xaml"/>
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IReadOnlyCollection<CommandViewModel> _commands;
        private IReadOnlyCollection<CommandViewModel> _toolbarCommands;
        private string _coordinates;
        private CanvasHostViewModel _canvasHost;


        public MainWindowViewModel()
        {
            _commands = new ReadOnlyCollection<CommandViewModel>(CreateCommands());
            _toolbarCommands = new ReadOnlyCollection<CommandViewModel>(CreateToolbarCommands());
        }


        public event Action RequestClose;

        /// <summary>
        /// Controls the center of the application. The drawing layer.
        /// </summary>
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

        /// <summary>
        /// Command on the left sidebar of the application
        /// </summary>
        [UsedImplicitly]
        public IReadOnlyCollection<CommandViewModel> Commands
        {
            get { return _commands; }
        }

        /// <summary>
        /// Commands in the "File" menu
        /// </summary>
        [UsedImplicitly]
        public IReadOnlyCollection<CommandViewModel> ToolbarCommands
        {
            get { return _toolbarCommands; }
        }

        private IList<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>()
            {
                new CommandViewModel("Open...", "Resources/folder_Open_32xLG.png",
                    new RelayCommand(param => this.OpenFile())),
                new CommandViewModel("Export to Eagle BRD...", null,
                    new RelayCommand(param => this.ExportEagle(), param => this.ImageLoaded)),
                new CommandViewModel("Export to Gerber...", null,
                    new RelayCommand(param => this.ExportGerber(), param => this.ImageLoaded)),
                new CommandViewModel("Close...", new RelayCommand(param => this.CloseFile(), param => this.CanClose)),
                new CommandViewModel("Exit...", "Resources/Close_16xLG.png", new RelayCommand(param => this.CloseApp())),
            };
        }

        /// <summary>
        /// Creates an in memory representation of an LED PCB using the data from the canvashost model.
        /// </summary>
        /// <returns></returns>
        private LedBoardBuilder CreateBoardFromModel()
        {
            var ledBoard = new LedBoardBuilder(this.CanvasHost.BoardWidth, this.CanvasHost.BoardHeight);
            Animation a = Animation.getInstance();
            foreach (var led in a.getLeds())
            {
                ledBoard.AddLedAtPoint(led.X * _canvasHost.imageToOutputScale, (this._canvasHost.ImageHeight - led.Y) * _canvasHost.imageToOutputScale); // transform to board space
            }
            ledBoard.attachToOutputPin();

            foreach(var touchRegion in a.touchRegions)
            {
                ledBoard.AddTouchPadAtPoint(touchRegion.X * _canvasHost.imageToOutputScale, (this._canvasHost.ImageHeight - touchRegion.Y) * _canvasHost.imageToOutputScale);
            }
      
            return ledBoard;
        }

        /// <summary>
        /// Runs the export to Gerber and saves all the files in a zip. <seealso cref="README.md"/>
        /// </summary>
        private void ExportGerber()
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Zip File|*.zip";
            var result = dialog.ShowDialog();
            if (result != true) return;

            var exporter = new GerberExporter(dialog.FileName, CreateBoardFromModel());
            exporter.Export();
        }

        /// <summary>
        /// Exports only the Eagle board file. This is useful for inspecting manually what the application is generating.
        /// </summary>
        private void ExportEagle()
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Eagle Board File|*.brd";
            var result = dialog.ShowDialog();
            if (result != true) return;
            var exporter = new EagleExporter(dialog.FileName, CreateBoardFromModel());
            exporter.Export();
        }

        private IList<CommandViewModel> CreateToolbarCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel("Place LED", "Resources/LightBulb_32xMD.png",
                    new RelayCommand(param => this.CanvasHost.CanvasMode = CanvasHostMode.PlaceLED,
                        param => this.ImageLoaded)),

                new CommandViewModel("Choose Colors", "Resources/PaintBucket.ico",
                    new RelayCommand(param => this.CanvasHost.CanvasMode = CanvasHostMode.ColorFill,
                        param => this.ImageLoaded)),
            
                new CommandViewModel("Create Touch Region", "Resources/fingerTouch.png",
                    new RelayCommand(param => this.CanvasHost.CanvasMode = CanvasHostMode.CreateTouchRegion,
                        param => this.ImageLoaded)),
            };
        }

        private void CloseApp()
        {
            this.OnRequestClose();
        }

        private void CloseFile()
        {
            ImageLoaded = false;
            CanvasHost = null;
            CanClose = false;
        }

        public bool CanClose { get; set; }

        /// <summary>
        /// Loads the image file into a new CanvasHost model.
        /// </summary>
        private void OpenFile()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Images|*.jpg;*.png;*.gif";
            var result = dialog.ShowDialog();
            if (result == true)
            {
                CanvasHost = new CanvasHostViewModel(new Uri(dialog.FileName));
                ImageLoaded = true;
                CanClose = true;
            }
        }

        public bool ImageLoaded { get; set; }

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