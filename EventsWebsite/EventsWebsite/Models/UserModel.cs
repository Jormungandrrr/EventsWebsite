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

        public string Username {get; set; }
        public string Password { get; set; }
        public string PasswordCheck { get; set; }
        public string Email { get; set; }
        public string Voornaam { get; set; }
        public string tussenvoegsel { get; set; }
        public string Achternaam { get; set; }
        public int AccesLevel { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string Toevoeging { get; set; }
        public string City { get; set; }
        public string banknr { get; set; }
        public int Accountid { get; set; }
        public int PersoonId { get; set; }

        public UserModel(string username, string email, string voornaam, string tussenvoegsel, string achternaam, int acceslevel, string street, int houseNumber, string toevoeging, string city)
        {
            this.Username = username;
            this.Email = email;
            this.Voornaam = voornaam;
            this.tussenvoegsel = tussenvoegsel;
            this.Achternaam = achternaam;
            this.AccesLevel = acceslevel;
            this.Street = street;
            this.HouseNumber = houseNumber;
            this.Toevoeging = toevoeging;
            this.City = city;
        }

        public string AccesLevelNaarNaam(int acceslevel)
        {
            string accessName = "default";
            switch (acceslevel)
            {
                case 1:
                    accessName = "Gebruiker";
                    break;
                case 2:
                    accessName = "Beveiliging";
                    break;
                case 3:
                    accessName = "Verhuurder";
                    break;
                case 4:
                    accessName = "Admin";
                    break;

            }

            return accessName;
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