namespace FarnahadManufacturing.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class added_productAssociatePrice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Configuration.ProductAssociatePrice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 128),
                        Price = c.Double(nullable: false),
                        CreatedByUserId = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.User", t => t.CreatedByUserId)
                .ForeignKey("Configuration.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "Configuration.ProductSubstitute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        SubstituteProductId = c.Int(nullable: false),
                        Note = c.String(),
                        CreatedByUserId = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.User", t => t.CreatedByUserId)
                .ForeignKey("Configuration.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("Configuration.Product", t => t.SubstituteProductId)
                .Index(t => t.ProductId)
                .Index(t => t.SubstituteProductId)
                .Index(t => t.CreatedByUserId);
            
            AddColumn("Configuration.Product", "SaleOrderItemType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("Configuration.ProductSubstitute", "SubstituteProductId", "Configuration.Product");
            DropForeignKey("Configuration.ProductSubstitute", "ProductId", "Configuration.Product");
            DropForeignKey("Configuration.ProductSubstitute", "CreatedByUserId", "Configuration.User");
            DropForeignKey("Configuration.ProductAssociatePrice", "ProductId", "Configuration.Product");
            DropForeignKey("Configuration.ProductAssociatePrice", "CreatedByUserId", "Configuration.User");
            DropIndex("Configuration.ProductSubstitute", new[] { "CreatedByUserId" });
            DropIndex("Configuration.ProductSubstitute", new[] { "SubstituteProductId" });
            DropIndex("Configuration.ProductSubstitute", new[] { "ProductId" });
            DropIndex("Configuration.ProductAssociatePrice", new[] { "CreatedByUserId" });
            DropIndex("Configuration.ProductAssociatePrice", new[] { "ProductId" });
            DropColumn("Configuration.Product", "SaleOrderItemType");
            DropTable("Configuration.ProductSubstitute");
            DropTable("Configuration.ProductAssociatePrice");
        }
    }
}
