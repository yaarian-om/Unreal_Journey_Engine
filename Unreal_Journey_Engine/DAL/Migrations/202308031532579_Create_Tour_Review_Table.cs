namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Tour_Review_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tour_Review",
                c => new
                    {
                        Review_ID = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false),
                        Tour_ID = c.Int(nullable: false),
                        Tourist_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Review_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tour_Review");
        }
    }
}
