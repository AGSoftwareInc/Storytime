using Entities;
using Storytime.Providers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;

namespace Storytime.Controllers
{
    [Authorize]
    [RoutePrefix("api/Story")]
    public class StoryController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromUri] string id, [FromBody]Entities.Storytime storytime)
        {
            var db = new PetaPoco.Database("AGSoftware");
            storytime.DateCreated = System.DateTime.Now;
            storytime.UserId = Storytime.Providers.UserHelper.GetUserId(this.User.Identity.Name);
            db.Insert(storytime);

            if (storytime.StorytimeType == StorytimeType.Group)
            {
                StorytimeGroup storytimegroup = new StorytimeGroup();
                storytimegroup.StorytimeId = storytime.StorytimeId;
                storytimegroup.StorytimeGroupId = storytime.StorytimeGroupId;
                db.Insert(storytimegroup);

                return Ok(storytime.StorytimeId);
            }
            else if (storytime.StorytimeType == StorytimeType.User)
            {
                StorytimeUserList storytimeuserlist = new StorytimeUserList();

                foreach (AspNetUsers user in storytime.Users)
                {
                    storytimeuserlist.StorytimeId = storytime.StorytimeId;
                    storytimeuserlist.UserId = user.Id;
                    db.Insert(storytimeuserlist);
                }

                return Ok(storytime.StorytimeId);
            }
            else
                return BadRequest("StorytimeType is invalid");
        }


        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            var db = new PetaPoco.Database("AGSoftware");

            System.Collections.Generic.List<Entities.StorytimePost> storytimewinnerlist = new List<StorytimePost>();

            foreach (var a in db.Query<Entities.StorytimePost>("Select * From StoryTimePost Where StorytimeId = @0 Group By UserId, StorytimeId, SeriesId, StorytimePostId, PostText, ImagePath, Votes, DateCreated Order By Votes Desc", id))
            {
                storytimewinnerlist.Add(a);
            }

            if (storytimewinnerlist.Count > 0)
                return Ok(storytimewinnerlist);
            else
                return NotFound();
        }
    }
}