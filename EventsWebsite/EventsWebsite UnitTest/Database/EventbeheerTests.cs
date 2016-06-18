using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsWebsite.Database;
using EventsWebsite.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventsWebsite.Database.Tests
{
    [TestClass()]
    public class EventbeheerTests
    {
        [TestMethod()]
        public void GetOngoingEventsTest()
        {
            int countevents = 0;
            List<EventModel> events = new List<EventModel>();
            EventBeheerDB edb = new EventBeheerDB();
            foreach (EventModel em in edb.GetOngoingEvents())
            {
                countevents++;
            }
            Assert.AreEqual(1,countevents);
        }
    }
}

namespace EventsWebsite_UnitTest.Database
{
    [TestClass]
    public class EventbeheerTests
    {
        EventBeheerDB db = new EventBeheerDB();
        [TestMethod]
        public void TestAddEvent()
        {
            EventModel add = new EventModel("FapFest",DateTime.Today,DateTime.Today, "Straatstaat","12","1234AA","StraatStraat","LandLand" );
            db.AddEvent(add);
            Assert.AreEqual("FapFest",db.ReadStringWithCondition("Event","naam","Datumstart",DateTime.Today.ToShortDateString()));
        }

        [TestMethod]
        public void TestDeleteEvent()
        {
            int eventid;
            string dbid = db.ReadStringWithCondition("Event", "EventID", "Naam", "FapFest");
            int.TryParse(dbid, out eventid);
            db.DeleteEvent(eventid);
            Assert.AreEqual(0,db.Count("Event","EventID","Naam","FapFest"));
        }
    }
}