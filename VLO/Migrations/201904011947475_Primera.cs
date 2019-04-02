namespace VLO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Primera : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Menus", "IdTermino", "dbo.TerminoCarnes");
            DropIndex("dbo.Menus", new[] { "IdTermino" });
            DropColumn("dbo.Menus", "IdTermino");
            DropTable("dbo.TerminoCarnes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TerminoCarnes",
                c => new
                    {
                        IdTermino = c.Int(nullable: false, identity: true),
                        Termino = c.String(),
                    })
                .PrimaryKey(t => t.IdTermino);
            
            AddColumn("dbo.Menus", "IdTermino", c => c.Int(nullable: false));
            CreateIndex("dbo.Menus", "IdTermino");
            AddForeignKey("dbo.Menus", "IdTermino", "dbo.TerminoCarnes", "IdTermino", cascadeDelete: true);
        }
    }
}
