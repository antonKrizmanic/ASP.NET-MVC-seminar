using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Seminar.Web.Startup))]
namespace Seminar.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
