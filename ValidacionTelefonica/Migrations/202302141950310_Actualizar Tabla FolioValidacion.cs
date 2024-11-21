namespace ValidacionTelefonica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizarTablaFolioValidacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("Negocio.FolioValidacion", "Extension", c => c.Int());
            AddColumn("Negocio.FolioValidacion", "NombreReferencia1", c => c.String(maxLength: 100, unicode: false));
            AddColumn("Negocio.FolioValidacion", "NumeroReferencia1", c => c.Long());
            AddColumn("Negocio.FolioValidacion", "NombreReferencia2", c => c.String(maxLength: 100, unicode: false));
            AddColumn("Negocio.FolioValidacion", "NumeroReferencia2", c => c.Long());
            AddColumn("Negocio.FolioValidacion", "Comentarios", c => c.String());
            AddColumn("Negocio.FolioValidacion", "FechaRegistro", c => c.DateTime(nullable: false));
            AddColumn("Negocio.FolioValidacion", "UsuarioRegistro", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("Negocio.FolioValidacion", "UsuarioUltimoCambio", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("Negocio.FolioValidacion", "FechaUltimoCambio", c => c.DateTime(nullable: false));
            AlterColumn("Negocio.FolioValidacion", "FuerzaVenta", c => c.String(maxLength: 20, unicode: false));
            AlterColumn("Negocio.FolioValidacion", "NombreCliente", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("Negocio.FolioValidacion", "RFCCliente", c => c.String(maxLength: 15, unicode: false));
            AlterColumn("Negocio.FolioValidacion", "NumeroCasa", c => c.Long());
            AlterColumn("Negocio.FolioValidacion", "NumeroOficina", c => c.Long());
            AlterColumn("Catalogo.MotivoNoContesta", "Nombre", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AlterColumn("Negocio.ResultadoFinalEstatus", "Nombre", c => c.String(nullable: false, maxLength: 300, unicode: false));
            AlterColumn("Negocio.ResultadoFinalVT", "Nombre", c => c.String(nullable: false, maxLength: 300, unicode: false));
            AlterColumn("Negocio.ResultadoVT", "Nombre", c => c.String(nullable: false, maxLength: 300, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("Negocio.ResultadoVT", "Nombre", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("Negocio.ResultadoFinalVT", "Nombre", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("Negocio.ResultadoFinalEstatus", "Nombre", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("Catalogo.MotivoNoContesta", "Nombre", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("Negocio.FolioValidacion", "NumeroOficina", c => c.Long(nullable: false));
            AlterColumn("Negocio.FolioValidacion", "NumeroCasa", c => c.Long(nullable: false));
            AlterColumn("Negocio.FolioValidacion", "RFCCliente", c => c.String());
            AlterColumn("Negocio.FolioValidacion", "NombreCliente", c => c.String());
            AlterColumn("Negocio.FolioValidacion", "FuerzaVenta", c => c.String());
            DropColumn("Negocio.FolioValidacion", "FechaUltimoCambio");
            DropColumn("Negocio.FolioValidacion", "UsuarioUltimoCambio");
            DropColumn("Negocio.FolioValidacion", "UsuarioRegistro");
            DropColumn("Negocio.FolioValidacion", "FechaRegistro");
            DropColumn("Negocio.FolioValidacion", "Comentarios");
            DropColumn("Negocio.FolioValidacion", "NumeroReferencia2");
            DropColumn("Negocio.FolioValidacion", "NombreReferencia2");
            DropColumn("Negocio.FolioValidacion", "NumeroReferencia1");
            DropColumn("Negocio.FolioValidacion", "NombreReferencia1");
            DropColumn("Negocio.FolioValidacion", "Extension");
        }
    }
}
