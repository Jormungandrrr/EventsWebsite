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
        [Required]
        [Display(Name="Gebruikersnaam")]
        public string Username {get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name="Bevestig wachtwoord")]
        [Compare("Password",ErrorMessage = "De wachtwoorden komen niet met elkaar overeen")]
        public string PasswordCheck { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name="E-mail")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Naam")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Telefoonnummer")]
        [DataType(DataType.PhoneNumber)]
        public int Telnr { get; set; }

        public int AccesLevel { get; set; }
        //Hier moet eigenlijk alle velden voor het adres bij staan, anders dan kan je het model niet aanmaken bij registeren, of we moeten dit idd het user model laten en nog een registratiemodel toevoegen met deze gegevens plus gegevens uit het adresmodel
        public Address  Adress{ get; set; }

        public UserModel(string username, string password, string email, string name, int telnr, int acceslevel)
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