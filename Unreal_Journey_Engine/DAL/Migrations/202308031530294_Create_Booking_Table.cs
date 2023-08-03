namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Booking_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Booking_ID = c.Int(nullable: false, identity: true),
                        Tour_Status = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Payment_Status = c.String(nullable: false),
                        Tourist_ID = c.Int(nullable: false),
                        Tour_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Booking_ID);
            
            CreateTable(
                "dbo.Tour_Package",
                c => new
                    {
                        Tour_ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Location = c.String(nullable: false),
                        Duration = c.String(nullable: false),
                        Cost = c.String(nullable: false),
                        Image = c.String(nullable: false),
                        Tour_Guide_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Tour_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tour_Package");
            DropTable("dbo.Bookings");
        }
    }
}
