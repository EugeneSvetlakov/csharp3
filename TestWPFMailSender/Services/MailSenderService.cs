using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MailSender.Data;

namespace MailSender.Services
{
    public class MailSenderService : IMailSenderService
    {
        public string _Host { get; }
        public int _Port { get; }
        public bool _UseSSL { get; }
        public string _Login { get; }
        public string _Password { get; }


        public MailSenderService(string Host, int Port, bool UseSSL, string Login, string Password)
        {
            this._Host = Host;
            this._Port = Port;
            this._UseSSL = UseSSL;
            this._Login = Login;
            this._Password = Password;
        }

        public void Send(Message Message, Sender From, Recipient To)
        {
            using (var server = new System.Net.Mail.SmtpClient(_Host, _Port) { EnableSsl = _UseSSL })
            {
                server.Credentials = new NetworkCredential(_Login, _Password);

                using(var msg = new System.Net.Mail.MailMessage())
                {
                    msg.From = new System.Net.Mail.MailAddress(From.Address, From.Name);
                    msg.To.Add(new System.Net.Mail.MailAddress(To.Address, To.Name));

                    server.Send(msg);
                }
            }
        }

        public void Send(Message Message, Sender From, IEnumerable<Recipient> To)
        {
            foreach(var recipient in To)
            {
                Send(Message, From, recipient);
            }
        }

        public void SendParallel(Message Message, Sender From, IEnumerable<Recipient> To)
        {
            foreach (var recipient in To)
            {
                var current_recipient = recipient;
                ThreadPool.QueueUserWorkItem(p => Send(Message, From, current_recipient));
            }
        }

        public async Task SendAsync(Message Message, Sender From, Recipient To)
        {
            using (var server = new System.Net.Mail.SmtpClient(_Host, _Port) { EnableSsl = _UseSSL })
            {
                server.Credentials = new NetworkCredential(_Login, _Password);

                using (var msg = new System.Net.Mail.MailMessage())
                {
                    msg.From = new System.Net.Mail.MailAddress(From.Address, From.Name);
                    msg.To.Add(new System.Net.Mail.MailAddress(To.Address, To.Name));

                    server.SendCompleted += new System.Net.Mail.SendCompletedEventHandler(SendCompletedCallback);
                    
                    await server.SendMailAsync(msg); //стандартная ф-ция SmptClient
                }
            }
        }

        public async Task SendAsync(
            Message Message, 
            Sender From, 
            IEnumerable<Recipient> To, 
            IProgress<double> Progress = null, 
            CancellationToken Cancel = default)
        {
            //Параллельный Асинхолнный процесс
            //await Task.WhenAll(To.Select(to => SendAsync(Message, From, to)));
            
            #region Последовательный Асинхронный процесс
            var to = To.ToArray();
            var ToLength = to.Length;

            for (int i = 0; i < ToLength; i++)
            {
                Cancel.ThrowIfCancellationRequested();
                await SendAsync(Message, From, to[i]);
                Progress?.Report((double)i / ToLength);
            }

            Progress?.Report(1);
            #endregion
        }


        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Уникальный идентификатор для Асинхронной задачи.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Отправка не удалась.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Сообщение отправлено.");
            }
        }
    }
}
