using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

using Telcel.Portal.MiTelcelR9;
namespace ValidacionTelefonica.DataAcces.Services.Identidad
{
  public class Token
  {
    #region Propiedades
    private string TokenUrl;
    #endregion

    #region Constructores
    public Token()
    {
      this.TokenUrl = Telcel.Portal.MiTelcelR9.ConfiguracionesPortal.ApiTokenDat_Url;
    }

    public Token(string accessTokenUri)
    {
      this.TokenUrl = accessTokenUri;
    }
    #endregion

    #region Metodos
    public Entities.DataAccess.Services.TokenResponse ObtenerToken(string pClientID, string pClientSecret, string pGrantType = "client_credentials")
    {
      var lToken = new Entities.DataAccess.Services.TokenResponse();

      try
      {
        HttpClient lClient = new HttpClient { BaseAddress = new Uri(TokenUrl) };

        var lEncabezado = new Dictionary<string, string>
        {
          {"grant_type", pGrantType},
          {"client_id", pClientID},
          {"client_secret", pClientSecret}
        };

        lClient.DefaultRequestHeaders.Clear();
        lClient.DefaultRequestHeaders.Add("cache-control", "no-cache");

        HttpRequestMessage lRequest = new HttpRequestMessage(HttpMethod.Post, TokenUrl) { Content = new FormUrlEncodedContent(lEncabezado) };
        lRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        // List data response.
        HttpResponseMessage lResponse = lClient.SendAsync(lRequest).Result; // Blocking call! Program will wait here until a response is received or a timeout occurs.
        lToken.IdentidadToken = lResponse.IsSuccessStatusCode ? lResponse.Content.ReadAsAsync<Entities.DataAccess.Services.Token>().Result : null;
        lToken.EsExitoso = lResponse.IsSuccessStatusCode;
        lToken.CodigoEstatus = lResponse.StatusCode;
        lToken.Mensaje = lResponse.ReasonPhrase;

        lClient.Dispose();
      }
      catch (Exception pError)
      {
        lToken.IdentidadToken = null;
        lToken.EsExitoso = false;
        lToken.CodigoEstatus = System.Net.HttpStatusCode.InternalServerError;
        lToken.Mensaje = pError.Message;
      }

      return lToken;
    }
    #endregion Metodos
  }
}