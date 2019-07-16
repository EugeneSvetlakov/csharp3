namespace SaleTickets.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TicketsDb : DbContext
    {
        // Контекст настроен для использования строки подключения "TicketsDb" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "SaleTickets.Data.TicketsDb" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "TicketsDb" 
        // в файле конфигурации приложения.
        public TicketsDb()
            : base("name=TicketsDb")
        {
        }

        public DbSet<Ticket> Tickets {get;set;}
    

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}