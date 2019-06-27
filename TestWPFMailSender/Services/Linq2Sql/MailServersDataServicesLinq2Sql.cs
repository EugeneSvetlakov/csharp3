using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data.Linq2Sql;

namespace MailSender.Services.Linq2Sql
{
    public class MailServersDataServicesLinq2Sql : IMailServersDataService
    {
        private readonly MailSenderDbContext _Db;

        public MailServersDataServicesLinq2Sql(MailSenderDbContext db)
        {
            _Db = db;
        }

        public IEnumerable<MailServer> GetAll()
        {
            return _Db.MailServers.ToArray();
        }

        public void Create(MailServer item)
        {
            if (item.id != 0) return; 
            _Db.MailServers.InsertOnSubmit(item);
            _Db.SubmitChanges();
        }

        public void Update(MailServer item)
        {
            _Db.SubmitChanges();
        }

        public void Delete(MailServer item)
        {
            _Db.MailServers.DeleteOnSubmit(item);
            _Db.SubmitChanges();
        }
    }
}
