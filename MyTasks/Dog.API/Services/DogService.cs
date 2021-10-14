using Dog.API.Configuration;
using Dog.API.Model;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dog.API.Services
{
  public class DogService
  {
    private ResponseModel _dogResult;
    private string _url;
    private RequestSender _requestSender;

    public DogService(RequestSender requestSender, ConfigurationProvider configuration)
    {
      _requestSender = requestSender;
      var config = configuration.GetDogSettings();
      _url = config.Url;
    }

    public string GetPicture()
    {
      _dogResult = _requestSender.Get<ResponseModel>(_url);
      return _dogResult.Message;
    }

    public void SavePictureInDesktop(string pictureUrl)
    {
      //Разделяю строку "pictureUrl" символом "/" и заношу получившиеся строки в массив. 
      var tmpArray = pictureUrl.Split("/");
      // Получаю путь к рабочему столу.
      var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
      
      //Создаю объект DictionaryInfo, с помощью которого буду создавать каталог на рабочем столе.
      DirectoryInfo dirInfo = new DirectoryInfo($"{desktopPath}\\{tmpArray[4]}");
      if (!dirInfo.Exists)
      {
        dirInfo.Create();
      }

      // С помощью webClient сохраняю картинку в созданный каталог.
      WebClient webClient = new WebClient();
      
      webClient.DownloadFile(pictureUrl, $"{desktopPath}\\{tmpArray[4]}\\{tmpArray[5]}");
    }
  }
}
