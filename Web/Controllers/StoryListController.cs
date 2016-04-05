using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    [Authorize]
    public class StoryListController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            var db = new PetaPoco.Database("AGSoftware");

            System.Collections.Generic.List<Entities.Storytime> storytimelist = new List<Entities.Storytime>();

            foreach (var c in db.Query<Entities.Storytime>("Select * from StorytimePost Where UserId = @0", Storytime.Providers.UserHelper.GetUserId(this.User.Identity.Name)))
            {
                storytimelist.Add(c);
            }

            if (storytimelist.Count > 0)
                return Ok(storytimelist);
            else
                return NotFound();
        }
    }
}
