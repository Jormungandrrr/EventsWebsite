using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventsWebsite.Models;
namespace EventsWebsite_UnitTest
{
    [TestClass]
    public class UserModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            UserModel u = new UserModel("Gebruiker1", "test@test.com", "Henk","Van","Henkert", 3, "Straat", 12,"B", "Tilburg");
            Assert.AreEqual("Gebruiker1", u.Username);
            Assert.AreEqual("wachtwoord123", u.Password);
            Assert.AreEqual("test@test.com", u.Email);
            Assert.AreEqual("Henk", u.Voornaam);
            Assert.AreEqual(3, u.AccesLevel);
            Assert.AreEqual("Straat", u.Street);
            Assert.AreEqual("12a", u.HouseNumber);
            Assert.AreEqual("Tilburg", u.City);
        }
    }
}
