namespace MailSender.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchedulerTasksAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RecipientsLists",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.SchedulerTasks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Message_id = c.Int(),
                        Recipients_id = c.Int(),
                        Sender_id = c.Int(),
                        Server_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.MailMessages", t => t.Message_id)
                .ForeignKey("dbo.RecipientsLists", t => t.Recipients_id)
                .ForeignKey("dbo.Senders", t => t.Sender_id)
                .ForeignKey("dbo.Servers", t => t.Server_id)
                .Index(t => t.Message_id)
                .Index(t => t.Recipients_id)
                .Index(t => t.Sender_id)
                .Index(t => t.Server_id);
            
            CreateTable(
                "dbo.RecipientsListRecipients",
                c => new
                    {
                        RecipientsList_id = c.Int(nullable: false),
                        Recipient_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RecipientsList_id, t.Recipient_id })
                .ForeignKey("dbo.RecipientsLists", t => t.RecipientsList_id, cascadeDelete: true)
                .ForeignKey("dbo.Recipients", t => t.Recipient_id, cascadeDelete: true)
                .Index(t => t.RecipientsList_id)
                .Index(t => t.Recipient_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchedulerTasks", "Server_id", "dbo.Servers");
            DropForeignKey("dbo.SchedulerTasks", "Sender_id", "dbo.Senders");
            DropForeignKey("dbo.SchedulerTasks", "Recipients_id", "dbo.RecipientsLists");
            DropForeignKey("dbo.SchedulerTasks", "Message_id", "dbo.MailMessages");
            DropForeignKey("dbo.RecipientsListRecipients", "Recipient_id", "dbo.Recipients");
            DropForeignKey("dbo.RecipientsListRecipients", "RecipientsList_id", "dbo.RecipientsLists");
            DropIndex("dbo.RecipientsListRecipients", new[] { "Recipient_id" });
            DropIndex("dbo.RecipientsListRecipients", new[] { "RecipientsList_id" });
            DropIndex("dbo.SchedulerTasks", new[] { "Server_id" });
            DropIndex("dbo.SchedulerTasks", new[] { "Sender_id" });
            DropIndex("dbo.SchedulerTasks", new[] { "Recipients_id" });
            DropIndex("dbo.SchedulerTasks", new[] { "Message_id" });
            DropTable("dbo.RecipientsListRecipients");
            DropTable("dbo.SchedulerTasks");
            DropTable("dbo.RecipientsLists");
        }
    }
}
