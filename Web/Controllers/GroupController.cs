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
                usergroupuser.UserGroupId = usergroup.UserGroupId;
                usergroupuser.UserId = db2.SingleOrDefault<Entities.AspNetUsers>("Select Id from AspNetUsers where Id = @0", user.Id).Id; ;
                db2.Insert(usergroupuser);
            }

           return Ok(usergroup.UserGroupId);
        }

        [HttpGet]
        public IHttpActionResult Get(string page, string count)
        {
            var db = new PetaPoco.Database("AGSoftware");
            string userid = Storytime.Providers.UserHelper.GetUserId(this.User.Identity.Name);

            var b = db.Page<Entities.UserGroup>(int.Parse(page), int.Parse(count), "Select * from UserGroup Where UserId = @0 order by DateCreated Desc", new object[] { userid });

            if (b.Items.Count > 0)
            {
                return Ok(b);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
