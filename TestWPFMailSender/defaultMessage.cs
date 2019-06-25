using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
    public static class defaultMessage
    {
        public static MailAddress mailFrom = new MailAddress("sxron@yandex.ru", "Евгений");
        public static MailAddress mailTo = new MailAddress("sxron@yandex.ru", "Евгений");
        public static string subject = $"письмо от {DateTime.Now}";
        public static string body = $"Содержание письма, отправленного пограммой WPFMailSend в {DateTime.Now}.";
    }
}
