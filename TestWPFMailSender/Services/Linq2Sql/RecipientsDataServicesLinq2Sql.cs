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

        public Recipient GetById(int id) => _Db.Recipients
            .Select(r => new Recipient
            {
                id = r.id,
                Name = r.Name,
                Address = r.MailAddr,
                Comment = r.Comment
            })
            .FirstOrDefault(r => r.id == id);

        public void Add(Recipient item)
        {
            if (item.id != 0) return; 
            _Db.Recipients.InsertOnSubmit(new Data.Linq2Sql.Recipient
            {
                Name = item.Name,
                MailAddr = item.Address,
                Comment = item.Comment
            });
            _Db.SubmitChanges();
        }

        public void Edit(Recipient item)
        {
            _Db.SubmitChanges();
        }

        public void Delete(Recipient item)
        {
            var db_item = _Db.Recipients.FirstOrDefault(i => i.id == item.id);
            if (db_item is null) return;
            _Db.Recipients.DeleteOnSubmit(db_item);
            _Db.SubmitChanges();
        }
    }
}
