namespace FarnahadManufacturing.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class added_props_into_taxRate : DbMigration
    {
        public override void Up()
        {
            AddColumn("Configuration.TaxRate", "Description", c => c.String());
            AddColumn("Configuration.TaxRate", "IsDefault", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Configuration.TaxRate", "IsDefault");
            DropColumn("Configuration.TaxRate", "Description");
        }
    }
}
