using System;
using System.Security.Cryptography.X509Certificates;
using EventsWebsite.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventsWebsite_UnitTest
{
    [TestClass]
    public class ToegangTests
    {
        [TestMethod]
        public void TestCheckin()
        {
            ToegangscontroleDatabase db = new ToegangscontroleDatabase();
            Assert.AreEqual(true, db.HasAccess(1),"Correct user has no access!");
            Assert.AreEqual(false,db.HasAccess(544923811),"Non existing user has access!");
        }
    }
}
