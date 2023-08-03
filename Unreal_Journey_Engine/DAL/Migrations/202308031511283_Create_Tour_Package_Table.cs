namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Tour_Package_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tourist_Profile", "User_ID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tourist_Profile", "User_ID");
        }
    }
}
