using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(JWTApi.Authorization.Startup))]

namespace JWTApi.Authorization
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            var configuration = new HttpConfiguration();

            //Wep api routes
            configuration.MapHttpAttributeRoutes();

            ConfigureOAuth(app);

            //support
            app.Use(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.Use(configuration);

        }


        //config OAuthorizaion
        private void ConfigureOAuth(IAppBuilder app)
        {
            //todo:config oauth

        }
    }
}
