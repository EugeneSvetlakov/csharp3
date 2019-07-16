using System;
using MailSender.Data;

namespace MailSender.Services.InMemory
{
    public class SendersDataInMemory : DataInMemory<Sender>, ISendersDataService
    {
        public override Sender Edit(int id, Sender item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            var db_item = GetById(id);

            if (db_item is null) return null;

            db_item.Name = item.Name;
            db_item.Address = item.Address;
            db_item.Comment = item.Comment;
            return db_item;
        }
    }
}
