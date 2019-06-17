using System.Web.Http;
using WorkoutTracker.Data.EFCore;

namespace WorkoutTracker.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            System.Data.Entity.Database.SetInitializer(new WorkoutDbInitializer());
        }
    }
}
