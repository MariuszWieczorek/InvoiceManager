namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.InvoicePossitions", new[] { "ProductID" });
            CreateIndex("dbo.InvoicePossitions", "ProductId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.InvoicePossitions", new[] { "ProductId" });
            CreateIndex("dbo.InvoicePossitions", "ProductID");
        }
    }
}
