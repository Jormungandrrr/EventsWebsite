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


        // GET: /Reserveer/LoginReservering
        public ActionResult Reservering()
        {
            //AccountID = (int)Session["Acountid"];
            //int PersoonID = Convert.ToInt32(userdb.ReadStringWithCondition("account", "persoonid", "accountid", AccountID.ToString()));
            //int ReserveringID = ResDB.InsertReservering(EventID, AccountID, PersoonID);
            return View();
        }

        // POST: /Reserveer/LoginReservering
        [HttpPost]
        public ActionResult Reservering(ReserveringUsers Users)
        {
            int AccountID = (int)Session["Acountid"];
            //int PersoonID = userdb.GetPersoonIDByAccountID(AccountID);
            //int ReserveringID = ResDB.InsertReservering(EventID, AccountID, PersoonID , Aantal);

            //foreach (ReserveringUsers Gebruiker in Gebruikers)
            //{
            //    ResDB.Insertbandjes(ReserveringID, user.Accountid, user.PersoonId);
            //}

            return View();
        }

    }
}