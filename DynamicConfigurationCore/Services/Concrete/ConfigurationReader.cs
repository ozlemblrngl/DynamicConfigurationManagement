using DynamicConfigurationCore.Providers.Abstract;
using DynamicConfigurationCore.Providers.Concrete;
using DynamicConfigurationCore.Services.Abstract;
using Entities;

namespace DynamicConfigurationCore.Services.Concrete
{
	public class ConfigurationReader : IConfigurationReader
	{
		private readonly string _applicationName;
		private readonly string _connectionString;
		private readonly int _refreshTimerIntervalInMs;
		private readonly IConfigProvider _provider;
		private System.Timers.Timer _refreshTimer;
		private Dictionary<string, Configuration> _configurations;

		public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs)
		{
			_applicationName = applicationName;
			_connectionString = connectionString;
			_refreshTimerIntervalInMs = refreshTimerIntervalInMs;
			_provider = new MssqlConfigurationProvider();
			_configurations = new Dictionary<string, Configuration>();

			LoadConfigurations();
			StartRefreshTimer();

		}

		private void LoadConfigurations()
		{

			var configurations = _provider.LoadConfigurations(_applicationName, _connectionString);

			_configurations = configurations.ToDictionary(c => c.Name, c => c);
		}

		private void StartRefreshTimer()
		{
			_refreshTimer = new System.Timers.Timer(_refreshTimerIntervalInMs);
			_refreshTimer.Elapsed += (sender, args) => LoadConfigurations();
			_refreshTimer.Start();
		}

		public T GetValue<T>(string key)
		{
			if (_configurations.ContainsKey(key))
			{
				var configItem = _configurations[key];

				if (typeof(T) == typeof(int) && int.TryParse(configItem.Value, out var intValue))
				{
					return (T)(object)intValue;
				}

				if (typeof(T) == typeof(bool) && bool.TryParse(configItem.Value, out var boolValue))
				{
					return (T)(object)boolValue;
				}

				if (typeof(T) == typeof(double) && double.TryParse(configItem.Value, out var doubleValue))
				{
					return (T)(object)doubleValue;
				}

				return (T)(object)configItem.Value;
			}

			throw new KeyNotFoundException($"Key '{key}' not found in configuration.");
		}
	}
}
