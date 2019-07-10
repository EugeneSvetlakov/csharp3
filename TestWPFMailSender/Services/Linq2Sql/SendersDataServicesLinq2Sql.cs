using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data;

namespace MailSender.Services.Linq2Sql
{
    public class SendersDataServicesLinq2Sql : ISendersDataService
    {
        private readonly MailSender.Data.Linq2Sql.MailSenderDbContext _Db;

        public SendersDataServicesLinq2Sql(MailSender.Data.Linq2Sql.MailSenderDbContext db)
        {
            _Db = db;
        }

        public IEnumerable<Sender> GetAll()
        {
            return _Db.Senders
                .Select(r => new Sender
                {
                    id = r.id,
                    Name = r.Name,
                    Address = r.MailAddr,
                    Comment = r.Comment
                })
                .ToArray();
        }

        public Task<IEnumerable<Sender>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Sender GetById(int id) => _Db.Senders.Select(r => new Sender
            {
                id = r.id,
                Name = r.Name,
                Address = r.MailAddr,
                Comment = r.Comment
            })
            .FirstOrDefault(r => r.id == id);

        public Task<Sender> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public int Add(Sender item)
        {
            if (item.id != 0) return -1; 
            _Db.Senders.InsertOnSubmit(new MailSender.Data.Linq2Sql.Sender
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

        public Task<int> AddAsync(Sender item)
        {
            throw new NotImplementedException();
        }

        public Sender Edit(int id, Sender item)
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

        public Task<Sender> EditAsync(int id, Sender item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            var db_item = _Db.Senders.FirstOrDefault(r => r.id == id);
            _Db.Senders.DeleteOnSubmit(db_item);
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
