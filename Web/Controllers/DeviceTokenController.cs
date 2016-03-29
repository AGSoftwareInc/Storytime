using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    [Authorize]
    public class DeviceTokenController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromBody]Entities.AspNetUsers user)
        {
            if (String.IsNullOrEmpty(user.DeviceToken))
            {
                return BadRequest("Device token is null or empty");
            }

            var db = new PetaPoco.Database("AGSoftware");

            var dbuser = db.SingleOrDefault<Entities.AspNetUsers>("Select * From AspNetUsers where Id = @0", Storytime.Providers.UserHelper.GetUserId(this.User.Identity.Name));

            if (dbuser != null)
            {
                dbuser.DeviceToken = user.DeviceToken;
                db.Update(dbuser);

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
