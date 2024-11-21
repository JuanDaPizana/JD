using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValidacionTelefonica.Entities
{
  public class ValidacionTelefonicaConsultar
  {
    public int FolioValidacionID { get; set; }

    public DateTime FechaRegistro { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime Fechafin { get; set; }

    public string FuerzaVenta { get; set; }

    public long FolioUnico { get; set; }

    public string NombrePlataforma { get; set; }


    public string UsuarioRegistro { get; set; }

    public string ResultadoFinalEstatus { get; set; }
    public string ResultadoFinalVT { get; set; }

    public int EstatusID { get; set; }

    public string NombreEstatus { get; set; }


  }
}