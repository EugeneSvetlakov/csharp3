namespace MailSender.Data.EF.Migrations // -MigrationsDirectory Data\EF\Migrations
{
    using MailSender.Data.BaseEntityes;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;


    // Включение миграции: В консоли диспетчера пакетов в "Проект по умолчанию" выбирается проект источник EF классов для базы данных
    // -StartUpProjectName MailSender - Название основного проекта
    // -MigrationsDirectory Data\EF\Migrations - путь к размещению миграций в "Проект по умолчанию"
    // -EnableAutomaticMigrations - включение автоматической миграции
    // -ConnectionStringName MailSenderDB - название cтроки подключения к базе должна быть прописана в App.config в проекте, указанном в "StartUpProjectName"
    // Enable-Migrations -StartUpProjectName MailSender -EnableAutomaticMigrations -MigrationsDirectory Data\EF\Migrations -ConnectionStringName MailSenderDB

    //Создание миграции:
    // Add-Migration Initial -StartUpProjectName MailSender -Verbose

    // Выполнение миграции
    // Update-Database -Verbose -StartUpProjectName MailSender

    internal sealed class Configuration : DbMigrationsConfiguration<MailSender.Data.EF.MailSenderDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Data\EF\Migrations";
        }

        protected override void Seed(MailSender.Data.EF.MailSenderDB db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
