using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data;

namespace MailSender.Services.Linq2Sql
{
    public class MailServersDataServicesLinq2Sql : IServersDataService
    {
        private readonly MailSender.Data.Linq2Sql.MailSenderDbContext _Db;

        public MailServersDataServicesLinq2Sql(MailSender.Data.Linq2Sql.MailSenderDbContext db)
        {
            _Db = db;
        }

        public IEnumerable<Server> GetAll()
        {
            return _Db.MailServers
                .Select(r => new Server
                {
                    id = r.id,
                    Name = r.Host,
                    Address = r.Host,
                    Port = r.Port,
                    Ssl = bool.Parse(r.Ssl),
                    Login = r.Login,
                    Pwd = r.Pwd
                })
                .ToArray();
        }

        public Server GetById(int id) => _Db.MailServers
            .Select(r => new Server
            {
                id = r.id,
                Name = r.Host,
                Address = r.Host,
                Port = r.Port,
                Ssl = bool.Parse(r.Ssl),
                Login = r.Login,
                Pwd = r.Pwd
            })
            .FirstOrDefault(r => r.id == id);

        public void Add(Server item)
        {
            if (item.id != 0) return;
            _Db.MailServers.InsertOnSubmit(new MailSender.Data.Linq2Sql.MailServer
            {
                Host = item.Address,
                Port = item.Port,
                Ssl = item.Ssl.ToString(),
                Login = item.Login,
                Pwd = item.Pwd
                
            });
            _Db.SubmitChanges();
        }

        public void Edit(Server item)
        {
            _Db.SubmitChanges();
        }

        public void Delete(Server item)
        {
            var db_item = _Db.MailServers.FirstOrDefault(r => r.id == item.id);
            if (db_item is null) return;

            _Db.MailServers.DeleteOnSubmit(db_item);
            _Db.SubmitChanges();
        }
    }
}
