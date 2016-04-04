using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    [Authorize]
    public class SeriesListController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            var db = new PetaPoco.Database("AGSoftware");

            System.Collections.Generic.List<Entities.StorytimeSeries> storytimeserieslist = new List<Entities.StorytimeSeries>();

            foreach (Entities.StorytimeSeries c in db.Query<Entities.StorytimeSeries>("Select * From StorytimeSeries Where StorytimeId = @0", id))
            {
                storytimeserieslist.Add(c);
            }

            if (storytimeserieslist.Count > 0)
            {
                return Ok(storytimeserieslist);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
