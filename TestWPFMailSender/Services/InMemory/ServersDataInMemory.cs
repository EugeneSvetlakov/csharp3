using System;
using MailSender.Data;

namespace MailSender.Services.InMemory
{
    public class ServersDataInMemory : DataInMemory<Server>, IServersDataService
    {
        public ServersDataInMemory()
        {
            for(int i = 0; i < 10; i++)
            {
                _Items.Add(new Server { id = i, Name = $"server_{i}", 
                    Address = $"smtp.server{i}.localhost", Port = 25 });
            }
        }

        public override Server Edit(int id, Server item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            var db_item = GetById(id);

            if (db_item is null) return null;

            db_item.Name = item.Name;
            db_item.Address = item.Address;
            db_item.Port = item.Port;
            db_item.Ssl = item.Ssl;
            db_item.Login = item.Login;
            db_item.Pwd = item.Pwd;
            return db_item;
        }
    }
}
