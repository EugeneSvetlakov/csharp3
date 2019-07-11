using MailSender.Data.BaseEntityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data
{
    public class RecipientsList : NamedEntity
    {
        public virtual ICollection<Recipient> Recipients { get; set; }
    }
}
