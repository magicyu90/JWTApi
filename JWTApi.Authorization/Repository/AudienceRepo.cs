using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using JWTApi.Authorization.Entities;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace JWTApi.Authorization.Repository
{
    public class AudienceRepo
    {

        //todo: maybe can store in db

        private static ConcurrentDictionary<string, Audience> _audienceList = new ConcurrentDictionary<string, Audience>();

        static AudienceRepo()
        {
            _audienceList.TryAdd("50de53cc8cf04d469eb433ed117895ae", new Audience
            {
                ClientId = "50de53cc8cf04d469eb433ed117895ae",
                Name = "Hugo Api",
                Base64Secret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw"
            });
        }

        public static Audience AddAudience(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");
            var key = new byte[32];
            RandomNumberGenerator.Create().GetBytes(key);
            var secret = TextEncodings.Base64Url.Encode(key);

            Audience audience = new Audience
            {
                ClientId = clientId,
                Base64Secret = secret,
                Name = name
            };

            _audienceList.TryAdd(clientId, audience);

            return audience;
        }

        public static Audience FindAudienceByClientId(string clientId)
        {

            Audience audience;
            if (_audienceList.TryGetValue(clientId, out audience))
            {
                return audience;
            }
            return null;
        }
    }
}