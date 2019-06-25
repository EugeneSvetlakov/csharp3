using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.WorkTypes
{
    class MailServer
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }

        public MailServer()
        {
            Host = "smtp.yandex.ru";
            Port = 25;
            Ssl = true;
            Login = string.Empty;
            Pwd = string.Empty;
        }
    }
}
