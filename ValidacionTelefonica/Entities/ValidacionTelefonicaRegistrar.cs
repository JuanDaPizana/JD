using System;
using System.ComponentModel.DataAnnotations;
using Telcel.Portal.MiTelcelR9.DataAnnotations;
using Telcel.Portal.MiTelcelR9.Helpers;

namespace ValidacionTelefonica.Entities
{
  public class ValidacionTelefonicaRegistrar
  {
    public int ValidacionTelefonicaID { get; set; }

    [Display(Name = "Fuerza de Venta")]
    [Required(ErrorMessage = "El campo Fuerza de Venta es requerido")]
    public int FuerzaVenta { get; set; }

    public string NombreFuerzaVenta { get; set; }

    [Display(Name = "Folio")]
    [StringLength(15, ErrorMessage = "Excedió el máximo permitido de 15 caracteres")]
    [Required(ErrorMessage = "El campo Folio es requerido")]
    public string FolioUnico { get; set; }

    [Display(Name = "Clase de Crédito")]
    [Required(ErrorMessage = "El campo Clase de Crédito es requerido")]
    public int ClaseCreditoID { get; set; }

    [Display(Name = "Clasificación del Cliente")]
    [Required(ErrorMessage = "El campo Clasificación del Cliente es requerido")]
    public int ClasificacionClienteID { get; set; }

    [Display(Name = "Tipo de Solicitud")]
    [Required(ErrorMessage = "El campo Tipo de Solicitud es requerido")]
    public int TipoSolicitudID { get; set; }

    [Display(Name = "Plataforma")]
    [Required(ErrorMessage = "El campo Plataforma es requerido")]
    public int PlataformaID { get; set; }
  }
}