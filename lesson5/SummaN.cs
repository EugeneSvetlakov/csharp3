using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson5
{
    static class SummaN
    {
        public static int? GetSumm(int num)
        {
            if (num < 0) return null;
            int res = 0;
            for (var i = 0; i <= num; i++)
            {
                res += i;
            }
            return res;
        }


    }
}
