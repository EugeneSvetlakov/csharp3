using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data.BaseEntityes;

namespace MailSender.Data
{
    public class Server : NamedEntity
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }
    }
}
