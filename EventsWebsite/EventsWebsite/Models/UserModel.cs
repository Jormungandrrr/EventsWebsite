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
        [Required(ErrorMessage = "Vul een telefoonnummer in")]
        [Display(Name = "Telefoonnummer")]
        [DataType(DataType.PhoneNumber)]
        public int Telnr { get; set; }
        [Required(ErrorMessage = "Vul de hoeveelheid rechten in")]
        [Display(Name = "Rechten")]
        public int AccesLevel { get; set; }
        [Required(ErrorMessage = "Vul een straat in")]
        [Display(Name = "Straat")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Vul een huisnummer in")]
        [Display(Name = "Huisnummer")]
        public string HouseNumber { get; set; }
        [Required(ErrorMessage = "Vul een postcode in")]
        [Display(Name = "Postcode")]
        public string Zipcode { get; set; }
        [Required(ErrorMessage = "Vul een stad in")]
        [Display(Name = "Stad")]
        public string City { get; set; }
        [Required(ErrorMessage = "Vul een land in")]
        [Display(Name = "Land")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Vul een banknummer in")]
        [Display(Name = "Banknummer")]
        public string banknr { get; set; }


        public UserModel(string username, string password, string passwordcheck, string email, string name, int telnr, int acceslevel, string street,
                          string houseNumber, string zipcode, string city, string country)
        {
            Username = username;
            Password = password;
            PasswordCheck = passwordcheck;
            Email = email;
            Name = name;
            Telnr = telnr;
            AccesLevel = acceslevel;
            Street = street;
            HouseNumber = houseNumber;
            Zipcode = zipcode;
            City = city;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Gebruikersnaam")]
        public string Gebruikersnaam { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Gebruikersnaam")]
        public string Gebruikersnaam { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Voornaam")]
        public string Voornaam { get; set; }

        [Required]
        [Display(Name = "Tussenvoegsel")]
        public string Tussenvoegsel { get; set; }

        [Required]
        [Display(Name = "Achternaam")]
        public string Achternaam { get; set; }

        [Required]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [Required]
        [Display(Name = "Plaatsnaam")]
        public string Plaatsnaam { get; set; }

        [Required]
        [Display(Name = "Straatnaam")]
        public string Straatnaam { get; set; }

        [Required]
        [Display(Name = "Huisnummer")]
        public int Huisnummer { get; set; }

        [Required]
        [Display(Name = "Toevoeging")]
        public string Toevoeging { get; set; }
    }
}