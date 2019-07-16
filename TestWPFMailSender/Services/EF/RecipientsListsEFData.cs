using System;
using MailSender.Data;
using MailSender.Data.EF;
using System.Threading.Tasks;

namespace MailSender.Services.EF
{
    public class RecipientsListsEFData : EFData<RecipientsList>, IRecipientsListsDataService
    {
        public RecipientsListsEFData(MailSenderDB Db) : base(Db) { }

        public override RecipientsList Edit(int id, RecipientsList item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var db_item = GetById(id);
            if (db_item is null) return null;
            db_item.Name = item.Name;
            db_item.Recipients = item.Recipients;
            db_item.Comment = item.Comment;
            Commit();
            return db_item;
        }

        public override async Task<RecipientsList> EditAsync(int id, RecipientsList item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var db_item = await GetByIdAsync(id).ConfigureAwait(false);
            if (db_item is null) return null;
            db_item.Name = item.Name;
            db_item.Recipients = item.Recipients;
            db_item.Comment = item.Comment;
            await CommitAsync();
            return db_item;
        }

        public void AddRecipientToList(int id, Recipient item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var CurrentRecipientsList = GetById(id);

            if (CurrentRecipientsList is null) throw new ArgumentNullException(nameof(CurrentRecipientsList));
            if (CurrentRecipientsList.Recipients.Contains(item)) return;

            CurrentRecipientsList.Recipients.Add(item);

        }

        public void RemoveRecipientFromList(int id, Recipient item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            var CurrentRecipientsList = GetById(id);

            if (CurrentRecipientsList is null) throw new ArgumentNullException(nameof(CurrentRecipientsList));

            if (CurrentRecipientsList.Recipients.Contains(item))
            {
                CurrentRecipientsList.Recipients.Remove(item);
            }
        }

        public void ClearRecipientsFromList(int id)
        {
            var CurrentRecipientsList = GetById(id);

            if (CurrentRecipientsList is null) throw new ArgumentNullException(nameof(CurrentRecipientsList));

            CurrentRecipientsList.Recipients.Clear();
        }
    }
}
