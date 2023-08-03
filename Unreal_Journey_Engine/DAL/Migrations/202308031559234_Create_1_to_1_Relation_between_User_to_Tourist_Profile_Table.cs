namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_1_to_1_Relation_between_User_to_Tourist_Profile_Table : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Tourist_Profile", "User_ID");
            AddForeignKey("dbo.Tourist_Profile", "User_ID", "dbo.Users", "User_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tourist_Profile", "User_ID", "dbo.Users");
            DropIndex("dbo.Tourist_Profile", new[] { "User_ID" });
        }
    }
}
