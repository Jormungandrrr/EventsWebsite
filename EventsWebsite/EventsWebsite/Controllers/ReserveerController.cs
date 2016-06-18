using EventsWebsite.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventsWebsite.Controllers
{
    public class ReserveerController : Controller
    {
        // GET: Reserveer
        public ActionResult Index()
        {
            ReserveerDB ResDB = new ReserveerDB();
            return View();
        }

        // GET: Reserveer
        public ActionResult Event()
        {
            return View();
        }
    }
}