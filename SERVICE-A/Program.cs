using DynamicConfigurationCore.Services.Concrete;

internal class Program
{
	private static void Main(string[] args)
	{
		ConfigurationReader configurationReader = new ConfigurationReader("SERVICE-A", "Server=DESKTOP-N9UK4PN;Database=ConfigurationDB;Trusted_Connection=True;TrustServerCertificate=True;", 15000);
		while (true)
		{
			var result = configurationReader.GetValue("SiteName");
			Console.WriteLine("For GetValue :" + result);

			var resultT = configurationReader.GetValue<String>("SiteName");
			Console.WriteLine("For GetValueT :" + resultT);
			Thread.Sleep(10000);
		}
	}
}