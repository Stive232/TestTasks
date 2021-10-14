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
      /*Необходимо, используя класс HttpClient, реализовать клиент для загрузки случайной картинки
       * с сайта https://dog.ceo/dog-api/. В результате исполнения программы на рабочем столе должен
       * появиться каталог, в котором будет сохранена картинка, именем каталога должна быть 
       * соответствующая порода собаки. 
         При использовании класса HttpClient не должно создаваться больше одного соединения.*/

      var dogService = DiContainer.GetService<DogService>();
      var pictureUrl = dogService.GetPicture();
      dogService.SavePictureInDesktop(pictureUrl);
    }
  }
}
