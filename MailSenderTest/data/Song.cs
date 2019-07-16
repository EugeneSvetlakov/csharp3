using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailSenderTest.data
{
    [Table("Songs")]
    public class Song
    {
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; }

        public double Length { get; set; }

        public DateTime? CreationDate { get; set; }

        public string Description { get; set; }

        public virtual Artist Artist { get; set; } //virtual - навигационное свойство, данные будут загружаться автоматически и из таблицы Artists
    }
}
