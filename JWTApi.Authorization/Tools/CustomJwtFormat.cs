using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web;
using JWTApi.Authorization.Entities;
using JWTApi.Authorization.Repository;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Thinktecture.IdentityModel.Tokens;

namespace JWTApi.Authorization.Tools
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {

        private const string AudiencePropertyKey = "audience";

        private readonly string _issuer = string.Empty;

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }



        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            string audienceId = data.Properties.Dictionary.ContainsKey(AudiencePropertyKey) ? data.Properties.Dictionary[AudiencePropertyKey] : null;

            if (string.IsNullOrWhiteSpace(audienceId)) throw new InvalidOperationException("AuthenticationTicket.Properties does not include audience");

            Audience audience = AudienceRepo.FindAudienceByClientId(audienceId);

            string symmetricKeyAsBase64 = audience.Base64Secret;

            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            var secKey = new SymmetricSecurityKey(keyByteArray);

            //  var signingKey = new HmacSigningCredentials(keyByteArray);
            var signKey = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);

            var issued = DateTime.UtcNow;

            var expires = DateTime.UtcNow + TimeSpan.FromDays(1);

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued, expires, signKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}