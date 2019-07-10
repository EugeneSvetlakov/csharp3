using System;
using MailSender.Data;

namespace MailSender.Services.InMemory
{
    public class MailMessagesDataInMemory : DataInMemory<MailMessage>, IMailMessageDataService
    {
        public override MailMessage Edit(int id, MailMessage item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            var db_item = GetById(id);

            if (db_item is null) return null;

            db_item.Subject = item.Subject;
            db_item.Body = item.Body;
            return db_item;
        }
    }
}
