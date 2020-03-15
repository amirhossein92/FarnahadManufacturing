namespace FarnahadManufacturing.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class changed_company : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Configuration.CompanyAddress", "CompanyId", "Configuration.Company");
            DropForeignKey("Configuration.CompanyAddress", "AddressId", "Configuration.Address");
            DropForeignKey("Configuration.Company", "DefaultAddressId", "Configuration.Address");
            DropIndex("Configuration.Company", new[] { "DefaultAddressId" });
            DropIndex("Configuration.CompanyAddress", new[] { "CompanyId" });
            DropIndex("Configuration.CompanyAddress", new[] { "AddressId" });
            AddColumn("Configuration.Address", "CompanyId", c => c.Int(nullable: false));
            AddColumn("Configuration.Address", "CompanyDefaultAddressId", c => c.Int(nullable: false));
            AddColumn("Configuration.Company", "DefaultAddress_Id", c => c.Int());
            AlterColumn("Configuration.Part", "Upc", c => c.String(maxLength: 32));
            AlterColumn("Configuration.Uom", "Abbreviation", c => c.String(nullable: false, maxLength: 4));
            CreateIndex("Configuration.Address", "CompanyId");
            CreateIndex("Configuration.Company", "DefaultAddress_Id");
            AddForeignKey("Configuration.Address", "CompanyId", "Configuration.Company", "Id");
            AddForeignKey("Configuration.Company", "DefaultAddress_Id", "Configuration.Address", "Id");
            DropTable("Configuration.CompanyAddress");
        }
        
        public override void Down()
        {
            CreateTable(
                "Configuration.CompanyAddress",
                c => new
                    {
                        CompanyId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CompanyId, t.AddressId });
            
            DropForeignKey("Configuration.Company", "DefaultAddress_Id", "Configuration.Address");
            DropForeignKey("Configuration.Address", "CompanyId", "Configuration.Company");
            DropIndex("Configuration.Company", new[] { "DefaultAddress_Id" });
            DropIndex("Configuration.Address", new[] { "CompanyId" });
            AlterColumn("Configuration.Uom", "Abbreviation", c => c.String(maxLength: 4));
            AlterColumn("Configuration.Part", "Upc", c => c.String(nullable: false, maxLength: 32));
            DropColumn("Configuration.Company", "DefaultAddress_Id");
            DropColumn("Configuration.Address", "CompanyDefaultAddressId");
            DropColumn("Configuration.Address", "CompanyId");
            CreateIndex("Configuration.CompanyAddress", "AddressId");
            CreateIndex("Configuration.CompanyAddress", "CompanyId");
            CreateIndex("Configuration.Company", "DefaultAddressId");
            AddForeignKey("Configuration.Company", "DefaultAddressId", "Configuration.Address", "Id");
            AddForeignKey("Configuration.CompanyAddress", "AddressId", "Configuration.Address", "Id", cascadeDelete: true);
            AddForeignKey("Configuration.CompanyAddress", "CompanyId", "Configuration.Company", "Id", cascadeDelete: true);
        }
    }
}
