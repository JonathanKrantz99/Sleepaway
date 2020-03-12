namespace CamperSleepaway.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add : DbMigration
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
                "dbo.Campers",
                c => new
                    {
                        CamperId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Cabin_CabinId = c.Int(),
                    })
                .PrimaryKey(t => t.CamperId)
                .ForeignKey("dbo.Cabins", t => t.Cabin_CabinId)
                .Index(t => t.Cabin_CabinId);
            
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
                "dbo.Counselors",
                c => new
                    {
                        CounselorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Cabin_CabinId = c.Int(),
                    })
                .PrimaryKey(t => t.CounselorId)
                .ForeignKey("dbo.Cabins", t => t.Cabin_CabinId)
                .Index(t => t.Cabin_CabinId);
            
            CreateTable(
                "dbo.NextOfKinCampers",
                c => new
                    {
                        NextOfKin_NextOfKinId = c.Int(nullable: false),
                        Camper_CamperId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NextOfKin_NextOfKinId, t.Camper_CamperId })
                .ForeignKey("dbo.NextOfKins", t => t.NextOfKin_NextOfKinId, cascadeDelete: true)
                .ForeignKey("dbo.Campers", t => t.Camper_CamperId, cascadeDelete: true)
                .Index(t => t.NextOfKin_NextOfKinId)
                .Index(t => t.Camper_CamperId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Counselors", "Cabin_CabinId", "dbo.Cabins");
            DropForeignKey("dbo.Campers", "Cabin_CabinId", "dbo.Cabins");
            DropForeignKey("dbo.NextOfKinCampers", "Camper_CamperId", "dbo.Campers");
            DropForeignKey("dbo.NextOfKinCampers", "NextOfKin_NextOfKinId", "dbo.NextOfKins");
            DropIndex("dbo.NextOfKinCampers", new[] { "Camper_CamperId" });
            DropIndex("dbo.NextOfKinCampers", new[] { "NextOfKin_NextOfKinId" });
            DropIndex("dbo.Counselors", new[] { "Cabin_CabinId" });
            DropIndex("dbo.Campers", new[] { "Cabin_CabinId" });
            DropTable("dbo.NextOfKinCampers");
            DropTable("dbo.Counselors");
            DropTable("dbo.NextOfKins");
            DropTable("dbo.Campers");
            DropTable("dbo.Cabins");
        }
    }
}
