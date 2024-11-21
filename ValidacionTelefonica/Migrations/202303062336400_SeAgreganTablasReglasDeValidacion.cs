namespace ValidacionTelefonica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeAgreganTablasReglasDeValidacion : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "Negocio.ResultadoVT", newName: "ResultadoVTCasa");
            CreateTable(
                "Auditoria.BitacoraMovimiento",
                c => new
                    {
                        BitacoraMovimientoID = c.Int(nullable: false, identity: true),
                        FechaRegistro = c.DateTime(),
                        FolioValidacion = c.Int(nullable: false),
                        UsuarioUltimoCambio = c.String(maxLength: 100, unicode: false),
                        Campo = c.String(maxLength: 100, unicode: false),
                        Valor = c.String(maxLength: 100, unicode: false),
                        Movimiento = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.BitacoraMovimientoID);
            
            CreateTable(
                "Negocio.CoincidenciaLNBitacora",
                c => new
                    {
                        CoincidenciaLNBitacoraID = c.Int(nullable: false, identity: true),
                        NumeroTelefonico = c.Long(nullable: false),
                        FechaUltimoCambio = c.DateTime(),
                        FolioValidacionID = c.Int(nullable: false),
                        CoincidenciaLNID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CoincidenciaLNBitacoraID)
                .ForeignKey("Negocio.CoincidenciaLN", t => t.CoincidenciaLNID, cascadeDelete: true)
                .Index(t => t.CoincidenciaLNID);
            
            CreateTable(
                "Negocio.ResultadoVTOficina",
                c => new
                    {
                        ResultadoVTOficinaID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 300, unicode: false),
                        EstaActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ResultadoVTOficinaID);
            
            CreateTable(
                "Negocio.ResultadoVTReferencia1",
                c => new
                    {
                        ResultadoVTReferencia1ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 300, unicode: false),
                        EstaActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ResultadoVTReferencia1ID);
            
            CreateTable(
                "Negocio.ResultadoVTReferencia2",
                c => new
                    {
                        ResultadoVTReferencia2ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 300, unicode: false),
                        EstaActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ResultadoVTReferencia2ID);
            
            CreateTable(
                "Negocio.ReglaValidacion",
                c => new
                    {
                        CoincidenciaLNID = c.Int(nullable: false),
                        PlataformaID = c.Int(nullable: false),
                        ProyectoID = c.Int(nullable: false),
                        ResultadoVTCasaID = c.Int(nullable: false),
                        ResultadoVTOficinaID = c.Int(nullable: false),
                        ResultadoVTReferencia1ID = c.Int(nullable: false),
                        ResultadoVTReferencia2ID = c.Int(nullable: false),
                        ResultadoFinalEstatusID = c.Int(nullable: false),
                        ResultadoFinalVTID = c.Int(nullable: false),
                        Concatenado = c.String(maxLength: 500, unicode: false),
                        FechaUltimoCambio = c.DateTime(nullable: false),
                        UsuarioUltimoCambio = c.String(maxLength: 100, unicode: false),
                        EstaActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.CoincidenciaLNID, t.PlataformaID, t.ProyectoID, t.ResultadoVTCasaID, t.ResultadoVTOficinaID, t.ResultadoVTReferencia1ID, t.ResultadoVTReferencia2ID, t.ResultadoFinalEstatusID, t.ResultadoFinalVTID });
            
            AddColumn("Negocio.FolioValidacion", "ResultadoVTOficinaID", c => c.Int(nullable: false));
            AddColumn("Negocio.FolioValidacion", "ResultadoVTReferencia1ID", c => c.Int(nullable: false));
            AddColumn("Negocio.FolioValidacion", "ResultadoVTReferencia2ID", c => c.Int(nullable: false));
            CreateIndex("Negocio.FolioValidacion", "ResultadoVTOficinaID");
            CreateIndex("Negocio.FolioValidacion", "ResultadoVTReferencia1ID");
            CreateIndex("Negocio.FolioValidacion", "ResultadoVTReferencia2ID");
            AddForeignKey("Negocio.FolioValidacion", "ResultadoVTOficinaID", "Negocio.ResultadoVTOficina", "ResultadoVTOficinaID", cascadeDelete: true);
            AddForeignKey("Negocio.FolioValidacion", "ResultadoVTReferencia1ID", "Negocio.ResultadoVTReferencia1", "ResultadoVTReferencia1ID", cascadeDelete: true);
            AddForeignKey("Negocio.FolioValidacion", "ResultadoVTReferencia2ID", "Negocio.ResultadoVTReferencia2", "ResultadoVTReferencia2ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Negocio.FolioValidacion", "ResultadoVTReferencia2ID", "Negocio.ResultadoVTReferencia2");
            DropForeignKey("Negocio.FolioValidacion", "ResultadoVTReferencia1ID", "Negocio.ResultadoVTReferencia1");
            DropForeignKey("Negocio.FolioValidacion", "ResultadoVTOficinaID", "Negocio.ResultadoVTOficina");
            DropForeignKey("Negocio.CoincidenciaLNBitacora", "CoincidenciaLNID", "Negocio.CoincidenciaLN");
            DropIndex("Negocio.CoincidenciaLNBitacora", new[] { "CoincidenciaLNID" });
            DropIndex("Negocio.FolioValidacion", new[] { "ResultadoVTReferencia2ID" });
            DropIndex("Negocio.FolioValidacion", new[] { "ResultadoVTReferencia1ID" });
            DropIndex("Negocio.FolioValidacion", new[] { "ResultadoVTOficinaID" });
            DropColumn("Negocio.FolioValidacion", "ResultadoVTReferencia2ID");
            DropColumn("Negocio.FolioValidacion", "ResultadoVTReferencia1ID");
            DropColumn("Negocio.FolioValidacion", "ResultadoVTOficinaID");
            DropTable("Negocio.ReglaValidacion");
            DropTable("Negocio.ResultadoVTReferencia2");
            DropTable("Negocio.ResultadoVTReferencia1");
            DropTable("Negocio.ResultadoVTOficina");
            DropTable("Negocio.CoincidenciaLNBitacora");
            DropTable("Auditoria.BitacoraMovimiento");
            RenameTable(name: "Negocio.ResultadoVTCasa", newName: "ResultadoVT");
        }
    }
}
