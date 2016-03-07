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
    }
}
