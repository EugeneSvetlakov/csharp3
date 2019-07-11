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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Servers");
            DropTable("dbo.Senders");
            DropTable("dbo.Recipients");
            DropTable("dbo.MailMessages");
        }
    }
}
