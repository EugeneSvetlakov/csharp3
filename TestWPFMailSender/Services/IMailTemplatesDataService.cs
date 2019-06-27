using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data.Linq2Sql;

namespace MailSender.Services
{
    public interface IMailTemplatesDataService
    {
        IEnumerable<MailTemplate> GetAll();

        void Create(MailTemplate item);

        void Update(MailTemplate item);

        void Delete(MailTemplate item);
    }
}
