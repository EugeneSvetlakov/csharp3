using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data.BaseEntityes;

namespace MailSender.Data
{
    /// <summary>
    /// Сообщение электронной почты
    /// </summary>
    public class MailMessage : Entity
    {
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
