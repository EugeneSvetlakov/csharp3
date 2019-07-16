using MailSender.Data.BaseEntityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data
{
    public class MailTask : Entity
    {
        public DateTime Time { get; set; } = DateTime.Now.AddHours(3);

        public virtual Message Message { get; set; }

        public virtual Sender Sender { get; set; }

        public virtual RecipientsList RecipientsList { get; set; }

        public virtual Server Server { get; set; }

        public SendStatusEnum SendStatusEnum { get; set; } = SendStatusEnum.Unknown;
    }
}
