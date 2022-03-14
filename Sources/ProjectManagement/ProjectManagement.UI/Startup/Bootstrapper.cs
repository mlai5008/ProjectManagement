using Autofac;
using Microsoft.Extensions.Configuration;
using ProjectManagement.DataAccess.Context;
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
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf().SingleInstance();
            builder.RegisterType<MainViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<ProjectManagementDbContext>().AsSelf();
            builder.RegisterType<DeveloperDataService>().As<IDeveloperDataService>().SingleInstance();
            builder.RegisterType<ConfigSettingsService>().As<IConfigSettingsService>().SingleInstance();
            builder.Register(_ => new ConfigurationBuilder()).As<IConfigurationBuilder>().SingleInstance();

            return builder.Build();
        }
        #endregion
    }
}