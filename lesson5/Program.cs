using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lesson5
{
    class Program
    {
        static void Main(string[] args)
        {
            // Задание 1: 
            // а) факториал числа N, которое вводится с клавиатуру
            // b. сумму целых чисел до N, которое также вводится с клавиатуры
            Console.Write("Введите число > 0: ");
            while(int.TryParse(Console.ReadLine(), out int num))
            {
                var internal_num = num;
                var thread1 = new Thread(() => Console.WriteLine($"Факториал {internal_num} = {factorial.GetFactorial(internal_num)}"));
                thread1.Start();
                thread1.Join(100);
                var thread2 = new Thread(() => Console.WriteLine($"Сумма чисел до {internal_num} = {SummaN.GetSumm(internal_num)}"));
                thread2.Start();
                thread2.Join(100);
                Console.Write("Введите число > 0: ");
            }
        }
    }
}
