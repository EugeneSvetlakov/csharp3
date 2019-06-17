using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WPFTestMailSender
{
    public static class defaultMailServer
    {
        public static string smtpServer = "smtp.yandex.ru";
        public static int smtpPort = 25;
        public static bool Ssl = true;

        public static SmtpClient defClient = new SmtpClient()
        {
            EnableSsl = true,
            Port = 25,
            Host = "smtp.yandex.ru",
            Credentials = new NetworkCredential("login", "pwd")
        };
    }
    
}
