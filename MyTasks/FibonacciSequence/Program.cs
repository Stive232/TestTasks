using System;
using System.Collections.Generic;

namespace FibonacciSequence
{
  class Program
  {
    static void Main(string[] args)
    {
      //Напишите программу для вывода следующих N чисел Фибоначчи за заданным целым числом X.
     
      Console.WriteLine("Введите кол-во чисел Фибоначчи, которые нужно вывести на экран");
      int quantity = int.Parse(Console.ReadLine());
      
      Console.WriteLine("За каким целым числом вывести последовательность?");
      int startNumber = int.Parse(Console.ReadLine());

      /// <summary>
      /// Переменные для расчета ряда Фибоначчи.  
      /// </summary>
      int x = 1, y = 1, i = 0;

      /// <summary>
      ///  Цикл на 36 строке вычисляет ряд Фибоначчи начиная с "2". Поэтому нужно учесть (startNumber < 1).  
      /// </summary>
      if (startNumber < 1)
      {
        Console.Write("1 1 ");
        i = 2;
      }

      /// <summary>
      /// Использую цикл for для вычисления значений ряда Фибоначчи, если (startNumber >= y),
      /// уменьшаю счетчик на "1" в консоль значение "y" не выводим. 
      /// </summary>
      for (; i < quantity; i++)
      {
        y = x + y;
        x = y - x;
        if (startNumber >= y)
        {
          i--;
          continue;
        }       
        Console.Write($"{y} ");
      }
    }
  }
}
