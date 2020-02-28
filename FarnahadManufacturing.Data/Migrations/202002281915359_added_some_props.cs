namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_some_props : DbMigration
    {
        public override void Up()
        {
            AddColumn("Configuration.TrackingPart", "Description", c => c.String());
            AddColumn("Configuration.TrackingPart", "IsPrimary", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Configuration.TrackingPart", "IsPrimary");
            DropColumn("Configuration.TrackingPart", "Description");
        }
    }
}
