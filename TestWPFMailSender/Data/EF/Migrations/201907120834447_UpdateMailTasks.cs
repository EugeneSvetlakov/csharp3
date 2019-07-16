namespace MailSender.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMailTasks : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MailMessages", newName: "Messages");
            RenameColumn(table: "dbo.MailTasks", name: "Recipients_id", newName: "RecipientsList_id");
            RenameIndex(table: "dbo.MailTasks", name: "IX_Recipients_id", newName: "IX_RecipientsList_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MailTasks", name: "IX_RecipientsList_id", newName: "IX_Recipients_id");
            RenameColumn(table: "dbo.MailTasks", name: "RecipientsList_id", newName: "Recipients_id");
            RenameTable(name: "dbo.Messages", newName: "MailMessages");
        }
    }
}
