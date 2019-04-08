namespace VLO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sesion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DetallePedidoes", "sesion", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DetallePedidoes", "sesion");
        }
    }
}
