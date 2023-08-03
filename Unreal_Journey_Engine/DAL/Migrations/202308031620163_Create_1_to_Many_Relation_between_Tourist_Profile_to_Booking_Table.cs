namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_1_to_Many_Relation_between_Tourist_Profile_to_Booking_Table : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Bookings", "Tourist_ID");
            AddForeignKey("dbo.Bookings", "Tourist_ID", "dbo.Tourist_Profile", "Tourist_ID", cascadeDelete: true);
            DropColumn("dbo.Bookings", "Tour_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "Tour_ID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Bookings", "Tourist_ID", "dbo.Tourist_Profile");
            DropIndex("dbo.Bookings", new[] { "Tourist_ID" });
        }
    }
}
