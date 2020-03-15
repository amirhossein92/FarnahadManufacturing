namespace FarnahadManufacturing.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class added_userGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Configuration.UserGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        CreatedByUserId = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.User", t => t.CreatedByUserId)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "Configuration.UserUserGroup",
                c => new
                    {
                        UserGroupId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserGroupId, t.UserId })
                .ForeignKey("Configuration.UserGroup", t => t.UserGroupId, cascadeDelete: true)
                .ForeignKey("Configuration.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserGroupId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Configuration.UserUserGroup", "UserId", "Configuration.User");
            DropForeignKey("Configuration.UserUserGroup", "UserGroupId", "Configuration.UserGroup");
            DropForeignKey("Configuration.UserGroup", "CreatedByUserId", "Configuration.User");
            DropIndex("Configuration.UserUserGroup", new[] { "UserId" });
            DropIndex("Configuration.UserUserGroup", new[] { "UserGroupId" });
            DropIndex("Configuration.UserGroup", new[] { "CreatedByUserId" });
            DropTable("Configuration.UserUserGroup");
            DropTable("Configuration.UserGroup");
        }
    }
}
