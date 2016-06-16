using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using EventsWebsite.Models;
using EventsWebsite.Database;

namespace EventsWebsite.Controllers
{
    public class UserController : Controller
    {
        //// GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "Eyect4events.local" , "Administrator", "Eyect4events"))
            {
                bool isValid = pc.ValidateCredentials(model.Gebruikersnaam, model.Password);
                if (isValid)
                {
                    UserDB userdb = new UserDB();
                    UserModel user = userdb.GetPerson(model.Gebruikersnaam);

                    Session["Acountid"] = user.Accountid;
                    Session["Gebruikersnaam"] = model.Gebruikersnaam;
                    Session["Niveau"] = user.AccesLevel;
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return View(model);
                }
            }
        }
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var pc = new PrincipalContext(ContextType.Domain, "Eyect4events.local", "Administrator", "Eyect4events"))
                {
                    using (var up = new UserPrincipal(pc))
                    {
                        up.SamAccountName = model.Gebruikersnaam;
                        up.SetPassword(model.Password);
                        up.GivenName = model.Voornaam;
                        up.Surname = model.Achternaam;
                        up.Enabled = true;
                        up.Save();
                    }
                    
                    UserDB userdb = new UserDB();
                    UserModel user = new UserModel(model.Gebruikersnaam, model.Email, model.Voornaam, model.Tussenvoegsel, model.Achternaam, 1 , model.Straatnaam, model.Huisnummer,model.Toevoeging, model.Plaatsnaam);
                    userdb.InsertPerson(user);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["Gebruikersnaam"] = null;
            Session["Niveau"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}