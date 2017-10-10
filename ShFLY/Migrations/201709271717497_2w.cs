namespace ShFLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2w : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatrixWagon",
                c => new
                    {
                        MatrixWagonId = c.Int(nullable: false, identity: true),
                        WagonNumberPP = c.Int(nullable: false),
                        WagonNumberMatrix = c.Int(),
                        Speed = c.String(),
                        Weight = c.String(),
                        MatrixId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MatrixWagonId)
                .ForeignKey("dbo.Matrix", t => t.MatrixId, cascadeDelete: true)
                .Index(t => t.MatrixId);
            
            CreateTable(
                "dbo.Matrix",
                c => new
                    {
                        MatrixxId = c.Int(nullable: false, identity: true),
                        MatrixNum = c.Int(nullable: false),
                        MatrixDate = c.DateTime(nullable: false),
                        MatrixType = c.String(),
                    })
                .PrimaryKey(t => t.MatrixxId);
            
            CreateTable(
                "dbo.WagInSmgs",
                c => new
                    {
                        WagonSmgsId = c.Int(nullable: false, identity: true),
                        WgaonId = c.Int(nullable: false),
                        SmgsNaklId = c.Int(nullable: false),
                        Tarapr = c.String(),
                        Weightb = c.String(),
                        Weight = c.String(),
                        MatrixWagonBrutto_MatrixWagonId = c.Int(),
                        MatrixWagonTara_MatrixWagonId = c.Int(),
                    })
                .PrimaryKey(t => t.WagonSmgsId)
                .ForeignKey("dbo.MatrixWagon", t => t.MatrixWagonBrutto_MatrixWagonId)
                .ForeignKey("dbo.SmgsNakl", t => t.SmgsNaklId, cascadeDelete: true)
                .ForeignKey("dbo.Wagon", t => t.WgaonId, cascadeDelete: true)
                .ForeignKey("dbo.MatrixWagon", t => t.MatrixWagonTara_MatrixWagonId)
                .Index(t => t.WgaonId)
                .Index(t => t.SmgsNaklId)
                .Index(t => t.MatrixWagonBrutto_MatrixWagonId)
                .Index(t => t.MatrixWagonTara_MatrixWagonId);
            
            CreateTable(
                "dbo.SmgsNakl",
                c => new
                    {
                        SmgsId = c.Int(nullable: false, identity: true),
                        Smgs = c.Int(nullable: false),
                        Smgsdat = c.DateTime(nullable: false),
                        gngc = c.String(),
                        gngn = c.String(),
                        etsngc = c.String(),
                        Etsngn = c.String(),
                        mnet = c.String(),
                        mbrt = c.String(),
                    })
                .PrimaryKey(t => t.SmgsId);
            
            CreateTable(
                "dbo.Wagon",
                c => new
                    {
                        IdWagon = c.Int(nullable: false, identity: true),
                        Nwag = c.Int(nullable: false),
                        Ownerc = c.String(),
                        Gp = c.String(),
                        Tara = c.String(),
                    })
                .PrimaryKey(t => t.IdWagon);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WagInSmgs", "MatrixWagonTara_MatrixWagonId", "dbo.MatrixWagon");
            DropForeignKey("dbo.WagInSmgs", "WgaonId", "dbo.Wagon");
            DropForeignKey("dbo.WagInSmgs", "SmgsNaklId", "dbo.SmgsNakl");
            DropForeignKey("dbo.WagInSmgs", "MatrixWagonBrutto_MatrixWagonId", "dbo.MatrixWagon");
            DropForeignKey("dbo.MatrixWagon", "MatrixId", "dbo.Matrix");
            DropIndex("dbo.WagInSmgs", new[] { "MatrixWagonTara_MatrixWagonId" });
            DropIndex("dbo.WagInSmgs", new[] { "MatrixWagonBrutto_MatrixWagonId" });
            DropIndex("dbo.WagInSmgs", new[] { "SmgsNaklId" });
            DropIndex("dbo.WagInSmgs", new[] { "WgaonId" });
            DropIndex("dbo.MatrixWagon", new[] { "MatrixId" });
            DropTable("dbo.Wagon");
            DropTable("dbo.SmgsNakl");
            DropTable("dbo.WagInSmgs");
            DropTable("dbo.Matrix");
            DropTable("dbo.MatrixWagon");
        }
    }
}
