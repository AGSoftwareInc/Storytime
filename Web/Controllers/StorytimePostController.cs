using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using System.Web.Http;

namespace Storytime.Controllers
{
    [Authorize]
    public class StorytimePostController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post()
        {
            var file = HttpContext.Current.Request.Files[0];

            if (file != null)
            {
                string filename = Guid.NewGuid().ToString() + file.FileName;
                string pic = System.IO.Path.GetFileName(filename);
                string path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Upload"), pic);
                // file is uploaded
                file.SaveAs(path);

                var db = new PetaPoco.Database("AGSoftware");

                Entities.StorytimePost storytimepost = new Entities.StorytimePost();
                storytimepost.DateCreated = System.DateTime.Now;
                storytimepost.ImagePath = filename;
                storytimepost.PostText = HttpContext.Current.Request.Form["posttext"];
                storytimepost.UserId = Storytime.Providers.UserHelper.GetUserId(HttpContext.Current.User.Identity.Name);
                storytimepost.StorytimeId = int.Parse(HttpContext.Current.Request.Form["storytimeid"]);
                storytimepost.SeriesId = int.Parse(HttpContext.Current.Request.Form["SeriesId"]);

                db.Insert(storytimepost);

                return Ok(storytimepost.StorytimePostId);
            }
            else
            {
                return BadRequest("File upload missing.");
            }
        }

        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            var db = new PetaPoco.Database("AGSoftware");

            var a = db.SingleOrDefault<Entities.StorytimePost>("Select * from StorytimePost Where StorytimePostId = @0  Order By DateCreated Desc", id);

            if (a != null)
            {
                a.ImagePath = Providers.ImageHelper.GetImagePath(a.ImagePath);
                a.ImagePath = a.ImagePath.Replace(@"\", @"/");

                return Ok(a);
            }
            else
            {
                return NotFound();
            }
        }
    }
}