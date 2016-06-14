using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsWebsite.Models
{
    public class User
    {
        public string Username {get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Telnr { get; set; }
        public int AccesLevel { get; set; }
        public Address  Adress{ get; set; }

        public User(string username, string password, string email, string name, int telnr, int acceslevel)
        {
            Username = username;
            Password = password;
            Email = email;
            Name = name;
            Telnr = telnr;
            AccesLevel = acceslevel;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}