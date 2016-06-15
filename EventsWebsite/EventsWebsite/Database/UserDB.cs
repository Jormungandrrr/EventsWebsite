using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsWebsite.Models;

namespace EventsWebsite.Database
{
    public class UserDB : Database
    {
        public void InsertPerson(UserModel User)
        {
            Dictionary<string, string> PersonData = new Dictionary<string, string>();
            PersonData.Add("voornaam",User.Name);
            PersonData.Add("straat", User.Street);
            PersonData.Add("huisnr", User.HouseNumber);
            PersonData.Add("woonplaats", User.City);
            Insert("Persoon",PersonData);

            Dictionary<string, string> AccountData = new Dictionary<string, string>();
            AccountData.Add("gebruikersnaam", User.Username);
            AccountData.Add("email", User.Email);
            Insert("Account", AccountData);
        }

        public UserModel GetPerson(string username)
        {
            List<string> PersonData = new List<string>();
            PersonData.Add("a.gebruikersnaam");
            PersonData.Add("a.Email");
            PersonData.Add("p.voornaam");
            PersonData.Add("a.accesslevel");
            PersonData.Add("p.straat");
            PersonData.Add("p.huisnr");
            PersonData.Add("p.woonplaats");

            UserModel User = (UserModel)ReadObjectWithJoinCondition("account a Join reservering_polsbandje rp on a.accountid = rp.accountid Join reservering r on rp.reserveringid = r.reserveringid Join Persoon p on r.persoonid = p.persoonid ", PersonData,"a.gebruikersnaam", username, "User");
            return User;
        }
    }
}