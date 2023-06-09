﻿using System;
using System.Net.Http;

namespace Dog.API.Services
{
  /// <summary>
  ///  Универсальный класс по отправки запросов к API.
  /// </summary>
  public class RequestSender
  {
    private HttpClient _client;

    public RequestSender()
    {
      _client = new HttpClient();
    }

    public T Get<T>(string url)
    {
      var response = _client.GetAsync(url).Result;
      response.EnsureSuccessStatusCode();

      return response.Content.ReadAsAsync<T>().Result;
    }
  }
}
