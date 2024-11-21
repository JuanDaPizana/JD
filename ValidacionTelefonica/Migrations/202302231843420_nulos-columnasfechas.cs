namespace ValidacionTelefonica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nuloscolumnasfechas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Negocio.FolioValidacion", "HoraInicioValidacionTelefonica", c => c.DateTime());
            AlterColumn("Negocio.FolioValidacion", "HoraFinValidacionTelefonica", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("Negocio.FolioValidacion", "HoraFinValidacionTelefonica", c => c.DateTime(nullable: false));
            AlterColumn("Negocio.FolioValidacion", "HoraInicioValidacionTelefonica", c => c.DateTime(nullable: false));
        }
    }
}
