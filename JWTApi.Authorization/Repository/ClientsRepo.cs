using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using JWTApi.Authorization.Models;


namespace JWTApi.Authorization.Repository
{
    public class ClientsRepo
    {

        private static readonly HugoContext Context;

        static ClientsRepo()
        {
            Context = new HugoContext();
        }

        public static ClientModel AddClient(ClientModel client)
        {

            Context.Clients.Add(client);

            Context.SaveChanges();

            return client;
        }

        public static ClientModel FindByClientId(string clientId)
        {
            Guid clientGuid = Guid.Parse(clientId);
            return Context.Clients.FirstOrDefault(x => x.AppId == clientGuid);
        }

    }
}