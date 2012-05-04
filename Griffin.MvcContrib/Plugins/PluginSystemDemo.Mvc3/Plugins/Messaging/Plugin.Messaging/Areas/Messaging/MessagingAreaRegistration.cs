using System.Web.Mvc;

namespace Plugin.Messaging.Areas.Messaging
{
    public class MessagingAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Messaging";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Messaging",
                "Messaging/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                new[] { GetType().Namespace + ".Controllers" }
            );
        }
    }
}
