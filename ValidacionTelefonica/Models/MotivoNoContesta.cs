using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValidacionTelefonica.Models
{

  [Table("MotivoNoContesta", Schema = "Catalogo")]
  public class MotivoNoContesta
  {
    [Key]
    public int MotivoNoContestaID { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(200)]
    [Required]
    public string Nombre { get; set; }

    [Column(TypeName = "bit")]
    public Boolean EstaActivo { get; set; }

    public virtual ICollection<FolioValidacion> FolioValidacion { get; set; }
  }
}