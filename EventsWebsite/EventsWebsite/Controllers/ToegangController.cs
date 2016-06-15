using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventsWebsite.Controllers
{
    public class ToegangController : Controller
    {
        
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
    }
}