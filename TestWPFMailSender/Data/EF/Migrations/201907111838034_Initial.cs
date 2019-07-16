namespace MailSender.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MailMessages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.MailTasks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        SendStatusEnum = c.Byte(nullable: false),
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
                "dbo.RecipientsLists",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Recipients",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Name = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Senders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Name = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Servers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Port = c.Int(nullable: false),
                        Ssl = c.Boolean(nullable: false),
                        Login = c.String(),
                        Pwd = c.String(),
                        Name = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.RecipientRecipientsLists",
                c => new
                    {
                        Recipient_id = c.Int(nullable: false),
                        RecipientsList_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Recipient_id, t.RecipientsList_id })
                .ForeignKey("dbo.Recipients", t => t.Recipient_id, cascadeDelete: true)
                .ForeignKey("dbo.RecipientsLists", t => t.RecipientsList_id, cascadeDelete: true)
                .Index(t => t.Recipient_id)
                .Index(t => t.RecipientsList_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MailTasks", "Server_id", "dbo.Servers");
            DropForeignKey("dbo.MailTasks", "Sender_id", "dbo.Senders");
            DropForeignKey("dbo.MailTasks", "Recipients_id", "dbo.RecipientsLists");
            DropForeignKey("dbo.RecipientRecipientsLists", "RecipientsList_id", "dbo.RecipientsLists");
            DropForeignKey("dbo.RecipientRecipientsLists", "Recipient_id", "dbo.Recipients");
            DropForeignKey("dbo.MailTasks", "Message_id", "dbo.MailMessages");
            DropIndex("dbo.RecipientRecipientsLists", new[] { "RecipientsList_id" });
            DropIndex("dbo.RecipientRecipientsLists", new[] { "Recipient_id" });
            DropIndex("dbo.MailTasks", new[] { "Server_id" });
            DropIndex("dbo.MailTasks", new[] { "Sender_id" });
            DropIndex("dbo.MailTasks", new[] { "Recipients_id" });
            DropIndex("dbo.MailTasks", new[] { "Message_id" });
            DropTable("dbo.RecipientRecipientsLists");
            DropTable("dbo.Servers");
            DropTable("dbo.Senders");
            DropTable("dbo.Recipients");
            DropTable("dbo.RecipientsLists");
            DropTable("dbo.MailTasks");
            DropTable("dbo.MailMessages");
        }
    }
}
