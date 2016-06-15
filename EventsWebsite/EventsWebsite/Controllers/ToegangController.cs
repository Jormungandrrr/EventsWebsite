using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsWebsite.Database;

namespace EventsWebsite.Controllers
{
    public class ToegangController : Controller
    {
        private ToegangscontroleDatabase _db = new ToegangscontroleDatabase();
        
        public ActionResult Index()
        {
            if (Session.Count > 0)
            {
                if ((int) Session["Admin"] == 2 || (int) Session["Admin"] == 4)
                {
                    return View();
                }
            }
            //return RedirectToAction("Index", "Home"); 
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            int bar;
            string barcode = form.Get(0);
            if (int.TryParse(barcode, out bar))
            {
                bool granted = _db.HasAccess(bar);
                if (granted)
                {
                    return RedirectToAction("Granted", "Toegang");
                }
                return RedirectToAction("Denied", "Toegang");
            }
            return View();
        }
    }
}