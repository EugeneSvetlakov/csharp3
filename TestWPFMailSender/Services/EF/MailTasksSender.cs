using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MailSender.Data;
using MailSender.Data.BaseEntityes;
using MailSender.Services;

namespace MailSender.Services
{
    public class MailTasksSender : IMailTasksSender
    {
        public bool SendMail(Sender From, Recipient To, Message message, Server server)
        {
            using (var smtpClient = new SmtpClient(server.Address, server.Port) { EnableSsl = server.Ssl })
            {
                smtpClient.Credentials = new NetworkCredential(server.Login, server.Pwd);

                using (var msg = new System.Net.Mail.MailMessage())
                {
                    msg.From = new System.Net.Mail.MailAddress(From.Address, From.Name);
                    msg.To.Add(new System.Net.Mail.MailAddress(To.Address, To.Name));

                    #region Заглушка для тестов
                    int RandomInt = DateTime.Now.Millisecond;
                    if (RandomInt != 250 || RandomInt != 350)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    #endregion
                    
                    //try
                    //{
                    //    smtpClient.Send(msg);
                    //    return true;
                    //}
                    //catch (Exception)
                    //{
                    //    //ToDo: написать логер ошибок
                    //    return false;
                    //}
                }
            }
        }

        //public void SendMail(Sender sender, RecipientsList recipientsList, Message message, Server server)
        //{
        //    foreach (var recipient in recipientsList.Recipients)
        //    {
        //        var curent_recipient = recipient;
        //        SendMail(sender, curent_recipient, message, server);
        //    }
        //}

        public async Task<bool> SendMailAsync(Sender From, Recipient To, Message message, Server server)
        {
            return await Task.Run(() => SendMail(From, To, message, server));

        }

        public async Task SendTaskAsync(MailTask mailTask
            ,IProgress<double> Progress = null
            ,CancellationToken Cancel = default)
        {
            #region Последовательный Асинхронный процесс
            bool good_status = mailTask.SendStatusEnum != SendStatusEnum.Canceled 
                || mailTask.SendStatusEnum != SendStatusEnum.Processing 
                || mailTask.SendStatusEnum == SendStatusEnum.Unknown;
            if (good_status)
            {
                mailTask.SendStatusEnum = SendStatusEnum.Processing;
                var to = mailTask.RecipientsList.Recipients.ToArray();
                var ToLength = to.Length;
                bool HasSuccessSend = false;
                bool HasUnSuccessSend = false;

                for (int i = 0; i < ToLength; i++)
                {
                    Cancel.ThrowIfCancellationRequested();
                    var res = await SendMailAsync(mailTask.Sender, to[i], mailTask.Message, mailTask.Server);
                    if (res)
                    {
                        HasSuccessSend = true;
                    }
                    else
                    {
                        HasUnSuccessSend = true;
                    }
                    Progress?.Report((double)i / ToLength);
                }
                if (HasSuccessSend && HasUnSuccessSend)
                    mailTask.SendStatusEnum = SendStatusEnum.Partialy;

                if (!HasSuccessSend)
                    mailTask.SendStatusEnum = SendStatusEnum.Error;

                if (HasSuccessSend && !HasUnSuccessSend)
                    mailTask.SendStatusEnum = SendStatusEnum.Ok;

                Progress?.Report(1);
            }
            #endregion
        }
    }
}
