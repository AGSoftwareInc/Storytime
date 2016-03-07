﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities;

namespace Storytime.Controllers
{
    public class PasswordController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromUri] string id, [FromBody]Entities.Login login)
        {
            var db = new PetaPoco.Database("AGSoftware");


            var a = db.SingleOrDefault<Entities.User>("Select * from [User] Where Email = @0", login.Email);

            Email email = new Email(ConfigurationManager.AppSettings["EmailHost"]);
            email.To.Add(login.Email);
            email.From = "info@agsoftwareinc.com";
            email.Subject = "Username/Password";
            email.Body = "<p><strong>Hello</strong></p><p>Username: " + login.Username + "</p><p>Password: " + login.Password + "</p>";
            email.Send();

            if (a != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}