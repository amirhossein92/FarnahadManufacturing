namespace FarnahadManufacturing.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class added_new_objects_related_to_part : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Configuration.PartDefaultLocation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartId = c.Int(nullable: false),
                        LocationGroupId = c.Int(nullable: false),
                        DefaultLocationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Location", t => t.DefaultLocationId)
                .ForeignKey("Configuration.LocationGroup", t => t.LocationGroupId, cascadeDelete: true)
                .ForeignKey("Configuration.Part", t => t.PartId, cascadeDelete: true)
                .Index(t => t.PartId)
                .Index(t => t.LocationGroupId)
                .Index(t => t.DefaultLocationId);
            
            CreateTable(
                "Configuration.PartReorderInformation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartId = c.Int(nullable: false),
                        LocationGroupId = c.Int(nullable: false),
                        OrderUpToLevel = c.Double(nullable: false),
                        ReorderPoint = c.Double(nullable: false),
                        CreatedByUserId = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.User", t => t.CreatedByUserId)
                .ForeignKey("Configuration.LocationGroup", t => t.LocationGroupId, cascadeDelete: true)
                .ForeignKey("Configuration.Part", t => t.PartId, cascadeDelete: true)
                .Index(t => t.PartId)
                .Index(t => t.LocationGroupId)
                .Index(t => t.CreatedByUserId);
            
            AddColumn("Configuration.Part", "RevisionNumber", c => c.String());
            AddColumn("Configuration.Part", "AlertNote", c => c.String());
            AddColumn("Configuration.Part", "PartAbcCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("Configuration.PartDefaultLocation", "PartId", "Configuration.Part");
            DropForeignKey("Configuration.PartReorderInformation", "PartId", "Configuration.Part");
            DropForeignKey("Configuration.PartReorderInformation", "LocationGroupId", "Configuration.LocationGroup");
            DropForeignKey("Configuration.PartReorderInformation", "CreatedByUserId", "Configuration.User");
            DropForeignKey("Configuration.PartDefaultLocation", "LocationGroupId", "Configuration.LocationGroup");
            DropForeignKey("Configuration.PartDefaultLocation", "DefaultLocationId", "Configuration.Location");
            DropIndex("Configuration.PartReorderInformation", new[] { "CreatedByUserId" });
            DropIndex("Configuration.PartReorderInformation", new[] { "LocationGroupId" });
            DropIndex("Configuration.PartReorderInformation", new[] { "PartId" });
            DropIndex("Configuration.PartDefaultLocation", new[] { "DefaultLocationId" });
            DropIndex("Configuration.PartDefaultLocation", new[] { "LocationGroupId" });
            DropIndex("Configuration.PartDefaultLocation", new[] { "PartId" });
            DropColumn("Configuration.Part", "PartAbcCode");
            DropColumn("Configuration.Part", "AlertNote");
            DropColumn("Configuration.Part", "RevisionNumber");
            DropTable("Configuration.PartReorderInformation");
            DropTable("Configuration.PartDefaultLocation");
        }
    }
}
