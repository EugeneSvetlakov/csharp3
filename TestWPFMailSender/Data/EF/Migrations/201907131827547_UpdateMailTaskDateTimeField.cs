namespace MailSender.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMailTaskDateTimeField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MailTasks", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MailTasks", "Time", c => c.DateTime());
        }
    }
}
