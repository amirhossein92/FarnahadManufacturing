namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_locationType_to_enum : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("BaseConfiguration.LocationType", "CreatedByUserId", "Configuration.User");
            DropForeignKey("Configuration.Location", "LocationTypeId", "BaseConfiguration.LocationType");
            DropIndex("Configuration.Location", new[] { "LocationTypeId" });
            DropIndex("BaseConfiguration.LocationType", new[] { "CreatedByUserId" });
            AddColumn("Configuration.Location", "LocationType", c => c.Int(nullable: false));
            DropColumn("Configuration.Location", "LocationTypeId");
            DropTable("BaseConfiguration.LocationType");
        }
        
        public override void Down()
        {
            CreateTable(
                "BaseConfiguration.LocationType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        ReadOnly = c.Boolean(nullable: false),
                        CreatedByUserId = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("Configuration.Location", "LocationTypeId", c => c.Int(nullable: false));
            DropColumn("Configuration.Location", "LocationType");
            CreateIndex("BaseConfiguration.LocationType", "CreatedByUserId");
            CreateIndex("Configuration.Location", "LocationTypeId");
            AddForeignKey("Configuration.Location", "LocationTypeId", "BaseConfiguration.LocationType", "Id", cascadeDelete: true);
            AddForeignKey("BaseConfiguration.LocationType", "CreatedByUserId", "Configuration.User", "Id");
        }
    }
}
