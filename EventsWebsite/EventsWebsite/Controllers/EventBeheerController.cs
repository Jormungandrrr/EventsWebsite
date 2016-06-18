using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsWebsite.Database;
using EventsWebsite.Models;

namespace EventsWebsite.Controllers
{
    public class EventBeheerController : Controller
    {
        private EventBeheerDB _database = new EventBeheerDB();
        // GET: EventBeheer
        public ActionResult Index()
        {
            return View(_database.GetEvents());
        }

        public ActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEvent(EventModel model)
        {
            if (ModelState.IsValid)
            {
                if (_database.AddEvent(model))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public ActionResult DeleteEvent(int id)
        {
            _database.DeleteEvent(id);
            return RedirectToAction("Index");
        }
    }
}