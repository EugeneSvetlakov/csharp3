using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson5
{
    /// <summary>
    /// Класс для вычисления факториала целого числа > 0
    /// </summary>
    static class factorial
    {
        private static int _Res;

        public static int GetFactorial(int num)
        {
            if (num < 0) throw new ArgumentOutOfRangeException();
            if (num >= 0) _Res = 1;
            for (int i = 1; i <= num; i++)
            {
                _Res = _Res * i;
            }
            return _Res;
        }
    }
}
