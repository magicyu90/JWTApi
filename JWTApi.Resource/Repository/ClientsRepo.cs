using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JWTApi.Resource.Repository
{
    public class ClientsRepo
    {

        public static readonly HugoContext Context;

        static ClientsRepo()
        {
            Context = new HugoContext();
        }


        public static string[] GetAudienceIds()
        {
            var audienceGuids = Context.Clients.Select(x => x.AppId).AsEnumerable();

            return audienceGuids.Select(x => x.ToString("N")).ToArray();
        }

        public static string[] GetAudienceSecrets()
        {
            var audienceSecrets = Context.Clients.Select(x => x.AppSecret).AsEnumerable();

            return audienceSecrets.Select(x => x.ToString("N")).ToArray();
        }
    }
}