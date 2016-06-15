using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace EventsWebsite.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Vul een gebruikernaam in")]
        [Display(Name="Gebruikersnaam")]
        public string Username {get; set; }
        [Required(ErrorMessage = "Vul een wachtwoord in")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vul een bevestigings wachtwoord in")]
        [DataType(DataType.Password)]
        [Display(Name="Bevestig wachtwoord")]
        [Compare("Password",ErrorMessage = "De wachtwoorden komen niet met elkaar overeen")]
        public string PasswordCheck { get; set; }
        [Required(ErrorMessage = "Vul een E-mail adres in")]
        [DataType(DataType.EmailAddress)]
        [Display(Name="E-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vul een naam in")]
        [Display(Name = "Naam")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vul de hoeveelheid rechten in")]
        [Display(Name = "Rechten")]
        public int AccesLevel { get; set; }
        [Required(ErrorMessage = "Vul een straat in")]
        [Display(Name = "Straat")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Vul een huisnummer in")]
        [Display(Name = "Huisnummer")]
        public string HouseNumber { get; set; }
        [Required(ErrorMessage = "Vul een stad in")]
        [Display(Name = "Stad")]
        public string City { get; set; }
        [Required(ErrorMessage = "Vul een banknummer in")]
        [Display(Name = "Banknummer")]
        public string banknr { get; set; }


        public UserModel(string username, string email, string name, int acceslevel, string street,string houseNumber, string city)
        {
            Username = username;
            Email = email;
            Name = name;
            AccesLevel = acceslevel;
            Street = street;
            HouseNumber = houseNumber;
            City = city;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}