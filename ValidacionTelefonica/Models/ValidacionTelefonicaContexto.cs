
using System.Data.Entity;

namespace ValidacionTelefonica.Models
{
  public class ValidacionTelefonicaContexto : DbContext
  {
    #region Constructor
    public ValidacionTelefonicaContexto() : base("ValidacionTelefonica")
    {
        
    }
    #endregion Constructor

    public DbSet<BitacoraMovimiento> BitacoraMovimiento { get; set; }
    public DbSet<ClaseCredito> ClaseCredito { get; set; }
    public DbSet<ClasificacionCliente> ClasificacionCliente { get; set; }
    public DbSet<CoincidenciaLN> CoincidenciaLN { get; set; }
    public DbSet<CoincidenciaLNBitacora> CoincidenciaLNBitacora { get; set; }
    public DbSet<Estatus> Estatus { get; set; }
    public DbSet<FolioValidacion> FolioValidacion { get; set; }
    public DbSet<MotivoNoContesta> MotivoNoContesta { get; set; }
    public DbSet<Plataforma> Plataforma { get; set; } 
    public DbSet<Proyecto> Proyecto { get; set; }
    public DbSet<ReglaValidacion> ReglaValidacion { get; set; }
    public DbSet<ResultadoFinalEstatus> ResultadoFinalEstatus { get; set; }
    public DbSet<ResultadoFinalVT> ResultadoFinalVT { get; set; }
    public DbSet<ResultadoVTCasa> ResultadoVTCasa { get; set; }
    public DbSet<ResultadoVTOficina> ResultadoVTOficina { get; set; }
    public DbSet<ResultadoVTReferencia1> ResultadoVTReferencia1 { get; set; }
    public DbSet<ResultadoVTReferencia2> ResultadoVTReferencia2 { get; set; }
    public DbSet<TipoSolicitud> TipoSolicitud { get; set; }

  }
}