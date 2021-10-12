using System;
using System.Collections.Generic;

namespace Duplicates
{
  class Program
  {
    static void Main(string[] args)
    {
      int[] array = { 2, 2, 3, 4, 5, 6, 7, 7, 7, 8, 10};
      var duplicateValue = new Dictionary<int, int>();

      foreach (var i in array)
      {
        if (duplicateValue.ContainsKey(i))
          duplicateValue[i]++;
        else
          duplicateValue[i] = 1;
      }

      foreach (var i in duplicateValue)
      {
        if (i.Value > 1)
          Console.WriteLine("Значение {0}  Кол-во {1}", i.Key, i.Value);
      }
      Console.ReadKey();
    }
  }
}
