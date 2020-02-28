namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_last_change_user_and_date : DbMigration
    {
        public override void Up()
        {
            AddColumn("Configuration.Part", "LastChangedByUserId", c => c.Int(nullable: false));
            AddColumn("Configuration.Part", "LastChangedDateTime", c => c.DateTime(nullable: false));
            CreateIndex("Configuration.Part", "LastChangedByUserId");
            AddForeignKey("Configuration.Part", "LastChangedByUserId", "Configuration.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Configuration.Part", "LastChangedByUserId", "Configuration.User");
            DropIndex("Configuration.Part", new[] { "LastChangedByUserId" });
            DropColumn("Configuration.Part", "LastChangedDateTime");
            DropColumn("Configuration.Part", "LastChangedByUserId");
        }
    }
}
