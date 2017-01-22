using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JWTApi.Authorization.Entities;
using JWTApi.Authorization.Models;
using JWTApi.Authorization.Repository;
using JWTApi.Authorization.Tools;

namespace JWTApi.Authorization.Controllers
{
    [RoutePrefix("api/clients")]
    public class ClientController : ApiController
    {
        
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(Client model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Client is not valid.");
            }

            var clientModel = new ClientModel
            {
                AppId = Guid.NewGuid(),
                AppSecret = Guid.NewGuid(),
                AppName = model.AppName,
                CompanyName = model.CompanyName,
                RedirectUrl = model.RedirectUrl
            };

            var res = ClientsRepo.AddClient(clientModel);

            return new CustomHttpActionResult<ClientModel>(this.Request, res, HttpStatusCode.OK, null);


        }

    }
}
