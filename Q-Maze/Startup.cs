using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Q_Maze.Startup))]
namespace Q_Maze
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
