using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValidacionTelefonica.Models
{
  [Table("ReglaValidacion", Schema = "Negocio")]
  public class ReglaValidacion
  {

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(TypeName = "int")]
    public int ReglaValiddacionID { get; set; }

    [Key]
    [Column(Order = 1)]
    public int CoincidenciaLNID { get; set; }

    [Key]
    [Column(Order = 2)]

    public int PlataformaID { get; set; }

    [Key]
    [Column(Order = 3)]

    public int ProyectoID { get; set; }

    [Key]
    [Column(Order = 4)]

    public int ResultadoVTCasaID { get; set; }

    [Key]
    [Column(Order = 5)]

    public int ResultadoVTOficinaID { get; set; }

    [Key]
    [Column(Order = 6)]

    public int ResultadoVTReferencia1ID { get; set; }

    [Key]
    [Column(Order = 7)]

    public int ResultadoVTReferencia2ID { get; set; }

    [Key]
    [Column(Order = 8)]

    public int ResultadoFinalEstatusID{ get; set; }

    [Key]
    [Column(Order = 9)]

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