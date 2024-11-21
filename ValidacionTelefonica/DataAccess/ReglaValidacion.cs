using System;
using System.Data;
using System.Data.SqlClient;

namespace ValidacionTelefonica.DataAccess
{
  public class ReglaValidacion
  {
    #region Miembros

    public readonly Models.ValidacionTelefonicaContexto mContexto;
    #endregion Miembros

    #region Constructor
    public ReglaValidacion()
    {
      this.mContexto = new Models.ValidacionTelefonicaContexto();
    }
    #endregion Constructor

    public DataTable ConsultarListadoReglasValidacion(string pCoincidenciaLN, string pPlataforma, string pProyecto, string pCasa, string pOficina, string pRef1, string pRef2, string pEstatusFinal, string pResultadoFinal)
    {
      try
      {
        DataTable lResultadoDataTable = new DataTable();
        
        using (SqlConnection lConexion = new SqlConnection(this.mContexto.Database.Connection.ConnectionString))
        {
          SqlCommand lCommand = new SqlCommand();

          lCommand.Connection = lConexion;
          lCommand.CommandText = "[Negocio].[pReglasNegocioConsultar]";
          lCommand.CommandType = CommandType.StoredProcedure;
          lCommand.CommandTimeout = 0;
          lCommand.Parameters.Add(new SqlParameter("@pCoincidenciaID", pCoincidenciaLN));
          lCommand.Parameters.Add(new SqlParameter("@pPlataformaID", pPlataforma));
          lCommand.Parameters.Add(new SqlParameter("@pProyectoID", pProyecto));
          lCommand.Parameters.Add(new SqlParameter("@pCasaID", pCasa));
          lCommand.Parameters.Add(new SqlParameter("@pOficinaID", pOficina));
          lCommand.Parameters.Add(new SqlParameter("@pReferencia1ID", pRef1));
          lCommand.Parameters.Add(new SqlParameter("@pReferencia2ID", pRef2));
          lCommand.Parameters.Add(new SqlParameter("@pResultadoEstatusID", pEstatusFinal));
          lCommand.Parameters.Add(new SqlParameter("@pResultadoFinalID", pResultadoFinal));

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
  }
}