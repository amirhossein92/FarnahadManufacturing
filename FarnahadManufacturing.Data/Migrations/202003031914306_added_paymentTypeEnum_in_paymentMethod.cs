namespace FarnahadManufacturing.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class added_paymentTypeEnum_in_paymentMethod : DbMigration
    {
        public override void Up()
        {
            AddColumn("BaseConfiguration.PaymentMethod", "PaymentType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("BaseConfiguration.PaymentMethod", "PaymentType");
        }
    }
}
