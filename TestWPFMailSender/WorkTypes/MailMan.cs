using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.WorkTypes
{
    /// <summary>
    /// Класс основа для классов Получатель/Отправитель
    /// </summary>
    abstract class MailMan
    {
        string id { get; }
        string Name { get; set; }
        string MailAddr { get; set; }
        string Comment { get; set; }

        public MailMan(string id, string name, string mailAddr, string comment)
        {
            this.id = id;
            Name = name;
            MailAddr = mailAddr;
            Comment = comment;
        }
    }
    /// <summary>
    /// Класс Получатель почтового сообщения
    /// </summary>
    class mailRecipient : MailMan
    {
        public mailRecipient(string id, string Name, string MailAddr, string Comment) : base(id, Name, MailAddr, Comment)
        {
        }
    }
    /// <summary>
    /// Класс Отправитель почтового сообщения
    /// </summary>
    class mailSender : MailMan
    {
        public mailSender(string id, string Name, string MailAddr, string Comment) : base(id, Name, MailAddr, Comment)
        {
        }
    }
}
