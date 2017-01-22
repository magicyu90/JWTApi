using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using JWTApi.Resource.Repository;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Owin;
using Microsoft.Owin.Security;

[assembly: OwinStartup(typeof(JWTApi.Resource.Startup))]

namespace JWTApi.Resource
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(config);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = "http://localhost";
            //var audience = "50de53cc8cf04d469eb433ed117895ae";
            var audiences = ClientsRepo.GetAudienceIds();
            //var secret = TextEncodings.Base64Url.Decode("Yjc0OTM0Mjg5Y2QzNGMwYzgyY2MxZmMwYWUwYzMzNTA=");
            var audienceSecrets = ClientsRepo.GetAudienceSecrets();

            List<IIssuerSecurityTokenProvider> providers = new List<IIssuerSecurityTokenProvider>();
            foreach (var secret in audienceSecrets)
            {
                providers.Add(new SymmetricKeyIssuerSecurityTokenProvider(issuer, Encoding.UTF8.GetBytes(secret)));
            }

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    //todo:check from database
                    // AllowedAudiences = new[] { audience },
                    AllowedAudiences = audiences,
                    //IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    //{
                    //    new SymmetricKeyIssuerSecurityTokenProvider(issuer,secret)
                    //}
                    IssuerSecurityTokenProviders = providers
                });

        }
    }
}
