using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsWebsite.Models;

namespace EventsWebsite.Database
{
    public class DashboardDB : Database
    {
        public List<Thumbnail> GetThumbnails(int accesslevel)
        {
            List<string> all = new List<string> {"*"};
            List<Thumbnail> thumbnails =  new List<Thumbnail>();
            thumbnails = (List<Thumbnail>) ReadObjects("thumbnail", all, "accesslevel", accesslevel.ToString(), "thumbnail").Cast<Thumbnail>();
            return thumbnails;
        }
    }
}
