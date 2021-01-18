using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(starteAlkemy.App_Start.StartUp))]

namespace starteAlkemy.App_Start
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            // Para obtener más información sobre cómo configurar la aplicación, visite https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
