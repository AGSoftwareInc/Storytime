using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    public class GroupController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromUri] string id, [FromBody]Entities.UserGroup usergroup)
        {
            var db = new PetaPoco.Database("AGSoftware");
            usergroup.UserId = int.Parse(id);
            usergroup.DateCreated = System.DateTime.Now;
            db.Insert(usergroup);

            foreach (var a in db.Query<Entities.ContactList>("Select * from [User] Where UserId = @0", id))
            {
                //todo see if there is a better way to do this with normalization and/or one connection.
                var db2 = new PetaPoco.Database("AGSoftware");
                var b = db2.SingleOrDefault<Entities.User>("Select * from [User] Where UserId = @0", a.ContactId);
                contactlist.Add(b);
            }

            if (contactlist.Count > 0)
            {
                return Ok(contactlist);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
