using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EdotNet.Startup))]
namespace EdotNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
