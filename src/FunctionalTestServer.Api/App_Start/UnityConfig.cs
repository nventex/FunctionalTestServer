using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace FunctionalTestServer.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}