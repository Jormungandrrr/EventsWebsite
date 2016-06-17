using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventsWebsite.Models;

namespace EventsWebsite_UnitTest
{
    [TestClass]
    public class ConstructorTests
    {
        [TestMethod]
        public void MaterialTest()
        {
            MaterialModel m = new MaterialModel(3, 14);
            Assert.AreEqual(3, m.Number);
            Assert.AreEqual(14, m.Barcode);
        }

        [TestMethod]
        public void UserTest()
        {
            UserModel u = new UserModel("Henkie1", "test@test.com", "Henk", "van", "blabla", 3, "TestStraat", 12, "a", "Tilburg");
            Assert.AreEqual("Henkie1", u.Username);
            Assert.AreEqual("test@test.com", u.Email);
            Assert.AreEqual("Henk", u.Voornaam);
            Assert.AreEqual("van", u.tussenvoegsel);
            Assert.AreEqual("blabla", u.Achternaam);
            Assert.AreEqual(3, u.AccesLevel);
            Assert.AreEqual("TestStraat", u.City);
            Assert.AreEqual(12, u.HouseNumber);
            Assert.AreEqual("a", u.Toevoeging);
            Assert.AreEqual("Tilburg", u.City);
        }

        [TestMethod]
        public void SocialMediaMessageTest()
        {
            SocialMediaMessageModel s = new SocialMediaMessageModel("Hallo", Convert.ToDateTime("2016-6-15"), 1, "Gebruiker1");
            Assert.AreEqual("Hallo", s.Message);
            Assert.AreEqual("2016-6-15", s.UploadTime);
            Assert.AreEqual(1, s.Messageid);
            Assert.AreEqual("Gebruiker1", s.Username);
        }

        [TestMethod]
        public void ReservationTest()
        {
            ReservationModel r = new ReservationModel(Convert.ToDateTime("2016-6-14"), Convert.ToDateTime("2016-6-15"), true, 1);
            Assert.AreEqual("Monitor", r.StartDate);
            Assert.AreEqual("24 inch monitor", r.EndDate);
            Assert.AreEqual(false, r.Paid);
            Assert.AreEqual(1, r.Reservationid);
        }

        [TestMethod]
        public void EventTest()
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
