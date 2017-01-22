using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using JWTApi.Authorization.Entities;
using JWTApi.Authorization.Repository;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Thinktecture.IdentityModel.Tokens;
using System.Text;
using Jose;
using JWTApi.Authorization.Models;


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

            string audienceId = data.Properties.Dictionary.ContainsKey(AudiencePropertyKey) ? data.Properties.Dictionary[AudiencePropertyKey].ToLower() : null;

            if (string.IsNullOrWhiteSpace(audienceId)) throw new InvalidOperationException("AuthenticationTicket.Properties does not include audience");

            // Audience audience = AudienceRepo.FindAudienceByClientId(audienceId);
            ClientModel client = ClientsRepo.FindByClientId(audienceId);

            //string symmetricKeyAsBase64 = audience.Base64Secret;

            //var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            string appSecretStr = client.AppSecret.ToString("N");
            var keyByteArray = Encoding.UTF8.GetBytes(appSecretStr);

            #region  use jose-jwt to generate json web token
            //DateTime issued = DateTime.UtcNow;
            //DateTime expired = DateTime.UtcNow.AddDays(1);

            //var payload = new Dictionary<string, string>
            //{
            //    { "iss",_issuer},
            //    { "aud",audience.ClientId},
            //    { "iat",UnixTime.ToUnixTime(issued).ToString()},
            //    { "exp",UnixTime.ToUnixTime(expired).ToString()}
            //};

            //var jwt = JWT.Encode(payload, keyByteArray, JwsAlgorithm.HS256);
            #endregion

            #region use system.identitymodel.tokens.jwt 4.0.2 to generate jwt
            var signingKey = new HmacSigningCredentials(keyByteArray);

            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            #endregion


            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}