using MailSender.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.Services
{
    public interface IMailTasksDataService : IDataService<MailTask>
    {
        bool SendMail(Sender sender, Recipient recipient, Message Message, Server server);

        //void SendMail(Sender sender, RecipientsList recipientsList, Message mailMessage, Server server);

        Task<bool> SendMailAsync(Sender sender, Recipient recipient, Message Message, Server server);

        Task SendTaskAsync(MailTask mailTask
            , IProgress<double> Progress = null
            , CancellationToken Cancel = default);
    }
}
