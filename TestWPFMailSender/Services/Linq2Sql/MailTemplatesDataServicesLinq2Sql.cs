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

        public Task<IEnumerable<MailMessage>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public MailMessage GetById(int id) => _Db.MailTemplates.Select(r => new MailMessage
        {
            id = r.id,
            Subject = r.subject,
            Body = r.message
        }).FirstOrDefault(r => r.id == id);

        public Task<MailMessage> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public int Add(MailMessage item)
        {
            if (item.id != 0) return -1; 
            _Db.MailTemplates.InsertOnSubmit(new MailSender.Data.Linq2Sql.MailTemplate
            {
                subject = item.Subject,
                message = item.Body
            });

            try
            {
                _Db.SubmitChanges();
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public Task<int> AddAsync(MailMessage item)
        {
            throw new NotImplementedException();
        }

        public MailMessage Edit(int id, MailMessage item)
        {
            try
            {
                _Db.SubmitChanges();
                return GetById(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task<MailMessage> EditAsync(int id, MailMessage item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            var db_item = _Db.MailTemplates.FirstOrDefault(i => i.id == id);
            if (db_item is null) return false;

            _Db.MailTemplates.DeleteOnSubmit(db_item);
            try
            {
                _Db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
