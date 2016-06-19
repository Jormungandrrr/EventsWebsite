using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
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
            if (Session["niveau"] != null)
            {
                return View(_database.getallposts());
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult CreatePost()
        {
            if (Session["niveau"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
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
                        return RedirectToAction("Index","SocialMedia");
                    }
                }
            }
            return View(model);
        }

        public ActionResult ShowFullPost(int id)
        {
            if (Session["niveau"] != null)
            {
                return View(_database.GetDetailView(id));
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Reply(int id)
        {
            if (Session["niveau"] != null)
            {
                SocialMediaMessageModel send = new SocialMediaMessageModel();
                send.Messageid = id;
                return View(send);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply(SocialMediaMessageModel model)
        {
            if (ModelState.IsValid)
            {
                if (_database.Reply(model, Session["Acountid"]))
                {
                    return RedirectToAction("ShowFullPost", new {id = model.Messageid});
                }
            }
            return View(model);
        }
    }
}