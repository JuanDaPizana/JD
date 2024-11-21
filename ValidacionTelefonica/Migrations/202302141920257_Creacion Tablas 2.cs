namespace ValidacionTelefonica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionTablas2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Catalogo.ClaseCredito",
                c => new
                    {
                        ClaseCreditoID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ClaseCreditoID);
            
            CreateTable(
                "Negocio.FolioValidacion",
                c => new
                    {
                        FolioValidacionID = c.Int(nullable: false, identity: true),
                        ClaseCreditoID = c.Int(nullable: false),
                        EstatusID = c.Int(nullable: false),
                        TipoSolicitudID = c.Int(nullable: false),
                        ProyectoID = c.Int(nullable: false),
                        CoincidenciaLNID = c.Int(nullable: false),
                        ResultadoVTID = c.Int(nullable: false),
                        ResultadoFinalVTID = c.Int(nullable: false),
                        ResultadoFinalEstatusID = c.Int(nullable: false),
                        PlataformaID = c.Int(nullable: false),
                        MotivoNoContestaID = c.Int(nullable: false),
                        ClasificacionClienteID = c.Int(nullable: false),
                        FolioUnico = c.Long(nullable: false),
                        FuerzaVenta = c.String(),
                        NombreCliente = c.String(),
                        RFCCliente = c.String(),
                        NumeroCasa = c.Long(nullable: false),
                        NumeroOficina = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.FolioValidacionID)
                .ForeignKey("Catalogo.ClaseCredito", t => t.ClaseCreditoID, cascadeDelete: true)
                .ForeignKey("Catalogo.ClasificacionCliente", t => t.ClasificacionClienteID, cascadeDelete: true)
                .ForeignKey("Negocio.CoincidenciaLN", t => t.CoincidenciaLNID, cascadeDelete: true)
                .ForeignKey("Negocio.Estatus", t => t.EstatusID, cascadeDelete: true)
                .ForeignKey("Catalogo.MotivoNoContesta", t => t.MotivoNoContestaID, cascadeDelete: true)
                .ForeignKey("Catalogo.Plataforma", t => t.PlataformaID, cascadeDelete: true)
                .ForeignKey("Catalogo.Proyecto", t => t.ProyectoID, cascadeDelete: true)
                .ForeignKey("Negocio.ResultadoFinalEstatus", t => t.ResultadoFinalEstatusID, cascadeDelete: true)
                .ForeignKey("Negocio.ResultadoFinalVT", t => t.ResultadoFinalVTID, cascadeDelete: true)
                .ForeignKey("Negocio.ResultadoVT", t => t.ResultadoVTID, cascadeDelete: true)
                .ForeignKey("Catalogo.TipoSolicitud", t => t.TipoSolicitudID, cascadeDelete: true)
                .Index(t => t.ClaseCreditoID)
                .Index(t => t.EstatusID)
                .Index(t => t.TipoSolicitudID)
                .Index(t => t.ProyectoID)
                .Index(t => t.CoincidenciaLNID)
                .Index(t => t.ResultadoVTID)
                .Index(t => t.ResultadoFinalVTID)
                .Index(t => t.ResultadoFinalEstatusID)
                .Index(t => t.PlataformaID)
                .Index(t => t.MotivoNoContestaID)
                .Index(t => t.ClasificacionClienteID);
            
            CreateTable(
                "Catalogo.ClasificacionCliente",
                c => new
                    {
                        ClasificacionClienteID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ClasificacionClienteID);
            
            CreateTable(
                "Negocio.CoincidenciaLN",
                c => new
                    {
                        CoincidenciaLNID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.CoincidenciaLNID);
            
            CreateTable(
                "Negocio.Estatus",
                c => new
                    {
                        EstatusID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.EstatusID);
            
            CreateTable(
                "Catalogo.MotivoNoContesta",
                c => new
                    {
                        MotivoNoContestaID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.MotivoNoContestaID);
            
            CreateTable(
                "Catalogo.Plataforma",
                c => new
                    {
                        PlataformaID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.PlataformaID);
            
            CreateTable(
                "Catalogo.Proyecto",
                c => new
                    {
                        ProyectoID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ProyectoID);
            
            CreateTable(
                "Negocio.ResultadoFinalEstatus",
                c => new
                    {
                        ResultadoFinalEstatusID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ResultadoFinalEstatusID);
            
            CreateTable(
                "Negocio.ResultadoFinalVT",
                c => new
                    {
                        ResultadoFinalVTID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ResultadoFinalVTID);
            
            CreateTable(
                "Negocio.ResultadoVT",
                c => new
                    {
                        ResultadoVTID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ResultadoVTID);
            
            CreateTable(
                "Catalogo.TipoSolicitud",
                c => new
                    {
                        TipoSolicitudID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.TipoSolicitudID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Negocio.FolioValidacion", "TipoSolicitudID", "Catalogo.TipoSolicitud");
            DropForeignKey("Negocio.FolioValidacion", "ResultadoVTID", "Negocio.ResultadoVT");
            DropForeignKey("Negocio.FolioValidacion", "ResultadoFinalVTID", "Negocio.ResultadoFinalVT");
            DropForeignKey("Negocio.FolioValidacion", "ResultadoFinalEstatusID", "Negocio.ResultadoFinalEstatus");
            DropForeignKey("Negocio.FolioValidacion", "ProyectoID", "Catalogo.Proyecto");
            DropForeignKey("Negocio.FolioValidacion", "PlataformaID", "Catalogo.Plataforma");
            DropForeignKey("Negocio.FolioValidacion", "MotivoNoContestaID", "Catalogo.MotivoNoContesta");
            DropForeignKey("Negocio.FolioValidacion", "EstatusID", "Negocio.Estatus");
            DropForeignKey("Negocio.FolioValidacion", "CoincidenciaLNID", "Negocio.CoincidenciaLN");
            DropForeignKey("Negocio.FolioValidacion", "ClasificacionClienteID", "Catalogo.ClasificacionCliente");
            DropForeignKey("Negocio.FolioValidacion", "ClaseCreditoID", "Catalogo.ClaseCredito");
            DropIndex("Negocio.FolioValidacion", new[] { "ClasificacionClienteID" });
            DropIndex("Negocio.FolioValidacion", new[] { "MotivoNoContestaID" });
            DropIndex("Negocio.FolioValidacion", new[] { "PlataformaID" });
            DropIndex("Negocio.FolioValidacion", new[] { "ResultadoFinalEstatusID" });
            DropIndex("Negocio.FolioValidacion", new[] { "ResultadoFinalVTID" });
            DropIndex("Negocio.FolioValidacion", new[] { "ResultadoVTID" });
            DropIndex("Negocio.FolioValidacion", new[] { "CoincidenciaLNID" });
            DropIndex("Negocio.FolioValidacion", new[] { "ProyectoID" });
            DropIndex("Negocio.FolioValidacion", new[] { "TipoSolicitudID" });
            DropIndex("Negocio.FolioValidacion", new[] { "EstatusID" });
            DropIndex("Negocio.FolioValidacion", new[] { "ClaseCreditoID" });
            DropTable("Catalogo.TipoSolicitud");
            DropTable("Negocio.ResultadoVT");
            DropTable("Negocio.ResultadoFinalVT");
            DropTable("Negocio.ResultadoFinalEstatus");
            DropTable("Catalogo.Proyecto");
            DropTable("Catalogo.Plataforma");
            DropTable("Catalogo.MotivoNoContesta");
            DropTable("Negocio.Estatus");
            DropTable("Negocio.CoincidenciaLN");
            DropTable("Catalogo.ClasificacionCliente");
            DropTable("Negocio.FolioValidacion");
            DropTable("Catalogo.ClaseCredito");
        }
    }
}
