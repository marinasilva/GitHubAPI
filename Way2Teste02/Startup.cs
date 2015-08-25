using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Way2Teste02.Startup))]
namespace Way2Teste02
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
