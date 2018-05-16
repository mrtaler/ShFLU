namespace ShFLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Weigth4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SmgsNakl", "IsWeigherCalc", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SmgsNakl", "IsWeigherCalc");
        }
    }
}
