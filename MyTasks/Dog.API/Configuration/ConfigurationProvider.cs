using Microsoft.Extensions.Configuration;

namespace Dog.API.Configuration
{
  public class ConfigurationProvider
  {
    IConfigurationRoot _configuration;

    public ConfigurationProvider()
    {
      _configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();
    }

    public DogSettings GetDogSettings() => _configuration.GetSection("DogSettings").Get<DogSettings>();
  }
}
