using System.Collections.Generic;
using MailSender.Data;

namespace MailSender.Services
{
    public interface IMailSender
    {
        void Send(MailMessage Message, Sender From, Recipient To);

        void Send(MailMessage Message, Sender From, IEnumerable<Recipient> To);

        void SendParallel(MailMessage Message, Sender From, IEnumerable<Recipient> To);
    }
}
