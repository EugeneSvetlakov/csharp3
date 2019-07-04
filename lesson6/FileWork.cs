using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson6
{
    class FileWork
    {
        public static string[] FileNames(string path)
        {
            return Directory.GetFiles(path);
        }

        public static string[] FileNames(string path, string searchPattern)
        {
            return Directory.GetFiles(path, searchPattern);
        }

        public static decimal ParceString(string data, char Separator = ' ')
        {
            decimal res;
            string[] items = data.Split(Separator);
            if (items.Length != 3) throw new Exception("Ошибка в данных. Количество элементов меньше 3.");
            if (!int.TryParse(items[0], out int NumOperator)) throw new Exception();
            if (!(NumOperator == 1 || NumOperator == 2)) throw new Exception();
            if(!decimal.TryParse(items[1], out decimal Num1)) throw new Exception();
            if(!decimal.TryParse(items[2], out decimal Num2)) throw new Exception();
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
