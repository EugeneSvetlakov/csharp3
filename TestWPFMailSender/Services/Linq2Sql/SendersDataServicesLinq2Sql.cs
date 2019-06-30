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

        public Sender GetById(int id) => _Db.Senders.Select(r => new Sender
            {
                id = r.id,
                Name = r.Name,
                Address = r.MailAddr,
                Comment = r.Comment
            })
            .FirstOrDefault(r => r.id == id);

        public void Add(Sender item)
        {
            if (item.id != 0) return; 
            _Db.Senders.InsertOnSubmit(new MailSender.Data.Linq2Sql.Sender
            {
                Name = item.Name,
                MailAddr = item.Address,
                Comment = item.Comment
            });
            _Db.SubmitChanges();
        }

        public void Edit(Sender item)
        {
            _Db.SubmitChanges();
        }

        public void Delete(Sender item)
        {
            var db_item = _Db.Senders.FirstOrDefault(r => r.id == item.id);
            _Db.Senders.DeleteOnSubmit(db_item);
            _Db.SubmitChanges();
        }

        
    }
}
