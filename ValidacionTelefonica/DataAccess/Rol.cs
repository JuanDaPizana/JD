using System.Collections.Generic;
using System.Configuration;
using System;
using System.Data.SqlClient;
using System.Data;

namespace ValidacionTelefonica.DataAccess
{
  public class Rol
  {
    #region Miembros
    #endregion Miembros

    #region Constructor
    public Rol()
    {

    }

    #endregion Constructor

    #region Metodos
    public DataTable ConsultarRol(string pCuentaUsuarioXO)
    {
      try
      {
        DataTable lResultadoDataTable = new DataTable();

        using (SqlConnection lConexion = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductividadAyP"].ConnectionString))
        {
          SqlCommand lCommand = new SqlCommand();

          lCommand.Connection = lConexion;
          lCommand.CommandText = "[Sistema].[pRolPorUsuarioUniversalConsultar]";
          lCommand.CommandType = CommandType.StoredProcedure;
          lCommand.CommandTimeout = 0;

          var lUsuarioUniversal = lCommand.CreateParameter();
          lUsuarioUniversal.Value = pCuentaUsuarioXO;
          lUsuarioUniversal.ParameterName = "@pUsuarioUniversal";

          lCommand.Parameters.Add(lUsuarioUniversal);

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

    public DataTable ConsultarDatosAsesorValidador()
    {
      try
      {
        DataTable lResultadoDataTable = new DataTable();

        using (SqlConnection lConexion = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductividadAyP"].ConnectionString))
        {
          SqlCommand lCommand = new SqlCommand();

          lCommand.Connection = lConexion;
          lCommand.CommandText = "[Sistema].[pUsuarioValidadorConsultar]";
          lCommand.CommandType = CommandType.StoredProcedure;
          lCommand.CommandTimeout = 0;

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