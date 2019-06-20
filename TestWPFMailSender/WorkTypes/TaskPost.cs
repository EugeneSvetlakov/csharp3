using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPFMailSender.WorkTypes
{
    /// <summary>
    /// Класс экземпляра задания Планировщика рассылки
    /// </summary>
    class TaskPost
    {
        string id;
        DateTime CreateDate;
        DateTime SendDate;
        MailTemplate mailTemplate;
        MailServer mailServer;
        List<mailRecipient> Recipients;
        sendStatus sendStatus;
        string report;
    }
}
