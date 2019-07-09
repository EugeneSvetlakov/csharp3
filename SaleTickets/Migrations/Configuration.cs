namespace SaleTickets.Migrations
{
    using SaleTickets.Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SaleTickets.Data.TicketsDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SaleTickets.Data.TicketsDb db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var tickets = new Ticket[10];
            var rnd = new Random();
            for (int i = 0; i < tickets.Length; i++)
            {
                tickets[i] = new Ticket
                {
                    SeanceName = $"Кино {i}",
                    Quantity = rnd.Next(50)
                };
            }

            db.Tickets.AddRange(tickets);

            db.SaveChanges();
        }
    }
}
