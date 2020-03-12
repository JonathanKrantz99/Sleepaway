namespace CamperSleepaway.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cabins",
                c => new
                    {
                        CabinId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CabinId);
            
            CreateTable(
                "dbo.NextOfKins",
                c => new
                    {
                        NextOfKinId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.NextOfKinId);
            
            CreateTable(
                "dbo.Campers",
                c => new
                    {
                        CamperId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CabinId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CamperId)
                .ForeignKey("dbo.Cabins", t => t.CabinId, cascadeDelete: true)
                .Index(t => t.CabinId);
            
            CreateTable(
                "dbo.CamperNextOfKins",
                c => new
                    {
                        Camper_CamperId = c.Int(nullable: false),
                        NextOfKin_NextOfKinId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Camper_CamperId, t.NextOfKin_NextOfKinId })
                .ForeignKey("dbo.Campers", t => t.Camper_CamperId, cascadeDelete: true)
                .ForeignKey("dbo.NextOfKins", t => t.NextOfKin_NextOfKinId, cascadeDelete: true)
                .Index(t => t.Camper_CamperId)
                .Index(t => t.NextOfKin_NextOfKinId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CamperNextOfKins", "NextOfKin_NextOfKinId", "dbo.NextOfKins");
            DropForeignKey("dbo.CamperNextOfKins", "Camper_CamperId", "dbo.Campers");
            DropForeignKey("dbo.Campers", "CabinId", "dbo.Cabins");
            DropIndex("dbo.CamperNextOfKins", new[] { "NextOfKin_NextOfKinId" });
            DropIndex("dbo.CamperNextOfKins", new[] { "Camper_CamperId" });
            DropIndex("dbo.Campers", new[] { "CabinId" });
            DropTable("dbo.CamperNextOfKins");
            DropTable("dbo.Campers");
            DropTable("dbo.NextOfKins");
            DropTable("dbo.Cabins");
        }
    }
}
