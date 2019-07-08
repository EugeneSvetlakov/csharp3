using System;
using System.Collections.Generic;
using System.Management;
using Microsoft.VisualBasic.Devices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace lesson6
{


    class Program
    {
        static void Main(string[] args)
        {
            #region Получение информации о памяти в Мегабайтах (Мб)
            var AvailablePhysicalMemory = (new ComputerInfo().AvailablePhysicalMemory) / 1048576;
            var TotalPhysicalMemory = (new ComputerInfo().TotalPhysicalMemory) / 1048576;
            var currentProc = Process.GetCurrentProcess().PrivateMemorySize64 / 1048576;
            #endregion
            Console.WriteLine("Данные о памяти получены. Для продолжения нажмите любую клавиш...");
            Console.ReadKey();

            #region ДЗ Задача 1.
            // Задание 1: Даны 2 двумерных матрицы. Размерность 100х100 каждая. 
            //Напишите приложение, производящее параллельное умножение матриц. 
            //Матрицы заполняются случайными целыми числами от 0 до10.

            var A = new MyMatrix(50, 2000000000);
            var A_row = A.Rows();
            var A_col = A.Columns();

            var B = new MyMatrix(50, 2000000000);
            var B_row = B.Rows();
            var B_col = B.Columns();

            var timestart1 = DateTime.Now;
            var AB = A.Myltiply(B);
            var timestart1End = DateTime.Now;
            Console.WriteLine($"Myltiply выполнена за {timestart1End - timestart1}");

            var timestart2 = DateTime.Now;
            var AB2 = A.MyltiplyParallele(B);
            Console.WriteLine($"MyltiplyParallele выполнена за {DateTime.Now - timestart2}");

            Pause();
            #endregion

            #region ДЗ Задача 2.
            // Задание 2: В некой директории лежат файлы. 
            // По структуре они содержат 3 числа, разделенные пробелами. 
            // Первое число — целое, обозначает действие, 1 — умножение и 2 — деление, 
            // остальные два — числа с плавающей точкой. Написать многопоточное приложение, 
            // выполняющее вышеуказанные действия над числами и сохраняющее результат в файл result.dat. 
            // Количество файлов в директории заведомо много.

            string HomePath = @"..\..\test_data";
            string ResultFile = @"result.dat";
            string SearchPattern = "*.txt";
            if (!Directory.Exists(HomePath)) Directory.CreateDirectory(HomePath);

            if (Directory.GetFiles(HomePath, SearchPattern).Length < 1)
                throw new Exception($"Нет файлов для обработки в дириктории {(new DirectoryInfo(HomePath)).FullName}\nВыполните процедуру генерации тестовых файлов с данными.");
            
            #region Генерация тестовых файлов
            //GenerateTestDataFiles(HomePath);
            #endregion

            var time3 = DateTime.Now;
            FileWork fileWork =
                new FileWork($@"{HomePath}", $@"{HomePath}\{ResultFile}", SearchPattern);
            fileWork.WriteResults();
            Console.WriteLine($"Конвертация выполнена за: {DateTime.Now - time3}");

            Pause();
            #endregion
        }

        private static void GenerateTestDataFiles(string HomePath)
        {
            Random rnd = new Random();
            string tmp;
            for (int i = 0; i < 1000; i++)
            {
                var oper = rnd.Next(1, 2);
                var num1 = (decimal)(rnd.NextDouble() * 100);
                var num2 = (decimal)(rnd.NextDouble() * 100);
                tmp = $"{ oper} {num1} {num2}";
                File.AppendAllText($@"{HomePath}\file_0{i}.txt", tmp);
            }
        }

        private static void Pause()
        {
            Console.Write("Для завершения работы нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
