using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    [Authorize]
    public class ContactController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromUri] string id, [FromBody]System.Collections.Generic.List<Entities.Contact> contactlist)
        {
            if (contactlist != null)
            {
                var db = new PetaPoco.Database("AGSoftware");

                List<Entities.Contact> newcontactlist = new List<Entities.Contact>();

                foreach (Entities.Contact contact in contactlist)
                {
                    var iscontact = db.SingleOrDefault<Entities.AspNetUsers>("Select * From ASPNetUsers Where PhoneNumber = @0", contact.PhoneNumber);

                    if (iscontact != null)
                    {
                        newcontactlist.Add(new Entities.Contact(iscontact.Id, iscontact.PhoneNumber, contact.FirstName, contact.LastName));
                    }
                }

                return Ok(newcontactlist);
            }
            else
            {
                return BadRequest("Contact List was empty");
            }
        }
    }
}
