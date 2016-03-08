using Entities;
using Storytime.Providers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;

namespace Storytime.Controllers
{
    public class StoryController : ApiController
    {
        private readonly IBlobService _service = new BlobService();

        //[HttpPost]
        //public IHttpActionResult Post([FromUri] string id, [FromBody]Entities.StorytimePost storytimepost)
        //{
        //    storytimepost.DateCreated = System.DateTime.Now;
        //    var db = new PetaPoco.Database("AGSoftware");

        //    db.Insert(storytimepost);

        //    return Ok();
        //}
        [HttpPost]
        public async Task<HttpResponseMessage> UploadImage()
        {
            //pick file with name: file
            HttpPostedFile uploadedFile = HttpContext.Current.Request.Files["file"];
            if (uploadedFile == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            //retrieve the string with name value...
            String value = HttpContext.Current.Request.Form["value"] ?? ""; //adding empty string incase no content was recieved...
            string physicalPath = Path.Combine(Environment.CurrentDirectory, "App_Data/Images");
            uploadedFile.SaveAs(physicalPath);
            //validate and save/process the file as you wish...

            //return positive respnse...
            return await Task.FromResult(Request.CreateResponse(HttpStatusCode.OK, String.Format("Successfully Uploaded File. String recieved: {0}", value)));
        }
    }
}