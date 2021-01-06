namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodane_pola_wartosc_do_product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Value");
        }
    }
}
