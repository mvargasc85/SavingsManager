using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SavingsManager.Startup))]
namespace SavingsManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
