using Microsoft.Extensions.Configuration;
using ProjectManagement.Infrastructure.Constants;
using ProjectManagement.Infrastructure.Services.Interfaces;

namespace ProjectManagement.Infrastructure.Services
{
    public class ConfigSettingsService : IConfigSettingsService
    {
        #region Field
        private readonly IConfigurationBuilder _configurationBuilder;
        private string _connectionString = string.Empty;
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
            return string.IsNullOrWhiteSpace(_connectionString) ? BuilderConnectionString() : _connectionString;
        }

        private string BuilderConnectionString()
        {
            IConfiguration configuration = _configurationBuilder.AddJsonFile(ConfigFilePathConstants.AppSettingsConfigFile).Build();
            return _connectionString = configuration.GetConnectionString(ConfigSettingNameConstants.AppSettingsConfigName);
        }
        #endregion
    }
}