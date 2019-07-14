using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MailSender.Data;

namespace MailSender.Services
{
    public interface IMailTasksSender
    {
        bool SendMail(Sender sender, Recipient recipient, Message Message, Server server);

        //void SendMail(Sender sender, RecipientsList recipientsList, Message mailMessage, Server server);

        Task<bool> SendMailAsync(Sender sender, Recipient recipient, Message Message, Server server);

        Task SendTaskAsync(MailTask mailTask
            ,IProgress<double> Progress = null
            ,CancellationToken Cancel = default);
    }
}
