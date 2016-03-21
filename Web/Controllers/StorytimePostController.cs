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
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Upload"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

                var db = new PetaPoco.Database("AGSoftware");

                Entities.StorytimePost storytimepost = new Entities.StorytimePost();
                storytimepost.DateCreated = System.DateTime.Now;
                storytimepost.ImagePath = path;
                storytimepost.PostText = HttpContext.Current.Request.Form["posttext"];
                storytimepost.UserId = Storytime.Providers.UserHelper.GetUserId(HttpContext.Current.User.Identity.Name);
                storytimepost.StorytimeId = int.Parse(HttpContext.Current.Request.Form["storytimeid"]);

                db.Insert(storytimepost);

                return Ok(storytimepost.StorytimePostId);
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

            var a = db.SingleOrDefault<Entities.StorytimePost>("Select * from StorytimePost Where StorytimePostId = @0", id);
            return Ok(a);
        }
    }
}