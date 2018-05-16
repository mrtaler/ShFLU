namespace ShFLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Weigth2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WagInSmgs", "IsWeigherCalc", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WagInSmgs", "IsWeigherCalc");
        }
    }
}
