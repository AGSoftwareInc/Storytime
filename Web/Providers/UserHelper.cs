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
            return GetUser(username).Id;
        }

        public static string GetPhoneNumber(string username)
        {
            return GetUser(username).PhoneNumber;
        }

        public static string GetPhoneNumberById(string id)
        {
            return GetUserById(id).PhoneNumber;
        }

        private static Entities.AspNetUsers GetUser(string username)
        {
            var db = new PetaPoco.Database("AGSoftware");

            var a = db.SingleOrDefault<Entities.AspNetUsers>("Select * from AspNetUsers Where UserName = @0", username);

            return a;
        }

        private static Entities.AspNetUsers GetUserById(string id)
        {
            var db = new PetaPoco.Database("AGSoftware");

            var a = db.SingleOrDefault<Entities.AspNetUsers>("Select * from AspNetUsers Where Id = @0", id);

            return a;
        }
    }
}