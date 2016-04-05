using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    [Authorize]
    public class VoteController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromUri] string id)
        {
            var db = new PetaPoco.Database("AGSoftware");
            var vote = new Entities.Vote();
            vote.StorytimePostId = int.Parse(id);
            vote.DateCreated = System.DateTime.Now;
            vote.UserId = Storytime.Providers.UserHelper.GetUserId(this.User.Identity.Name);
            db.Insert(vote);

            var storytimepost = db.SingleOrDefault<Entities.StorytimePost>("Select * from StorytimePost Where StorytimePostId = @0", id);
            storytimepost.Votes = storytimepost.Votes + 1;
            db.Update(storytimepost);

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get(string id, string seriesid)
        {
            var db = new PetaPoco.Database("AGSoftware");

            System.Collections.Generic.List<Entities.StorytimePost> storytimepostlist = new List<Entities.StorytimePost>();

            foreach(Entities.StorytimePost a in db.Query<Entities.StorytimePost>("Select stp1.* From StorytimePost stp1 Where stp1.StorytimeId = @0 And stp1.SeriesId = @1 And stp1.Votes = (Select MAX(stp2.Votes) From StorytimePost stp2 Where stp2.StorytimeId = stp1.StorytimeId)", new object [] {id,seriesid}))
            {
                a.ImagePath = a.ImagePath.Replace("C:\\Storytime\\Web\\", "http://" + System.Configuration.ConfigurationManager.AppSettings["Server"] + @"\");
                a.ImagePath = a.ImagePath.Replace(@"\", @"/");
                storytimepostlist.Add(a);
            }

            if (storytimepostlist.Count > 0)
                return Ok(storytimepostlist);
            else
                return NotFound();
        }
    }
}