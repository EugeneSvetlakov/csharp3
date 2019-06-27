using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data.Linq2Sql;

namespace MailSender.Services.Linq2Sql
{
    public class MailTemplatesDataServicesLinq2Sql : IMailTemplatesDataService
    {
        private readonly MailSenderDbContext _Db;

        public MailTemplatesDataServicesLinq2Sql(MailSenderDbContext db)
        {
            _Db = db;
        }

        public IEnumerable<MailTemplate> GetAll()
        {
            return _Db.MailTemplates.ToArray();
        }

        public void Create(MailTemplate item)
        {
            if (item.id != 0) return; 
            _Db.MailTemplates.InsertOnSubmit(item);
            _Db.SubmitChanges();
        }

        public void Update(MailTemplate item)
        {
            _Db.SubmitChanges();
        }

        public void Delete(MailTemplate item)
        {
            _Db.MailTemplates.DeleteOnSubmit(item);
            _Db.SubmitChanges();
        }
    }
}
