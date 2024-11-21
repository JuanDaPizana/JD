using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValidacionTelefonica.Entities.Enumerados
{
  public struct EstatusFolio
  {
    public const int Registrado = 1;
    public const int EnProceso = 2;
    public const int Revisado = 3;
    public const int Corregido = 4;
  }
}