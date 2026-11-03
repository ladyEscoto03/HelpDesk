namespace HelpDesk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTicketModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "UpdatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "UpdatedDate", c => c.DateTime(nullable: false));
        }
    }
}
