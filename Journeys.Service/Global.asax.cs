using System.Web;
using System.Web.Http;

namespace Journeys.Service
{
    public class WebApiApplication : HttpApplication
    {
        private Bootstrapper _bootstrapper;

        public override void Init()
        {
            base.Init();
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var eventFilePath = Server.MapPath("data/events.txt");
            _bootstrapper = new Bootstrapper();
            _bootstrapper.Bootstrap(eventFilePath);
        }
    }
}
