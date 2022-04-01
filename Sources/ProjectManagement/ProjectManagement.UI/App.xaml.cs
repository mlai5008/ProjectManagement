using Autofac;
using ProjectManagement.UI.Startup;
using ProjectManagement.UI.Views;
using System.Windows;
using System.Windows.Threading;

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

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occured.", "Unexpected error");
            e.Handled = true;
        }
        #endregion
    }
}