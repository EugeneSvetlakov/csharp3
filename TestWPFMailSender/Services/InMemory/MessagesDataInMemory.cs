using System;
using MailSender.Data;

namespace MailSender.Services.InMemory
{
    public class MessagesDataInMemory : DataInMemory<Message>, IMessageDataService
    {
        public override Message Edit(int id, Message item)
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
