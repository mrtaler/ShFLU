namespace ShFLY.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            this.DropForeignKey("dbo.Venues", "CityId", "dbo.Cities");
            this.DropForeignKey("dbo.Tickets", "EventId", "dbo.Events");
            this.DropForeignKey("dbo.Orders", "BuyerId", "dbo.Users");
            this.DropForeignKey("dbo.Orders", "Status_Id", "dbo.Status");
            this.DropForeignKey("dbo.Orders", "Id", "dbo.Tickets");
            this.DropForeignKey("dbo.Tickets", "SellerId", "dbo.Users");
            this.DropForeignKey("dbo.Events", "VenueId", "dbo.Venues");
            this.DropIndex("dbo.Venues", new[] { "CityId" });
            this.DropIndex("dbo.Events", new[] { "VenueId" });
            this.DropIndex("dbo.Tickets", new[] { "SellerId" });
            this.DropIndex("dbo.Tickets", new[] { "EventId" });
            this.DropIndex("dbo.Orders", new[] { "Id" });
            this.DropIndex("dbo.Orders", new[] { "BuyerId" });
            this.DropIndex("dbo.Orders", new[] { "Status_Id" });
            this.CreateTable(
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

            this.CreateTable(
                "dbo.Matrix",
                c => new
                    {
                        MatrixxId = c.Int(nullable: false, identity: true),
                        MatrixNum = c.Int(nullable: false),
                        MatrixDate = c.DateTime(nullable: false),
                        MatrixType = c.String(),
                    })
                .PrimaryKey(t => t.MatrixxId);

            this.CreateTable(
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

            this.CreateTable(
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

            this.CreateTable(
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

            this.DropTable("dbo.Cities");
            this.DropTable("dbo.Venues");
            this.DropTable("dbo.Events");
            this.DropTable("dbo.Tickets");
            this.DropTable("dbo.Orders");
            this.DropTable("dbo.Users");
            this.DropTable("dbo.Status");
        }
        
        public override void Down()
        {
            this.CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
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

            this.CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TrackNo = c.String(),
                        BuyerId = c.Int(nullable: false),
                        Status_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellerId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
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

            this.CreateTable(
                "dbo.Venues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            this.DropForeignKey("dbo.WagInSmgs", "MatrixWagonTara_MatrixWagonId", "dbo.MatrixWagon");
            this.DropForeignKey("dbo.WagInSmgs", "WgaonId", "dbo.Wagon");
            this.DropForeignKey("dbo.WagInSmgs", "SmgsNaklId", "dbo.SmgsNakl");
            this.DropForeignKey("dbo.WagInSmgs", "MatrixWagonBrutto_MatrixWagonId", "dbo.MatrixWagon");
            this.DropForeignKey("dbo.MatrixWagon", "MatrixId", "dbo.Matrix");
            this.DropIndex("dbo.WagInSmgs", new[] { "MatrixWagonTara_MatrixWagonId" });
            this.DropIndex("dbo.WagInSmgs", new[] { "MatrixWagonBrutto_MatrixWagonId" });
            this.DropIndex("dbo.WagInSmgs", new[] { "SmgsNaklId" });
            this.DropIndex("dbo.WagInSmgs", new[] { "WgaonId" });
            this.DropIndex("dbo.MatrixWagon", new[] { "MatrixId" });
            this.DropTable("dbo.Wagon");
            this.DropTable("dbo.SmgsNakl");
            this.DropTable("dbo.WagInSmgs");
            this.DropTable("dbo.Matrix");
            this.DropTable("dbo.MatrixWagon");
            this.CreateIndex("dbo.Orders", "Status_Id");
            this.CreateIndex("dbo.Orders", "BuyerId");
            this.CreateIndex("dbo.Orders", "Id");
            this.CreateIndex("dbo.Tickets", "EventId");
            this.CreateIndex("dbo.Tickets", "SellerId");
            this.CreateIndex("dbo.Events", "VenueId");
            this.CreateIndex("dbo.Venues", "CityId");
            this.AddForeignKey("dbo.Events", "VenueId", "dbo.Venues", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.Tickets", "SellerId", "dbo.Users", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.Orders", "Id", "dbo.Tickets", "Id");
            this.AddForeignKey("dbo.Orders", "Status_Id", "dbo.Status", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.Orders", "BuyerId", "dbo.Users", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.Tickets", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.Venues", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
