using System.ComponentModel.DataAnnotations;

namespace ValidacionTelefonica.Entities
{
  public class ValidacionTelefonicaValidar
  {
    public int FolioValidacionID { get; set; }

    [Display(Name = "Fuerza de Venta")]
    [Required(ErrorMessage = "El campo Fuerza de Venta es requerido")]
    public string FuerzaVenta { get; set; }

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

    public int EstatusID { get; set; }

    public string NombreEstatus { get; set; }

    public string FechaRegistro { get; set; }

    public string HoraInicioValidacion { get; set; }

    public string HoraFinValidacion { get; set; }

    [Display(Name = "Tipo de Solicitud")]
    [Required(ErrorMessage = "El campo Tipo de Solicitud es requerido")]
    public int TipoSolicitudID { get; set; }

    [Display(Name = "Proyecto")]
    [Required(ErrorMessage = "El campo Proyecto es requerido")]
    public int ProyectoID { get; set; }

    [Display(Name = "Plataforma")]
    [Required(ErrorMessage = "El campo Plataforma es requerido")]
    public int PlataformaID { get; set; }

    [Display(Name = "Coincidencia Lista Negra")]
    [Required(ErrorMessage = "El campo Coincidencia Lista Negra es requerido")]
    public int CoincidenciaLNID { get; set; }

    [Display(Name = "Motivo No Contesta")]
    [Required(ErrorMessage = "El campo Motivo No Contesta es requerido")]
    public int MotivoNoContestaID { get; set; }

    [Display(Name = "Nombre del Cliente")]
    [Required(ErrorMessage = "El campo Nombre del Cliente es requerido")]
    [StringLength(70, ErrorMessage = "Excedió el máximo permitido de 70 caracteres")]
    public string NombreCliente { get; set; }

    [Display(Name = "RFC del Cliente")]
    [Required(ErrorMessage = "El campo RFC del Cliente es requerido")]
    [StringLength(13, ErrorMessage = "Excedió el máximo permitido de 13 caracteres")]
    public string RFCCliente { get; set; }

    [Display(Name = "Número de Casa")]
    [StringLength(10, ErrorMessage = "Excedió el máximo permitido de 10 dígitos")]
    [MinLength(10, ErrorMessage = "Numero incorrecto, 10 dígitos obligatorios")]
    [Required(ErrorMessage = "El campo Número de Casa es requerido")]
    public string NumeroCasa { get; set; }

    [Display(Name = "Número de Oficina")]
    [StringLength(10, ErrorMessage = "Excedió el máximo permitido de 10 dígitos")]
    [MinLength(10, ErrorMessage = "Numero incorrecto, 10 dígitos obligatorios")]
    [Required(ErrorMessage = "El campo Número de Oficina es requerido")]

    public string NumeroOficina { get; set; }

    [Display(Name = "Extensión")]
    [StringLength(4, ErrorMessage = "Excedió el máximo permitido de 4 dígitos")]
    public string Extension { get; set; }

    [Display(Name = "Nombre Referencia 1")]
    [Required(ErrorMessage = "El campo Nombre Referencia 1 es requerido")]
    [StringLength(70, ErrorMessage = "Excedió el máximo permitido de 70 caracteres")]
    public string NombreReferencia1 { get; set; }

    [Display(Name = "Telefóno Referencia 1")]
    [StringLength(10, ErrorMessage = "Excedió el máximo permitido de 10 dígitos")]
    [MinLength(10, ErrorMessage = "Numero incorrecto, 10 dígitos obligatorios")]
    [Required(ErrorMessage = "El campo Telefóno Referencia 1 es requerido")]
    public string TelefonoReferencia1 { get; set; }

    [Display(Name = "Nombre Referencia 2")]
    [StringLength(70, ErrorMessage = "Excedió el máximo permitido de 70 caracteres")]
    [Required(ErrorMessage = "El campo Nombre Referencia 2 es requerido")]

    public string NombreReferencia2 { get; set; }

    [Display(Name = "Telefóno Referencia 2")]
    [StringLength(10, ErrorMessage = "Excedió el máximo permitido de 10 dígitos")]
    [MinLength(10, ErrorMessage = "Numero incorrecto, 10 digitos obligatorios")]
    [Required(ErrorMessage = "El campo Telefóno Referencia 2 es requerido")]

    public string TelefonoReferencia2 { get; set; }

    [Display(Name = "Comentarios")]
    [Required(ErrorMessage = "El campo Comentarios es requerido")]
    [StringLength(2000, ErrorMessage = "Excedió el máximo permitido de 2000 caracteres")]
    public string Comentarios { get; set; }

    [Display(Name = "Resultado VT Casa")]
    public int ResultadoVTCasaID { get; set; }

    [Display(Name = "Resultado VT Oficina")]
    [Required(ErrorMessage = "El campo Resultado VT Oficina es requerido")]
    public int ResultadoVTOficinaID { get; set; }

    [Display(Name = "Resultado VT Referencia1")]
    public int ResultadoVTReferencia1ID { get; set; }

    [Display(Name = "Resultado VT Referencia2")]
    public int ResultadoVTReferencia2ID { get; set; }

    [Display(Name = "Resultado Estatus Final ")]
    public int ResultadoFinalEstatusID { get; set; }

    [Display(Name = "Resultado Final Validación Telefónica ")]
    public int ResultadoFinalVTID { get; set; }

  }
}