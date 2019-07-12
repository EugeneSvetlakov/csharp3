using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MailSender.Data;

namespace MailSender.Services
{
    public interface IMailSenderService
    {
        void Send(Message Message, Sender From, Recipient To);

        void Send(Message Message, Sender From, IEnumerable<Recipient> To);

        void SendParallel(Message Message, Sender From, IEnumerable<Recipient> To);

        Task SendAsync(Message Message, Sender From, Recipient To);

        Task SendAsync(Message Message, Sender From, IEnumerable<Recipient> To, 
            IProgress<double> Progress = null, CancellationToken Cancel = default);
    }
}
