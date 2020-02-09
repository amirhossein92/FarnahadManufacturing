namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_partCost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Configuration.PartCost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartId = c.Int(nullable: false),
                        Cost = c.Double(nullable: false),
                        CreatedByUserId = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.User", t => t.CreatedByUserId)
                .ForeignKey("Configuration.Part", t => t.PartId, cascadeDelete: true)
                .Index(t => t.PartId)
                .Index(t => t.CreatedByUserId);
            
            DropColumn("Configuration.Part", "Cost");
        }
        
        public override void Down()
        {
            AddColumn("Configuration.Part", "Cost", c => c.Double());
            DropForeignKey("Configuration.PartCost", "PartId", "Configuration.Part");
            DropForeignKey("Configuration.PartCost", "CreatedByUserId", "Configuration.User");
            DropIndex("Configuration.PartCost", new[] { "CreatedByUserId" });
            DropIndex("Configuration.PartCost", new[] { "PartId" });
            DropTable("Configuration.PartCost");
        }
    }
}
