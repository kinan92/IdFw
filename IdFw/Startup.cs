using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdFw.Startup))]
namespace IdFw
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
