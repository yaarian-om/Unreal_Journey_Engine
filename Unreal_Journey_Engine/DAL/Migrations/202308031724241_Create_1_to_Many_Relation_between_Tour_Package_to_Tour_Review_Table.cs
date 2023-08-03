namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_1_to_Many_Relation_between_Tour_Package_to_Tour_Review_Table : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Tour_Review", "Tour_ID");
            AddForeignKey("dbo.Tour_Review", "Tour_ID", "dbo.Tour_Package", "Tour_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tour_Review", "Tour_ID", "dbo.Tour_Package");
            DropIndex("dbo.Tour_Review", new[] { "Tour_ID" });
        }
    }
}
