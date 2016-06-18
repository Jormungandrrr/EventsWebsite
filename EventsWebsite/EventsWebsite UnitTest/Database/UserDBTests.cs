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
            UserModel User = new UserModel("Billie", "Bill@gaat.nl", "Bill", "Van", "Gaat", 1, "Billyweg", 69, "A", "Billcity");
            UDB.InsertPerson(User);
            Assert.IsTrue(UDB.Count("Persoon", "voornaam", "voornaam", User.Voornaam) > 0);
            Assert.IsTrue(UDB.Count("Account", "email", "email", User.Email) > 0);
        }

        [TestMethod()]
        public void GetPersonTest()
        {
            Assert.AreEqual(UDB.GetPerson("Mvg013").Voornaam, "Marc");
        }

        [TestMethod()]
        public void UpdateUserTest()
        {
            UserModel testuser = UDB.GetPerson("Mvg013");
            testuser.Email = "Marc@Marc.com";
            UDB.UpdateUser(testuser);
            Assert.AreEqual(UDB.GetPerson("Mvg013").Email, "Marc@Marc.com");
        }

        [TestMethod()]
        public void GetAllUsersTest()
        {
            int usercount = 0;
            foreach (UserModel user in UDB.GetAllUsers())
            {
                usercount++;
            }
            Assert.AreEqual(12, usercount);
        }
    }
}