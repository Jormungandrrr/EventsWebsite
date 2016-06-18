using EventsWebsite.Database;
using EventsWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventsWebsite.Controllers
{
    public class ReserveerController : Controller
    {
        ReserveerDB ResDB = new ReserveerDB();
        UserDB userdb = new UserDB();


        public ActionResult Index()
        {
            List<EventModel> Events = ResDB.GetEvents();
            return View(Events);
        }

        public ActionResult Event(int ID)
        {
            EventModel Event = ResDB.GetEventByID(ID);
            return View(Event);
        }

        public ActionResult Reservering(int AccountID , int EventID)
        {

            int persoonid = userdb.GetPersoonIDByAccountID(AccountID);
            AccountID = (int)Session["Acountid"];

            return View();
        }

    }
}