using System;
using MailSender.Data;
using MailSender.Data.EF;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Services.EF
{
    public class SendersEFData : EFData<Sender>, ISendersDataService
    {
        public SendersEFData(MailSenderDB Db) : base(Db) { }

        public override Sender Edit(int id, Sender item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var db_item = GetById(id);
            if (db_item is null) return null;
            db_item.Name = item.Name;
            db_item.Address = item.Address;
            db_item.Comment = item.Comment;
            Commit();
            return db_item;
        }

        public override async Task<Sender> EditAsync(int id, Sender item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var db_item = await GetByIdAsync(id).ConfigureAwait(false);
            if (db_item is null) return null;
            db_item.Name = item.Name;
            db_item.Address = item.Address;
            db_item.Comment = item.Comment;
            await CommitAsync();
            return db_item;
        }
    }
}
