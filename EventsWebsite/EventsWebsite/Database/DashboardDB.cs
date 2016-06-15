using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventsWebsite.Models;

namespace EventsWebsite.Database
{
    public class DashboardDB : Database
    {
        public List<Thumbnail> GetThumbnails(int accesslevel)
        {
            List<string> all = new List<string> { "*" };
            List<Thumbnail> thumbnails = new List<Thumbnail>();
            List<object> o = new List<object>();
            thumbnails = GetThumbnails("thumbnail", all, "accesslevel", accesslevel.ToString(), "thumbnail");
            //o = ReadObjects("thumbnail", all, "accesslevel", accesslevel.ToString(), "thumbnail");

            return thumbnails;
        }
    }
}
