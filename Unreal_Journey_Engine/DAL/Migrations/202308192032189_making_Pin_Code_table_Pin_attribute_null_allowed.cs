namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class making_Pin_Code_table_Pin_attribute_null_allowed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pin_Code", "Pin", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pin_Code", "Pin", c => c.Int(nullable: false));
        }
    }
}
