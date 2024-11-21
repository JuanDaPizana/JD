using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValidacionTelefonica.Entities
{
  public class ReglaValidacionRegistrar
  {
    public int ReglaValidacionID { get; set; }

    [Display(Name = "Coincidencia LN")]
    [Required(ErrorMessage = "El campo coincidencia LN es requerido")]
    public int CoincidenciaLNID { get; set; }

    [Display(Name = "Plataforma")]
    [Required(ErrorMessage = "El campo Plataforma es requerido")]
    public int PlataformaID { get; set; }

    [Display(Name = "Proyecto")]
    [Required(ErrorMessage = "El campo Proyecto es requerido")]
    public int ProyectoID { get; set; }

    [Display(Name = "Resultado VT Casa")]
    [Required(ErrorMessage = "El campo Resultado VT Casa es requerido")]
    public int ResultadoVTCasaID { get; set; }

    [Display(Name = "Resultado VT Oficina")]
    public int? ResultadoVTOficinaID { get; set; } 

    [Display(Name = "Resultado VT Referencia 1")]
    [Required(ErrorMessage = "El campo Resultado VT Referencia 1 es requerido")]
    public int ResultadoVTReferencia1ID { get; set; }

    [Display(Name = "Resultado VT Referencia 2")]
    public int? ResultadoVTReferencia2ID { get; set; }

    [Display(Name = "Resultado Final Estatus")]
    [Required(ErrorMessage = "El campo Resultado Final Estatus es requerido")]
    public int ResultadoFinalEstatusID { get; set; }

    [Display(Name = "Resultado Final VT")]
    [Required(ErrorMessage = "El campo Resultado Final VT es requerido")]
    public int ResultadoFinalVTID { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(500)]
    public string Concatenado { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaUltimoCambio { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(100)]
    public string UsuarioUltimoCambio { get; set; }

    [Column(TypeName = "bit")]
    public Boolean EstaActivo { get; set; }
  }
}