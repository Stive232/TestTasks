using System;
using Dog.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dog.API.Configuration
{
  public static class DiContainer
  {
    static ServiceProvider _serviceProvider;
    static DiContainer()
    {
      _serviceProvider = new ServiceCollection()
        .AddSingleton<RequestSender>()
        .AddSingleton<DogService>()
        .AddSingleton<ConfigurationProvider>()
        .BuildServiceProvider();
    }

    public static T GetService<T>() => _serviceProvider.GetService<T>();
  }
}
