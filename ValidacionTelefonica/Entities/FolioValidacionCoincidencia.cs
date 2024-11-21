using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValidacionTelefonica.Entities
{
  public class FolioValidacionCoincidencia
  {
    public int FolioValidacionID { get; set; }

    public string RFCCliente { get; set; }

    public string NombreCliente { get; set; }

    public long NumeroCasa { get; set; }

    public string ResultadoCasa { get; set; }

    public long NumeroOficina { get; set; }

    public string ResultadoOficina { get; set; }

    public long NumeroReferencia1 { get; set; }

    public string ResultadoRef1 { get; set; }

    public long NumeroReferencia2 { get; set; }

    public string Resultadoref2 { get; set; }

    public string Comentarios { get; set; }

    public string ResultadoFinalVT { get; set; }

    public string ResultadoEstusFinal { get; set; }

  }
}