using MailSender.Data;

namespace MailSender.Services
{
    public interface IRecipientsListsDataService : IDataService<RecipientsList>
    {
        void AddRecipientToList(int id, Recipient item);
        void RemoveRecipientFromList(int id, Recipient item);
        void ClearRecipientsFromList(int id);
    }
}
