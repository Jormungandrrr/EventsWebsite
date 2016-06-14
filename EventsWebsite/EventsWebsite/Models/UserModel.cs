﻿using System;
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
        [Required]
        [Display(Name = "Rechten")]
        public int AccesLevel { get; set; }
        [Required]
        [Display(Name = "Straat")]
        public string Street { get; set; }
        [Required]
        [Display(Name = "HouseNumber")]
        public string HouseNumber { get; set; }
        [Required]
        [Display(Name = "Zipcode")]
        public string Zipcode { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public UserModel(string username, string password, string email, string name, int telnr, int acceslevel, string street, 
                         string houseNumber, string zipcode, string city, string country)
        {
            Username = username;
            Password = password;
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
}