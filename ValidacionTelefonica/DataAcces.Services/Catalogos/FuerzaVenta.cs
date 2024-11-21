using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace ValidacionTelefonica.DataAcces.Services.Catalogos
{
  public class FuerzaVenta
  {
    #region Miembros
    public Identidad.Token mToken;
    #endregion Miebros

    #region Constructor
    public FuerzaVenta()
    {
      this.mToken = new Identidad.Token();
    }
    #endregion Constructor

    #region Metodos
    public List<Entities.DataAccess.Services.ResponseConsultaFV> ConsultarDistribuidores()
    {
      string lParametros = string.Empty;

      var lToken = this.mToken.ObtenerToken(ConfigurationManager.AppSettings["ValidacionTelefonica.Token.Api.ClientID"].ToString(),
                                                  ConfigurationManager.AppSettings["ValidacionTelefonica.Token.Api.ClientSecret"].ToString());

      string lCanales = ConfigurationManager.AppSettings["ValidacionTelefonica.CanalesFV"].ToString();

      if (!lToken.EsExitoso)
      {
        throw new Exception("Error al obtener token: " + (int)lToken.CodigoEstatus + "-" + lToken.Mensaje);
      }

      var lUrl = ConfigurationManager.AppSettings["ValidacionTelefonica.ConsultarFV.Api.Url"].ToString() + "?pCanalesID" + lCanales; //En duro en SP el parámetro

      HttpClient lClient = new HttpClient
      {
        BaseAddress = new Uri(lUrl)
      };

      // Add an Accept header for JSON format.
      lClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      lClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", lToken.IdentidadToken.AccessToken);
      lClient.DefaultRequestHeaders.Host = ConfigurationManager.AppSettings["ValidacionTelefonica.Api.Host"].ToString();

      HttpResponseMessage lResponse = lClient.GetAsync(lParametros).Result;

      if (lResponse.IsSuccessStatusCode)
      {
        // Parse the response body.
        var lResultado = lResponse.Content.ReadAsAsync<List<Entities.DataAccess.Services.ResponseConsultaFV>>().Result;
        lClient.Dispose();

        return lResultado;
      }
      else
      {
        //Console.WriteLine("{0} ({1})", (int)lResponse.StatusCode, lResponse.ReasonPhrase);

        throw new Exception("Error: " + lResponse.ReasonPhrase);
      }

    }
    #endregion Metodos

  }
}