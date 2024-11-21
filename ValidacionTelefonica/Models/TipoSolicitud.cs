using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValidacionTelefonica.Models
{
  [Table("TipoSolicitud", Schema = "Catalogo")]
  public class TipoSolicitud
  {
    [Key]
    public int TipoSolicitudID { get; set; }

    [Column(TypeName = "varchar")]
    [StringLength(100)]
    [Required]
    public string Nombre { get; set; }


    [Column(TypeName = "bit")]
    public Boolean EstaActivo { get; set; }

    public virtual ICollection<FolioValidacion> FolioValidacion { get; set; }
  }
} 