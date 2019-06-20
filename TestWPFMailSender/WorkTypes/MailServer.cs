using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TestWPFMailSender.WorkTypes
{
    class MailServer
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public string login { get; set; }
        public string pwd { get; set; }

        public MailServer()
        {
            Host = "smtp.yandex.ru";
            Port = 25;
            Ssl = true;
            login = string.Empty;
            pwd = string.Empty;
        }

        public SmtpClient CreateSmtpClient()
        {
            return new SmtpClient()
            {
                EnableSsl = this.Ssl,
                Port = this.Port,
                Host = this.Host,
                Credentials = new NetworkCredential(this.login, this.pwd)
            };
        }
    }
}
