using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsWebsite.Models;

namespace EventsWebsite.Database
{
    public class EventBeheerDB : Database
    {
        public bool AddEvent(EventModel model)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"Naam", model.Name},
                {"Datumstart", model.DateStart.ToString("dd/MMM/yyyy")},
                {"Datumeinde", model.DateEnd.ToString("dd/MMM/yyyy")}
            };
            Dictionary<string, string> locationdata = new Dictionary<string, string>
            {
                {"Naam",model.Name},
                {"Straat",model.Street },
                {"postcode",model.Zipcode },
                {"Plaats",model.City },
                {"nr",model.HouseNumber}
            };
            try
            {
                Insert("Locatie", locationdata);
                string locatieid = ReadStringWithCondition("Locatie", "Locatieid", "naam", model.Name);
                data.Add("locatieid", locatieid);
                Insert("Event", data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteEvent(int EventID)
        {
            bool success = false;
            try
            {
                success = Delete("Event", "EventID", EventID.ToString());
                
            }
            catch
            {
            }
            return success;
        }

        public List<EventModel> GetEvents()
        {
            return GetAllEvents();
        }
        public List<EventModel> GetOngoingEvents()
        {
            List<string> data = new List<string>();
            List<EventModel> events = new List<EventModel>();
            data.Add("eventid");
            data.Add("naam");
            data.Add("datumstart");
            data.Add("datumeinde");
            data.Add("maxbezoekers");
            data.Add("locatieid");
            foreach (EventModel e in ReadObjects("Event", data, "datumeinde < " + "'" + DateTime.Now.ToString("dd/MMM/yyyy") + "'", "Event"))
            {
               int bezoekers = Count("reservering r join plek_reservering pr on r.reserveringid = pr.reserveringid join plek p on pr.plekid = p.plekid join locatie l on p.locatieid = l.locatieid join event e on l.locatieid = e.locatieid", "*", "e.eventid", e.EventID.ToString());
                if (bezoekers < e.MaxBezoekers)
                {
                    EventModel evenement = GetEventLocation(e);
                    events.Add(evenement);
                }
            }
            return events;
        }

        public EventModel GetEventById(int eventid)
        {
            List<string> eventdata = new List<string>();
            eventdata.Add("eventid");
            eventdata.Add("naam");
            eventdata.Add("datumstart");
            eventdata.Add("datumeinde");
            eventdata.Add("maxbezoekers");
            eventdata.Add("locatieid");
            EventModel e = (EventModel)ReadObjectWithCondition("Event", eventdata, "eventid",eventid.ToString(),"Event");
            e = GetEventLocation(e);
            return e;
        }

        private EventModel GetEventLocation(EventModel e)
        {
            e.LocationName = ReadStringWithCondition("Locatie", "naam", "locatieid", e.LocationID.ToString());
            e.Street = ReadStringWithCondition("Locatie", "straat", "locatieid", e.LocationID.ToString());
            e.HouseNumber = ReadStringWithCondition("Locatie", "nr", "locatieid", e.LocationID.ToString());
            e.Zipcode = ReadStringWithCondition("Locatie", "postcode", "locatieid", e.LocationID.ToString());
            e.City = ReadStringWithCondition("Locatie", "plaats", "locatieid", e.LocationID.ToString());
            return e;
        }
    }
}
