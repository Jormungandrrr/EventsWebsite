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
                {"Datumstart", model.DateStart.ToShortDateString()},
                {"Datumeinde", model.DateEnd.ToShortDateString()}
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
    }
}
