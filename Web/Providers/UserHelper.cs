using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Storytime.Providers
{
    public static class UserHelper
    {
        public static string GetUserId(string username)
        {
            var db = new PetaPoco.Database("AGSoftware");

            var a = db.SingleOrDefault<Entities.AspNetUsers>("Select * from AspNetUsers Where UserName = @0", username);
            return a.Id;
        }
    }
}