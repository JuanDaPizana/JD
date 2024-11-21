namespace ValidacionTelefonica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Catalogos : DbMigration
    {
        public override void Up()
        {
            AddColumn("Catalogo.ClaseCredito", "EstaActivo", c => c.Boolean(nullable: false));
            AddColumn("Catalogo.ClasificacionCliente", "EstaActivo", c => c.Boolean(nullable: false));
            AddColumn("Negocio.Estatus", "EstaActivo", c => c.Boolean(nullable: false));
            AddColumn("Catalogo.MotivoNoContesta", "EstaActivo", c => c.Boolean(nullable: false));
            AddColumn("Catalogo.Plataforma", "EstaActivo", c => c.Boolean(nullable: false));
            AddColumn("Catalogo.Proyecto", "EstaActivo", c => c.Boolean(nullable: false));
            AddColumn("Negocio.ResultadoFinalEstatus", "EstaActivo", c => c.Boolean(nullable: false));
            AddColumn("Negocio.ResultadoFinalVT", "EstaActivo", c => c.Boolean(nullable: false));
            AddColumn("Negocio.ResultadoVT", "EstaActivo", c => c.Boolean(nullable: false));
            AddColumn("Catalogo.TipoSolicitud", "EstaActivo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Catalogo.TipoSolicitud", "EstaActivo");
            DropColumn("Negocio.ResultadoVT", "EstaActivo");
            DropColumn("Negocio.ResultadoFinalVT", "EstaActivo");
            DropColumn("Negocio.ResultadoFinalEstatus", "EstaActivo");
            DropColumn("Catalogo.Proyecto", "EstaActivo");
            DropColumn("Catalogo.Plataforma", "EstaActivo");
            DropColumn("Catalogo.MotivoNoContesta", "EstaActivo");
            DropColumn("Negocio.Estatus", "EstaActivo");
            DropColumn("Catalogo.ClasificacionCliente", "EstaActivo");
            DropColumn("Catalogo.ClaseCredito", "EstaActivo");
        }
    }
}
