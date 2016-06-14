using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsWebsite.Models;

namespace EventsWebsite.Database
{
    public class UserDB : Database
    {
        private void InsertPerson(UserModel User)
        {
            Dictionary<string, string> PersonData = new Dictionary<string, string>();
            PersonData.Add("voornaam",User.Name);
            PersonData.Add("straat", User.Street);
            PersonData.Add("huisnr", User.HouseNumber);
            PersonData.Add("woonplaats", User.City);
            PersonData.Add("banknr", User.ba);
            Insert("Persoon",PersonData);

            Dictionary<string, string> AccountData = new Dictionary<string, string>();
            PersonData.Add("gebruikersnaam", User.Username);
            PersonData.Add("email", User.Email);
        }
    }
}