using Microsoft.Owin;
using Owin;
using System.Web;

[assembly: OwinStartupAttribute(typeof(projeoneritakipsistemi.Startup))]
namespace projeoneritakipsistemi
{
    public partial class Startup
    {


        public void Configuration(IAppBuilder app)
        {

           
            ConfigureAuth(app);
        }
    }
}
