using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValidacionTelefonica.Models
{
  [Table("FolioValidacion", Schema = "Negocio")]
  public class FolioValidacion
  {
    [Key]
    public int FolioValidacionID { get; set; }

    public int ClaseCreditoID { get; set; }

    public virtual ClaseCredito ClaseCredito { get; set; }

    public int EstatusID { get; set; }

    public virtual Estatus Estatus { get; set; }

    public int TipoSolicitudID { get; set; }

    public virtual TipoSolicitud TipoSolicitud { get; set; }

    public int ProyectoID { get; set; }

    public virtual Proyecto Proyecto { get; set; }

    public int CoincidenciaLNID { get; set; }

    public virtual CoincidenciaLN CoincidenciaLN { get; set; } // Lista Negra (LN)

    public int ResultadoVTID { get; set; }

    public virtual ResultadoVTCasa ResultadoVTCasa { get; set;} // Validacion Telefonica Casa (VT)

    public int ResultadoVTOficinaID { get; set; }
     
    public virtual ResultadoVTOficina ResultadoVTOficina { get; set; }

    public int ResultadoVTReferencia1ID { get; set; }

    public virtual ResultadoVTReferencia1 ResultadoVTReferencia1 { get; set; }

    public int ResultadoVTReferencia2ID { get; set; }

    public virtual ResultadoVTReferencia2 ResultadoVTReferencia2 { get; set; }

    public int ResultadoFinalVTID { get; set; }

    public virtual ResultadoFinalVT ResultadoFinalVT { get; set; } 

    public int ResultadoFinalEstatusID { get; set; }

    public virtual ResultadoFinalEstatus ResultadoFinalEstatus { get; set; }

    public int PlataformaID { get; set; }

    public virtual Plataforma Plataforma { get; set; }

    public int MotivoNoContestaID{ get; set; }

    public virtual MotivoNoContesta MotivoNoContesta { get; set; }

    public int ClasificacionClienteID { get; set; }

    public virtual ClasificacionCliente ClasificacionCliente { get; set; }

    [Column(TypeName = "bigint")]
    [Required]
    public long FolioUnico { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(20)]
    public string FuerzaVenta { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(100)]
    public string NombreCliente { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(15)]
    public string RFCCliente { get; set; }

    [Column(TypeName = "bigint")]
    public long? NumeroCasa { get; set; }

    [Column(TypeName = "bigint")]
    public long? NumeroOficina { get; set; }

    [Column(TypeName = "int")]
    public int? Extension { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(100)]
    public string NombreReferencia1 { get; set; }

    [Column(TypeName = "bigint")]
    public long? NumeroReferencia1 { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(100)]
    public string NombreReferencia2 { get; set; }

    [Column(TypeName = "bigint")]
    public long? NumeroReferencia2 { get; set; }

    public string Comentarios { get; set; }

    [Column(TypeName = "datetime")]
    [Required]
    public DateTime FechaRegistro { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? HoraInicioValidacionTelefonica { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(100)]
    public string UsuarioValidaInicio { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? HoraFinValidacionTelefonica { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(100)]
    public string UsuarioValidaFin { get; set; }

    [Column(TypeName = "varchar")]
    [Required]
    [StringLength(100)]
    public string UsuarioRegistro { get; set; }

    [Column(TypeName = "varchar")]
    [Required]
    [StringLength(100)]
    public string UsuarioUltimoCambio { get; set; }

    [Column(TypeName = "datetime")]
    [Required]
    public DateTime FechaUltimoCambio { get; set; }

  }
}