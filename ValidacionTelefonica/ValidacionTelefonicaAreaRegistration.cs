using System.Web.Mvc;

namespace Telcel.Portal.MiTelcelR9
{
    public class ValidacionTelefonicaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ValidacionTelefonica";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ValidacionTelefonica_default",
                "ValidacionTelefonica/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
								new string[] { "ValidacionTelefonica.Controllers" }
            );
        }
    }
}