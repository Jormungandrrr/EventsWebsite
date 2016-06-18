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
            database.ReserveMaterial(number, "06-06-2016");
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
            
            database.ReturnMaterial(database.GetMaterial(1), DateTime.Now.ToString());
            return View("HiredMaterials", database.GetAllHiredMaterial(1));
        }
    }
}