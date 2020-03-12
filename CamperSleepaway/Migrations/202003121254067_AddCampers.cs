namespace CamperSleepaway.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCampers : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CamperNextOfKins", newName: "NextOfKinCampers");
            DropForeignKey("dbo.Campers", "CabinId", "dbo.Cabins");
            DropIndex("dbo.Campers", new[] { "CabinId" });
            RenameColumn(table: "dbo.Campers", name: "CabinId", newName: "Cabin_CabinId");
            DropPrimaryKey("dbo.NextOfKinCampers");
            AlterColumn("dbo.Campers", "Cabin_CabinId", c => c.Int());
            AddPrimaryKey("dbo.NextOfKinCampers", new[] { "NextOfKin_NextOfKinId", "Camper_CamperId" });
            CreateIndex("dbo.Campers", "Cabin_CabinId");
            AddForeignKey("dbo.Campers", "Cabin_CabinId", "dbo.Cabins", "CabinId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Campers", "Cabin_CabinId", "dbo.Cabins");
            DropIndex("dbo.Campers", new[] { "Cabin_CabinId" });
            DropPrimaryKey("dbo.NextOfKinCampers");
            AlterColumn("dbo.Campers", "Cabin_CabinId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.NextOfKinCampers", new[] { "Camper_CamperId", "NextOfKin_NextOfKinId" });
            RenameColumn(table: "dbo.Campers", name: "Cabin_CabinId", newName: "CabinId");
            CreateIndex("dbo.Campers", "CabinId");
            AddForeignKey("dbo.Campers", "CabinId", "dbo.Cabins", "CabinId", cascadeDelete: true);
            RenameTable(name: "dbo.NextOfKinCampers", newName: "CamperNextOfKins");
        }
    }
}
