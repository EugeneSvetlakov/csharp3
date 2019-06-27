using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data.Linq2Sql;

namespace MailSender.Services.Linq2Sql
{
    public class SendersDataServicesLinq2Sql : ISendersDataService
    {
        private readonly MailSenderDbContext _Db;

        public SendersDataServicesLinq2Sql(MailSenderDbContext db)
        {
            _Db = db;
        }

        public IEnumerable<Sender> GetAll()
        {
            return _Db.Senders.ToArray();
        }

        public void Create(Sender item)
        {
            if (item.id != 0) return; 
            _Db.Senders.InsertOnSubmit(item);
            _Db.SubmitChanges();
        }

        public void Update(Sender item)
        {
            _Db.SubmitChanges();
        }

        public void Delete(Sender item)
        {
            _Db.Senders.DeleteOnSubmit(item);
            _Db.SubmitChanges();
        }
    }
}
