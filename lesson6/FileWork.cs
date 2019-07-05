using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lesson6
{
    class FileWork
    {
        private string _DirectoryPath;
        private string _SearchPattern = "*.txt";
        private string _ResultFile;
        static ReaderWriterLock rwl = new ReaderWriterLock();

        public FileWork()
        {
        }

        public FileWork(string DirectoryPath, string ResultFile, string SearchPattern)
        {
            _DirectoryPath = DirectoryPath;
            _ResultFile = ResultFile;
            _SearchPattern = SearchPattern;
        }

        public void WriteResults()
        {
            var ListFiles = Directory.GetFiles(_DirectoryPath, _SearchPattern);
            if (File.Exists(_ResultFile)) File.Delete(_ResultFile);
            var CountFiles = ListFiles.Length;
            int BatchAmount = 98;
            var NumBatches = (int)Math.Ceiling((decimal)CountFiles / (decimal)BatchAmount);
            Task[] ListTask = new Task[NumBatches];
            int Counter = 0;
            int CurrentBatch = 0;
            List<string> Results = new List<string>();
            string res;
            for (int i = 0; i < CountFiles; i++)
            {
                Counter++;
                FileInfo fi1 = new FileInfo(ListFiles[i]);
                res = $"{fi1.Name} :: {FileWork.GetMathResult(File.ReadAllLines(ListFiles[i])[0])}";
                Results.Add(res);

                if (Counter > BatchAmount || i == CountFiles - 1)
                {
                    List<string> Batch = new List<string>(Results);
                    
                    ListTask[CurrentBatch] = new Task(() => this.WriteToResource(98, Batch));

                    CurrentBatch++;
                    Results.Clear();
                    Counter = 0;
                }
            }
            for (var i = CurrentBatch - 1; i >= 0; i--)
            {
                var iAlive = ListTask[i].Status;
                ListTask[i].Start();
                ListTask[i].Wait();

                Thread.Sleep(50);
            }
        }

        private void WriteToResource(int timeOut, List<string> ListToWrite)
        {
            try
            {
                rwl.AcquireWriterLock(timeOut);
                try
                {
                    File.AppendAllLines(_ResultFile, ListToWrite);
                }
                finally
                {
                    rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
                Debug.WriteLine("В отведенное время поток не смог записать данные в файл.");
            }
        }

        public static decimal GetMathResult(string data, char Separator = ' ')
        {
            if (string.IsNullOrWhiteSpace(data)) throw new Exception("Входная строка пустая");
            string CurrentDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            switch (CurrentDecimalSeparator)
            {
                case ",":
                    data = data.Replace(".", ",");
                    break;
                case ".":
                    data = data.Replace(",", ".");
                    break;
            }
            string[] items = data.Split(Separator);
            if (items.Length != 3) throw new Exception("Ошибка в данных. Количество элементов меньше 3.");
            if (!int.TryParse(items[0], out int NumOperator)) throw new Exception("Ошибка определения кода оператоа");
            if (!(NumOperator == 1 || NumOperator == 2)) throw new Exception("Неверный код оператора");
            if(!decimal.TryParse(items[1], out decimal Num1)) throw new Exception("Ошибка определения первого числа");
            if(!decimal.TryParse(items[2], out decimal Num2)) throw new Exception("Ошибка определения второго числа");
            if(Num2 == 0) throw new Exception("Деление на ноль");
            switch (NumOperator)
            {
                case 1:
                    return Num1 * Num2;
                default:
                    return Num1 / Num2;
            }
        }
    }
}
