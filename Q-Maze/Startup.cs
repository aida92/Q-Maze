using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QMaze.Startup))]
namespace QMaze
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
