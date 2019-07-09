namespace MailSenderTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRenameColumnArtistSurname : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Artists", "Surname", "Group");
            AlterColumn("dbo.Artists", "Group", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Artists", "Group", "Surname");
            AlterColumn("dbo.Artists", "Surname", c => c.String(maxLength: 120));
        }
    }
}
