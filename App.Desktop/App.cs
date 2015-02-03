using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Walle.ViewModel;

namespace Walle
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var window = new MainWindow();
            var model = new MainWindowViewModel();
            model.RequestClose += () => window.Close();
            window.DataContext = model;
            window.Show();
        }
    }
}