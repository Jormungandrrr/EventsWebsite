using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventsWebsite.Models;
namespace EventsWebsite_UnitTest
{
    [TestClass]
    public class MaterialModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            MaterialModel m = new MaterialModel("Monitor", "24 inch monitor", false, true);
            Assert.AreEqual("Monitor", m.Name);
            Assert.AreEqual("24 inch monitor", m.Description);
            Assert.AreEqual(false, m.Damaged);
            Assert.AreEqual(true, m.Rented);
        }
    }
}
