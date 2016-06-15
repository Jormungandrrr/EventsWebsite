using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventsWebsite.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsWebsite.Models;

namespace EventsWebsite.Database.Tests
{
    [TestClass()]
    public class UserDBTests
    {
        UserDB UDB = new UserDB();

        [TestMethod()]
        public void InsertPersonTest()
        {
            UserModel User = new UserModel("Marc", "Marc@Marc.nl", "Marc","Van","Gool", 1, "MarcWeg", "69", "MarcCity");
            UDB.InsertPerson(User);
            Assert.IsTrue(UDB.Count("Persoon", "voornaam", "voornaam", User.Voornaam) > 0);
            Assert.IsTrue(UDB.Count("Account", "email", "email", User.Email) > 0);
        }

        [TestMethod()]
        public void GetPersonTest()
        {
            Assert.AreEqual(UDB.GetPerson("gebruiker").Voornaam, "naam");
        }
    }
}