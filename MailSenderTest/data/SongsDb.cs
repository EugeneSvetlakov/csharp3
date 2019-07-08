using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderTest.data
{
    class SongsDb : DbContext
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public SongsDb() : this("Name=SongsDb") { }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ConnectionString">Имя строки подключения из App.config или строка подключенияк БД</param>
        public SongsDb(string ConnectionString) : base(ConnectionString)
        {

        }

        public DbSet<Song> Songs { get; set; }

        public DbSet<Artist> Artists { get; set; }
    }

    [Table("Songs")]
    public class Song
    {
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; }

        public double Length { get; set; }

        public string Description { get; set; }

        public virtual Artist Artist { get; set; } //virtual - навигационное свойство, данные будут загружаться автоматически и из таблицы Artists
    }

    //[Table("Artists")]
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}
