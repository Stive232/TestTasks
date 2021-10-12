using System;
using System.Collections.Generic;

namespace FibonacciSequence
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Введите кол-во чисел Фибоначчи, которых нужно вывести на экран");
      int quantity = int.Parse(Console.ReadLine());
      
      Console.WriteLine("За каким целым числом вывести последовательность?");
      int startNumber = int.Parse(Console.ReadLine());

      int x = 1;
      int y = 1;
      int i = 0;

      if(startNumber < 1)
      {
        Console.Write("1 1 ");
        i = 2;
      }
        
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
