internal class Program
{
	private static void Main(string[] args)
	{
		DynamicConfiguration.Services.ConfigurationService configurationService = new DynamicConfiguration.Services.ConfigurationService(new ConfigurationDbContext contex);
		configurationService.GetConfigurationById()
	}
}