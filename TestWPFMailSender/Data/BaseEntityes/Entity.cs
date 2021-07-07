using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.BaseEntityes
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int id { get; set; }
    }
}
