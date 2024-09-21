using DynamicConfiguration.Services.Abstract;

namespace DynamicConfiguration.Services.Concrete
{
	public class ConfigurationReader
	{
		private readonly string _applicationName;
		private readonly string _connectionString;
		private readonly int _refreshTimerIntervalInMs;
		private readonly IConfigurationService _configurationService;

		public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs, IConfigurationService configurationService)
		{
			_applicationName = applicationName;
			_connectionString = connectionString;
			_refreshTimerIntervalInMs = refreshTimerIntervalInMs;
			_configurationService = configurationService;

		}

		public T GetValue<T>(string key)
		{
			var config = _configurationService.GetConfigurationByName(_applicationName, key);

			if (config == null || !config.Result.IsActive)
				throw new KeyNotFoundException($"Key {key} not found for {_applicationName}");

			// Dönüş tipine göre ayarlama
			return (T)Convert.ChangeType(config.Result.Value, typeof(T));
		}
	}

}
