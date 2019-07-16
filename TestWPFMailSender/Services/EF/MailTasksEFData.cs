using System;
using MailSender.Data;
using MailSender.Data.EF;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Mail;
using System.Net;
using MailSender.Data.BaseEntityes;
using System.Linq;

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
            , IProgress<double> Progress = null
            , CancellationToken Cancel = default)
        {
            #region Последовательный Асинхронный процесс
            bool good_status = mailTask.SendStatusEnum != SendStatusEnum.Canceled
                || mailTask.SendStatusEnum != SendStatusEnum.Processing
                || mailTask.SendStatusEnum == SendStatusEnum.Unknown;
            if (good_status)
            {
                mailTask.SendStatusEnum = SendStatusEnum.Processing;
                mailTask = Edit(mailTask.id, mailTask);
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

                await EditAsync(mailTask.id, mailTask);

                Progress?.Report(1);
            }
            #endregion
        }
    }
}
