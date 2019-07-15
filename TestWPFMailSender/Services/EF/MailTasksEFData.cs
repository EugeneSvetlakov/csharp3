using System;
using MailSender.Data;
using MailSender.Data.EF;
using System.Threading.Tasks;

namespace MailSender.Services.EF
{
    public class MailTasksEFData : EFData<MailTask>, IMailTasksDataService
    {
        public MailTasksEFData(MailSenderDB Db) : base(Db) { }

        public override MailTask Edit(int id, MailTask item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var db_item = GetById(id);
            if (db_item is null) return null;
            db_item.Time = item.Time; 
            db_item.Message = item.Message;
            db_item.Sender = item.Sender;
            db_item.RecipientsList = item.RecipientsList;
            db_item.Server = item.Server;
            db_item.SendStatusEnum = item.SendStatusEnum;
            Commit();
            return db_item;
        }

        public override async Task<MailTask> EditAsync(int id, MailTask item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var db_item = await GetByIdAsync(id).ConfigureAwait(false);
            if (db_item is null) return null;
            db_item.Time = item.Time;
            db_item.Message = item.Message;
            db_item.Sender = item.Sender;
            db_item.RecipientsList = item.RecipientsList;
            db_item.Server = item.Server;
            db_item.SendStatusEnum = item.SendStatusEnum;
            await CommitAsync();
            return db_item;
        }
    }
}
