using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ValidacionTelefonica.Bussines
{
  public class Rol
  {
    #region Miembros
    public ValidacionTelefonica.DataAccess.Rol mRolDataAcces;
    #endregion Miembros

    #region Constructor
    public Rol()
    {
      this.mRolDataAcces = new DataAccess.Rol();
    }
    #endregion Constructor

    #region Metodos
    public int ConsultarRol(string pCuentaUsuarioXO)
    {
      DataTable lDatos = this.mRolDataAcces.ConsultarRol(pCuentaUsuarioXO);

      int lPerfilID = 0;

      if (lDatos.Rows.Count > 0)
      {
        foreach (DataRow lDato in lDatos.Rows)
        {
          lPerfilID  = Convert.ToInt32(lDato["RolID"]);
        }
      }

      return lPerfilID;

    }
    #endregion Metodos

  }
}