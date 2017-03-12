using System.Web;
using System.Web.Http;

namespace FunctionalTestServer.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.Start();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}