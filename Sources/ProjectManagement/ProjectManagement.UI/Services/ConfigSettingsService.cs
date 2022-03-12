using Microsoft.Extensions.Configuration;
using ProjectManagement.UI.Constants;
using ProjectManagement.UI.Services.Interfaces;

namespace ProjectManagement.UI.Services
{
    public class ConfigSettingsService : IConfigSettingsService
    {
        #region Field
        private readonly IConfigurationBuilder _configurationBuilder;
        #endregion

        #region Ctor
        public ConfigSettingsService(IConfigurationBuilder configurationBuilder)
        {
            _configurationBuilder = configurationBuilder;
        }
        #endregion

        #region Methods
        public string GetConnectionString()
        {
            IConfiguration configuration = _configurationBuilder.AddJsonFile(ConfigFilePathConstants.AppSettingsConfigFile).Build();
            return configuration.GetConnectionString(ConfigSettingNameConstants.AppSettingsConfigName);
        } 
        #endregion
    }
}