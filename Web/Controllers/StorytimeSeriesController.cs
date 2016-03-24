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
        public IHttpActionResult Post([FromUri] string id)
        {
            var db = new PetaPoco.Database("AGSoftware");

            Entities.StorytimeSeries storytimeseries = new Entities.StorytimeSeries();
            storytimeseries.StorytimeId = int.Parse(id);
            storytimeseries.DateCreated = System.DateTime.Now;
            storytimeseries.UserId = Storytime.Providers.UserHelper.GetUserId(this.User.Identity.Name);
            db.Insert(storytimeseries);

            return Ok(storytimeseries);
        }

        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            return Ok(1);
        }
    }
}
