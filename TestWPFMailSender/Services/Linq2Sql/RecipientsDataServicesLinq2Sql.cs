using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Linq2Sql;

namespace MailSender.Services.Linq2Sql
{
    public class RecipientsDataServicesLinq2Sql : IRecipientsDataService
    {
        private readonly MailSenderDbContextDataContext _Db;

        public RecipientsDataServicesLinq2Sql(MailSenderDbContextDataContext db)
        {
            _Db = db;
        }

        public IEnumerable<Recipients> GetAll()
        {
            return _Db.Recipients.ToArray();
        }

        public void Create(Recipients item)
        {
            if (item.id != 0) return; 
            _Db.Recipients.InsertOnSubmit(item);
            _Db.SubmitChanges();
        }

        public void Update(Recipients item)
        {
            _Db.SubmitChanges();
        }

        public void Delete(Recipients item)
        {
            _Db.Recipients.DeleteOnSubmit(item);
            _Db.SubmitChanges();
        }
    }
}
