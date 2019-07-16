using MailSender.Data.BaseEntityes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailSender.Data
{
    /// <summary>
    /// Получатель почтового сообщения
    /// </summary>
    public class Recipient : Human
    {
        public virtual ICollection<RecipientsList> Lists { get; set; }
    }


}
