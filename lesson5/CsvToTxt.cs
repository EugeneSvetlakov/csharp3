using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson5
{
    class CsvToTxt
    {

        public void ToTxt(string path)
        {
            string line = string.Empty;
            System.IO.StreamReader file = 
                new System.IO.StreamReader($@"{path}");
            while ((line = file.ReadLine()) != null)
            {
                WriteTxt(ParceLine(line));
            }

            file.Close();
        }

        private string ParceLine(string line)
        {
            throw new NotImplementedException();
        }

        private void WriteTxt(string str)
        {
            throw new NotImplementedException();
        }
    }
}
