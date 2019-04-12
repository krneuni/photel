namespace VLO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Factura : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Facturas", "Propina", c => c.Double(nullable: false));
            AddColumn("dbo.Facturas", "Descripcion", c => c.String());
            AlterColumn("dbo.Facturas", "FechaFactura", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Facturas", "FechaFactura", c => c.String(nullable: false));
            DropColumn("dbo.Facturas", "Descripcion");
            DropColumn("dbo.Facturas", "Propina");
        }
    }
}
