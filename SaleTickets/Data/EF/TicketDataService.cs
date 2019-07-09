using SaleTickets.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleTickets.Data.EF
{
    public class TicketDataService : ITicketDataService
    {
        public Ticket GetById(int id)
        {
            using(var db = new TicketsDb())
            {
                return db.Tickets.FirstOrDefault(t => (t.Id == id));
            }
        }

        public IEnumerable<Ticket> GetAll()
        {
            using (var db = new TicketsDb())
            {
                return db.Tickets.ToArray<Ticket>();
            }
        }

        public void Add(Ticket item)
        {
            using (var db = new TicketsDb())
            {
                var sel = db.Tickets.FirstOrDefault(t => t.Id == item.Id);
                if (sel is null)
                {
                    db.Tickets.Add(item);
                    db.SaveChanges();
                }
                
            }
        }

        public void Delete(Ticket item)
        {
            using (var db = new TicketsDb())
            {
                var sel = db.Tickets.FirstOrDefault(t => t == item);
                if (!(db.Tickets.FirstOrDefault(t => t.Id == item.Id) is null))
                {
                    db.Tickets.Remove(item);
                    db.SaveChanges();
                }
            }
        }

        public void Edit(Ticket item)
        {
            using (var db = new TicketsDb())
            {
                var sel = db.Tickets.FirstOrDefault(t => t.Id == item.Id);
                if (!(sel is null))
                {
                    sel.SeanceName = item.SeanceName;
                    sel.Quantity = item.Quantity;
                    db.SaveChanges();
                }
            }
        }
    }
}
