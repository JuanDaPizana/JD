using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// Referencia Microsoft.AspNet.Identity.EntityFramework de Nugget Package v2.2.2
using Microsoft.AspNet.Identity;
// Referencia del Portal o proyecto principal
using Telcel.Portal.MiTelcelR9;

namespace ValidacionTelefonica.Controllers
{
  [Seguridad] // Seguridad del portal. Descomentar para usar la seguridad
  [ControlErrores] // Control de errores, envio a pantalla de error automatica, registro de errores.
  public class ValidacionTelefonicaController : Controller
  {

    #region Miembros
    public readonly Models.ValidacionTelefonicaContexto mContexto;
    public Bussines.FolioValidacion mFolioValidacionNegocio;
    public Bussines.Rol mRolNegocio;
    #endregion Miembros

    #region Constructor
    public ValidacionTelefonicaController()
    {
      this.mContexto = new Models.ValidacionTelefonicaContexto();
      this.mFolioValidacionNegocio = new Bussines.FolioValidacion();
      this.mRolNegocio = new Bussines.Rol();
    }

    #endregion Constructor

    #region Metodos

    // GET: ValidacionTelefonica
    public ActionResult Index()
    {
      try
      {
        int lPerfilID = this.ConsultarPerfil();

        if (lPerfilID <= 0)
        {
          Notificacion.Show(this, "No se encontró el Rol Asignado en la administración de Roles. Favor de verificarlo con su supervisor.", NotificationType.Warning);
          return RedirectToAction("Index", "Home", new { area = "" });
        }



        List<Models.FolioValidacion> lListaDatosBD = new List<Models.FolioValidacion>();

        lListaDatosBD = this.mContexto.FolioValidacion.OrderByDescending(x => x.FechaRegistro).ThenBy(x => x.EstatusID).Take(50).ToList();

        List<Entities.ValidacionTelefonicaConsultar> lListaDatosEntities = new List<Entities.ValidacionTelefonicaConsultar>();

        foreach (Models.FolioValidacion lFolioValidacion in lListaDatosBD)
        {
          Entities.ValidacionTelefonicaConsultar lDatosEntities = new Entities.ValidacionTelefonicaConsultar();

          lDatosEntities.FolioValidacionID = lFolioValidacion.FolioValidacionID;
          lDatosEntities.FechaRegistro = lFolioValidacion.FechaRegistro;
          lDatosEntities.FuerzaVenta = lFolioValidacion.FuerzaVenta;
          lDatosEntities.FolioUnico = lFolioValidacion.FolioUnico;
          lDatosEntities.NombrePlataforma = lFolioValidacion.Plataforma.Nombre;
          lDatosEntities.EstatusID = lFolioValidacion.EstatusID;
          lDatosEntities.NombreEstatus = lFolioValidacion.Estatus.Nombre;

          if (lFolioValidacion.EstatusID == Entities.Enumerados.EstatusFolio.Registrado || lFolioValidacion.EstatusID == Entities.Enumerados.EstatusFolio.EnProceso)
          {
            lDatosEntities.ResultadoFinalEstatus = lFolioValidacion.Estatus.Nombre;
            lDatosEntities.ResultadoFinalVT = lFolioValidacion.Estatus.Nombre;
          }
          else
          {
            lDatosEntities.ResultadoFinalEstatus = lFolioValidacion.ResultadoFinalEstatus.Nombre;
            lDatosEntities.ResultadoFinalVT = lFolioValidacion.ResultadoFinalVT.Nombre;
          }

          lListaDatosEntities.Add(lDatosEntities);
        }

       

        return View(lListaDatosEntities);
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
        return RedirectToAction("Index");
      }
    }

    public ActionResult ConsultarPorFiltros(string pFechaInicio, string pFechaFin, string pFolio)
    {
      try
      {
        this.ConsultarPerfil();

        List<Models.FolioValidacion> lListaDatosBD = new List<Models.FolioValidacion>();

        DateTime lFechaInicio = new DateTime();
        DateTime lFechaFin = new DateTime();

        if (string.IsNullOrEmpty(pFolio))
        {
          if (!string.IsNullOrEmpty(pFechaInicio))
          {
            if (string.IsNullOrEmpty(pFechaFin))
            {
              throw new Exception("El campo Fecha Final es requerido para realizar la busqueda.");
            }

            lFechaInicio = Convert.ToDateTime(pFechaInicio);
          }
          if (!string.IsNullOrEmpty(pFechaFin))
          {
            if (string.IsNullOrEmpty(pFechaInicio))
            {
              throw new Exception("El campo Fecha Inicial es requerido para realizar la busqueda.");
            }
            lFechaFin = Convert.ToDateTime(pFechaFin).AddHours(23);
          }

          if (string.IsNullOrEmpty(pFechaInicio) && string.IsNullOrEmpty(pFechaFin) && string.IsNullOrEmpty(pFolio))
          {
            throw new Exception("Favor de agregar un rango de fechas para realizar la busqueda o ingresar un folio en especifico");
          }

          lListaDatosBD = this.mContexto.FolioValidacion.Where(x => x.FechaRegistro >= lFechaInicio && x.FechaRegistro <= lFechaFin).ToList();
        }
        else
        {
          int lFolio = Convert.ToInt32(pFolio);

          lListaDatosBD = this.mContexto.FolioValidacion.Where(x => x.FolioValidacionID == lFolio).ToList();
        }

        List<Entities.ValidacionTelefonicaConsultar> lListaDatosEntities = new List<Entities.ValidacionTelefonicaConsultar>();

        foreach (Models.FolioValidacion lFolioValidacion in lListaDatosBD)
        {
          Entities.ValidacionTelefonicaConsultar lDatosEntities = new Entities.ValidacionTelefonicaConsultar();

          lDatosEntities.FolioValidacionID = lFolioValidacion.FolioValidacionID;
          lDatosEntities.FechaRegistro = lFolioValidacion.FechaRegistro;
          lDatosEntities.FuerzaVenta = lFolioValidacion.FuerzaVenta;
          lDatosEntities.FolioUnico = lFolioValidacion.FolioUnico;
          lDatosEntities.NombrePlataforma = lFolioValidacion.Plataforma.Nombre;
          lDatosEntities.EstatusID = lFolioValidacion.EstatusID;
          lDatosEntities.NombreEstatus = lFolioValidacion.Estatus.Nombre;

          if (lFolioValidacion.EstatusID == Entities.Enumerados.EstatusFolio.Registrado || lFolioValidacion.EstatusID == Entities.Enumerados.EstatusFolio.EnProceso)
          {
            lDatosEntities.ResultadoFinalEstatus = lFolioValidacion.Estatus.Nombre;
            lDatosEntities.ResultadoFinalVT = lFolioValidacion.Estatus.Nombre;
          }
          else
          {
            lDatosEntities.ResultadoFinalEstatus = lFolioValidacion.ResultadoFinalEstatus.Nombre;
            lDatosEntities.ResultadoFinalVT = lFolioValidacion.ResultadoFinalVT.Nombre;
          }

          lListaDatosEntities.Add(lDatosEntities);
        }

        return View("Index", lListaDatosEntities);
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
        return RedirectToAction("Index");
      }
    }

    // GET: 

    public ActionResult Registrar()
    {
      try
      {
        List<Entities.FuerzaVenta> lListaFuerzaDeVenta = this.mFolioValidacionNegocio.ConsultarFuerzaVenta();
        ViewBag.FuerzaVenta = new SelectList(lListaFuerzaDeVenta, "FuerzaVentaID", "Nombre");
        ViewBag.ClaseCredito = new SelectList(mContexto.ClaseCredito.Where(x => x.EstaActivo == true), "ClaseCreditoID", "Nombre");
        ViewBag.ClasificacionCliente = new SelectList(mContexto.ClasificacionCliente.Where(x => x.EstaActivo == true), "ClasificacionClienteID", "Nombre");
        ViewBag.TipoSolicitud = new SelectList(mContexto.TipoSolicitud.Where(x => x.EstaActivo == true), "TipoSolicitudID", "Nombre");
        ViewBag.Plataforma = new SelectList(mContexto.Plataforma.Where(x => x.EstaActivo == true), "PlataformaID", "Nombre");


        return View();
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
        return RedirectToAction("Index");
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Registrar(Entities.ValidacionTelefonicaRegistrar pDatosRegistroFolioValidacion)
    {
      try
      {
        this.ConsultarPerfil();

        string lNombreUsuario = User.Identity.NombreCompleto();
        string lFechaRegistro = DateTime.Now.ToString();
        string lCuentaUsuarioXO = User.Identity.UsuarioUniversal();
        string lUrlCorreo = ConfiguracionesPortal.UrlPortal + Url.Action("Index", "ValidacionTelefonica");
        string lCorreoEYAP = ConfigurationManager.AppSettings["ValidacionTelefonica.CorreoEYAP"].ToString();

        if (ModelState.IsValid)
        {
          this.mFolioValidacionNegocio.Registrar(pDatosRegistroFolioValidacion, lNombreUsuario, lFechaRegistro, lCuentaUsuarioXO);

          Models.FolioValidacion lUltimoFolio = this.mContexto.FolioValidacion.OrderByDescending(x => x.FechaRegistro).First();
          this.mFolioValidacionNegocio.EnviarNotificacion(pDatosRegistroFolioValidacion, lUltimoFolio, lFechaRegistro, lUrlCorreo, lCorreoEYAP);

          Notificacion.Show(this, "El folio se registró correctamente.", NotificationType.Success);

          return RedirectToAction("Index");
        }
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
      }

      List<Entities.FuerzaVenta> lListaFuerzaDeVenta = this.mFolioValidacionNegocio.ConsultarFuerzaVenta();
      ViewBag.FuerzaVenta = new SelectList(lListaFuerzaDeVenta, "FuerzaVentaID", "Nombre");
      ViewBag.ClaseCredito = new SelectList(mContexto.ClaseCredito.Where(x => x.EstaActivo == true), "ClaseCreditoID", "Nombre");
      ViewBag.ClasificacionCliente = new SelectList(mContexto.ClasificacionCliente.Where(x => x.EstaActivo == true), "ClasificacionClienteID", "Nombre");
      ViewBag.TipoSolicitud = new SelectList(mContexto.TipoSolicitud.Where(x => x.EstaActivo == true), "TipoSolicitudID", "Nombre");
      ViewBag.Plataforma = new SelectList(mContexto.Plataforma.Where(x => x.EstaActivo == true), "PlataformaID", "Nombre");

      return View(pDatosRegistroFolioValidacion);
    }

    //GET - Al cargar la página
    public ActionResult Validar(int pFolioValidacionID)
    {
      try
      {
        this.ConsultarPerfil();

        Models.FolioValidacion lDatosFolioBD = this.mContexto.FolioValidacion.Where(x => x.FolioValidacionID == pFolioValidacionID).FirstOrDefault();

        Entities.ValidacionTelefonicaValidar lDatosValidacionEntities = new Entities.ValidacionTelefonicaValidar();

        lDatosValidacionEntities.FolioValidacionID = lDatosFolioBD.FolioValidacionID;
        lDatosValidacionEntities.NombreEstatus = lDatosFolioBD.Estatus.Nombre;
        lDatosValidacionEntities.EstatusID = lDatosFolioBD.EstatusID;
        lDatosValidacionEntities.FechaRegistro = lDatosFolioBD.FechaRegistro.ToString();
        lDatosValidacionEntities.HoraInicioValidacion = lDatosFolioBD.HoraInicioValidacionTelefonica.ToString();

        lDatosValidacionEntities.FuerzaVenta = lDatosFolioBD.FuerzaVenta;
        lDatosValidacionEntities.FolioUnico = lDatosFolioBD.FolioUnico.ToString();
        lDatosValidacionEntities.ClaseCreditoID = lDatosFolioBD.ClaseCreditoID;
        lDatosValidacionEntities.ClasificacionClienteID = lDatosFolioBD.ClasificacionClienteID;
        lDatosValidacionEntities.TipoSolicitudID = lDatosFolioBD.TipoSolicitudID;
        lDatosValidacionEntities.PlataformaID = lDatosFolioBD.PlataformaID;

        if (lDatosFolioBD.EstatusID == Entities.Enumerados.EstatusFolio.Revisado || lDatosFolioBD.EstatusID == Entities.Enumerados.EstatusFolio.Corregido)
        {
          ViewBag.ResultadoFinalEstatus = new SelectList(mContexto.ResultadoFinalEstatus.Where(x => x.EstaActivo == true), "ResultadoFinalEstatusID", "Nombre", lDatosFolioBD.ResultadoFinalEstatusID);
          ViewBag.ResultadoFinalVT = new SelectList(mContexto.ResultadoFinalVT.Where(x => x.EstaActivo == true), "ResultadoFinalVTID", "Nombre", lDatosFolioBD.ResultadoFinalVTID);
        }
        else
        {
          ViewBag.ResultadoFinalEstatus = new SelectList(mContexto.ResultadoFinalEstatus.Where(x => x.EstaActivo == true), "ResultadoFinalEstatusID", "Nombre");
          ViewBag.ResultadoFinalVT = new SelectList(mContexto.ResultadoFinalVT.Where(x => x.EstaActivo == true), "ResultadoFinalVTID", "Nombre");
        }

        ViewBag.ClaseCredito = new SelectList(mContexto.ClaseCredito.Where(x => x.EstaActivo == true), "ClaseCreditoID", "Nombre", lDatosFolioBD.ClaseCreditoID);
        ViewBag.ClasificacionCliente = new SelectList(mContexto.ClasificacionCliente.Where(x => x.EstaActivo == true), "ClasificacionClienteID", "Nombre", lDatosFolioBD.ClasificacionClienteID);

        ViewBag.TipoSolicitud = new SelectList(mContexto.TipoSolicitud.Where(x => x.EstaActivo == true), "TipoSolicitudID", "Nombre", lDatosFolioBD.TipoSolicitudID);
        ViewBag.Proyecto = new SelectList(mContexto.Proyecto.Where(x => x.EstaActivo == true), "ProyectoID", "Nombre");
        ViewBag.Plataforma = new SelectList(mContexto.Plataforma.Where(x => x.EstaActivo == true), "PlataformaID", "Nombre", lDatosFolioBD.PlataformaID);
        ViewBag.CoincidenciaLN = new SelectList(mContexto.CoincidenciaLN.Where(x => x.EstaActivo == true), "CoincidenciaLNID", "Nombre");

        ViewBag.ResultadoVTCasa = new SelectList(mContexto.ResultadoVTCasa.Where(x => x.EstaActivo == true), "ResultadoVTID", "Nombre");
        ViewBag.ResultadoVTOficina = new SelectList(mContexto.ResultadoVTOficina.Where(x => x.EstaActivo == true), "ResultadoVTOficinaID", "Nombre");
        ViewBag.ResultadoVTRef1 = new SelectList(mContexto.ResultadoVTReferencia1.Where(x => x.EstaActivo == true), "ResultadoVTReferencia1ID", "Nombre");
        ViewBag.ResultadoVTRef2 = new SelectList(mContexto.ResultadoVTReferencia2.Where(x => x.EstaActivo == true), "ResultadoVTReferencia2ID", "Nombre");

        return View(lDatosValidacionEntities);
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
        return RedirectToAction("Index");
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Validar(Entities.ValidacionTelefonicaValidar pDatosValidacion)
    {
      try
      {

        string lNombreUsuario = User.Identity.NombreCompleto();
        string lFechaRegistro = DateTime.Now.ToString();

        if (pDatosValidacion.PlataformaID == Entities.Enumerados.Catalogos.Plataforma.SISACT)
        {
          pDatosValidacion.ResultadoVTReferencia2ID = 7;
        }

        Models.ReglaValidacion lResultadoValidacion = this.mContexto.ReglaValidacion.Where(x => x.CoincidenciaLNID == pDatosValidacion.CoincidenciaLNID && x.PlataformaID == pDatosValidacion.PlataformaID && x.ProyectoID == pDatosValidacion.ProyectoID && x.ResultadoVTCasaID == pDatosValidacion.ResultadoVTCasaID && x.ResultadoVTOficinaID == pDatosValidacion.ResultadoVTOficinaID && x.ResultadoVTReferencia1ID == pDatosValidacion.ResultadoVTReferencia1ID && x.ResultadoVTReferencia2ID == pDatosValidacion.ResultadoVTReferencia2ID && x.EstaActivo == true).FirstOrDefault();

        if (lResultadoValidacion == null)
        {
          throw new Exception("La combinación que acaba de agregar no coincide en base de datos, intente de nuevo o favor de validar con su supervisor que exista esa regla en el catálogo para dar el resultado final.");
        }

        pDatosValidacion.ResultadoFinalEstatusID = lResultadoValidacion.ResultadoFinalEstatusID;
        pDatosValidacion.ResultadoFinalVTID = lResultadoValidacion.ResultadoFinalVTID;

        this.mFolioValidacionNegocio.Validar(pDatosValidacion, lNombreUsuario, lFechaRegistro);

        Notificacion.Show(this, "El folio se ha validado correctamente y se ha dado un resultado final exitoso.", NotificationType.Success);

        return RedirectToAction("Index");
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
      }

      ViewBag.ClaseCredito = new SelectList(mContexto.ClaseCredito.Where(x => x.EstaActivo == true), "ClaseCreditoID", "Nombre", pDatosValidacion.ClaseCreditoID);
      ViewBag.ClasificacionCliente = new SelectList(mContexto.ClasificacionCliente.Where(x => x.EstaActivo == true), "ClasificacionClienteID", "Nombre", pDatosValidacion.ClasificacionClienteID);

      ViewBag.TipoSolicitud = new SelectList(mContexto.TipoSolicitud.Where(x => x.EstaActivo == true), "TipoSolicitudID", "Nombre", pDatosValidacion.TipoSolicitudID);

      ViewBag.Proyecto = new SelectList(mContexto.Proyecto.Where(x => x.EstaActivo == true), "ProyectoID", "Nombre");
      ViewBag.Plataforma = new SelectList(mContexto.Plataforma.Where(x => x.EstaActivo == true), "PlataformaID", "Nombre", pDatosValidacion.PlataformaID);

      ViewBag.CoincidenciaLN = new SelectList(mContexto.CoincidenciaLN.Where(x => x.EstaActivo == true), "CoincidenciaLNID", "Nombre");

      ViewBag.ResultadoVTCasa = new SelectList(mContexto.ResultadoVTCasa.Where(x => x.EstaActivo == true), "ResultadoVTID", "Nombre");
      ViewBag.ResultadoVTOficina = new SelectList(mContexto.ResultadoVTOficina.Where(x => x.EstaActivo == true), "ResultadoVTOficinaID", "Nombre");
      ViewBag.ResultadoVTRef1 = new SelectList(mContexto.ResultadoVTReferencia1.Where(x => x.EstaActivo == true), "ResultadoVTReferencia1ID", "Nombre");
      ViewBag.ResultadoVTRef2 = new SelectList(mContexto.ResultadoVTReferencia2.Where(x => x.EstaActivo == true), "ResultadoVTReferencia2ID", "Nombre");

      if (pDatosValidacion.EstatusID == Entities.Enumerados.EstatusFolio.Revisado || pDatosValidacion.EstatusID == Entities.Enumerados.EstatusFolio.Corregido)
      {
        ViewBag.ResultadoFinalEstatus = new SelectList(mContexto.ResultadoFinalEstatus.Where(x => x.EstaActivo == true), "ResultadoFinalEstatusID", "Nombre", pDatosValidacion.ResultadoFinalEstatusID);
        ViewBag.ResultadoFinalVT = new SelectList(mContexto.ResultadoFinalVT.Where(x => x.EstaActivo == true), "ResultadoFinalVTID", "Nombre", pDatosValidacion.ResultadoFinalVTID);
      }
      else
      {
        ViewBag.ResultadoFinalEstatus = new SelectList(mContexto.ResultadoFinalEstatus.Where(x => x.EstaActivo == true), "ResultadoFinalEstatusID", "Nombre");
        ViewBag.ResultadoFinalVT = new SelectList(mContexto.ResultadoFinalVT.Where(x => x.EstaActivo == true), "ResultadoFinalVTID", "Nombre");
      }
      return View(pDatosValidacion);
    }



    //GET - Al cargar la página
    public ActionResult Iniciar(int pFolioValidacionID)
    {
      try
      {
        Models.FolioValidacion lDatosFolioBD = this.mContexto.FolioValidacion.Where(x => x.FolioValidacionID == pFolioValidacionID).FirstOrDefault();

        Entities.ValidacionTelefonicaValidar lDatosValidacionEntities = new Entities.ValidacionTelefonicaValidar();

        lDatosValidacionEntities.FolioValidacionID = lDatosFolioBD.FolioValidacionID;
        lDatosValidacionEntities.NombreEstatus = lDatosFolioBD.Estatus.Nombre;
        lDatosValidacionEntities.EstatusID = lDatosFolioBD.EstatusID;
        lDatosValidacionEntities.FechaRegistro = lDatosFolioBD.FechaRegistro.ToString();
        lDatosValidacionEntities.HoraInicioValidacion = DateTime.Now.ToString();

        lDatosValidacionEntities.FuerzaVenta = lDatosFolioBD.FuerzaVenta;
        lDatosValidacionEntities.FolioUnico = lDatosFolioBD.FolioUnico.ToString();
        lDatosValidacionEntities.ClaseCreditoID = lDatosFolioBD.ClaseCreditoID;
        lDatosValidacionEntities.ClasificacionClienteID = lDatosFolioBD.ClasificacionClienteID;
        lDatosValidacionEntities.TipoSolicitudID = lDatosFolioBD.TipoSolicitudID;
        lDatosValidacionEntities.PlataformaID = lDatosFolioBD.PlataformaID;

        ViewBag.ClaseCredito = new SelectList(mContexto.ClaseCredito.Where(x => x.EstaActivo == true), "ClaseCreditoID", "Nombre", lDatosFolioBD.ClaseCreditoID);
        ViewBag.ClasificacionCliente = new SelectList(mContexto.ClasificacionCliente.Where(x => x.EstaActivo == true), "ClasificacionClienteID", "Nombre", lDatosFolioBD.ClasificacionClienteID);

        ViewBag.TipoSolicitud = new SelectList(mContexto.TipoSolicitud.Where(x => x.EstaActivo == true), "TipoSolicitudID", "Nombre", lDatosFolioBD.TipoSolicitudID);
        ViewBag.Plataforma = new SelectList(mContexto.Plataforma.Where(x => x.EstaActivo == true), "PlataformaID", "Nombre", lDatosFolioBD.PlataformaID);

        return View(lDatosValidacionEntities);
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
        return RedirectToAction("Index");
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Iniciar(Entities.ValidacionTelefonicaValidar pDatosValidacion)
    {
      try
      {
        string lNombreUsuario = User.Identity.NombreCompleto();
        string lFechaRegistro = DateTime.Now.ToString();
        string lCuentaUsuarioXO = User.Identity.UsuarioUniversal();
        int pFolioValidacionID = pDatosValidacion.FolioValidacionID;

        this.mFolioValidacionNegocio.Iniciar(pDatosValidacion, lNombreUsuario, lFechaRegistro, lCuentaUsuarioXO);

        Notificacion.Show(this, "El folio se inició correctamente.", NotificationType.Success);

        return RedirectToAction("Validar", routeValues: new { pFolioValidacionID });
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
      }

      ViewBag.ClaseCredito = new SelectList(mContexto.ClaseCredito.Where(x => x.EstaActivo == true), "ClaseCreditoID", "Nombre", pDatosValidacion.ClaseCreditoID);
      ViewBag.ClasificacionCliente = new SelectList(mContexto.ClasificacionCliente.Where(x => x.EstaActivo == true), "ClasificacionClienteID", "Nombre", pDatosValidacion.ClasificacionClienteID);
      ViewBag.TipoSolicitud = new SelectList(mContexto.TipoSolicitud.Where(x => x.EstaActivo == true), "TipoSolicitudID", "Nombre", pDatosValidacion.TipoSolicitudID);
      ViewBag.Plataforma = new SelectList(mContexto.Plataforma.Where(x => x.EstaActivo == true), "PlataformaID", "Nombre", pDatosValidacion.PlataformaID);

      return View(pDatosValidacion);
    }


    public ActionResult Editar(int pFolioValidacionID)
    {
      try
      {
        this.ConsultarPerfil();

        Models.FolioValidacion lDatosFolioBD = this.mContexto.FolioValidacion.Where(x => x.FolioValidacionID == pFolioValidacionID).FirstOrDefault();

        Entities.ValidacionTelefonicaEditar lDatosValidacionEntities = new Entities.ValidacionTelefonicaEditar();

        lDatosValidacionEntities.FolioValidacionID = lDatosFolioBD.FolioValidacionID;
        lDatosValidacionEntities.NombreEstatus = lDatosFolioBD.Estatus.Nombre;
        lDatosValidacionEntities.EstatusID = lDatosFolioBD.EstatusID;
        lDatosValidacionEntities.FechaRegistro = lDatosFolioBD.FechaRegistro.ToString();
        lDatosValidacionEntities.HoraInicioValidacion = DateTime.Now.ToString();

        lDatosValidacionEntities.FuerzaVenta = lDatosFolioBD.FuerzaVenta;
        lDatosValidacionEntities.FolioUnico = lDatosFolioBD.FolioUnico.ToString();
        lDatosValidacionEntities.ClaseCreditoID = lDatosFolioBD.ClaseCreditoID;
        lDatosValidacionEntities.ClasificacionClienteID = lDatosFolioBD.ClasificacionClienteID;

        lDatosValidacionEntities.NombreCliente = lDatosFolioBD.NombreCliente;
        lDatosValidacionEntities.RFCCliente = lDatosFolioBD.RFCCliente;
        lDatosValidacionEntities.NumeroCasa = lDatosFolioBD.NumeroCasa.ToString();
        lDatosValidacionEntities.NumeroOficina = lDatosFolioBD.NumeroOficina.ToString();
        lDatosValidacionEntities.Extension = lDatosFolioBD.Extension.ToString();
        lDatosValidacionEntities.NombreReferencia1 = lDatosFolioBD.NombreReferencia1.ToString();
        lDatosValidacionEntities.TelefonoReferencia1 = lDatosFolioBD.NumeroReferencia1.ToString();
        if (lDatosFolioBD.PlataformaID == Entities.Enumerados.Catalogos.Plataforma.SeguimientoFolios)
        {
          lDatosValidacionEntities.NombreReferencia2 = lDatosFolioBD.NombreReferencia2.ToString();
          lDatosValidacionEntities.TelefonoReferencia2 = lDatosFolioBD.NumeroReferencia2.ToString();
        }

        lDatosValidacionEntities.Comentarios = lDatosFolioBD.Comentarios;

        lDatosValidacionEntities.ResultadoFinalEstatusID = lDatosFolioBD.ResultadoFinalEstatusID;
        lDatosValidacionEntities.ResultadoFinalVTID = lDatosFolioBD.ResultadoFinalVTID;
        lDatosValidacionEntities.TipoSolicitudID = lDatosFolioBD.TipoSolicitudID;
        lDatosValidacionEntities.ProyectoID = lDatosFolioBD.ProyectoID;
        lDatosValidacionEntities.PlataformaID = lDatosFolioBD.PlataformaID;
        lDatosValidacionEntities.CoincidenciaLNID = lDatosFolioBD.CoincidenciaLNID;
        lDatosValidacionEntities.ResultadoVTCasaID = lDatosFolioBD.ResultadoVTID;
        lDatosValidacionEntities.ResultadoVTOficinaID = lDatosFolioBD.ResultadoVTOficinaID;
        lDatosValidacionEntities.ResultadoVTReferencia1ID = lDatosFolioBD.ResultadoVTReferencia1ID;
        lDatosValidacionEntities.ResultadoVTReferencia2ID = lDatosFolioBD.ResultadoVTReferencia2ID;

        List<Entities.FuerzaVenta> lListaFuerzaDeVenta = this.mFolioValidacionNegocio.ConsultarFuerzaVenta();
        Entities.FuerzaVenta lFuerzaVenta = new Entities.FuerzaVenta();

        lFuerzaVenta = lListaFuerzaDeVenta.Where(x => x.Nombre.Contains(lDatosFolioBD.FuerzaVenta)).FirstOrDefault();
        lDatosValidacionEntities.FuerzaVentaID = lFuerzaVenta.FuerzaVentaID;

        ViewBag.FuerzaVenta = new SelectList(lListaFuerzaDeVenta, "FuerzaVentaID", "Nombre", lFuerzaVenta.FuerzaVentaID);

        ViewBag.ClaseCredito = new SelectList(mContexto.ClaseCredito.Where(x => x.EstaActivo == true), "ClaseCreditoID", "Nombre", lDatosFolioBD.ClaseCreditoID);
        ViewBag.ClasificacionCliente = new SelectList(mContexto.ClasificacionCliente.Where(x => x.EstaActivo == true), "ClasificacionClienteID", "Nombre", lDatosFolioBD.ClasificacionClienteID);

        ViewBag.ResultadoFinalEstatus = new SelectList(mContexto.ResultadoFinalEstatus.Where(x => x.EstaActivo == true), "ResultadoFinalEstatusID", "Nombre", lDatosFolioBD.ResultadoFinalEstatusID);
        ViewBag.ResultadoFinalVT = new SelectList(mContexto.ResultadoFinalVT.Where(x => x.EstaActivo == true), "ResultadoFinalVTID", "Nombre", lDatosFolioBD.ResultadoFinalVTID);

        ViewBag.TipoSolicitud = new SelectList(mContexto.TipoSolicitud.Where(x => x.EstaActivo == true), "TipoSolicitudID", "Nombre", lDatosFolioBD.TipoSolicitudID);
        ViewBag.Proyecto = new SelectList(mContexto.Proyecto.Where(x => x.EstaActivo == true), "ProyectoID", "Nombre", lDatosFolioBD.ProyectoID);
        ViewBag.Plataforma = new SelectList(mContexto.Plataforma.Where(x => x.EstaActivo == true), "PlataformaID", "Nombre", lDatosFolioBD.PlataformaID);
        ViewBag.CoincidenciaLN = new SelectList(mContexto.CoincidenciaLN.Where(x => x.EstaActivo == true), "CoincidenciaLNID", "Nombre", lDatosFolioBD.CoincidenciaLNID);

        ViewBag.ResultadoVTCasa = new SelectList(mContexto.ResultadoVTCasa.Where(x => x.EstaActivo == true), "ResultadoVTID", "Nombre", lDatosFolioBD.ResultadoVTID);
        ViewBag.ResultadoVTOficina = new SelectList(mContexto.ResultadoVTOficina.Where(x => x.EstaActivo == true), "ResultadoVTOficinaID", "Nombre", lDatosFolioBD.ResultadoVTOficinaID);
        ViewBag.ResultadoVTRef1 = new SelectList(mContexto.ResultadoVTReferencia1, "ResultadoVTReferencia1ID", "Nombre", lDatosFolioBD.ResultadoVTReferencia1ID);

        if (lDatosFolioBD.PlataformaID == Entities.Enumerados.Catalogos.Plataforma.SeguimientoFolios)
        {
          ViewBag.ResultadoVTRef2 = new SelectList(mContexto.ResultadoVTReferencia2, "ResultadoVTReferencia2ID", "Nombre", lDatosFolioBD.ResultadoVTReferencia2ID);
        }

        return View(lDatosValidacionEntities);
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
        return RedirectToAction("Index");
      }
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Editar(Entities.ValidacionTelefonicaEditar pDatosValidacionEditar)
    {
      try
      {
        string lNombreUsuario = User.Identity.NombreCompleto();
        string lFechaRegistro = DateTime.Now.ToString();

        if (ModelState.IsValid)
        {
          this.mFolioValidacionNegocio.Editar(pDatosValidacionEditar, lNombreUsuario, lFechaRegistro);

          Notificacion.Show(this, "El folio se ha validado correctamente.", NotificationType.Success);

          return RedirectToAction("Index");
        }
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
      }
      this.ConsultarPerfil();

      ViewBag.ClaseCredito = new SelectList(mContexto.ClaseCredito.Where(x => x.EstaActivo == true), "ClaseCreditoID", "Nombre", pDatosValidacionEditar.ClaseCreditoID);
      ViewBag.ClasificacionCliente = new SelectList(mContexto.ClasificacionCliente.Where(x => x.EstaActivo == true), "ClasificacionClienteID", "Nombre", pDatosValidacionEditar.ClasificacionClienteID);

      ViewBag.ResultadoFinalEstatus = new SelectList(mContexto.ResultadoFinalEstatus.Where(x => x.EstaActivo == true), "ResultadoFinalEstatusID", "Nombre", pDatosValidacionEditar.ResultadoFinalEstatusID);
      ViewBag.ResultadoFinalVT = new SelectList(mContexto.ResultadoFinalVT.Where(x => x.EstaActivo == true), "ResultadoFinalVTID", "Nombre", pDatosValidacionEditar.ResultadoFinalVTID);

      ViewBag.TipoSolicitud = new SelectList(mContexto.TipoSolicitud.Where(x => x.EstaActivo == true), "TipoSolicitudID", "Nombre", pDatosValidacionEditar.TipoSolicitudID);
      ViewBag.Proyecto = new SelectList(mContexto.Proyecto.Where(x => x.EstaActivo == true), "ProyectoID", "Nombre", pDatosValidacionEditar.ProyectoID);
      ViewBag.Plataforma = new SelectList(mContexto.Plataforma.Where(x => x.EstaActivo == true), "PlataformaID", "Nombre", pDatosValidacionEditar.PlataformaID);
      ViewBag.CoincidenciaLN = new SelectList(mContexto.CoincidenciaLN.Where(x => x.EstaActivo == true), "CoincidenciaLNID", "Nombre", pDatosValidacionEditar.CoincidenciaLNID);

      ViewBag.ResultadoVTCasa = new SelectList(mContexto.ResultadoVTCasa.Where(x => x.EstaActivo == true), "ResultadoVTID", "Nombre", pDatosValidacionEditar.ResultadoVTCasaID);
      ViewBag.ResultadoVTOficina = new SelectList(mContexto.ResultadoVTOficina.Where(x => x.EstaActivo == true), "ResultadoVTOficinaID", "Nombre", pDatosValidacionEditar.ResultadoVTOficinaID);
      ViewBag.ResultadoVTRef1 = new SelectList(mContexto.ResultadoVTReferencia1, "ResultadoVTReferencia1ID", "Nombre", pDatosValidacionEditar.ResultadoVTReferencia1ID);
      ViewBag.ResultadoVTRef2 = new SelectList(mContexto.ResultadoVTReferencia2, "ResultadoVTReferencia2ID", "Nombre", pDatosValidacionEditar.ResultadoVTReferencia2ID);

      return View(pDatosValidacionEditar);
    }

    public int ConsultarPerfil()
    {
      string lCuentaUsuarioXO = User.Identity.UsuarioUniversal();

      int lPerfil = this.mRolNegocio.ConsultarRol(lCuentaUsuarioXO);
      ViewBag.PerfilID = lPerfil;

      return lPerfil;
    }

    public ActionResult ExportarReporte(string pFechaInicio, string pFechaFin)
    {

      try
      {
        List<Models.FolioValidacion> lListaDatosBD = new List<Models.FolioValidacion>();

        DateTime lFechaInicio = new DateTime();
        DateTime lFechaFin = new DateTime();


        if (!string.IsNullOrEmpty(pFechaInicio))
        {
          if (string.IsNullOrEmpty(pFechaFin))
          {
            throw new Exception("El campo Fecha Final es requerido para realizar la busqueda.");
          }

          lFechaInicio = Convert.ToDateTime(pFechaInicio);
        }
        if (!string.IsNullOrEmpty(pFechaFin))
        {
          if (string.IsNullOrEmpty(pFechaInicio))
          {
            throw new Exception("El campo Fecha Inicial es requerido para realizar la busqueda.");
          }
          lFechaFin = Convert.ToDateTime(pFechaFin).AddHours(23);
        }

        if (string.IsNullOrEmpty(pFechaInicio) && string.IsNullOrEmpty(pFechaFin))
        {
          throw new Exception("Favor de agregar un rango de fechas para realizar la busqueda y exportar la información");
        }

        lListaDatosBD = this.mContexto.FolioValidacion.Where(x => x.FechaRegistro >= lFechaInicio && x.FechaRegistro <= lFechaFin).ToList();

        string lNombreDocumento = "Reporte_ValidacionTelefonica_" + DateTime.Now.ToString();

        List<Entities.ValidacionTelefonicaExportar> lListaDatosExportar = new List<Entities.ValidacionTelefonicaExportar>();

        foreach (Models.FolioValidacion lFolio in lListaDatosBD)
        {
          Entities.ValidacionTelefonicaExportar lDatosFolioEntities = new Entities.ValidacionTelefonicaExportar();

          lDatosFolioEntities.FolioValidacionID = lFolio.FolioValidacionID;
          lDatosFolioEntities.FechaRegistro = lFolio.FechaRegistro;
          lDatosFolioEntities.FuerzaVenta = lFolio.FuerzaVenta;
          lDatosFolioEntities.FolioUnico = lFolio.FolioUnico;
          lDatosFolioEntities.NombrePlataforma = lFolio.Plataforma.Nombre;
          lDatosFolioEntities.UsuarioRegistro = lFolio.UsuarioRegistro;
          lDatosFolioEntities.UsuarioIniciaValidacion = lFolio.UsuarioValidaInicio;
          lDatosFolioEntities.HoraInicioValidacion = lFolio.HoraInicioValidacionTelefonica;
          lDatosFolioEntities.UsuarioFinalizaValidacion = lFolio.UsuarioValidaFin;
          lDatosFolioEntities.HoraFinValidacion = lFolio.HoraFinValidacionTelefonica;
          lDatosFolioEntities.ResultadoFinalEstatus = lFolio.ResultadoFinalEstatus.Nombre;
          lDatosFolioEntities.ResultadoFinalVT = lFolio.ResultadoFinalVT.Nombre;
          lDatosFolioEntities.NombreEstatus = lFolio.Estatus.Nombre;

          lListaDatosExportar.Add(lDatosFolioEntities);
        }

        string lPlantilla = Server.MapPath(Url.ContentArea("~/Content/Documentos/ValidacionTelefonicaReporte.xlsx"));
        ExcelReader _excel = new ExcelReader(lPlantilla);
        _excel.CargarInformacion(lListaDatosExportar, worksheet: "Reporte");
        _excel.Exportar(lNombreDocumento);

        return RedirectToAction("Index");
      }

      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
        return RedirectToAction("Index");
      }

    }

    [InformacionModulo(Titulo = "Eliminar Folio de Validación", Descripcion = "Permite eliminar el folio de validación.", Icono = "fa-trash", MostrarMenu = false)]
    public ActionResult _Eliminar(int pFolio)
    {
      try
      {
        Entities.ValidacionTelefonicaEliminar lDatos = new Entities.ValidacionTelefonicaEliminar();

        lDatos.ValidacionTelefonicaID = pFolio;

        return PartialView(lDatos);
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
        return RedirectToAction("Index");
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult _Eliminar(Entities.ValidacionTelefonicaEliminar pDatosEliminar)
    {
      try
      {
        string lNombreUsuario = User.Identity.NombreCompleto();
        string lFechaRegistro = DateTime.Now.ToString();

        if (ModelState.IsValid)
        {
          this.mFolioValidacionNegocio.Eliminar(pDatosEliminar, lNombreUsuario, lFechaRegistro);

          Notificacion.Show(this, "El folio de validación se eliminó correctamente.", NotificationType.Success);

          return RedirectToAction("Index");
        }
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
      }
      return RedirectToAction("Index");

    }

    [InformacionModulo(Titulo = "Consultar coincidencias en Folios de Validación", Descripcion = "Permite consultar coincidecnias de folio de validación.", Icono = "fa-share", MostrarMenu = false)]
    public ActionResult _ConsultarCoincidencias()
    {
      try
      {
        Entities.BusquedaCoincidencia lParametros = new Entities.BusquedaCoincidencia();

        return PartialView(lParametros);
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
      }
      return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult _ConsultarCoincidencias(Entities.BusquedaCoincidencia pParametros)
    {
      try
      {
        if (string.IsNullOrEmpty(pParametros.NumeroTelefono) && string.IsNullOrEmpty(pParametros.RFC) && string.IsNullOrEmpty(pParametros.Nombre))
        {
          throw new Exception("Favor de ingresar Numero Celular o RFC para realizar las coincidencias.");
        }

        List<Entities.FolioValidacionCoincidencia> pCoincidencias = this.mFolioValidacionNegocio.ConsultarCoincidencias(pParametros);

        TempData["Coincidencias"] = pCoincidencias;

        return RedirectToAction("ResultadoCoincidencias", pCoincidencias);
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
      }
      return RedirectToAction("Index");
    }

    public ActionResult ResultadoCoincidencias(List<Entities.FolioValidacionCoincidencia> pCoincidencias)
    {
      try
      {
        List<Entities.FolioValidacionCoincidencia> lDatos = new List<Entities.FolioValidacionCoincidencia>();

        lDatos = (List<Entities.FolioValidacionCoincidencia>)TempData["Coincidencias"];
        return View(lDatos);
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
        return RedirectToAction("Index");
      }
    }
    #endregion Metodos
    #region Helpers

    /// <summary>
    /// Vista para redireccionar una carga de vista parcial (modal) mostrando una notificación como resultado
    /// INVOCACIóN: Return PartialNotificationView("Hola mundo",notificationType.Success)
    /// </summary>
    /// <param name="message">Mensaje a mostrar</param>
    /// <param name="notificationType">Tipo de Notificación</param>
    /// <returns></returns>
    private ActionResult PartialNotificationView(string message, NotificationType notificationType = NotificationType.Error)
    {
      var view = "_" + notificationType.ToString();
      return PartialView(view, message);
    }

    /// <summary>
    /// Método para redireccionar las páginas de retorno, normalmente representado con la var "returnUrl"
    /// </summary>
    /// <param name="returnUrl">Página de Retorno o Redireccionamiento</param>
    /// <returns></returns>
    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      return RedirectToAction("Index");
    }

    #endregion

  }

}