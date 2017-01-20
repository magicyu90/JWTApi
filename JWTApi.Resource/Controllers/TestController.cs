using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace JWTApi.Resource.Controllers
{

    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            var identity = User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var res = identity.Claims.Select(c => new
                {
                    Type = c.Type,
                    Value = c.Value
                });

                return Ok(res);
            }

            return NotFound();

        }
    }
}
