using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValidacionTelefonica.Models
{
  [Table("ResultadoVTOficina", Schema = "Negocio")]
  public class ResultadoVTOficina
  {
    [Key]
    public int ResultadoVTOficinaID { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(300)]
    [Required]
    public string Nombre { get; set; }

    [Column(TypeName = "bit")]
    public Boolean EstaActivo { get; set; }

    public virtual ICollection<FolioValidacion> FolioValidacion { get; set; }
  }
}