namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class changed_in_paymentTerm : DbMigration
    {
        public override void Up()
        {
            DropColumn("BaseConfiguration.PaymentTerm", "DateDrivenDueDate");
            DropColumn("BaseConfiguration.PaymentTerm", "DateDrivenDiscountDate");
            AddColumn("BaseConfiguration.PaymentTerm", "DateDrivenDueDate", c => c.Int());
            AddColumn("BaseConfiguration.PaymentTerm", "DateDrivenDiscountDate", c => c.Int());
        }

        public override void Down()
        {
            DropColumn("BaseConfiguration.PaymentTerm", "DateDrivenDiscountDate");
            DropColumn("BaseConfiguration.PaymentTerm", "DateDrivenDueDate");
            AddColumn("BaseConfiguration.PaymentTerm", "DateDrivenDueDate", c => c.DateTime());
            AddColumn("BaseConfiguration.PaymentTerm", "DateDrivenDiscountDate", c => c.DateTime());
        }
    }
}
