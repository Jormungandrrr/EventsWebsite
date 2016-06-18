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
            AccountID = (int)Session["Acountid"];
            int PersoonID = userdb.GetPersoonIDByAccountID(AccountID);
            int ReserveringID = ResDB.InsertReservering(EventID, AccountID, PersoonID);

            List<UserModel> Users = new List<UserModel>();

            foreach (UserModel user in Users)
            {
                ResDB.Insertbandjes(ReserveringID, user.Accountid, user.PersoonId);
            }

            return View();
        }

    }
}