using Newtonsoft.Json;


namespace ValidacionTelefonica.Entities.DataAccess.Services
{
  public class Token
  {
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("token_type")]
    public string TokenType { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }
  }

  public class TokenResponse
  {
    public bool EsExitoso { get; set; }
    public System.Net.HttpStatusCode CodigoEstatus { get; set; }
    public string Mensaje { get; set; }
    public Token IdentidadToken { get; set; }

    public TokenResponse()
    {
      IdentidadToken = new Token();
    }
  }
}