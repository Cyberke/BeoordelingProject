using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeoordelingProject.Startup))]
namespace BeoordelingProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
