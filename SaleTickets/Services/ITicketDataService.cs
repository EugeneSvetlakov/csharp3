using SaleTickets.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleTickets.Services
{
    public interface ITicketDataService : IEntityFrameworkService<Ticket> { }
}
