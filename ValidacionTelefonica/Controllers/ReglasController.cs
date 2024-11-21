using System;
using System.Collections.Generic;
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
  public class ReglasController : Controller
  {

    #region Miembros
    public readonly Models.ValidacionTelefonicaContexto mContexto;
    public Bussines.ReglaValidacion mReglaValidacionNegocio;

    #endregion Miembros

    #region Constructor
    public ReglasController()
    {
      this.mContexto = new Models.ValidacionTelefonicaContexto();
      this.mReglaValidacionNegocio = new Bussines.ReglaValidacion();
    }
    #endregion Constructor

    #region Metodos

    // GET: Reglas
    public ActionResult Index()
    {
      try
      {
        this.ListarFiltros();

        List<Entities.ReglaValidacionConsultar> lDatos = this.mReglaValidacionNegocio.ConsultarListadoReglasValidacion(null, null, null, null, null, null, null, null, null);

        return View(lDatos);
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
        return RedirectToAction("Index");
      }

    }

    [HttpPost]
    [AjaxActionOnly]
    public JsonResult ListarCoincidenciaLN()
    {

      var lDatos = this.mContexto.CoincidenciaLN.Select(x => new { value = x.CoincidenciaLNID, text = x.Nombre }).ToList();

      return Json(lDatos, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    [AjaxActionOnly]
    public JsonResult ListarPlataforma()
    {

      var lDatos = this.mContexto.Plataforma.Select(x => new { value = x.PlataformaID, text = x.Nombre }).ToList();

      return Json(lDatos, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    [AjaxActionOnly]
    public JsonResult ListarProyecto()
    {

      var lDatos = this.mContexto.Proyecto.Select(x => new { value = x.ProyectoID, text = x.Nombre }).ToList();

      return Json(lDatos, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    [AjaxActionOnly]
    public JsonResult ListarCasa()
    {

      var lDatos = this.mContexto.ResultadoVTCasa.Select(x => new { value = x.ResultadoVTID, text = x.Nombre }).ToList();

      return Json(lDatos, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    [AjaxActionOnly]
    public JsonResult ListarOficina()
    {

      var lDatos = this.mContexto.ResultadoVTOficina.Select(x => new { value = x.ResultadoVTOficinaID, text = x.Nombre }).ToList();

      return Json(lDatos, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    [AjaxActionOnly]
    public JsonResult ListarReferencia1()
    {

      var lDatos = this.mContexto.ResultadoVTReferencia1.Select(x => new { value = x.ResultadoVTReferencia1ID, text = x.Nombre }).ToList();

      return Json(lDatos, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    [AjaxActionOnly]
    public JsonResult ListarReferencia2()
    {

      var lDatos = this.mContexto.ResultadoVTReferencia2.Select(x => new { value = x.ResultadoVTReferencia2ID, text = x.Nombre }).ToList();

      return Json(lDatos, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    [AjaxActionOnly]
    public JsonResult ListarEstatusFinal()
    {

      var lDatos = this.mContexto.ResultadoFinalEstatus.Select(x => new { value = x.ResultadoFinalEstatusID, text = x.Nombre }).ToList();

      return Json(lDatos, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    [AjaxActionOnly]
    public JsonResult ListarResultadoFinal()
    {

      var lDatos = this.mContexto.ResultadoFinalVT.Select(x => new { value = x.ResultadoFinalVTID, text = x.Nombre }).ToList();

      return Json(lDatos, JsonRequestBehavior.AllowGet);
    }

    private void ListarFiltros()
    {
      ViewBag.CoincidenciaLN = Url.Action("ListarCoincidenciaLN");
      ViewBag.Plataforma = Url.Action("ListarPlataforma");
      ViewBag.Proyecto = Url.Action("ListarProyecto");
      ViewBag.Casa = Url.Action("ListarCasa");
      ViewBag.Oficina = Url.Action("ListarOficina");
      ViewBag.Referencia1 = Url.Action("ListarReferencia1");
      ViewBag.Referencia2 = Url.Action("ListarReferencia2");
      ViewBag.EstatusFinal = Url.Action("ListarEstatusFinal");
      ViewBag.ResultadoFinal = Url.Action("ListarResultadoFinal");
    }

    public ActionResult ConsultarPorFiltros(string pCoincidenciaLN, string pPlataforma, string pProyecto, string pCasa, string pOficina, string pRef1, string pRef2, string pEstatusFinal, string pResultadoFinal)
    {
      try
      {
        this.ListarFiltros();

        List<Entities.ReglaValidacionConsultar> lDatos = this.mReglaValidacionNegocio.ConsultarListadoReglasValidacion(string.IsNullOrEmpty(pCoincidenciaLN) ? null : pCoincidenciaLN, string.IsNullOrEmpty(pPlataforma) ? null : pPlataforma, string.IsNullOrEmpty(pProyecto) ? null : pProyecto, string.IsNullOrEmpty(pCasa) ? null : pCasa, string.IsNullOrEmpty(pOficina) ? null : pOficina, string.IsNullOrEmpty(pRef2) ? null : pRef2, string.IsNullOrEmpty(pRef2) ? null : pRef2, string.IsNullOrEmpty(pEstatusFinal) ? null : pEstatusFinal, string.IsNullOrEmpty(pResultadoFinal) ? null : pResultadoFinal);

        return View("Index", lDatos);

      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
        return RedirectToAction("Index");
      }
    }

    public ActionResult Registrar()
    {
      try
      {
        ViewBag.CoincidenciaLN = new SelectList(mContexto.CoincidenciaLN.Where(x => x.EstaActivo == true), "CoincidenciaLNID", "Nombre");
        ViewBag.Plataforma = new SelectList(mContexto.Plataforma.Where(x => x.EstaActivo == true), "PlataformaID", "Nombre");
        ViewBag.Proyecto = new SelectList(mContexto.Proyecto.Where(x => x.EstaActivo == true), "ProyectoID", "Nombre");
        ViewBag.ResultadoVTCasa = new SelectList(mContexto.ResultadoVTCasa.Where(x => x.EstaActivo == true), "ResultadoVTID", "Nombre");
        ViewBag.ResultadoVTOficina = new SelectList(mContexto.ResultadoVTOficina, "ResultadoVTOficinaID", "Nombre");
        ViewBag.ResultadoVTReferencia1 = new SelectList(mContexto.ResultadoVTReferencia1.Where(x => x.EstaActivo == true), "ResultadoVTReferencia1ID", "Nombre");
        ViewBag.ResultadoVTReferencia2 = new SelectList(mContexto.ResultadoVTReferencia2, "ResultadoVTReferencia2ID", "Nombre");
        ViewBag.ResultadoFinalEstatus = new SelectList(mContexto.ResultadoFinalEstatus.Where(x => x.EstaActivo == true), "ResultadoFinalEstatusID", "Nombre");
        ViewBag.ResultadoFinalVT = new SelectList(mContexto.ResultadoFinalVT.Where(x => x.EstaActivo == true), "ResultadoFinalVTID", "Nombre");

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
    public ActionResult Registrar(Entities.ReglaValidacionRegistrar pDatosReglaRegistrar)
    {
      try
      {
        string lNombreUsuario = User.Identity.NombreCompleto();
        string lFechaRegistro = DateTime.Now.ToString();
        string lCuentaUsuarioXO = User.Identity.UsuarioUniversal();

        if (ModelState.IsValid)
        {
          this.mReglaValidacionNegocio.Registrar(pDatosReglaRegistrar, lNombreUsuario, lFechaRegistro, lCuentaUsuarioXO);

          Notificacion.Show(this, "La regla de validación se registró correctamente.", NotificationType.Success);

          return RedirectToAction("Index");
        }
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
      }

      ViewBag.CoincidenciaLN = new SelectList(mContexto.CoincidenciaLN.Where(x => x.EstaActivo == true), "CoincidenciaLNID", "Nombre");
      ViewBag.Plataforma = new SelectList(mContexto.Plataforma.Where(x => x.EstaActivo == true), "PlataformaID", "Nombre");
      ViewBag.Proyecto = new SelectList(mContexto.Proyecto.Where(x => x.EstaActivo == true), "ProyectoID", "Nombre");
      ViewBag.ResultadoVTCasa = new SelectList(mContexto.ResultadoVTCasa.Where(x => x.EstaActivo == true), "ResultadoVTID", "Nombre");
      ViewBag.ResultadoVTOficina = new SelectList(mContexto.ResultadoVTOficina.Where(x => x.EstaActivo == true), "ResultadoVTOficinaID", "Nombre");
      ViewBag.ResultadoVTReferencia1 = new SelectList(mContexto.ResultadoVTReferencia1.Where(x => x.EstaActivo == true), "ResultadoVTReferencia1ID", "Nombre");
      ViewBag.ResultadoVTReferencia2 = new SelectList(mContexto.ResultadoVTReferencia2.Where(x => x.EstaActivo == true), "ResultadoVTReferencia2ID", "Nombre");
      ViewBag.ResultadoFinalEstatus = new SelectList(mContexto.ResultadoFinalEstatus.Where(x => x.EstaActivo == true), "ResultadoFinalEstatusID", "Nombre");
      ViewBag.ResultadoFinalVT = new SelectList(mContexto.ResultadoFinalVT.Where(x => x.EstaActivo == true), "ResultadoFinalVTID", "Nombre");

      return View(pDatosReglaRegistrar);
    }


    public ActionResult Editar(int pReglaValidacionID)
    {
      try
      {
        Models.ReglaValidacion lDatosReglaValidacion = this.mContexto.ReglaValidacion.Where(x => x.ReglaValiddacionID == pReglaValidacionID).FirstOrDefault();

        Entities.ReglaValidacionEditar lReglaValidacionEntities = new Entities.ReglaValidacionEditar();

        lReglaValidacionEntities.ReglaValidacionID = lDatosReglaValidacion.ReglaValiddacionID;
        lReglaValidacionEntities.CoincidenciaLNID = lDatosReglaValidacion.CoincidenciaLNID;
        lReglaValidacionEntities.PlataformaID = lDatosReglaValidacion.PlataformaID;
        lReglaValidacionEntities.ProyectoID = lDatosReglaValidacion.ProyectoID;
        lReglaValidacionEntities.ResultadoVTCasaID = lDatosReglaValidacion.ResultadoVTCasaID;
        lReglaValidacionEntities.ResultadoVTOficinaID = lDatosReglaValidacion.ResultadoVTOficinaID;
        lReglaValidacionEntities.ResultadoVTReferencia1ID = lDatosReglaValidacion.ResultadoVTReferencia1ID;
        lReglaValidacionEntities.ResultadoVTReferencia2ID = lDatosReglaValidacion.ResultadoVTReferencia2ID;
        lReglaValidacionEntities.ResultadoFinalEstatusID = lDatosReglaValidacion.ResultadoFinalEstatusID;
        lReglaValidacionEntities.ResultadoFinalVTID = lDatosReglaValidacion.ResultadoFinalVTID;
        lReglaValidacionEntities.EstaActivo = lDatosReglaValidacion.EstaActivo;



        ViewBag.CoincidenciaLN = new SelectList(mContexto.CoincidenciaLN.Where(x => x.EstaActivo == true), "CoincidenciaLNID", "Nombre", lDatosReglaValidacion.CoincidenciaLNID);
        ViewBag.Plataforma = new SelectList(mContexto.Plataforma.Where(x => x.EstaActivo == true), "PlataformaID", "Nombre", lDatosReglaValidacion.PlataformaID);
        ViewBag.Proyecto = new SelectList(mContexto.Proyecto.Where(x => x.EstaActivo == true), "ProyectoID", "Nombre", lDatosReglaValidacion.ProyectoID);
        ViewBag.ResultadoVTCasa = new SelectList(mContexto.ResultadoVTCasa.Where(x => x.EstaActivo == true), "ResultadoVTID", "Nombre", lDatosReglaValidacion.ResultadoVTCasaID);
        ViewBag.ResultadoVTOficina = new SelectList(mContexto.ResultadoVTOficina, "ResultadoVTOficinaID", "Nombre", lDatosReglaValidacion.ResultadoVTOficinaID);
        ViewBag.ResultadoVTReferencia1 = new SelectList(mContexto.ResultadoVTReferencia1.Where(x => x.EstaActivo == true), "ResultadoVTReferencia1ID", "Nombre", lDatosReglaValidacion.ResultadoVTReferencia1ID);
        ViewBag.ResultadoVTReferencia2 = new SelectList(mContexto.ResultadoVTReferencia2, "ResultadoVTReferencia2ID", "Nombre", lDatosReglaValidacion.ResultadoVTReferencia2ID);
        ViewBag.ResultadoFinalEstatus = new SelectList(mContexto.ResultadoFinalEstatus.Where(x => x.EstaActivo == true), "ResultadoFinalEstatusID", "Nombre", lDatosReglaValidacion.ResultadoFinalEstatusID);
        ViewBag.ResultadoFinalVT = new SelectList(mContexto.ResultadoFinalVT.Where(x => x.EstaActivo == true), "ResultadoFinalVTID", "Nombre", lDatosReglaValidacion.ResultadoFinalVTID);

        return View(lReglaValidacionEntities);
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
        return RedirectToAction("Index");
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Editar(Entities.ReglaValidacionEditar pDatosReglaEditar)
    {
      try
      {
        string lNombreUsuario = User.Identity.NombreCompleto();
        string lFechaRegistro = DateTime.Now.ToString();

        if (ModelState.IsValid)
        {
          this.mReglaValidacionNegocio.Editar(pDatosReglaEditar, lNombreUsuario, lFechaRegistro);

          Notificacion.Show(this, "La regla de validación se actualizó correctamente.", NotificationType.Success);

          return RedirectToAction("Index");
        }
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
      }

      ViewBag.CoincidenciaLN = new SelectList(mContexto.CoincidenciaLN.Where(x => x.EstaActivo == true), "CoincidenciaLNID", "Nombre");
      ViewBag.Plataforma = new SelectList(mContexto.Plataforma.Where(x => x.EstaActivo == true), "PlataformaID", "Nombre");
      ViewBag.Proyecto = new SelectList(mContexto.Proyecto.Where(x => x.EstaActivo == true), "ProyectoID", "Nombre");
      ViewBag.ResultadoVTCasa = new SelectList(mContexto.ResultadoVTCasa.Where(x => x.EstaActivo == true), "ResultadoVTID", "Nombre");
      ViewBag.ResultadoVTOficina = new SelectList(mContexto.ResultadoVTOficina.Where(x => x.EstaActivo == true), "ResultadoVTOficinaID", "Nombre");
      ViewBag.ResultadoVTReferencia1 = new SelectList(mContexto.ResultadoVTReferencia1.Where(x => x.EstaActivo == true), "ResultadoVTReferencia1ID", "Nombre");
      ViewBag.ResultadoVTReferencia2 = new SelectList(mContexto.ResultadoVTReferencia2.Where(x => x.EstaActivo == true), "ResultadoVTReferencia2ID", "Nombre");
      ViewBag.ResultadoFinalEstatus = new SelectList(mContexto.ResultadoFinalEstatus.Where(x => x.EstaActivo == true), "ResultadoFinalEstatusID", "Nombre");
      ViewBag.ResultadoFinalVT = new SelectList(mContexto.ResultadoFinalVT.Where(x => x.EstaActivo == true), "ResultadoFinalVTID", "Nombre");

      return View(pDatosReglaEditar);
    }

    [InformacionModulo(Titulo = "Eliminar Regla de Validación", Descripcion = "Permite eliminar la regla de validación.", Icono = "fa-add", MostrarMenu = false)]
    public ActionResult _Eliminar(int pReglaValidacionID)
    {
      try
      {
        Entities.ReglaValidacionEliminar lDatos = new Entities.ReglaValidacionEliminar();

        lDatos.ReglaValidacionID = pReglaValidacionID;

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
    public ActionResult _Eliminar(Entities.ReglaValidacionEliminar pDatosEliminar)
    {
      try
      {
        if (ModelState.IsValid)
        {
          this.mReglaValidacionNegocio.Eliminar(pDatosEliminar);

          Notificacion.Show(this, "La regla de validación se eliminó correctamente.", NotificationType.Success);

          return RedirectToAction("Index");
        }
      }
      catch (Exception lExcepcion)
      {
        Notificacion.Show(this, lExcepcion.Message, NotificationType.Warning);
      }
      return RedirectToAction("Index");

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