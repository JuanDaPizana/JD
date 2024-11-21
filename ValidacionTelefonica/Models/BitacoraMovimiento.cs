using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValidacionTelefonica.Models
{
  [Table("BitacoraMovimiento", Schema = "Auditoria")]
  public class BitacoraMovimiento
  {
    [Key]
    public int BitacoraMovimientoID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    public int FolioValidacion { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(100)]
    public string UsuarioUltimoCambio { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(100)]
    public string Campo { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(100)]
    public string Valor { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(100)]
    public string Movimiento { get; set; }

  }
}