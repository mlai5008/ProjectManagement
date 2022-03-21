using Autofac;
using Microsoft.Extensions.Configuration;
using Prism.Events;
using ProjectManagement.DataAccess.Context;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.Infrastructure.Services;
using ProjectManagement.Infrastructure.Services.Interfaces;
using ProjectManagement.UI.Services;
using ProjectManagement.UI.Services.Interfaces;
using ProjectManagement.UI.ViewModels;
using ProjectManagement.UI.Views;

namespace ProjectManagement.UI.Startup
{
    public class Bootstrapper
    {
        #region Methods
        public IContainer Bootstrap()
        {
            ContainerBuilder builder = new ContainerBuilder();

            RegisterWindows(builder);
            RegisterViewModels(builder);
            RegisterDbContexts(builder);
            RegisterServices(builder);
            RegisterTools(builder);

            return builder.Build();
        }

        private void RegisterWindows(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindow>().AsSelf().SingleInstance();
        }

        private void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>().AsSelf().SingleInstance();
            builder.RegisterType(typeof(NavigationViewModel)).As(typeof(INavigationViewModel)).SingleInstance();
            builder.RegisterType<DeveloperDetailViewModel>().As<IDeveloperDetailViewModel>().SingleInstance();
        }

        private void RegisterDbContexts(ContainerBuilder builder)
        {
            builder.RegisterType<ProjectManagementDbContext>().AsSelf();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigSettingsService>().As<IConfigSettingsService>().SingleInstance();
            builder.RegisterType<DeveloperDataService>().As<IDeveloperDataService>().SingleInstance();
            builder.RegisterType<DeveloperLookupDataService>().As<IDeveloperLookupDataService>().SingleInstance();
        }

        private void RegisterTools(ContainerBuilder builder)
        {
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.Register(_ => new ConfigurationBuilder()).As<IConfigurationBuilder>().SingleInstance();
        }
        #endregion
    }
}