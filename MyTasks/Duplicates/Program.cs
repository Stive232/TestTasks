using System;
using System.Collections.Generic;

namespace Duplicates
{
  class Program
  {
    static void Main(string[] args)
    {
      /*Дан массив элементов. Необходимо найти все значения-дубликаты в массиве, подсчитать 
       * их количество и вывести результат в консоль.*/

      int[] array = { 2, 2, 3, 4, 5, 6, 7, 7, 7, 8, 10};
      /// <summary>
      /// Хранилилище Dictionary<число, кол-во>.
      /// </summary>
      var quantityNumbers = new Dictionary<int, int>();

      /// <summary>
      /// Проходим циклом по массиву и проверяем есть ли данный ключ в Dictionary,
      /// если есть, то значение увеличивается на 1, если нет, то кол-во = 1.
      /// </summary>
      foreach (var i in array)
      {
        if (quantityNumbers.ContainsKey(i))
          quantityNumbers[i]++;
        else
          quantityNumbers[i] = 1;
      }

      foreach (var i in quantityNumbers)
      {
        if (i.Value > 1)
          Console.WriteLine("Значение {0}  Кол-во {1}", i.Key, i.Value);
      }
      Console.ReadKey();
    }
  }
}
