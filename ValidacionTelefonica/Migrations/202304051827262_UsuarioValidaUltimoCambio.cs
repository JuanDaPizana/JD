namespace ValidacionTelefonica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsuarioValidaUltimoCambio : DbMigration
    {
        public override void Up()
        {
            AddColumn("Negocio.FolioValidacion", "UsuarioValidaInicio", c => c.String(maxLength: 100, unicode: false));
            AddColumn("Negocio.FolioValidacion", "UsuarioValidaFin", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("Negocio.FolioValidacion", "UsuarioValidaFin");
            DropColumn("Negocio.FolioValidacion", "UsuarioValidaInicio");
        }
    }
}
