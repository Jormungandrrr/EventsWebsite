using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsWebsite.Database;
using EventsWebsite.Models;

namespace EventsWebsite.Controllers
{
    public class SocialMediaController : Controller
    {
        private SocialMediaDB _database = new SocialMediaDB();
        // GET: SocialMedia
        public ActionResult Index()
        {
            return View(_database.getallposts());
        }

        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(SocialMediaMessageModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.FileUpload != null && model.FileUpload.ContentLength > 0)
                {
                    string uldir = "~/Uploads";
                    string filepath = Path.Combine(Server.MapPath(uldir), model.FileUpload.FileName);
                    string fileurl = Path.Combine(uldir, model.FileUpload.FileName);
                    model.FileUpload.SaveAs(filepath);
                    if (_database.AddAttachment(model, (int) Session["Acountid"], fileurl))
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    if (_database.AddMessage(model, (int) Session["Acountid"]))
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);
        }

        public ActionResult ShowFullPost(int id)
        {
            return View(_database.GetDetailView(id));
        }

        public ActionResult Reply()
        {
            throw new NotImplementedException();
        }
    }
}