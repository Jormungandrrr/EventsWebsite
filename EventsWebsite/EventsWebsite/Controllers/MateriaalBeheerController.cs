using EventsWebsite.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventsWebsite.Controllers
{
    public class MateriaalBeheerController : Controller
    {
        private MateriaalverhuurDB database = new MateriaalverhuurDB();

        // GET: MateriaalBeheer
        public ActionResult Index()
        {
            if (Session.Count > 0)
            {
                    return View(database.GetAllFreeMaterial());
            }
            return View(database.GetAllFreeMaterial());
        }

        public ActionResult RentMaterial(int number)
        {
            database.ReserveMaterial(number, DateTime.Now.ToString("d"));
            return RedirectToAction("Index", database.GetAllFreeMaterial());
        }

        public ActionResult HiredMaterials()
        {
            if (Session.Count > 0)
            {
                return View(database.GetAllHiredMaterial(1));
            }
            return View(database.GetAllHiredMaterial(1));
        }

        public ActionResult ReturnMaterial(int number)
        {
            string exemplaarid = database.ReadStringWithCondition("EXEMPLAAR", "ExemplaarID", "Volgnummer", number.ToString());
            string condition1 = exemplaarid;
            string condition2 = "datumin";
            string verhuurid = database.ReadStringWith2Conditions("VERHUUR", "verhuurid", "ExemplaarID", condition1, condition2);
            database.ReturnMaterial(Convert.ToInt32(verhuurid), DateTime.Now.Date.ToString("d"));
            return View("HiredMaterials", database.GetAllHiredMaterial(1));
        }
    }
}