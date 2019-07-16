using MailSender.Data;
using MailSender.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Services.EF
{
    public class RecipientsEFData : EFData<Recipient>, IRecipientsDataService
    {
        public RecipientsEFData(MailSenderDB Db) : base(Db) { }

        public override Recipient Edit(int id, Recipient item)
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

        public override async Task<Recipient> EditAsync(int id, Recipient item)
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
