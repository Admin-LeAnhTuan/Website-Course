using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Course.Startup))]
namespace Course
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
