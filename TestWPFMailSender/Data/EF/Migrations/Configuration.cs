namespace MailSender.Data.EF.Migrations // -MigrationsDirectory Data\EF\Migrations
{
    using MailSender.Data.BaseEntityes;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;


    // ��������� ��������: � ������� ���������� ������� � "������ �� ���������" ���������� ������ �������� EF ������� ��� ���� ������
    // -StartUpProjectName MailSender - �������� ��������� �������
    // -MigrationsDirectory Data\EF\Migrations - ���� � ���������� �������� � "������ �� ���������"
    // -EnableAutomaticMigrations - ��������� �������������� ��������
    // -ConnectionStringName MailSenderDB - �������� c����� ����������� � ���� ������ ���� ��������� � App.config � �������, ��������� � "StartUpProjectName"
    // Enable-Migrations -StartUpProjectName MailSender -EnableAutomaticMigrations -MigrationsDirectory Data\EF\Migrations -ConnectionStringName MailSenderDB

    //�������� ��������:
    // Add-Migration Initial -StartUpProjectName MailSender -Verbose

    // ���������� ��������
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
