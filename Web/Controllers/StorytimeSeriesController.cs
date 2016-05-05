using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    [Authorize]

    public class StorytimeSeriesController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromUri] string id, [FromBody] Entities.StorytimeSeries storytimeseries) 
        {
            var db = new PetaPoco.Database("AGSoftware");

            storytimeseries.StorytimeId = int.Parse(id);
            storytimeseries.DateCreated = System.DateTime.Now;
            storytimeseries.UserId = Storytime.Providers.UserHelper.GetUserId(this.User.Identity.Name);
            storytimeseries.UsersNotified = false;
            db.Insert(storytimeseries);

            return Ok(storytimeseries);
        }

        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            var db = new PetaPoco.Database("AGSoftware");
            var db2 = new PetaPoco.Database("AGSoftware");
            System.Collections.Generic.List<Entities.StorytimePost> storytimepostlist = new List<Entities.StorytimePost>();

            string UserId = "";

            foreach (Entities.StorytimePost c in db.Query<Entities.StorytimePost>("Select * From StorytimePost Where SeriesId = @0 Order By DateCreated Desc", id))
            {
                c.ImagePath = Providers.ImageHelper.GetImagePath(c.ImagePath);
                c.ImagePath = c.ImagePath.Replace(@"\", @"/");
                UserId = Providers.UserHelper.GetUserId(this.User.Identity.Name);
                var voted  = db2.SingleOrDefault<Entities.Vote>("Select * From Vote Where StorytimePostId = @0 And UserId = @1", new object []{c.StorytimePostId, UserId});

                if (voted != null)
                    c.Voted = true;
                else
                    c.Voted = false;

                if (c.UserId == UserId)
                    c.UserPostedImage = true;

                c.PhoneNumber = Providers.UserHelper.GetPhoneNumber(this.User.Identity.Name);

                storytimepostlist.Add(c);
            }

            if (storytimepostlist.Count > 0)
                return Ok(storytimepostlist);
            else
                return NotFound();
        }
    }
}
