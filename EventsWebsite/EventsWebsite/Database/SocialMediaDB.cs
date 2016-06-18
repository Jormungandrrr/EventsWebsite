using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using EventsWebsite.Models;

namespace EventsWebsite.Database
{
    class SocialMediaDB : Database
    {
        public bool AddMessage(SocialMediaMessageModel model, int uid)
        {
            return AddMessage("BijdrageBericht", model.Title, model.Message, uid);
        }

        public bool AddAttachment(SocialMediaMessageModel model, int i, string fileurl)
        {
            return AddFile("BijdrageBestand", fileurl, model.FileUpload.ContentLength, i);
        }
    }
}
