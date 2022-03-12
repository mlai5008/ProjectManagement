using Autofac;
using ProjectManagement.UI.Startup;
using ProjectManagement.UI.Views;
using System.Windows;

namespace ProjectManagement.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Methods
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Bootstrapper bootstrapper = new Bootstrapper();
            IContainer container = bootstrapper.Bootstrap();
            MainWindow mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
        #endregion
    }
}
