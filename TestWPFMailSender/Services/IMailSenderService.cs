using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data;

namespace MailSender.Services
{
    public interface IMailSenderService
    {
        IMailSender CreateSender(Server Server);
    }
}
