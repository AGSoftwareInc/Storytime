using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    public class StoryController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromUri] string id, [FromBody]Entities.StorytimePost storytimepost)
        {
            storytimepost.DateCreated = System.DateTime.Now;
            var db = new PetaPoco.Database("AGSoftware");

            db.Insert(storytimepost);

            return Ok();
        }

    [HttpGet]
        public IHttpActionResult Get(string id)
        {
            var db = new PetaPoco.Database("AGSoftware");

            var a = db.SingleOrDefault<Entities.StorytimePost>("Select * from StoryTimePost Where StorytimePostId = @0", id);
            return Ok(a);
        }
    }
}
