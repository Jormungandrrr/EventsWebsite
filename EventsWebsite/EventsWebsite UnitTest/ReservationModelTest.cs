using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventsWebsite.Models;
namespace EventsWebsite_UnitTest
{
    [TestClass]
    public class ReservationModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            ReservationModel r = new ReservationModel(Convert.ToDateTime("2016-6-14"), Convert.ToDateTime("2016-6-15"), true, 1);
            Assert.AreEqual("Monitor", r.StartDate);
            Assert.AreEqual("24 inch monitor", r.EndDate);
            Assert.AreEqual(false, r.Paid);
            Assert.AreEqual(1, r.Reservationid);
        }
    }
}
