using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsWebsite.Models;

namespace EventsWebsite.Database
{
    public class ReserveerDB : Database
    {
        public List<EventModel> GetEvents()
        {
            List<EventModel> Events = new List<EventModel>();



            EventModel Event = new EventModel();
            Events.Add(Event);
            return Events;
        }

        public EventModel GetEventByID(int ID)
        {
            EventModel Event = new EventModel();
            return Event;
        }

        public int InsertReservering(int EventID , int AccountID , int PersoonID)
        {
            return 1;

        }
        public void Insertbandjes(int ReserveringID, int AccountID, int PersoonID)
        {


        }


    }
}