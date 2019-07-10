using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data;
using MailSender.Services;

namespace MailSender.Services.Linq2Sql
{
    public class RecipientsDataServicesLinq2Sql : IRecipientsDataService
    {
        private readonly MailSender.Data.Linq2Sql.MailSenderDbContext _Db;

        public RecipientsDataServicesLinq2Sql(MailSender.Data.Linq2Sql.MailSenderDbContext db)
        {
            _Db = db;
        }

        public IEnumerable<Recipient> GetAll()
        {
            return _Db.Recipients
                .Select(r => new Recipient
                {
                    id = r.id,
                    Name = r.Name,
                    Address = r.MailAddr,
                    Comment = r.Comment
                })
                .ToArray();
        }

        public Task<IEnumerable<Recipient>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Recipient GetById(int id) => _Db.Recipients
            .Select(r => new Recipient
            {
                id = r.id,
                Name = r.Name,
                Address = r.MailAddr,
                Comment = r.Comment
            })
            .FirstOrDefault(r => r.id == id);

        public Task<Recipient> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public int Add(Recipient item)
        {
            if (item.id != 0) return -1; 
            _Db.Recipients.InsertOnSubmit(new Data.Linq2Sql.Recipient
            {
                Name = item.Name,
                MailAddr = item.Address,
                Comment = item.Comment
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

        public Task<int> AddAsync(Recipient item)
        {
            throw new NotImplementedException();
        }

        public Recipient Edit(int id,Recipient item)
        {
            _Db.SubmitChanges();
            return GetById(id);
        }

        public Task<Recipient> EditAsync(int id, Recipient item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            var db_item = _Db.Recipients.FirstOrDefault(i => i.id == id);
            if (db_item is null) return false;
            _Db.Recipients.DeleteOnSubmit(db_item);
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
