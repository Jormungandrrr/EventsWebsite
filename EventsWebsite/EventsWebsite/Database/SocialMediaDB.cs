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

        public List<SocialMediaMessageModel> getallposts()
        {
            return AllPosts();
        }

        public List<SocialMediaMessageModel> GetDetailView(int i)
        {
            return DetailPost(i);
        }
        public bool Reply(SocialMediaMessageModel model, int uid, int msg)
        {
            return AddReply("BijdrageReactie", model.Title, model.Message, uid, msg);
        }
    }
}
