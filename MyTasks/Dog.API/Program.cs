using Dog.API.Configuration;
using Dog.API.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dog.API
{
  class Program
  {
    static async Task Main(string[] args)
    {
      var dogService = DiContainer.GetService<DogService>();
      var pictureUrl = dogService.GetPicture();
      dogService.SavePictureInDesktop(pictureUrl);
    }
  }
}
