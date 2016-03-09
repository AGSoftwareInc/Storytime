using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    [Authorize]
    public class ContactListController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromUri] string id, [FromBody]Entities.Contact contact)
        {
            var db = new PetaPoco.Database("AGSoftware");

            var a = db.SingleOrDefault<Entities.AspNetUsers>("Select * from AspNetUsers Where PhoneNumber = @0", contact.PhoneNumber);

            if (a != null)
            {
                Entities.ContactList contactlist = new Entities.ContactList();
                contactlist.UserId = Storytime.Providers.UserHelper.GetUserId(this.User.Identity.Name);
                contactlist.ContactId = a.Id;
                contactlist.DateCreated = System.DateTime.Now;

                db.Insert(contactlist);

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            var db = new PetaPoco.Database("AGSoftware");

            System.Collections.Generic.List<Entities.AspNetUsers> contactlist = new List<Entities.AspNetUsers>();

            foreach (var a in db.Query<Entities.ContactList>("Select * from ContactList Where UserId = @0", Storytime.Providers.UserHelper.GetUserId(this.User.Identity.Name)))
            {
                //todo see if there is a better way to do this with normalization and/or one connection.
                var db2 = new PetaPoco.Database("AGSoftware");
                var b = db2.SingleOrDefault<Entities.AspNetUsers>("Select * from AspNetUsers Where Id = @0", a.ContactId);
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
