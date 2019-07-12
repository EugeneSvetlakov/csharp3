using MailSender.Data.BaseEntityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.Memory
{
    /// <summary>
    /// Класс экземпляра задания Планировщика рассылки
    /// </summary>
    class TaskPost
    {
        string id;
        DateTime CreateDate = DateTime.Now;
        DateTime SendDate;
        Message mailTemplate;
        Server mailServer;
        List<Recipient> Recipients;
        SendStatusEnum sendStatus;
        string report;
    }
}
