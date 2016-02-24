using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    public class UserController : ApiController
    {
        public IHttpActionResult GetUser(string UserId)
        {
            var db = new PetaPoco.Database("AGSoftware");

            var a = db.SingleOrDefault<Entities.User>("Select * from [User] Where UserId = @0", UserId);
            return Ok(a);

        }
    }
}
