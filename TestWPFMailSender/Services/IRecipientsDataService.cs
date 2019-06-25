using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Linq2Sql;

namespace MailSender.Services
{
    public interface IRecipientsDataService
    {
        IEnumerable<Recipients> GetAll();

        void Create(Recipients item);

        void Update(Recipients item);

        void Delete(Recipients item);
    }
}
