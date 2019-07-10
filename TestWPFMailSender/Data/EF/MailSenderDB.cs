using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.EF
{
    public class MailSenderDB : DbContext
    {
        public MailSenderDB() : this("Name=MailSenderDB") { }

        public MailSenderDB(string ConnectionString) : base(ConnectionString) { }
    }
}
