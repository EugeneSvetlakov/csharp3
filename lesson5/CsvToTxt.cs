using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lesson5
{
    class CsvToTxt
    {
        public CsvToTxt(string csvFile, string txtFile, char separator)
        {
            CsvFile = csvFile;
            TxtFile = txtFile;
            Separator = separator;
        }

        public CsvToTxt(string csvFile, string txtFile, char separator, bool hasColumnNames)
        {
            CsvFile = csvFile;
            TxtFile = txtFile;
            Separator = separator;
            CsvHasColumnNames = hasColumnNames;
        }

        public bool CsvHasColumnNames { get; set; } = false;
        public string CsvFile { get; set; }
        public string TxtFile { get; set; }
        public int LinesToRead { get; set; } = 10000;
        public char Separator { get; set; }
        //public string InputString { get; set; }
        //public string ParsedString { get; set; }

        /// <summary>
        /// Преобразование csv-файла в txt-файл
        /// </summary>
        public void ToTxt()
        {
            //var srcEncoding = Encoding.GetEncoding(1251);
            //string line = string.Empty;
            //StreamReader file = 
            //    new StreamReader($@"{this.CsvFile}", encoding: srcEncoding);
            //while ((line = file.ReadLine()) != null)
            //{
            //    WriteTxt(ParceLine(line), this.TxtFile);
            //}
            //file.Close();

            //Цикл чтения файла кусками заданной в this.LinesToRead размерности
            for (int i = 0; i <= LinesInCsv(); i += this.LinesToRead)
            {
                int current_i = i;
                var thread_i = new Thread(() => BatchPart(current_i));
                thread_i.Start();
                //thread_i.IsBackground = true;
                thread_i.Join();
            }
        }

        /// <summary>
        /// Обработка i-го куска файла
        /// </summary>
        /// <param name="i"></param>
        private void BatchPart(int i)
        {
            // Полученеи списка строк из csv-файла
            var currentLines = ReadLines(i);

            // Получение преобразованного списка строк
            var parcedCurrentLines = ParceList(currentLines);

            // Запись преобразованного списка строк в txt-файл
            WriteStrings(parcedCurrentLines);
        }

        /// <summary>
        /// Получение списка строк из файла
        /// </summary>
        /// <param name="startLine">начальная строка</param>
        /// <returns></returns>
        List<string> ReadLines(int startLine)
        {
            string line;
            int endLine = startLine + this.LinesToRead;
            List<string> ListStrings = new List<string>();

            using (TextReader reader = new StreamReader($@"{this.CsvFile}", encoding: Encoding.GetEncoding(1251)))
            {
                int current_line = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    if (current_line >= endLine) return ListStrings;
                    if (current_line < startLine)
                    {
                        //Interlocked.Increment(current_line);
                        current_line++;
                        continue;
                    }
                    ListStrings.Add(line);
                    current_line++;
                }
            }

            return ListStrings;
        }

        /// <summary>
        /// Парсинг csv-строки в обычную
        /// </summary>
        /// <param name="line">исходная строка</param>
        /// <returns>string</returns>
        private string ParceLine(string line)
        {
            string[] str_arc = line.Split(this.Separator);
            string result = string.Empty;
            if (str_arc.Length < 1) return line;
            foreach (var item in str_arc)
            {
                result += $"{item.Trim()} ";
            }
            return $"{result.Trim()}";
        }

        /// <summary>
        /// Парсинг списка строк
        /// </summary>
        /// <param name="ListStrings">Список строк</param>
        /// <returns>List<string></returns>
        private List<string> ParceList(List<string> ListStrings)
        {
            List<string> ParcedList = new List<string>();
            foreach (var line in ListStrings)
            {
                var current_line = line;
                ParcedList.Add(ParceLine(current_line));
            }
            return ParcedList;
        }

        /// <summary>
        /// Парсинг csv-строки в простую
        /// </summary>
        /// <param name="line">исходная строка</param>
        /// <param name="separator">csv-разделитель</param>
        /// <returns>string</returns>
        public static string ParceString(string line, char separator = '#')
        {
            string[] str_arc = line.Split(separator);
            string result = string.Empty;
            if (str_arc.Length < 1) return line;
            foreach (var item in str_arc)
            {
                result += $"{item.Trim()} ";
            }
            return $"{result.Trim()}";
        }

        /// <summary>
        /// Количество строк в csv-файле
        /// </summary>
        /// <returns>int</returns>
        public int LinesInCsv()
        {
            int count = 0;
            string line;
            TextReader reader = new StreamReader(this.CsvFile);
            while ((line = reader.ReadLine()) != null)
            {
                count++;
            }
            reader.Close();
            return count;
        }

        public int LinesInTxt()
        {
            int count = 0;
            string line;
            TextReader reader = new StreamReader(this.TxtFile);
            while ((line = reader.ReadLine()) != null)
            {
                count++;
            }
            reader.Close();
            return count;
        }

        /// <summary>
        /// Запись строки в файл
        /// </summary>
        /// <param name="line">строка</param>
        /// <param name="outfile">файл</param>
        private void WriteString(string line)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(this.TxtFile, true))
            {
                file.WriteLine(line);
            }
        }

        /// <summary>
        /// Запись списка строк в файл
        /// </summary>
        /// <param name="ListStrings">Список строк</param>
        /// <param name="outfile">файл</param>
        private void WriteStrings(List<string> ListStrings)
        {
            System.IO.File.AppendAllLines(this.TxtFile, ListStrings);
        }
    }
}
