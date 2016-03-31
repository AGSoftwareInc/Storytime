using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    [Authorize]
    public class LogErrorController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromUri] string id, [FromBody]Entities.Logs logs)
        {
            var db = new PetaPoco.Database("AGSoftware");

            logs.EventDateTime = System.DateTime.Now;
            logs.UserName = this.User.Identity.Name;


            db.Insert(logs);

            return Ok();
        }
    }
}
