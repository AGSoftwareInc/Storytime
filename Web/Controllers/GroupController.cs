using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    [Authorize]
    public class GroupController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromUri] string id, [FromBody]Entities.UserGroup usergroup)
        {
            var db = new PetaPoco.Database("AGSoftware");
            usergroup.UserId = Storytime.Providers.UserHelper.GetUserId(this.User.Identity.Name);
            usergroup.DateCreated = System.DateTime.Now;
            db.Insert(usergroup);

            Entities.UserGroupUser usergroupuser = null;

            foreach (Entities.AspNetUsers user in usergroup.Users)
            {
                //todo see if there is a better way to do this with normalization and/or one connection.
                var db2 = new PetaPoco.Database("AGSoftware");
                usergroupuser = new Entities.UserGroupUser();
                usergroupuser.GroupId = usergroup.UserGroupId;
                usergroupuser.UserId = db2.SingleOrDefault<Entities.AspNetUsers>("Select Id from AspNetUsers where PhoneNumber = @0", user.PhoneNumber).Id; ;
                db2.Insert(usergroupuser);
            }

           return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            var db = new PetaPoco.Database("AGSoftware");

            var b = db.SingleOrDefault<Entities.UserGroup>("Select * from UserGroup Where UserGroupId = @0", id);

            if (b != null)
            {
                System.Collections.Generic.List<Entities.AspNetUsers> grouplist = new List<Entities.AspNetUsers>();

                foreach (var c in db.Query<Entities.ContactList>("Select * from UserGroupUser Where GroupId = @0", b.UserGroupId))
                {
                    //todo see if there is a better way to do this with normalization and/or one connection.
                    var db2 = new PetaPoco.Database("AGSoftware");
                    var d = db2.SingleOrDefault<Entities.AspNetUsers>("Select * from AspNetUsers Where Id = @0", c.UserId);
                    grouplist.Add(d);
                }

                return Ok(grouplist);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
