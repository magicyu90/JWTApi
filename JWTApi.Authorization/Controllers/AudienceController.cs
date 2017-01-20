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

    [RoutePrefix("api/audiences")]
    public class AudienceController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {

            return Ok("Ok");
        }


        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(AudienceModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Audience is not valid.");
            }

            Audience newAudience = AudienceRepo.AddAudience(model.Name);

            return new CustomHttpActionResult<Audience>(this.Request, newAudience, HttpStatusCode.OK, null);
        }
    }
}
