using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using JWTApi.Authorization.Repository;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace JWTApi.Authorization.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //return base.ValidateClientAuthentication(context);

            string clientId = string.Empty;
            string secret = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out secret))
            {
                context.TryGetFormCredentials(out clientId, out secret);
            }

            if (string.IsNullOrEmpty(clientId))
            {
                context.SetError("invali client", "client id is not set.");
                return Task.FromResult<object>(null);
            }
            //if (string.IsNullOrEmpty(secret))
            //{
            //    context.SetError("invalid client", "client secret is not set.");
            //    return Task.FromResult<object>(null);
            //}

            var audience = AudienceRepo.FindAudienceByClientId(clientId);
            if (audience == null)
            {
                context.SetError("invalid client", $"client not found by client id:{clientId}.");
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);

        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //return base.GrantResourceOwnerCredentials(context);

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            if (string.IsNullOrEmpty(context.UserName) || string.IsNullOrEmpty(context.Password))
            {
                context.SetError("invalid grant", "The username or password is empty.");
                return;
            }

            var doctor = await DocotorRepo.GetDoctorAccountAsync(context.UserName);
            if (doctor == null || doctor.Password != context.Password)
            {
                context.SetError("invalid grant", "Wrong username or password.");
                return;
            }

            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Supervior"));

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {"audience", context.ClientId??string.Empty }
            });

            var ticket = new AuthenticationTicket(identity, props);

            context.Validated(ticket);

        }
    }
}