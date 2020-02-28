namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_some_props_in_trackingPart : DbMigration
    {
        public override void Up()
        {
            AddColumn("Configuration.TrackingPart", "IsSelected", c => c.Boolean(nullable: false));
        }
        

        public override void Down()
        {
            DropColumn("Configuration.TrackingPart", "IsSelected");
        }
    }
}
