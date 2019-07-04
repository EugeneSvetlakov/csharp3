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
            //WorkWithNumber();

            // Задание 2: Написать приложение, 
            // выполняющее парсинг CSV-файла произвольной структуры 
            // и сохраняющее его в обычный TXT-файл.
            // Все операции проходят в потоках. 
            // CSV-файл заведомо имеет большой объём.

            string path_file = "..\\..\\test_data\\";
            string csv_file = "tmp";
            char separator = '#';
            CsvToTxt csvToTxt = new CsvToTxt($"{path_file}{csv_file}.csv", $"{path_file}converted_{csv_file}.txt", separator);

            int count_lines = csvToTxt.LinesInCsv();

            Console.WriteLine("Для начала конвертации нажмите любую клавишу...");
            Console.ReadKey();
            Console.WriteLine($"Конвертация {count_lines} строк в исходном csv-файле запущена.");

            Thread thread_txt = new Thread(() => csvToTxt.Convert(count_lines));
            thread_txt.Start();
            thread_txt.Join();

            Console.WriteLine($"Конвертация завершена. Переписано: {csvToTxt.LinesInTxt()} строк.");
            Console.Write("Для выхода нажмите любую клавишу...");
            Console.ReadKey();
        }

        private static void WorkWithNumber()
        {
            // Задание 1: 
            // а) факториал числа N, которое вводится с клавиатуру
            // b. сумму целых чисел до N, которое также вводится с клавиатуры
            Console.Write("Введите число > 0: ");
            while (int.TryParse(Console.ReadLine(), out int num))
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
