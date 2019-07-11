using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MailSender.Data;

namespace MailSender.Services
{
    public interface IMailSenderService
    {
        void Send(MailMessage Message, Sender From, Recipient To);

        void Send(MailMessage Message, Sender From, IEnumerable<Recipient> To);

        void SendParallel(MailMessage Message, Sender From, IEnumerable<Recipient> To);

        Task SendAsync(MailMessage Message, Sender From, Recipient To);

        Task SendAsync(MailMessage Message, Sender From, IEnumerable<Recipient> To, 
            IProgress<double> Progress = null, CancellationToken Cancel = default);
    }
}
