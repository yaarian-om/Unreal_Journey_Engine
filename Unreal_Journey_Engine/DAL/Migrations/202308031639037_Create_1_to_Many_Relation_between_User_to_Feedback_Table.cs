namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_1_to_Many_Relation_between_User_to_Feedback_Table : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Feedbacks", "User_ID");
            AddForeignKey("dbo.Feedbacks", "User_ID", "dbo.Users", "User_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "User_ID", "dbo.Users");
            DropIndex("dbo.Feedbacks", new[] { "User_ID" });
        }
    }
}
