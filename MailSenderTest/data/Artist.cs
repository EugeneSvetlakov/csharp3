using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MailSenderTest.data
{
    //[Table("Artists")]
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Group { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}
