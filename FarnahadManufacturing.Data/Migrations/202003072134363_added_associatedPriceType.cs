namespace FarnahadManufacturing.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class added_associatedPriceType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BaseConfiguration.ProductAssociatedPriceType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        IsTaxable = c.Boolean(nullable: false),
                        CreatedByUserId = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.User", t => t.CreatedByUserId)
                .Index(t => t.CreatedByUserId);
            
            AddColumn("Configuration.ProductAssociatePrice", "ProductAssociatedPriceTypeId", c => c.Int(nullable: false));
            CreateIndex("Configuration.ProductAssociatePrice", "ProductAssociatedPriceTypeId");
            AddForeignKey("Configuration.ProductAssociatePrice", "ProductAssociatedPriceTypeId", "BaseConfiguration.ProductAssociatedPriceType", "Id", cascadeDelete: true);
            DropColumn("Configuration.ProductAssociatePrice", "Title");
        }
        
        public override void Down()
        {
            AddColumn("Configuration.ProductAssociatePrice", "Title", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("Configuration.ProductAssociatePrice", "ProductAssociatedPriceTypeId", "BaseConfiguration.ProductAssociatedPriceType");
            DropForeignKey("BaseConfiguration.ProductAssociatedPriceType", "CreatedByUserId", "Configuration.User");
            DropIndex("BaseConfiguration.ProductAssociatedPriceType", new[] { "CreatedByUserId" });
            DropIndex("Configuration.ProductAssociatePrice", new[] { "ProductAssociatedPriceTypeId" });
            DropColumn("Configuration.ProductAssociatePrice", "ProductAssociatedPriceTypeId");
            DropTable("BaseConfiguration.ProductAssociatedPriceType");
        }
    }
}
