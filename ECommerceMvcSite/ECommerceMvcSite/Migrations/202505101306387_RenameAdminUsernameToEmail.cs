namespace ECommerceMvcSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameAdminUsernameToEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "Email", c => c.String(nullable: false));
            DropColumn("dbo.Admins", "Username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "Username", c => c.String(nullable: false));
            DropColumn("dbo.Admins", "Email");
        }
    }
}
