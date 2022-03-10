using Autofac;
using ProjectManagement.UI.Services;
using ProjectManagement.UI.ViewModels;
using ProjectManagement.UI.Views;

namespace ProjectManagement.UI.Startup
{
    public class Bootstrapper
    {
        #region Methods
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<DeveloperDataService>().As<IDeveloperDataService>();

            return builder.Build();
        }
        #endregion
    }
}