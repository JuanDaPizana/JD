namespace ValidacionTelefonica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFolioValidacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("Negocio.FolioValidacion", "HoraInicioValidacionTelefonica", c => c.DateTime(nullable: false));
            AddColumn("Negocio.FolioValidacion", "HoraFinValidacionTelefonica", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Negocio.FolioValidacion", "HoraFinValidacionTelefonica");
            DropColumn("Negocio.FolioValidacion", "HoraInicioValidacionTelefonica");
        }
    }
}
