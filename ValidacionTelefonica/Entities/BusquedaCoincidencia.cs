using System.ComponentModel.DataAnnotations;


namespace ValidacionTelefonica.Entities
{
  public class BusquedaCoincidencia
  {
    [Display(Name = "Número Telefónico")]
    [StringLength(10, ErrorMessage = "Excedió el máximo permitido de 10 dígitos")]
    [MinLength(10, ErrorMessage = "Numero incorrecto, 10 dígitos obligatorios")]
    public string NumeroTelefono { get; set; }

    [Display(Name = "RFC del Cliente")]
    [StringLength(13, ErrorMessage = "Excedió el máximo permitido de 13 caracteres")]
    public string RFC { get; set; }

    [Display(Name = "Nombre del Cliente")]
    [StringLength(50, ErrorMessage = "Excedió el máximo permitido de 50 caracteres")]
    public string Nombre { get; set; }
  }
}