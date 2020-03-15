namespace FarnahadManufacturing.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class fixed_companyAddress : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CompanyAddresses", newName: "CompanyAddress");
            MoveTable(name: "dbo.CompanyAddress", newSchema: "Configuration");
            RenameColumn(table: "Configuration.CompanyAddress", name: "Company_Id", newName: "CompanyId");
            RenameColumn(table: "Configuration.CompanyAddress", name: "Address_Id", newName: "AddressId");
            RenameIndex(table: "Configuration.CompanyAddress", name: "IX_Company_Id", newName: "IX_CompanyId");
            RenameIndex(table: "Configuration.CompanyAddress", name: "IX_Address_Id", newName: "IX_AddressId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "Configuration.CompanyAddress", name: "IX_AddressId", newName: "IX_Address_Id");
            RenameIndex(table: "Configuration.CompanyAddress", name: "IX_CompanyId", newName: "IX_Company_Id");
            RenameColumn(table: "Configuration.CompanyAddress", name: "AddressId", newName: "Address_Id");
            RenameColumn(table: "Configuration.CompanyAddress", name: "CompanyId", newName: "Company_Id");
            MoveTable(name: "Configuration.CompanyAddress", newSchema: "dbo");
            RenameTable(name: "dbo.CompanyAddress", newName: "CompanyAddresses");
        }
    }
}
