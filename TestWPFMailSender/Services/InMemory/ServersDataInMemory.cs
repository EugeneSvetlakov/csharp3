using System;
using MailSender.Data;

namespace MailSender.Services.InMemory
{
    public class ServersDataInMemory : DataInMemory<Server>, IServersDataService
    {
        public override void Edit(Server item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            var db_item = GetById(item.id);

            if (db_item is null) return;

            db_item.Name = item.Name;
            db_item.Address = item.Address;
            db_item.Port = item.Port;
            db_item.Ssl = item.Ssl;
            db_item.Login = item.Login;
            db_item.Pwd = item.Pwd;
        }
    }
}
