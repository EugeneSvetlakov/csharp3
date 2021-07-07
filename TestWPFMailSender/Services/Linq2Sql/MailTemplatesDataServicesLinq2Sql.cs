using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data;

namespace MailSender.Services.Linq2Sql
{
    public class MailTemplatesDataServicesLinq2Sql : IMailMessageDataService
    {
        private readonly MailSender.Data.Linq2Sql.MailSenderDbContext _Db;

        public MailTemplatesDataServicesLinq2Sql(MailSender.Data.Linq2Sql.MailSenderDbContext db)
        {
            _Db = db;
        }

        public IEnumerable<MailMessage> GetAll()
        {
            return _Db.MailTemplates.Select(r => new MailMessage
            {
                id = r.id,
                Subject = r.subject,
                Body = r.message
            }).ToArray();
        }

        public MailMessage GetById(int id) => _Db.MailTemplates.Select(r => new MailMessage
        {
            id = r.id,
            Subject = r.subject,
            Body = r.message
        }).FirstOrDefault(r => r.id == id);

        public void Add(MailMessage item)
        {
            if (item.id != 0) return; 
            _Db.MailTemplates.InsertOnSubmit(new MailSender.Data.Linq2Sql.MailTemplate
            {
                subject = item.Subject,
                message = item.Body
            });
            _Db.SubmitChanges();
        }

        public void Edit(MailMessage item)
        {
            _Db.SubmitChanges();
        }

        public void Delete(MailMessage item)
        {
            var db_item = _Db.MailTemplates.FirstOrDefault(i => i.id == item.id);
            if (db_item is null) return;

            _Db.MailTemplates.DeleteOnSubmit(db_item);
            _Db.SubmitChanges();
        }
    }
}
