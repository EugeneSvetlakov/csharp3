using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.Memory
{
    public enum sendStatus { Unknown, Processing, Ok, Error}

    public class EmailSendServiceClass
    {
        
        public EmailSendServiceClass(
            MailAddress from, 
            MailAddress to, 
            string subject, 
            string body, 
            SmtpClient client
            )
        {
            From = from;
            To = to;
            Subject = subject;
            Body = body;
            this.client = client;
        }

        public MailAddress From { get; set; }
        public MailAddress To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public SmtpClient client { get; set; }
        public string errMsg { get; set; } = string.Empty;
        public sendStatus result { get; set; } = sendStatus.Unknown;

        public void Send()
        {
            try
            {
                using (var message = new System.Net.Mail.MailMessage())
                {
                    message.From = this.From;
                    message.To.Add(this.To);
                    message.Subject = this.Subject;
                    message.Body = this.Body;
                    using (var client = new SmtpClient())
                    {
                        client.EnableSsl = this.client.EnableSsl;
                        client.Host = this.client.Host;
                        client.Port = this.client.Port;
                        client.Credentials = this.client.Credentials;
                        client.Send(message);
                        this.errMsg = "No errors";
                        this.result = sendStatus.Ok;
                    }
                }
            }
            catch (Exception e)
            {
                this.errMsg = $"Ошибка при отправке почты \r\n{ e.Message}";
                this.result = sendStatus.Error;
            }
        }
    }
}
