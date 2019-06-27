using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data.Linq2Sql;

namespace MailSender.Services
{
    public interface ISendersDataService
    {
        IEnumerable<Sender> GetAll();

        void Create(Sender item);

        void Update(Sender item);

        void Delete(Sender item);
    }
}
