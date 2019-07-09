namespace MailSenderTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateArtistAndSong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "Surname", c => c.String(nullable: false, maxLength: 120));
            AddColumn("dbo.Songs", "CreationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "CreationDate");
            DropColumn("dbo.Artists", "Surname");
        }
    }
}
