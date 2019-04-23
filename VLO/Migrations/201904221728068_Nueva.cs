namespace VLO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nueva : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DetallePedidoes", "Termino", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DetallePedidoes", "Termino");
        }
    }
}
