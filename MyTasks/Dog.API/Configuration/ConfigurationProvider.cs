using Microsoft.Extensions.Configuration;

namespace Dog.API.Configuration
{
  /// <summary>
  ///  Класс для получения настроек из файла "appsettings.json"
  /// </summary>
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
