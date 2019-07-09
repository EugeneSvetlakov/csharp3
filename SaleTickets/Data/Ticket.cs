using SaleTickets.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleTickets.Data
{
    [Table("Tickets")]
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string SeanceName { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}