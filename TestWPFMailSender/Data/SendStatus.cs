using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data.BaseEntityes;

namespace MailSender.Data
{

    public class SendStatus
    {
        private SendStatus(SendStatusEnum @enum)
        {
            Id = (int)@enum;
            Name = @enum.ToString();
            Description = @enum.GetEnumDescription();
        }

        protected SendStatus() { } //Для EF

        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public static implicit operator SendStatus(SendStatusEnum @enum) => new SendStatus(@enum);

        public static implicit operator SendStatusEnum(SendStatus sendStatus) => (SendStatusEnum)sendStatus.Id;
    }
}
