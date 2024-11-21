using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ValidacionTelefonica.Bussines
{
  public class ReglaValidacion
  {
    #region Miembros
    public readonly Models.ValidacionTelefonicaContexto mContexto;
    public DataAccess.ReglaValidacion mReglaValidacionDataAccess;
    #endregion Miembros

    #region Constructor
    public ReglaValidacion()
    {
      this.mContexto = new Models.ValidacionTelefonicaContexto();
      this.mReglaValidacionDataAccess = new DataAccess.ReglaValidacion();
    }

    #endregion Constructor

    #region Metodos
    public void Registrar(Entities.ReglaValidacionRegistrar pDatosReglaRegistrar, string pNombreUsuario, string pFechaRegistro, string pCuentaUsuarioXO)
    {
      using (var lDbTransaccion = this.mContexto.Database.BeginTransaction())
      {
        try
        {
          Models.ReglaValidacion lDatosReglaRegistro = this.ObtenerDatosReglaRegistro(pDatosReglaRegistrar, pNombreUsuario, pFechaRegistro, pCuentaUsuarioXO);
          this.mContexto.ReglaValidacion.Add(lDatosReglaRegistro);

          this.mContexto.SaveChanges();
          lDbTransaccion.Commit();
        }
        catch (Exception lExcepcion)
        {
          if (lExcepcion.InnerException.InnerException.HResult == -2146232060)
          {
            lDbTransaccion.Rollback();
            throw new Exception("No se registro correctamente, la regla de validación ya existe. Intente con una nueva combinación.");
          }
          else
          {
            lDbTransaccion.Rollback();
            throw new Exception(lExcepcion.Message);
          }
        }
      }
    }

    public Models.ReglaValidacion ObtenerDatosReglaRegistro(Entities.ReglaValidacionRegistrar pDatosReglaRegistrar, string pNombreUsuario, string pFechaRegistro, string pCuentaUsuarioXO)
    {

      if (pDatosReglaRegistrar.ResultadoVTOficinaID == null)
      {
        pDatosReglaRegistrar.ResultadoVTOficinaID = 7;
      }

      if (pDatosReglaRegistrar.ResultadoVTReferencia2ID == null)
      {
        pDatosReglaRegistrar.ResultadoVTReferencia2ID = 7;
      }

      Models.ReglaValidacion lReglaRegistro = new Models.ReglaValidacion
      {
        CoincidenciaLNID = pDatosReglaRegistrar.CoincidenciaLNID,
        PlataformaID = pDatosReglaRegistrar.PlataformaID,
        ProyectoID = pDatosReglaRegistrar.ProyectoID,
        ResultadoVTCasaID = pDatosReglaRegistrar.ResultadoVTCasaID,

        ResultadoVTOficinaID = (int)pDatosReglaRegistrar.ResultadoVTOficinaID,
        ResultadoVTReferencia1ID = pDatosReglaRegistrar.ResultadoVTReferencia1ID,
        ResultadoVTReferencia2ID = (int)pDatosReglaRegistrar.ResultadoVTReferencia2ID,
        ResultadoFinalEstatusID = pDatosReglaRegistrar.ResultadoFinalEstatusID,
        ResultadoFinalVTID = pDatosReglaRegistrar.ResultadoFinalVTID,
        FechaUltimoCambio = Convert.ToDateTime(pFechaRegistro),
        UsuarioUltimoCambio = pNombreUsuario,
        EstaActivo = true

      };

      return lReglaRegistro;
    }

    public void Editar(Entities.ReglaValidacionEditar pDatosReglaEditar, string pNombreUsuario, string pFechaRegistro)
    {
      using (var lDbTransaccion = this.mContexto.Database.BeginTransaction())
      {
        try
        {
          Models.ReglaValidacion lDatosRegla = this.mContexto.ReglaValidacion.Where(x => x.ReglaValiddacionID == pDatosReglaEditar.ReglaValidacionID).FirstOrDefault();

          this.mContexto.ReglaValidacion.Remove(lDatosRegla);

          if (pDatosReglaEditar.ResultadoVTOficinaID == null)
          {
            pDatosReglaEditar.ResultadoVTOficinaID = 7;
          }

          if (pDatosReglaEditar.ResultadoVTReferencia2ID == null)
          {
            pDatosReglaEditar.ResultadoVTReferencia2ID = 7;
          }

          Models.ReglaValidacion lReglaRegistro = new Models.ReglaValidacion
          {
            CoincidenciaLNID = pDatosReglaEditar.CoincidenciaLNID,
            PlataformaID = pDatosReglaEditar.PlataformaID,
            ProyectoID = pDatosReglaEditar.ProyectoID,
            ResultadoVTCasaID = pDatosReglaEditar.ResultadoVTCasaID,

            ResultadoVTOficinaID = (int)pDatosReglaEditar.ResultadoVTOficinaID,
            ResultadoVTReferencia1ID = pDatosReglaEditar.ResultadoVTReferencia1ID,
            ResultadoVTReferencia2ID = (int)pDatosReglaEditar.ResultadoVTReferencia2ID,
            ResultadoFinalEstatusID = pDatosReglaEditar.ResultadoFinalEstatusID,
            ResultadoFinalVTID = pDatosReglaEditar.ResultadoFinalVTID,
            FechaUltimoCambio = Convert.ToDateTime(pFechaRegistro),
            UsuarioUltimoCambio = pNombreUsuario,
            EstaActivo = pDatosReglaEditar.EstaActivo
          };

          this.mContexto.ReglaValidacion.Add(lReglaRegistro);

          this.mContexto.SaveChanges();
          lDbTransaccion.Commit();
        }
        catch (Exception lExcepcion)
        {
          if (lExcepcion.InnerException.InnerException.HResult == -2146232060)
          {
            lDbTransaccion.Rollback();
            throw new Exception("No se registro correctamente, la regla de validación ya existe. Intente con una nueva combinación.");
          }
          else
          {
            lDbTransaccion.Rollback();
            throw new Exception(lExcepcion.Message);
          }
        }
      }
    }

    public void Eliminar(Entities.ReglaValidacionEliminar pDatosEliminar)
    {
      using (var lDbTransaccion = this.mContexto.Database.BeginTransaction())
      {
        try
        {
          Models.ReglaValidacion lDatosRegla = this.mContexto.ReglaValidacion.Where(x => x.ReglaValiddacionID == pDatosEliminar.ReglaValidacionID).FirstOrDefault();

          this.mContexto.ReglaValidacion.Remove(lDatosRegla);

          this.mContexto.SaveChanges();
          lDbTransaccion.Commit();
        }
        catch (Exception lExcepcion)
        {
          lDbTransaccion.Rollback();
          throw new Exception(lExcepcion.Message);
        }
      }
    }

    public List<Entities.ReglaValidacionConsultar> ConsultarListadoReglasValidacion(string pCoincidenciaLN, string pPlataforma, string pProyecto, string pCasa, string pOficina, string pRef1, string pRef2, string pEstatusFinal, string pResultadoFinal)
    {
      DataTable lDatosReglasValdiacion = this.mReglaValidacionDataAccess.ConsultarListadoReglasValidacion(pCoincidenciaLN, pPlataforma, pProyecto, pCasa, pOficina, pRef1, pRef2, pEstatusFinal, pResultadoFinal);

      List<Entities.ReglaValidacionConsultar> lListadoReglasValidacionEntities = new List<Entities.ReglaValidacionConsultar>();

      foreach (DataRow lDatos in lDatosReglasValdiacion.Rows)
      {
        Entities.ReglaValidacionConsultar lDatosReglaValidacion = new Entities.ReglaValidacionConsultar();

        lDatosReglaValidacion.ReglaValidacionID = Convert.ToInt32(lDatos["ReglaValiddacionID"]);

        lDatosReglaValidacion.CoincidenciaLNID = Convert.ToInt32(lDatos["CoincidenciaLNID"]);
        lDatosReglaValidacion.NombreCoincidencia = lDatos["NombreCoincidencia"].ToString();

        lDatosReglaValidacion.PlataformaID = Convert.ToInt32(lDatos["PlataformaID"]);
        lDatosReglaValidacion.NombrePlataforma = lDatos["NombrePlataforma"].ToString();

        lDatosReglaValidacion.ProyectoID = Convert.ToInt32(lDatos["ProyectoID"]);
        lDatosReglaValidacion.NombreProyecto = lDatos["NombreProyecto"].ToString();

        lDatosReglaValidacion.ResultadoVTCasaID = Convert.ToInt32(lDatos["ResultadoVTCasaID"]);
        lDatosReglaValidacion.NombreResultadoVTCasa = lDatos["NombreResultadoVTC"].ToString();

        lDatosReglaValidacion.ResultadoVTOficinaID = Convert.ToInt32(lDatos["ResultadoVTOficinaID"]);
        lDatosReglaValidacion.NombreResultadoVTOficina = lDatos["NombreResultadoVTO"].ToString();

        lDatosReglaValidacion.ResultadoVTReferencia1ID = Convert.ToInt32(lDatos["ResultadoVTReferencia1ID"]);
        lDatosReglaValidacion.NombreResultadoVTReferencia1 = lDatos["NombreVTR1"].ToString();

        lDatosReglaValidacion.ResultadoVTReferencia2ID = Convert.ToInt32(lDatos["ResultadoVTReferencia1ID"]);
        lDatosReglaValidacion.NombreResultadoVTReferencia2 = lDatos["NombreVTR2"].ToString();

        lDatosReglaValidacion.ResultadoFinalEstatusID = Convert.ToInt32(lDatos["ResultadoFinalEstatusID"]);
        lDatosReglaValidacion.NombreResultadoFinalEstatus = lDatos["NombreRFE"].ToString();

        lDatosReglaValidacion.ResultadoFinalVTID = Convert.ToInt32(lDatos["ResultadoFinalVTID"]);
        lDatosReglaValidacion.NombreResultadoFinalVT = lDatos["NombreRF"].ToString();

        lDatosReglaValidacion.EstaActivo = Convert.ToBoolean(lDatos["EstaActivo"]);

        lListadoReglasValidacionEntities.Add(lDatosReglaValidacion);
      }
      return lListadoReglasValidacionEntities;
    }
    #endregion Metodos

  }
}