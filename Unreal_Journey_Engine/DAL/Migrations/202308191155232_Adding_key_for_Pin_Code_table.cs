namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_key_for_Pin_Code_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pin_Code",
                c => new
                    {
                        Pin_Code_ID = c.Int(nullable: false, identity: true),
                        Pin = c.Int(nullable: false),
                        User_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pin_Code_ID)
                .ForeignKey("dbo.Users", t => t.User_ID, cascadeDelete: true)
                .Index(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pin_Code", "User_ID", "dbo.Users");
            DropIndex("dbo.Pin_Code", new[] { "User_ID" });
            DropTable("dbo.Pin_Code");
        }
    }
}
