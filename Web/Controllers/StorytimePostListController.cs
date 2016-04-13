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
    public class StorytimePostListController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            var db = new PetaPoco.Database("AGSoftware");

            System.Collections.Generic.List<Entities.StorytimePost> storytimepostlist = new List<Entities.StorytimePost>();

            foreach (Entities.StorytimePost c in db.Query<Entities.StorytimePost>("Select * From StorytimePost Where StorytimeId = @0", id))
            {
                c.ImagePath = c.ImagePath.Replace(ConfigurationManager.AppSettings["UploadPath"], "http://" + System.Configuration.ConfigurationManager.AppSettings["Server"] + @"\");
                c.ImagePath = c.ImagePath.Replace(@"\", @"/");

                storytimepostlist.Add(c);
            }

            if (storytimepostlist.Count > 0)
                return Ok(storytimepostlist);
            else
                return NotFound();
        }
    }
}