namespace ShFLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Weigth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Weighers",
                c => new
                    {
                        WeigherId = c.Int(nullable: false),
                        WeigherTara = c.Int(nullable: false),
                        neettoSmgs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WeigherBrutto = c.Int(nullable: false),
                        WeigherDiff = c.Int(nullable: false),
                        WeigherDiffNotPer = c.Int(nullable: false),
                        WeigherDiffPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.WeigherId)
                .ForeignKey("dbo.WagInSmgs", t => t.WeigherId)
                .Index(t => t.WeigherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Weighers", "WeigherId", "dbo.WagInSmgs");
            DropIndex("dbo.Weighers", new[] { "WeigherId" });
            DropTable("dbo.Weighers");
        }
    }
}
