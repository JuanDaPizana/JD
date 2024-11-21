using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ValidacionTelefonica.Bussines
{
  public class FolioValidacion
  {
    #region Miembros
    public readonly Models.ValidacionTelefonicaContexto mContexto;
    public DataAccess.Rol mRolDataAcces;
    public DataAccess.FolioValidacion mFolioValidacionDataAccess;
    public DataAcces.Services.Catalogos.FuerzaVenta mFuerzaVenta;
    #endregion Miembros

    #region Constructor
    public FolioValidacion()
    {
      this.mContexto = new Models.ValidacionTelefonicaContexto();
      this.mRolDataAcces = new DataAccess.Rol();
      this.mFuerzaVenta = new DataAcces.Services.Catalogos.FuerzaVenta();
      this.mFolioValidacionDataAccess = new DataAccess.FolioValidacion();
    }

    #endregion Constructor

    #region Metodos
    public void Registrar(Entities.ValidacionTelefonicaRegistrar pDatosRegistroFolioValidacion, string pNombreUsuario, string pFechaRegistro, string pCuentaUsuarioXO)
    {
      using (var lDbTransaccion = this.mContexto.Database.BeginTransaction())
      {
        try
        {
          Models.FolioValidacion lDatosFolioRegistro = this.ObtenerDatosFolioRegistro(pDatosRegistroFolioValidacion, pNombreUsuario, pFechaRegistro, pCuentaUsuarioXO);
          this.mContexto.FolioValidacion.Add(lDatosFolioRegistro);

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

    public void EnviarNotificacion(Entities.ValidacionTelefonicaRegistrar pDatosRegistroFolioValidacion, Models.FolioValidacion lDatosUltimoFolio, string pFechaRegistro, string pUrl, string pCorreoDestino)
    {
      try
      {
        // Envía correo a grupo de asesores
        Telcel.Portal.MiTelcelR9.Correo.Enviar(pCorreoDestino, "Validación Telefónica",
          mensaje: Correo.FolioValidacionAsignado(
            lDatosUltimoFolio.FolioValidacionID.ToString(),
            pFechaRegistro,
            "Validación Telefónica",
            pUrl
       ));

      }
      catch (Exception lExcepcion)
      {
        throw new Exception("Ha ocurrido un error al mandar el correo electrónico. Favor de validar que la tarea si se registró correctamente. //  " + lExcepcion.Message);
      }
    }

    public Models.FolioValidacion ObtenerDatosFolioRegistro(Entities.ValidacionTelefonicaRegistrar pDatosRegistroFolioValidacion, string pNombreUsuario, string pFechaRegistro, string pCuentaUsuarioXO)
    {
      Models.FolioValidacion lRegistroTarea = new Models.FolioValidacion
      {
        FuerzaVenta = pDatosRegistroFolioValidacion.NombreFuerzaVenta,
        ClaseCreditoID = pDatosRegistroFolioValidacion.ClaseCreditoID,
        ClasificacionClienteID = pDatosRegistroFolioValidacion.ClasificacionClienteID,
        FolioUnico = Convert.ToInt64(pDatosRegistroFolioValidacion.FolioUnico),
        FechaRegistro = Convert.ToDateTime(pFechaRegistro),
        FechaUltimoCambio = Convert.ToDateTime(pFechaRegistro),
        UsuarioRegistro = pNombreUsuario,
        UsuarioUltimoCambio = pNombreUsuario,
        EstatusID = Entities.Enumerados.EstatusFolio.Registrado,
        MotivoNoContestaID = Entities.Enumerados.Catalogos.MotivoNoContesta.NoAplica,
        PlataformaID = pDatosRegistroFolioValidacion.PlataformaID,
        ProyectoID = Entities.Enumerados.Catalogos.Proyecto.NoAplica,
        TipoSolicitudID = pDatosRegistroFolioValidacion.TipoSolicitudID,
        CoincidenciaLNID = Entities.Enumerados.Catalogos.CoincidenciaLN.NoAplica,
        ResultadoVTID = Entities.Enumerados.ResultadoVT.SinValidacion, //casa
        ResultadoVTOficinaID = Entities.Enumerados.ResultadoVT.SinValidacion,
        ResultadoVTReferencia1ID = Entities.Enumerados.ResultadoVT.SinValidacion,
        ResultadoVTReferencia2ID = Entities.Enumerados.ResultadoVT.SinValidacion,
        ResultadoFinalVTID = Entities.Enumerados.ResultadoFinalVT.SinValidacion,
        ResultadoFinalEstatusID = Entities.Enumerados.ResultadoFinalVTEstatus.SinValidacion

      };

      return lRegistroTarea;
    }

    public List<Entities.FuerzaVenta> ConsultarFuerzaVenta()
    {
      List<Entities.DataAccess.Services.ResponseConsultaFV> lListaFuerzaVentaServicio = this.mFuerzaVenta.ConsultarDistribuidores();
      List<Entities.FuerzaVenta> lListaFuerzaDeVentaEntities = new List<Entities.FuerzaVenta>();

      foreach (var lDistribuidor in lListaFuerzaVentaServicio)
      {
        Entities.FuerzaVenta lDistribuidorEntities = new Entities.FuerzaVenta();

        lDistribuidorEntities.FuerzaVentaID = lDistribuidor.PrepagoSiapID;
        lDistribuidorEntities.Nombre = lDistribuidor.Clave;

        lListaFuerzaDeVentaEntities.Add(lDistribuidorEntities);
      }

     

      return lListaFuerzaDeVentaEntities;
    }

    public List<Entities.FolioValidacionCoincidencia> ConsultarCoincidencias(Entities.BusquedaCoincidencia pParametros)
    {
      DataTable lDatos = this.mFolioValidacionDataAccess.ConsultarCoincidencias(pParametros);

      if (lDatos.Rows.Count <= 0)
      {
        throw new Exception("No se encontraron coincidencias con respecto a la información ingresada.");
      }

      List<Entities.FolioValidacionCoincidencia> lListaCoincidencias = new List<Entities.FolioValidacionCoincidencia>();

      foreach (DataRow lDato in lDatos.Rows)
      {
        Entities.FolioValidacionCoincidencia lCoincidencia = new Entities.FolioValidacionCoincidencia();

        lCoincidencia.FolioValidacionID = Convert.ToInt32(lDato["FolioValidacionID"]);
        lCoincidencia.RFCCliente = lDato["RFCCliente"].ToString();
        lCoincidencia.NombreCliente = lDato["NombreCliente"].ToString();
        lCoincidencia.NumeroCasa = Convert.ToInt64(lDato["NumeroCasa"]);
        lCoincidencia.ResultadoCasa = lDato["ResultadoCasa"].ToString();
        lCoincidencia.NumeroOficina = Convert.ToInt64(lDato["NumeroOficina"]);
        lCoincidencia.ResultadoOficina = lDato["ResultadoOficina"].ToString();
        lCoincidencia.NumeroReferencia1 = Convert.ToInt64(lDato["NumeroReferencia1"]);
        lCoincidencia.ResultadoRef1 = lDato["ResultadoRef1"].ToString();
        lCoincidencia.NumeroReferencia2 = Convert.ToInt64(lDato["NumeroReferencia2"]);
        lCoincidencia.Resultadoref2 = lDato["Resultadoref2"].ToString();
        lCoincidencia.Comentarios = lDato["Comentarios"].ToString();
        lCoincidencia.ResultadoFinalVT = lDato["ResultadoFinalVT"].ToString();
        lCoincidencia.ResultadoEstusFinal = lDato["ResultadoEstusFinal"].ToString();

        lListaCoincidencias.Add(lCoincidencia);
      }

      return lListaCoincidencias;
    }

    public void Validar(Entities.ValidacionTelefonicaValidar pDatosValidacionFront, string pNombreUsuario, string pFechaRegistro)
    {
      using (var lDbTransaccion = this.mContexto.Database.BeginTransaction())
      {
        try
        {
          Models.FolioValidacion lDatosFolioBD = this.mContexto.FolioValidacion.Where(x => x.FolioValidacionID == pDatosValidacionFront.FolioValidacionID).FirstOrDefault();

          if (lDatosFolioBD == null)
          {
            throw new Exception("Hay problema al consultar la información en base de datos y hacer la actualización, favor de intentar de nuevo.");
          }

          lDatosFolioBD = this.MapearInformacion(lDatosFolioBD, pDatosValidacionFront, pNombreUsuario, pFechaRegistro);

          this.mContexto.Entry(lDatosFolioBD).State = EntityState.Modified;

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

    public void Editar(Entities.ValidacionTelefonicaEditar pDatosValidacionFront, string pNombreUsuario, string pFechaRegistro)
    {
      using (var lDbTransaccion = this.mContexto.Database.BeginTransaction())
      {
        try
        {
          Models.FolioValidacion lDatosFolioBD = this.mContexto.FolioValidacion.Where(x => x.FolioValidacionID == pDatosValidacionFront.FolioValidacionID).FirstOrDefault();

          if (lDatosFolioBD == null)
          {
            throw new Exception("Hay problema al consultar la información en base de datos y hacer la actualización, favor de intentar de nuevo.");
          }

          if (lDatosFolioBD.ClaseCreditoID != pDatosValidacionFront.ClaseCreditoID)
          {
            Models.BitacoraMovimiento lDatosMovimiento = new Models.BitacoraMovimiento();

            lDatosMovimiento.FolioValidacion = pDatosValidacionFront.FolioValidacionID;
            lDatosMovimiento.FechaRegistro = Convert.ToDateTime(pFechaRegistro);
            lDatosMovimiento.UsuarioUltimoCambio = pNombreUsuario;
            lDatosMovimiento.Movimiento = "Editar";
            lDatosMovimiento.Campo = "ClaseCredito";
            lDatosMovimiento.Valor = lDatosFolioBD.ClaseCredito.Nombre;
            this.mContexto.BitacoraMovimiento.Add(lDatosMovimiento);
          }

          if (lDatosFolioBD.ClasificacionClienteID != pDatosValidacionFront.ClasificacionClienteID)
          {
            Models.BitacoraMovimiento lDatosMovimiento = new Models.BitacoraMovimiento();

            lDatosMovimiento.FolioValidacion = pDatosValidacionFront.FolioValidacionID;
            lDatosMovimiento.FechaRegistro = Convert.ToDateTime(pFechaRegistro);
            lDatosMovimiento.UsuarioUltimoCambio = pNombreUsuario;
            lDatosMovimiento.Movimiento = "Editar";
            lDatosMovimiento.Campo = "ClasificacionCliente";
            lDatosMovimiento.Valor = lDatosFolioBD.ClasificacionCliente.Nombre;
            this.mContexto.BitacoraMovimiento.Add(lDatosMovimiento);
          }

          if (lDatosFolioBD.TipoSolicitudID != pDatosValidacionFront.TipoSolicitudID)
          {
            Models.BitacoraMovimiento lDatosMovimiento = new Models.BitacoraMovimiento();

            lDatosMovimiento.FolioValidacion = pDatosValidacionFront.FolioValidacionID;
            lDatosMovimiento.FechaRegistro = Convert.ToDateTime(pFechaRegistro);
            lDatosMovimiento.UsuarioUltimoCambio = pNombreUsuario;
            lDatosMovimiento.Movimiento = "Editar";
            lDatosMovimiento.Campo = "TipoSolicitud";
            lDatosMovimiento.Valor = lDatosFolioBD.TipoSolicitud.Nombre;
            this.mContexto.BitacoraMovimiento.Add(lDatosMovimiento);
          }

          if (lDatosFolioBD.FuerzaVenta != pDatosValidacionFront.NombreFuerzaVenta)
          {
            Models.BitacoraMovimiento lDatosMovimiento = new Models.BitacoraMovimiento();

            lDatosMovimiento.FolioValidacion = pDatosValidacionFront.FolioValidacionID;
            lDatosMovimiento.FechaRegistro = Convert.ToDateTime(pFechaRegistro);
            lDatosMovimiento.UsuarioUltimoCambio = pNombreUsuario;
            lDatosMovimiento.Movimiento = "Editar";
            lDatosMovimiento.Campo = "FuerzaVenta";
            lDatosMovimiento.Valor = lDatosFolioBD.FuerzaVenta;
            this.mContexto.BitacoraMovimiento.Add(lDatosMovimiento);
          }

          if (lDatosFolioBD.FolioUnico != Convert.ToInt64(pDatosValidacionFront.FolioUnico))
          {
            Models.BitacoraMovimiento lDatosMovimiento = new Models.BitacoraMovimiento();

            lDatosMovimiento.FolioValidacion = pDatosValidacionFront.FolioValidacionID;
            lDatosMovimiento.FechaRegistro = Convert.ToDateTime(pFechaRegistro);
            lDatosMovimiento.UsuarioUltimoCambio = pNombreUsuario;
            lDatosMovimiento.Movimiento = "Editar";
            lDatosMovimiento.Campo = "FolioUnico";
            lDatosMovimiento.Valor = pDatosValidacionFront.FolioUnico;
            this.mContexto.BitacoraMovimiento.Add(lDatosMovimiento);
          }

          if (lDatosFolioBD.NombreCliente != pDatosValidacionFront.NombreCliente)
          {
            Models.BitacoraMovimiento lDatosMovimiento = new Models.BitacoraMovimiento();

            lDatosMovimiento.FolioValidacion = pDatosValidacionFront.FolioValidacionID;
            lDatosMovimiento.FechaRegistro = Convert.ToDateTime(pFechaRegistro);
            lDatosMovimiento.UsuarioUltimoCambio = pNombreUsuario;
            lDatosMovimiento.Movimiento = "Editar";
            lDatosMovimiento.Campo = "NombreCliente";
            lDatosMovimiento.Valor = lDatosFolioBD.NombreCliente;
            this.mContexto.BitacoraMovimiento.Add(lDatosMovimiento);
          }

          if (lDatosFolioBD.RFCCliente != pDatosValidacionFront.RFCCliente)
          {
            Models.BitacoraMovimiento lDatosMovimiento = new Models.BitacoraMovimiento();

            lDatosMovimiento.FolioValidacion = pDatosValidacionFront.FolioValidacionID;
            lDatosMovimiento.FechaRegistro = Convert.ToDateTime(pFechaRegistro);
            lDatosMovimiento.UsuarioUltimoCambio = pNombreUsuario;
            lDatosMovimiento.Movimiento = "Editar";
            lDatosMovimiento.Campo = "RFCCliente";
            lDatosMovimiento.Valor = lDatosFolioBD.RFCCliente;
            this.mContexto.BitacoraMovimiento.Add(lDatosMovimiento);
          }

          lDatosFolioBD.EstatusID = Entities.Enumerados.EstatusFolio.Corregido;
          lDatosFolioBD.FuerzaVenta = pDatosValidacionFront.NombreFuerzaVenta;
          lDatosFolioBD.FolioUnico = Convert.ToInt64(pDatosValidacionFront.FolioUnico);
          lDatosFolioBD.NombreCliente = pDatosValidacionFront.NombreCliente;
          lDatosFolioBD.RFCCliente = pDatosValidacionFront.RFCCliente;
          lDatosFolioBD.TipoSolicitudID = pDatosValidacionFront.TipoSolicitudID;
          lDatosFolioBD.ClaseCreditoID = pDatosValidacionFront.ClaseCreditoID;
          lDatosFolioBD.ClasificacionClienteID = pDatosValidacionFront.ClasificacionClienteID;
          lDatosFolioBD.UsuarioUltimoCambio = pNombreUsuario;
          lDatosFolioBD.FechaUltimoCambio = Convert.ToDateTime(pFechaRegistro);

          this.mContexto.Entry(lDatosFolioBD).State = EntityState.Modified;

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


    public Models.FolioValidacion MapearInformacion(Models.FolioValidacion pDatosFolioBD, Entities.ValidacionTelefonicaValidar pDatosValidacionFront, string pNombreUsuario, string pFechaRegistroUltimoCambio)
    {
      pDatosFolioBD.TipoSolicitudID = pDatosValidacionFront.TipoSolicitudID;
      pDatosFolioBD.PlataformaID = pDatosValidacionFront.PlataformaID;
      pDatosFolioBD.ProyectoID = pDatosValidacionFront.ProyectoID;
      pDatosFolioBD.NombreCliente = pDatosValidacionFront.NombreCliente;
      pDatosFolioBD.RFCCliente = pDatosValidacionFront.RFCCliente;
      pDatosFolioBD.NumeroCasa = Convert.ToInt64(pDatosValidacionFront.NumeroCasa);
      pDatosFolioBD.ResultadoVTID = pDatosValidacionFront.ResultadoVTCasaID;
      pDatosFolioBD.NumeroOficina = Convert.ToInt64(pDatosValidacionFront.NumeroOficina);
      pDatosFolioBD.Extension = Convert.ToInt32(String.IsNullOrEmpty(pDatosValidacionFront.Extension) ? Convert.ToInt32("0000") : Convert.ToInt32(pDatosValidacionFront.Extension));
      pDatosFolioBD.ResultadoVTOficinaID = Convert.ToInt32(pDatosValidacionFront.ResultadoVTOficinaID);
      pDatosFolioBD.NombreReferencia1 = pDatosValidacionFront.NombreReferencia1;
      pDatosFolioBD.NumeroReferencia1 = Convert.ToInt64(pDatosValidacionFront.TelefonoReferencia1);
      pDatosFolioBD.ResultadoVTReferencia1ID = pDatosValidacionFront.ResultadoVTReferencia1ID;
      pDatosFolioBD.NombreReferencia2 = pDatosValidacionFront.NombreReferencia2;
      pDatosFolioBD.NumeroReferencia2 = Convert.ToInt64(pDatosValidacionFront.TelefonoReferencia2);
      pDatosFolioBD.ResultadoVTReferencia2ID = pDatosValidacionFront.ResultadoVTReferencia2ID;
      pDatosFolioBD.Comentarios = pDatosValidacionFront.Comentarios;
      pDatosFolioBD.CoincidenciaLNID = pDatosValidacionFront.CoincidenciaLNID;
      pDatosFolioBD.HoraFinValidacionTelefonica = Convert.ToDateTime(pFechaRegistroUltimoCambio);
      pDatosFolioBD.UsuarioValidaFin = pNombreUsuario;
      pDatosFolioBD.FechaUltimoCambio = Convert.ToDateTime(pFechaRegistroUltimoCambio);
      pDatosFolioBD.ResultadoFinalEstatusID = pDatosValidacionFront.ResultadoFinalEstatusID;
      pDatosFolioBD.ResultadoFinalVTID = pDatosValidacionFront.ResultadoFinalVTID;
      pDatosFolioBD.EstatusID = Entities.Enumerados.EstatusFolio.Revisado;

      return pDatosFolioBD;
    }

    public void Iniciar(Entities.ValidacionTelefonicaValidar pDatosFolioValidacion, string pNombreUsuario, string pFechaRegistro, string pCuentaUsuarioXO)
    {
      using (var lDbTransaccion = this.mContexto.Database.BeginTransaction())
      {
        try
        {
          Models.FolioValidacion lDatosBD = this.mContexto.FolioValidacion.Where(x => x.FolioValidacionID == pDatosFolioValidacion.FolioValidacionID).FirstOrDefault();

          if (lDatosBD == null)
          {
            throw new Exception("Hay problema al consultar la información en base de datos y hacer la actualización, favor de intentar de nuevo.");
          }
          lDatosBD.FechaUltimoCambio = Convert.ToDateTime(pFechaRegistro);
          lDatosBD.UsuarioValidaInicio = pNombreUsuario;
          lDatosBD.EstatusID = Entities.Enumerados.EstatusFolio.EnProceso;
          lDatosBD.HoraInicioValidacionTelefonica = Convert.ToDateTime(pFechaRegistro);

          this.mContexto.Entry(lDatosBD).State = EntityState.Modified;

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

    public void Eliminar(Entities.ValidacionTelefonicaEliminar pDatosEliminar, string pNombreUsuario, string pFechaRegistro)
    {
      using (var lDbTransaccion = this.mContexto.Database.BeginTransaction())
      {
        try
        {
          Models.FolioValidacion lDatosEliminar = this.mContexto.FolioValidacion.Where(x => x.FolioValidacionID == pDatosEliminar.ValidacionTelefonicaID).FirstOrDefault();

          Models.BitacoraMovimiento lDatosMovimiento = new Models.BitacoraMovimiento();

          lDatosMovimiento.FolioValidacion = lDatosEliminar.FolioValidacionID;
          lDatosMovimiento.FechaRegistro = Convert.ToDateTime(pFechaRegistro);
          lDatosMovimiento.UsuarioUltimoCambio = pNombreUsuario;
          lDatosMovimiento.Movimiento = "Eliminar";
          lDatosMovimiento.Campo = "Folio Unico";
          lDatosMovimiento.Valor = lDatosEliminar.FolioUnico.ToString();
          this.mContexto.BitacoraMovimiento.Add(lDatosMovimiento);

          this.mContexto.FolioValidacion.Remove(lDatosEliminar);


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
    #endregion Metodos

  }
}