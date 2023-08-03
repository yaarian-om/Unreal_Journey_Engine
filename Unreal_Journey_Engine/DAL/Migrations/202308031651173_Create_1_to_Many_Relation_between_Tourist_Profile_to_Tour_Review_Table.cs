namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_1_to_Many_Relation_between_Tourist_Profile_to_Tour_Review_Table : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Tour_Review", "Tourist_ID");
            AddForeignKey("dbo.Tour_Review", "Tourist_ID", "dbo.Tourist_Profile", "Tourist_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tour_Review", "Tourist_ID", "dbo.Tourist_Profile");
            DropIndex("dbo.Tour_Review", new[] { "Tourist_ID" });
        }
    }
}
