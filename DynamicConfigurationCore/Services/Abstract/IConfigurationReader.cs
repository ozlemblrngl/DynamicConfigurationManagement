namespace DynamicConfigurationCore.Services.Abstract
{
    public interface IConfigurationReader
    {
        T GetValue<T>(string key);
    }
}
