namespace ShFLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Venues", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Tickets", "EventId", "dbo.Events");
            DropForeignKey("dbo.Orders", "BuyerId", "dbo.Users");
            DropForeignKey("dbo.Orders", "Status_Id", "dbo.Status");
            DropForeignKey("dbo.Orders", "Id", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "SellerId", "dbo.Users");
            DropForeignKey("dbo.Events", "VenueId", "dbo.Venues");
            DropIndex("dbo.Venues", new[] { "CityId" });
            DropIndex("dbo.Events", new[] { "VenueId" });
            DropIndex("dbo.Tickets", new[] { "SellerId" });
            DropIndex("dbo.Tickets", new[] { "EventId" });
            DropIndex("dbo.Orders", new[] { "Id" });
            DropIndex("dbo.Orders", new[] { "BuyerId" });
            DropIndex("dbo.Orders", new[] { "Status_Id" });
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
            
            DropTable("dbo.Cities");
            DropTable("dbo.Venues");
            DropTable("dbo.Events");
            DropTable("dbo.Tickets");
            DropTable("dbo.Orders");
            DropTable("dbo.Users");
            DropTable("dbo.Status");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Localization = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TrackNo = c.String(),
                        BuyerId = c.Int(nullable: false),
                        Status_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellerId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Banner = c.Binary(),
                        Description = c.Int(nullable: false),
                        VenueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            CreateIndex("dbo.Orders", "Status_Id");
            CreateIndex("dbo.Orders", "BuyerId");
            CreateIndex("dbo.Orders", "Id");
            CreateIndex("dbo.Tickets", "EventId");
            CreateIndex("dbo.Tickets", "SellerId");
            CreateIndex("dbo.Events", "VenueId");
            CreateIndex("dbo.Venues", "CityId");
            AddForeignKey("dbo.Events", "VenueId", "dbo.Venues", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Tickets", "SellerId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "Id", "dbo.Tickets", "Id");
            AddForeignKey("dbo.Orders", "Status_Id", "dbo.Status", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "BuyerId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Tickets", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Venues", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
