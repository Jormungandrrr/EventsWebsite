using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsWebsite.Database;
using EventsWebsite.Models;

namespace EventsWebsite.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            DashboardDB database = new DashboardDB();
            int niveau = 4;// Convert.ToInt32(Session["Niveau"]);
            List<Thumbnail> thumbnails = database.GetThumbnails(niveau);
            return View(thumbnails);
        }
    }
}