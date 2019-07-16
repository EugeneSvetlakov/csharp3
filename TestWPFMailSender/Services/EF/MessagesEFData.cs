using System;
using MailSender.Data;
using MailSender.Data.EF;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Services.EF
{
    public class MessagesEFData : EFData<Message>, IMessageDataService
    {
        public MessagesEFData(MailSenderDB Db) : base(Db) { }

        public override Message Edit(int id, Message item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var db_item = GetById(id);
            if (db_item is null) return null;
            db_item.Subject = item.Subject;
            db_item.Body = item.Body;
            Commit();
            return db_item;
        }

        public override async Task<Message> EditAsync(int id, Message item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var db_item = await GetByIdAsync(id).ConfigureAwait(false);
            if (db_item is null) return null;
            db_item.Subject = item.Subject;
            db_item.Body = item.Body;
            await CommitAsync();
            return db_item;
        }
    }
}
