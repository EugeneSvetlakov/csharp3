using System;
using MailSender.Data;

namespace MailSender.Services.InMemory
{
    public class MailMessagesDataInMemory : DataInMemory<MailMessage>, IMailMessageDataService
    {
        public override void Edit(MailMessage item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            var db_item = GetById(item.id);

            if (db_item is null) return;

            db_item.Subject = item.Subject;
            db_item.Body = item.Body;
        }
    }
}
