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
        EventBeheerDB eventdb = new EventBeheerDB();


        public ActionResult Index()
        {
            List<EventModel> Events = eventdb.GetOngoingEvents();
            return View(Events);
        }

        public ActionResult Event(int ID)
        {
            EventModel Event = eventdb.GetEventById(ID);
            return View(Event);
        }

        // GET: /Reserveer/Reservering
        public ActionResult Reservering(int EventID)
        {
            return View();
        }
        // POST: /Reserveer/Reservering
        [HttpPost]
        public ActionResult Reservering(int EventID , ReserveringUsers Users)
        {
            List<string> Gebruikers = new List<string>();
            if (Users.Gebruikersnaam1 != "")
            {
                Gebruikers.Add(Users.Gebruikersnaam1);
            }

            if (Users.Gebruikersnaam2 != "")
            {
                Gebruikers.Add(Users.Gebruikersnaam2);
            }

            if (Users.Gebruikersnaam3 != "")
            {
                Gebruikers.Add(Users.Gebruikersnaam3);
            }

            if (Users.Gebruikersnaam4 != "")
            {
                Gebruikers.Add(Users.Gebruikersnaam4);
            }

            if (Users.Gebruikersnaam5 != "")
            {
                Gebruikers.Add(Users.Gebruikersnaam5);
            }

            if (Users.Gebruikersnaam6 != "")
            {
                Gebruikers.Add(Users.Gebruikersnaam6);
            }

            if (Users.Gebruikersnaam7 != "")
            {
                Gebruikers.Add(Users.Gebruikersnaam7);
            }

            int Aantal = 1 + Gebruikers.Count();

            int AccountID = (int)Session["Acountid"];
            int PersoonID = Convert.ToInt32(userdb.ReadStringWithCondition("persoon", "persoonid", "accountid", AccountID.ToString()));
            int ReserveringID = ResDB.InsertReservering(EventID, AccountID, PersoonID , Aantal);

            foreach (string Gebruiker in Gebruikers)
            {
                ResDB.Insertbandjes(ReserveringID, Gebruiker);
            }

            return View();
        }

    }
}