namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Deleting_Relationship_Admin_Profile_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin_Profile",
                c => new
                    {
                        Admin_ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Image = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Admin_ID);
            
            CreateTable(
                "dbo.Promotion_Table",
                c => new
                    {
                        Promotion_ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Discription = c.String(nullable: false),
                        Amount = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Promotion_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Promotion_Table");
            DropTable("dbo.Admin_Profile");
        }
    }
}
