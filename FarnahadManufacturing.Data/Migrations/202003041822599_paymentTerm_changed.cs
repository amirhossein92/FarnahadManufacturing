namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paymentTerm_changed : DbMigration
    {
        public override void Up()
        {
            AddColumn("BaseConfiguration.PaymentTerm", "ReadOnly", c => c.Boolean(nullable: false));
            DropColumn("BaseConfiguration.PaymentTerm", "IsDateDriven");
        }
        
        public override void Down()
        {
            AddColumn("BaseConfiguration.PaymentTerm", "IsDateDriven", c => c.Boolean(nullable: false));
            DropColumn("BaseConfiguration.PaymentTerm", "ReadOnly");
        }
    }
}
