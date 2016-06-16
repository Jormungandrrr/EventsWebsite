using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventsWebsite.Models;
namespace EventsWebsite_UnitTest
{
    [TestClass]
    public class EventModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            EventModel e = new EventModel("EyeCT4Events", Convert.ToDateTime("2016-6-14"), Convert.ToDateTime("2016-6-15"), "straat", "2", "5050AA", "tilburg", "Nederland");
            Assert.AreEqual("EyeCT4Events", e.Name);
            Assert.AreEqual("2016-6-14", e.DateStart);
            Assert.AreEqual("2016-6-14", e.DateEnd);
            Assert.AreEqual("straat", e.Street);
            Assert.AreEqual("2", e.HouseNumber);
            Assert.AreEqual("5050AA", e.Zipcode);
            Assert.AreEqual("tilburg", e.City);
            Assert.AreEqual("Nederland", e.Country);
        }
    }
}
