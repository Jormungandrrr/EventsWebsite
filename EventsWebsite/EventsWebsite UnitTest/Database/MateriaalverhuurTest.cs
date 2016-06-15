using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventsWebsite.Database;
using EventsWebsite.Models;
using System.Collections.Generic;

namespace EventsWebsite_UnitTest
{
    [TestClass]
    public class MateriaalverhuurTest
    {
        [TestMethod]
        public void ReserveMaterialTest()
        {
            MateriaalverhuurDB m = new MateriaalverhuurDB();
            m.ReserveMaterial(2, "10-11-2016");
            Assert.AreEqual("2", m.ReadStringWithCondition("VERHUUR", "verhuurID", "verhuurID", "2"));
        }

        [TestMethod]
        public void ReturnMaterialTest()
        {
            MateriaalverhuurDB m = new MateriaalverhuurDB();
            m.ReturnMaterial(1, "10-11-2016");
            Assert.AreEqual("10-11-2016 00:00:00", m.ReadStringWithCondition("VERHUUR", "datumin", "verhuurID", "1"));
        }

        [TestMethod]
        public void GetAllFreeMaterial()
        {
            
        }
    }
}
