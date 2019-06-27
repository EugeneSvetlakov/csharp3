using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data.Linq2Sql;

namespace MailSender.Services
{
    public interface IMailServersDataService
    {
        IEnumerable<MailServer> GetAll();

        void Create(MailServer item);

        void Update(MailServer item);

        void Delete(MailServer item);
    }
}
