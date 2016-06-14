using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventsWebsite.Models;
namespace EventsWebsite_UnitTest
{
    [TestClass]
    public class SocialMediaMessageModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            SocialMediaMessageModel s = new SocialMediaMessageModel("Hallo", Convert.ToDateTime("2016-6-15"), 1, "Gebruiker1");
            Assert.AreEqual("Hallo", s.Message);
            Assert.AreEqual("2016-6-15", s.UploadTime);
            Assert.AreEqual(1, s.Messageid);
            Assert.AreEqual("Gebruiker1", s.Username);
        }
    }
}
