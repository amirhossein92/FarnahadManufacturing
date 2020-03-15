namespace FarnahadManufacturing.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class added_prop_in_vendor : DbMigration
    {
        public override void Up()
        {
            AddColumn("Configuration.Vendor", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Configuration.Vendor", "IsActive");
        }
    }
}
