namespace ValidacionTelefonica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CatalogoCLN : DbMigration
    {
        public override void Up()
        {
            AddColumn("Negocio.CoincidenciaLN", "EstaActivo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Negocio.CoincidenciaLN", "EstaActivo");
        }
    }
}
