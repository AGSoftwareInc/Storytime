using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    public class LoginController : ApiController
    {
        //todo need to figure out why an id has to be passed in order for this to work
        [HttpPost]
        public IHttpActionResult Post([FromUri] string id, [FromBody]Entities.Login login)
        {
            var db = new PetaPoco.Database("AGSoftware");


            var a = db.SingleOrDefault<Entities.User>("Select * from [User] Where Username = @0 and Password = @1", login.Username, login.Password);

            if (a != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
