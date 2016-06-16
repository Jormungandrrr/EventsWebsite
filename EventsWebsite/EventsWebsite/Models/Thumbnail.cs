using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsWebsite.Models
{
    public class Thumbnail
    {  
        public int Id { get; private set; }
        public string ImgSource { get; private set; } 
        public string Title { get; private set; } 
        public string ActionName { get; private set; }
        public string ControllerName { get; private set; }
        public string Description { get; private set; } 
        public int Accesslevel { get; private set; }

        public Thumbnail(int id, string imgsrc, string title, string actionname, string controllername, string description, int accesslevel)
        {
            this.Id = id;
            this.ImgSource = imgsrc;
            this.ActionName = actionname;
            this.ControllerName = controllername;
            this.Description = description;
            this.Accesslevel = accesslevel;
            this.Title = title;
        } 

    }
}