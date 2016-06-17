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
    }
}
