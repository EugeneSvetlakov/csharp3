using System;

using System.Net;
using System.Net.Mail;

namespace MailSenderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var clinet = new SmtpClient("smtp.yandex.ru", 25)
            {
                Credentials = new NetworkCredential("UserName", "Password"),
                EnableSsl = true
            })
            {
                using(var msg = new MailMessage
                {
                    From = new MailAddress("sender@yandex.ru", "Отправитель"),
                    To =
                    {
                        new MailAddress("recipient@gmail.com", "Получатель")
                    },
                    Subject = "Тема письма",
                    Body = "Тело письма",
                    Attachments =
                    {
                        new Attachment("FlieName.zip")
                    }
                })
                {
                    clinet.Send(msg);
                }
            }

            //clinet.Dispose();
            //msg.Dispose();
        }
    }
}
