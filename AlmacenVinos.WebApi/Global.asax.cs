using AlmacenVinos.Services;
using System.Web.Http;
using Unity;

namespace AlmacenVinos.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityWebApiActivator.Start();
        }
    }
}
