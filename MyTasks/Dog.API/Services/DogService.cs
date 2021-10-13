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
      var tmp = pictureUrl.Split("/");
      var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
      
      DirectoryInfo dirInfo = new DirectoryInfo($"{desktopPath}\\{tmp[4]}");
      if (!dirInfo.Exists)
      {
        dirInfo.Create();
      }
      WebClient webClient = new WebClient();
      
      webClient.DownloadFile(pictureUrl, $"{desktopPath}\\{tmp[4]}\\{tmp[5]}");
    }
  }
}
