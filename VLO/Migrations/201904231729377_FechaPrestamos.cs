namespace VLO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FechaPrestamos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prestamos", "CantidadDevuelta", c => c.Double(nullable: false));
            AddColumn("dbo.Prestamos", "Fecha", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prestamos", "Fecha");
            DropColumn("dbo.Prestamos", "CantidadDevuelta");
        }
    }
}
