using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data.Linq2Sql;

namespace MailSender.Services
{
    public interface IRecipientsDataService
    {
        IEnumerable<Recipient> GetAll();

        void Create(Recipient item);

        void Update(Recipient item);

        void Delete(Recipient item);
    }
}
