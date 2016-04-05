using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            var db = new PetaPoco.Database("AGSoftware");

            var a = db.SingleOrDefault<Entities.AspNetUsers>("Select * from AspNetUsers Where UserId = @0", id);

            if (a != null)
                return Ok(a);
            else
                return NotFound();
        }
    }
}
