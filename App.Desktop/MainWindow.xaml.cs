using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using Walle.Model;

namespace Walle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFileCommand_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Images|*.jpg;*.png;*.gif";
            var result = dialog.ShowDialog();
            if (result == true)
            {
                MainImage.Source = new BitmapImage(new Uri(dialog.FileName));
            }

        }

        private void MainImage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CloseFileCommand_OnClick(object sender, RoutedEventArgs e)
        {
            MainImage.Source = null;
        }

        private void QuitCommand_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MainImage_OnMouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(sender as IInputElement);
            Coordinates.Content = String.Format("{0:F0},{1:F0}", point.X, point.Y);
        }

        private void MainImage_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Coordinates.Content = "";
        }
    }
}
