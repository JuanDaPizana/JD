using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ValidacionTelefonica.Bussines
{
  public class Correo
  {
    public static string FolioValidacionAsignado(string pFolio, string pFechaRegistro, string pProyecto, string pUrlModulo)
    {
      string file;
      using (StreamReader lStreamReader = new StreamReader(HttpContext.Current.Server.MapPath(@"~/Areas/ValidacionTelefonica/Content/MailTemplates/ValidacionTelefonica.html")))
      {
        file = lStreamReader.ReadToEnd();
      }
      return file.Replace("{FolioValidacionID}", pFolio).Replace("{proyecto}", pProyecto).Replace("{FechaRegistro}", pFechaRegistro).Replace("{urlmodulo}", pUrlModulo).Replace("{urlportal}", Telcel.Portal.MiTelcelR9.ConfiguracionesPortal.UrlPortal).Replace("{year}", DateTime.Now.Year.ToString());
    }
  }
}