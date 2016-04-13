using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Storytime.Providers
{
    public static class ImageHelper
    {
        public static string GetImagePath(string imagename)
        {
            return "http://" + ConfigurationManager.AppSettings["Server"] + @"\" + ConfigurationManager.AppSettings["UploadPath"] + @"\" + imagename;
        }
    }
}