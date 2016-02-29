using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    public class SignUpController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromUri] string id, [FromBody]Entities.User user)
        {
            user.DateCreated = System.DateTime.Now;
            var db = new PetaPoco.Database("AGSoftware");

            db.Insert(user);

            return Ok();
        }
    }
}
