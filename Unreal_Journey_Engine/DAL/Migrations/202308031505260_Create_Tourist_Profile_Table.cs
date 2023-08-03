namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Tourist_Profile_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tourist_Profile",
                c => new
                    {
                        Tourist_ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Image = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Tourist_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tourist_Profile");
        }
    }
}
