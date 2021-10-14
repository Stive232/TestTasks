using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dog.API.Model
{
  /// <summary>
  ///  ResponseModel, при получении рандомной картинки от dog-api.
  /// </summary>
  public class ResponseModel
  {
    public string Message { get; set; }
    public string Status  { get; set; }
  }
}
