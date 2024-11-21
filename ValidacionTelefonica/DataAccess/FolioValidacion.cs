using System;
using System.Data;
using System.Data.SqlClient;

namespace ValidacionTelefonica.DataAccess
{
  public class FolioValidacion
  {
    #region Miembros
    public readonly Models.ValidacionTelefonicaContexto mContexto;
    #endregion Miembros

    #region Constructor
    public FolioValidacion()
    {
      this.mContexto = new Models.ValidacionTelefonicaContexto();
    }
    #endregion Constructor

    #region Metodos
    public DataTable ConsultarCoincidencias(Entities.BusquedaCoincidencia pParametros)
    {
      try
      {
        DataTable lResultadoDataTable = new DataTable();

        using (SqlConnection lConexion = new SqlConnection(this.mContexto.Database.Connection.ConnectionString))
        {
          SqlCommand lCommand = new SqlCommand();

          lCommand.Connection = lConexion;
          lCommand.CommandText = "[Negocio].[pfolioValdiacionCoincidencia]";
          lCommand.CommandType = CommandType.StoredProcedure;
          lCommand.CommandTimeout = 0;

          var lNumeroCelular = lCommand.CreateParameter();
          lNumeroCelular.Value = pParametros.NumeroTelefono;
          lNumeroCelular.ParameterName = "@pNumeroCelular";

          lCommand.Parameters.Add(lNumeroCelular);

          var lRFC = lCommand.CreateParameter();
          lRFC.Value = pParametros.RFC;
          lRFC.ParameterName = "@pRFC";

          lCommand.Parameters.Add(lRFC);

          var lNombre = lCommand.CreateParameter();
          lNombre.Value = pParametros.Nombre;
          lNombre.ParameterName = "@pNombre";

          lCommand.Parameters.Add(lNombre);

          SqlDataAdapter lAdaptador = new SqlDataAdapter();

          lAdaptador.SelectCommand = lCommand;

          lConexion.Open();
          lAdaptador.Fill(lResultadoDataTable);
          lConexion.Close();
        }
        return lResultadoDataTable;
      }
      catch (Exception lException)
      {
        throw new Exception(lException.Message);
      }
    }
    #endregion Metodos
  }
}