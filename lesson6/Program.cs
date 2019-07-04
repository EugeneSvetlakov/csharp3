using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lesson6
{
    class Program
    {
        static void Main(string[] args)
        {
            // Задание 1: Даны 2 двумерных матрицы. Размерность 100х100 каждая. 
            //Напишите приложение, производящее параллельное умножение матриц. 
            //Матрицы заполняются случайными целыми числами от 0 до10.

            //var A = new MyMatrix(13,2000000000);
            //var A_row = A.Rows();
            //var A_col = A.Columns();

            //var B = new MyMatrix(13,2000000000);
            //var B_row = B.Rows();
            //var B_col = B.Columns();

            //var timestart1 = DateTime.Now;
            //var AB = A.Myltiply(B);
            //Console.WriteLine($"Выполнено за {DateTime.Now - timestart1}");

            //var timestart2 = DateTime.Now;
            //var AB2 = A.MyltiplyParallele(B);
            //Console.WriteLine($"Выполнено за {DateTime.Now - timestart2}");

            //Pause();

            // Задание 2: В некой директории лежат файлы. 
            // По структуре они содержат 3 числа, разделенные пробелами. 
            // Первое число — целое, обозначает действие, 1 — умножение и 2 — деление, 
            // остальные два — числа с плавающей точкой. Написать многопоточное приложение, 
            // выполняющее вышеуказанные действия над числами и сохраняющее результат в файл result.dat. 
            // Количество файлов в директории заведомо много.

            string path = @"c:\tmp";
            string searchPattern = "*.pdf";
            string[] files = FileWork.FileNames(path);
            string[] files_pdf = FileWork.FileNames(path, searchPattern);


            Pause();
        }

        private static void Pause()
        {
            Console.Write("Для завершения работы нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
