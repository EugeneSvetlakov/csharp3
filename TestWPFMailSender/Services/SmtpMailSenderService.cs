using MailSender.Data;

namespace MailSender.Services
{
    public class SmtpMailSenderService : IMailSenderService
    {
        public IMailSender CreateSender(Server Server) => 
            new SmtpMailSender(Server.Address, Server.Port, Server.Ssl, Server.Login, Server.Pwd);
    }
}
