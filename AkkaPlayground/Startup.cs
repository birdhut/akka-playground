using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(AkkaPlayground.Startup))]

namespace AkkaPlayground
{
    /// <summary>
    /// Creates a Startup Configuration for the Owin Web Server
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initialises the application
        /// </summary>
        /// <param name="app">The application builder</param>
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
