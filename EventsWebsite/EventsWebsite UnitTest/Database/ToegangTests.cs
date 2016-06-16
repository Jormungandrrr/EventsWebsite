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
        public void TestAccess()
        {
            ToegangscontroleDatabase db = new ToegangscontroleDatabase();
            Assert.AreEqual(true, db.HasAccess(554491472),"Correct user has no access!");
            Assert.AreEqual(false,db.HasAccess(544923811),"Non existing user has access!");
        }

        public void TestActivation()
        {
            ToegangscontroleDatabase db = new ToegangscontroleDatabase();
            int barcode = 554491472;
            db.UpdateTag(barcode,"1");
            Assert.AreEqual("1",db.ReadStringWithCondition("Polsbandje","Actief","barcode",barcode.ToString()),"Set 1 failed");
            db.UpdateTag(barcode,"0");
            Assert.AreEqual("0", db.ReadStringWithCondition("Polsbandje", "Actief", "barcode", barcode.ToString()), "Set 0 failed");
        }
    }
}
