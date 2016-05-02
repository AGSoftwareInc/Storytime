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
    public class StoryListController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string page, string count)
        {
            var db = new PetaPoco.Database("AGSoftware");
            string userid = Storytime.Providers.UserHelper.GetUserId(this.User.Identity.Name);
            
            //var b = db.Page<Entities.Storytime>(int.Parse(page), int.Parse(count), "Select * from Storytime Where UserId = @0 UNION Select st.* from Storytime st inner join StorytimeGroup stg on st.StorytimeId = stg.StorytimeId Where stg.UserGroupId in (Select ug.UserGroupId From UserGroup ug inner join UserGroupUser ugu on ug.UserGroupId = ugu.UserGroupId Where ugu.UserId = @0) UNION Select st.* from Storytime st inner join StorytimeUserList stul on st.StorytimeId = stul.StorytimeId Where stul.UserId = @0", new object[] { userid });

            int offset = (int.Parse(page) - 1) * int.Parse(count);

            var b = db.Page<Entities.Storytime>(int.Parse(page), int.Parse(count), "with c as (Select st.* from Storytime st Where UserId = @0 UNION Select st.* from Storytime st inner join StorytimeGroup stg on st.StorytimeId = stg.StorytimeId Where stg.UserGroupId in (Select ug.UserGroupId From UserGroup ug inner join UserGroupUser ugu on ug.UserGroupId = ugu.UserGroupId Where ugu.UserId = @0) UNION Select st.* from Storytime st inner join StorytimeUserList stul on st.StorytimeId = stul.StorytimeId Where stul.UserId = @0) select count(*) from c", new object[] { userid }, "Select * From (Select st.* from Storytime st Where UserId = @0 UNION Select st.* from Storytime st inner join StorytimeGroup stg on st.StorytimeId = stg.StorytimeId Where stg.UserGroupId in (Select ug.UserGroupId From UserGroup ug inner join UserGroupUser ugu on ug.UserGroupId = ugu.UserGroupId Where ugu.UserId = @0) UNION Select st.* from Storytime st inner join StorytimeUserList stul on st.StorytimeId = stul.StorytimeId Where stul.UserId = @0) as result order by DateCreated Desc Offset " + offset + " Rows Fetch Next " + count + " Rows Only", new object[] { userid });
            //var b = db.Page<Entities.Storytime>(int.Parse(page), int.Parse(count), "Select st.* from Storytime st Where UserId = @0 UNION Select st.* from Storytime st inner join StorytimeGroup stg on st.StorytimeId = stg.StorytimeId Where stg.UserGroupId in (Select ug.UserGroupId From UserGroup ug inner join UserGroupUser ugu on ug.UserGroupId = ugu.UserGroupId Where ugu.UserId = @0) UNION Select st.* from Storytime st inner join StorytimeUserList stul on st.StorytimeId = stul.StorytimeId Where stul.UserId = @0 group by st.datecreated, st.storytimeid, st.storytimetitle, st.storytimetypeid, st.userid order by st.datecreated desc, st.storytimeid, st.storytimetitle, st.storytimetypeid, st.userid", new object[] { userid });


            if (b.Items.Count > 0)
                return Ok(b);
            else
                return NotFound();
        }
    }
}
