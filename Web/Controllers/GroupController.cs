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

            Entities.UserGroupUser usergroupuser = null;

            foreach (Entities.User user in usergroup.Users)
            {
                //todo see if there is a better way to do this with normalization and/or one connection.
                var db2 = new PetaPoco.Database("AGSoftware");
                usergroupuser = new Entities.UserGroupUser();
                usergroupuser.GroupId = usergroup.UserGroupId;
                usergroupuser.UserId = db2.SingleOrDefault<Entities.User>("Select UserId from [USER] where PhoneNumber = @0", user.PhoneNumber).UserId; ;
                db2.Insert(usergroupuser);
            }

           return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            var db = new PetaPoco.Database("AGSoftware");

            var a = db.SingleOrDefault<Entities.UserGroup>("Select * from UserGroup Where UserGroupId = @0", id);

            if (a != null)
            {
                System.Collections.Generic.List<Entities.User> grouplist = new List<Entities.User>();

                foreach (var b in db.Query<Entities.ContactList>("Select * from UserGroupUser Where GroupId = @0", a.UserGroupId))
                {
                    //todo see if there is a better way to do this with normalization and/or one connection.
                    var db2 = new PetaPoco.Database("AGSoftware");
                    var c = db2.SingleOrDefault<Entities.User>("Select * from [User] Where UserId = @0", b.UserId);
                    grouplist.Add(c);
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
