using Autofac;
using Microsoft.Extensions.Configuration;
using Prism.Events;
using ProjectManagement.DataAccess.Context;
using ProjectManagement.Infrastructure.Interfaces.ViewModels;
using ProjectManagement.Infrastructure.Services;
using ProjectManagement.Infrastructure.Services.Interfaces;
using ProjectManagement.UI.Services;
using ProjectManagement.UI.Services.Interfaces;
using ProjectManagement.UI.Services.Lookups;
using ProjectManagement.UI.Services.Repositories;
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
            builder.RegisterType<MainWindow>().AsSelf();
        }

        private void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType(typeof(NavigationViewModel)).As(typeof(INavigationViewModel));
            builder.RegisterType<DeveloperDetailViewModel>().Keyed<IDetailViewModel>(nameof(DeveloperDetailViewModel));
            builder.RegisterType<MeetingDetailViewModel>().Keyed<IDetailViewModel>(nameof(MeetingDetailViewModel));
            builder.RegisterType<ProgrammingLanguageDetailViewModel>().Keyed<IDetailViewModel>(nameof(ProgrammingLanguageDetailViewModel));
        }

        private void RegisterDbContexts(ContainerBuilder builder)
        {
            builder.RegisterType<ProjectManagementDbContext>().AsSelf();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigSettingsService>().As<IConfigSettingsService>().SingleInstance();
            builder.RegisterType<DeveloperRepository>().As<IDeveloperRepository>();
            builder.RegisterType<MeetingRepository>().As<IMeetingRepository>();
            builder.RegisterType<ProgrammingLanguageRepository>().As<IProgrammingLanguageRepository>();
            builder.RegisterType<DeveloperLookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();
        }

        private void RegisterTools(ContainerBuilder builder)
        {
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.Register(_ => new ConfigurationBuilder()).As<IConfigurationBuilder>().SingleInstance();
        }
        #endregion
    }
}