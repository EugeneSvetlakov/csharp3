using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data.Linq2Sql;

namespace MailSender.Services.Linq2Sql
{
    public class RecipientsDataServicesLinq2Sql : IRecipientsDataService
    {
        private readonly MailSenderDbContext _Db;

        public RecipientsDataServicesLinq2Sql(MailSenderDbContext db)
        {
            _Db = db;
        }

        public IEnumerable<Recipient> GetAll()
        {
            return _Db.Recipients.ToArray();
        }

        public Recipient GetById(int id) => _Db.Recipients.FirstOrDefault(r => r.id == id);

        public void Create(Recipient item)
        {
            if (item.id != 0) return; 
            _Db.Recipients.InsertOnSubmit(item);
            _Db.SubmitChanges();
        }

        public void Update(Recipient item)
        {
            _Db.SubmitChanges();
        }

        public void Delete(Recipient item)
        {
            _Db.Recipients.DeleteOnSubmit(item);
            _Db.SubmitChanges();
        }
    }
}
