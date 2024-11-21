using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValidacionTelefonica.Models
{
  [Table("ResultadoVTReferencia2", Schema = "Negocio")]
  public class ResultadoVTReferencia2
  {
    [Key]
    public int ResultadoVTReferencia2ID { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(300)]
    [Required]
    public string Nombre { get; set; }

    [Column(TypeName = "bit")]
    public Boolean EstaActivo { get; set; }

    public virtual ICollection<FolioValidacion> FolioValidacion { get; set; }
  }
}