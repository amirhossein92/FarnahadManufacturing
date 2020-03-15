namespace FarnahadManufacturing.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class changed_uomType_to_enum : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("BaseConfiguration.UomType", "CreatedByUserId", "Configuration.User");
            DropForeignKey("Configuration.Uom", "UomTypeId", "BaseConfiguration.UomType");
            DropIndex("Configuration.Uom", new[] { "UomTypeId" });
            DropIndex("BaseConfiguration.UomType", new[] { "CreatedByUserId" });
            AddColumn("Configuration.Uom", "UomType", c => c.Int(nullable: false));
            DropColumn("Configuration.Uom", "UomTypeId");
            DropTable("BaseConfiguration.UomType");
        }
        
        public override void Down()
        {
            CreateTable(
                "BaseConfiguration.UomType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        CreatedByUserId = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("Configuration.Uom", "UomTypeId", c => c.Int(nullable: false));
            DropColumn("Configuration.Uom", "UomType");
            CreateIndex("BaseConfiguration.UomType", "CreatedByUserId");
            CreateIndex("Configuration.Uom", "UomTypeId");
            AddForeignKey("Configuration.Uom", "UomTypeId", "BaseConfiguration.UomType", "Id", cascadeDelete: true);
            AddForeignKey("BaseConfiguration.UomType", "CreatedByUserId", "Configuration.User", "Id");
        }
    }
}
