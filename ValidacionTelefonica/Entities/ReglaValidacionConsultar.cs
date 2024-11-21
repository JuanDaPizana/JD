using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValidacionTelefonica.Entities
{
  public class ReglaValidacionConsultar
  {
    public int ReglaValidacionID { get; set; }
    public int CoincidenciaLNID { get; set; }
    public string NombreCoincidencia { get; set; }

    public int PlataformaID { get; set; }
    public string NombrePlataforma { get; set; }
    public int ProyectoID { get; set; }
    public string NombreProyecto { get; set; }

    public int ResultadoVTCasaID { get; set; }
    public string NombreResultadoVTCasa { get; set; }


    public int ResultadoVTOficinaID { get; set; }
    public string NombreResultadoVTOficina { get; set; }


    public int ResultadoVTReferencia1ID { get; set; }
    public string NombreResultadoVTReferencia1 { get; set; }

    public int ResultadoVTReferencia2ID { get; set; }
    public string NombreResultadoVTReferencia2 { get; set; }


    public int ResultadoFinalEstatusID { get; set; }
    public string NombreResultadoFinalEstatus { get; set; }


    public int ResultadoFinalVTID { get; set; }
    public string NombreResultadoFinalVT { get; set; }


    public string Concatenado { get; set; }

    public DateTime FechaUltimoCambio { get; set; }

    public string UsuarioUltimoCambio { get; set; }

    public Boolean EstaActivo { get; set; }
  }
}