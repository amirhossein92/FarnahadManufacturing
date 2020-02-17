namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_province : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("BaseConfiguration.City", "CountryId", "BaseConfiguration.Country");
            DropIndex("BaseConfiguration.City", new[] { "CountryId" });
            CreateTable(
                "BaseConfiguration.Province",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Abbreviation = c.String(nullable: false, maxLength: 4),
                        Description = c.String(),
                        CountryId = c.Int(nullable: false),
                        CreatedByUserId = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BaseConfiguration.Country", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("Configuration.User", t => t.CreatedByUserId)
                .Index(t => t.CountryId)
                .Index(t => t.CreatedByUserId);
            
            AddColumn("Configuration.Address", "ProvinceId", c => c.Int());
            AddColumn("BaseConfiguration.City", "ProvinceId", c => c.Int(nullable: false));
            AddColumn("BaseConfiguration.Country", "Abbreviation", c => c.String(nullable: false, maxLength: 4));
            CreateIndex("Configuration.Address", "ProvinceId");
            CreateIndex("BaseConfiguration.City", "ProvinceId");
            AddForeignKey("BaseConfiguration.City", "ProvinceId", "BaseConfiguration.Province", "Id");
            AddForeignKey("Configuration.Address", "ProvinceId", "BaseConfiguration.Province", "Id");
            DropColumn("Configuration.Address", "Province");
            DropColumn("BaseConfiguration.City", "CountryId");
        }
        
        public override void Down()
        {
            AddColumn("BaseConfiguration.City", "CountryId", c => c.Int(nullable: false));
            AddColumn("Configuration.Address", "Province", c => c.String());
            DropForeignKey("Configuration.Address", "ProvinceId", "BaseConfiguration.Province");
            DropForeignKey("BaseConfiguration.City", "ProvinceId", "BaseConfiguration.Province");
            DropForeignKey("BaseConfiguration.Province", "CreatedByUserId", "Configuration.User");
            DropForeignKey("BaseConfiguration.Province", "CountryId", "BaseConfiguration.Country");
            DropIndex("BaseConfiguration.Province", new[] { "CreatedByUserId" });
            DropIndex("BaseConfiguration.Province", new[] { "CountryId" });
            DropIndex("BaseConfiguration.City", new[] { "ProvinceId" });
            DropIndex("Configuration.Address", new[] { "ProvinceId" });
            DropColumn("BaseConfiguration.Country", "Abbreviation");
            DropColumn("BaseConfiguration.City", "ProvinceId");
            DropColumn("Configuration.Address", "ProvinceId");
            DropTable("BaseConfiguration.Province");
            CreateIndex("BaseConfiguration.City", "CountryId");
            AddForeignKey("BaseConfiguration.City", "CountryId", "BaseConfiguration.Country", "Id");
        }
    }
}
