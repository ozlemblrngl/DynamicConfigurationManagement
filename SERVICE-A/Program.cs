using DynamicConfigurationCore.Services.Concrete;

internal class Program
{
	private static void Main(string[] args)
	{
		ConfigurationReader configurationReader = new ConfigurationReader("SERVICE-A", "Server=DESKTOP-N9UK4PN;Database=ConfigurationDB;Trusted_Connection=True;TrustServerCertificate=True;", 100000);
		var result = configurationReader.GetValue<int>("MaxItemCount");
		Console.WriteLine(result);

	}
}