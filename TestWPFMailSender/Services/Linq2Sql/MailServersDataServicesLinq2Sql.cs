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

        public Task<IEnumerable<Server>> GetAllAsync()
        {
            throw new NotImplementedException();
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

        public Task<Server> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="item"></param>
        /// <returns>1 - успешно, -1 - ошибка</returns>
        public int Add(Server item)
        {
            if (item.id != 0) return -1;
            _Db.MailServers.InsertOnSubmit(new MailSender.Data.Linq2Sql.MailServer
            {
                Host = item.Address,
                Port = item.Port,
                Ssl = item.Ssl.ToString(),
                Login = item.Login,
                Pwd = item.Pwd
                
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

        public Task<int> AddAsync(Server item)
        {
            throw new NotImplementedException();
        }

        public Server Edit(int id, Server item)
        {
            _Db.SubmitChanges();
            return GetById(id);
        }

        public Task<Server> EditAsync(int id, Server item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            var db_item = _Db.MailServers.FirstOrDefault(r => r.id == id);
            if (db_item is null) return false;

            _Db.MailServers.DeleteOnSubmit(db_item);

            try
            {
                _Db.SubmitChanges();
                return true;
            }
            catch(Exception e)
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
