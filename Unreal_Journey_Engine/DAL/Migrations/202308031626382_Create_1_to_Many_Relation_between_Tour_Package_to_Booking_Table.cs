namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_1_to_Many_Relation_between_Tour_Package_to_Booking_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "Tour_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Bookings", "Tour_ID");
            AddForeignKey("dbo.Bookings", "Tour_ID", "dbo.Tour_Package", "Tour_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "Tour_ID", "dbo.Tour_Package");
            DropIndex("dbo.Bookings", new[] { "Tour_ID" });
            DropColumn("dbo.Bookings", "Tour_ID");
        }
    }
}
