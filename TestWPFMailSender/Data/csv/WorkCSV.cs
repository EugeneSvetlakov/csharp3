using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.csv
{
    class WorkCSV
    {
        string _FilePath;

        public string FilePath { get => _FilePath; set => _FilePath = value; }

        List<Recipient> _Recipients;

        public List<Recipient> Recipients { get => _Recipients; set => _Recipients = value; }

        public void GetData()
        {
            int counter = 0;
            foreach (var item in File.ReadAllLines(_FilePath))
            {
                string[] line = item.Split(',');
                _Recipients.Add(new Recipient {
                    id = counter++,
                    Name = line[0],
                    Address = line[1],
                    Comment = line[2]
                });
            }
        }
    }
}
