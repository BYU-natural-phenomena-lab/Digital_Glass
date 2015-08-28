using System;
using System.Windows;
using System.Windows.Controls;
using DigitalGlass.ViewModel;

namespace DigitalGlass
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ((System.Windows.Controls.ListView)e.OriginalSource).SelectedIndex;
            CanvasHostViewModel.self.moveToFrame(index);
        }
    }
}