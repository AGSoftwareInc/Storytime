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

            var b = db.SingleOrDefault<Entities.Storytime>("Select * from Storytime Where StorytimeId = @0", id);

            if (b != null)
            {
                System.Collections.Generic.List<Entities.StorytimePost> storytimepostlist = new List<Entities.StorytimePost>();


                foreach (var c in db.Query<Entities.StorytimePost>("Select * from StorytimePost Where StorytimeId = @0", id))
                {
                    c.ImagePath = c.ImagePath.Replace("C:\\Storytime\\Web\\", "http://" + System.Configuration.ConfigurationManager.AppSettings["Server"] + @"\");
                    c.ImagePath = c.ImagePath.Replace(@"\", @"/");
                    storytimepostlist.Add(c);
                }
                return Ok(storytimepostlist);
            }
            else
                return NotFound();

        }
    }
}
