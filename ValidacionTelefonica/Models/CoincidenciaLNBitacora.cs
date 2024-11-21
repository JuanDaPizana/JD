using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValidacionTelefonica.Models
{
  [Table("CoincidenciaLNBitacora", Schema = "Negocio")]
  public class CoincidenciaLNBitacora
  {
    [Key]
    public int CoincidenciaLNBitacoraID { get; set; }

    [Column(TypeName = "bigint")]
    public long NumeroTelefonico { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaUltimoCambio { get; set; }

    public int FolioValidacionID { get; set; }

    public int CoincidenciaLNID { get; set; }

    public virtual CoincidenciaLN CoincidenciaLN { get; set; } // Lista Negra (LN)

  }
}