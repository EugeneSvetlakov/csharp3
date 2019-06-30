using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data;

namespace MailSender.Services
{
    public interface IMailSenderService
    {
        IMailSender CreateSender(Server Server);
    }

    public interface IMailSender
    {
        void Send(MailMessage Message, Sender From, Recipient To);
        void Send(MailMessage Message, Sender From, IEnumerable<Recipient> To);
    }

    public class SmtpMailSenderService : IMailSenderService
    {
        public IMailSender CreateSender(Server Server) => 
            new SmtpMailSender(Server.Address, Server.Port, Server.Ssl, Server.Login, Server.Pwd);
    }

    public class SmtpMailSender : IMailSender
    {
        public string _Host { get; }
        public int _Port { get; }
        public bool _UseSSL { get; }
        public string _Login { get; }
        public string _Password { get; }


        public SmtpMailSender(string Host, int Port, bool UseSSL, string Login, string Password)
        {
            this._Host = Host;
            this._Port = Port;
            this._UseSSL = UseSSL;
            this._Login = Login;
            this._Password = Password;
        }

        
        public void Send(MailMessage Message, Sender From, Recipient To)
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

        public void Send(MailMessage Message, Sender From, IEnumerable<Recipient> To)
        {
            foreach(var recipient in To)
            {
                Send(Message, From, recipient);
            }
        }
    }
}
