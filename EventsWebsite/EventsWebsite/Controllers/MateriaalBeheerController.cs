using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventsWebsite.Controllers
{
    public class MateriaalBeheerController : Controller
    {
        // GET: MateriaalBeheer
        public ActionResult Index()
        {
            if (Session.Count > 0)
            {
                if ((int)Session["Niveau"] > 3)
                {
                    return View();
                }
            }
            return View();
        }
    }
}