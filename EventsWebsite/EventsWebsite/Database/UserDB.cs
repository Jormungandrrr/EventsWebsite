﻿using System;
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
            Dictionary<string, string> AccountData = new Dictionary<string, string>();
            AccountData.Add("gebruikersnaam", User.Username);
            AccountData.Add("email", User.Email);
            Insert("Account", AccountData);

            Dictionary<string, string> PersonData = new Dictionary<string, string>();
            PersonData.Add("voornaam",User.Voornaam);
            PersonData.Add("tussenvoegsel", User.tussenvoegsel);
            PersonData.Add("achternaam", User.Achternaam);
            PersonData.Add("straat", User.Street);
            PersonData.Add("huisnr", User.HouseNumber.ToString());
            PersonData.Add("toevoeging", User.Toevoeging);
            PersonData.Add("woonplaats", User.City);
            PersonData.Add("accountid", ReadStringWithCondition("Account","Accountid","gebruikersnaam",User.Username));
            Insert("Persoon",PersonData);
        }

        public UserModel GetPerson(string username)
        {
            List<string> PersonData = new List<string>();
            PersonData.Add("a.gebruikersnaam");
            PersonData.Add("a.email");
            PersonData.Add("p.voornaam");
            PersonData.Add("p.tussenvoegsel");
            PersonData.Add("p.achternaam");
            PersonData.Add("a.accesslevel");
            PersonData.Add("p.straat");
            PersonData.Add("p.huisnr");
            PersonData.Add("p.toevoeging");
            PersonData.Add("p.woonplaats");
            PersonData.Add("a.accountid");
            //UserModel User = (UserModel)ReadObjectWithCondition("account a Join reservering_polsbandje rp on a.accountid = rp.accountid Join reservering r on rp.reserveringid = r.reserveringid Join Persoon p on r.persoonid = p.persoonid ", PersonData,"a.gebruikersnaam", username, "User");
            UserModel User = (UserModel)ReadObjectWithCondition("account a Join Persoon p ON a.accountid = p.accountid", PersonData,"a.gebruikersnaam","=", username, "User");
            User.Accountid = Convert.ToInt32(ReadStringWithCondition("account", "accountid", "gebruikersnaam", User.Username));
            return User;
        }

        public void UpdateUser(UserModel User)
        {
            Dictionary<string, string> UpdateData = new Dictionary<string, string>();
            UpdateData.Add("gebruikersnaam", User.Username);
            UpdateData.Add("email", User.Email);
            Update("account",UpdateData,"accountid",ReadStringWithCondition("account","accountid","gebruikersnaam",User.Username));

            UpdateData.Clear();
            UpdateData.Add("voornaam", User.Voornaam);
            UpdateData.Add("straat", User.Street);
            UpdateData.Add("huisnr", User.HouseNumber.ToString());
            UpdateData.Add("toevoeging", User.Toevoeging);
            UpdateData.Add("woonplaats", User.City);
            Update("persoon", UpdateData, "accountid", ReadStringWithCondition("Account", "accountid", "gebruikersnaam", User.Username));
        }
        public List<UserModel> GetAllUsers()
        {
            List<UserModel> Users = new List<UserModel>();
            List<string> PersonData = new List<string>();
            PersonData.Add("a.gebruikersnaam");
            PersonData.Add("a.email");
            PersonData.Add("p.voornaam");
            PersonData.Add("p.tussenvoegsel");
            PersonData.Add("p.achternaam");
            PersonData.Add("a.accesslevel");
            PersonData.Add("p.straat");
            PersonData.Add("p.huisnr");
            PersonData.Add("p.toevoeging");
            PersonData.Add("p.woonplaats");
            PersonData.Add("a.accountid");
            foreach (UserModel u in ReadObjects("account a Join Persoon p ON a.accountid = p.accountid", PersonData, "User"))
            {
                u.Accountid = Convert.ToInt32(ReadStringWithCondition("account", "accountid", "gebruikersnaam", u.Username));
                Users.Add(u);
            }
            return Users;
        }

        public void DisableUser(UserModel User)
        {
            Dictionary<string, string> UpdateData = new Dictionary<string, string>();
            UpdateData.Add("geactiveerd", "0");
            Update("account", UpdateData, "accountid", ReadStringWithCondition("Account", "accountid", "gebruikersnaam", User.Username));
        }
    }
}