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
            UserModel u = new UserModel("Gebruiker1", "wachtwoord123", "wachtwoord123", "test@test.com", "Henk", 061234567, 3, "Straat", "12a", "5050AA", "Tilburg", "Nederland");
            Assert.AreEqual("Gebruiker1", u.Username);
            Assert.AreEqual("wachtwoord123", u.Password);
            Assert.AreEqual("wachtwoord123", u.PasswordCheck);
            Assert.AreEqual("test@test.com", u.Email);
            Assert.AreEqual("Henk", u.Name);
            Assert.AreEqual(061234567, u.Telnr);
            Assert.AreEqual(3, u.AccesLevel);
            Assert.AreEqual("Straat", u.Street);
            Assert.AreEqual("12a", u.HouseNumber);
            Assert.AreEqual("5050AA", u.Zipcode);
            Assert.AreEqual("Tilburg", u.City);
            Assert.AreEqual("Nederland", u.Country);
        }
    }
}
