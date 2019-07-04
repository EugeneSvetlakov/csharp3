using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson6
{
    class dataFromFile
    {
        int _Operator;
        decimal _Num1;
        decimal _Num2;

        public dataFromFile()
        {

        }

        public dataFromFile(int Operator, decimal num1, decimal num2)
        {
            _Operator = Operator;
            _Num1 = num1;
            _Num2 = num2;
        }
    }

    class FileWork
    {
        private string _DirectoryPath;
        private string _SearchPattern = "*.txt";
        private string _ResultFile;

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
            
            var CountFiles = ListFiles.Length;
            List<string> Results = new List<string>();
            int BatchAmount = 100;
            int Counter = 0;
            string res;
            TaskFactory taskFactory = new TaskFactory();
            for (int i = 0; i < CountFiles; i++)
            {
                Counter++;
                FileInfo fi1 = new FileInfo(ListFiles[i]);
                res = $"{fi1.Name} :: {FileWork.GetMathResult(File.ReadAllLines(ListFiles[i])[0])}";
                Results.Add(res);

                //if (Counter > BatchAmount || i == CountFiles - 1)
                //{
                //    taskFactory.StartNew(() =>
                //    {
                //        var Batch = Results;
                //        File.AppendAllLines(_ResultFile, Batch);
                //    });
                //    Results.Clear();
                //    Counter = 0;
                //}
            }
            File.AppendAllLines(_ResultFile, Results);
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
