using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventsWebsite.Models
{
    public class EventModel
    {
        [Required(ErrorMessage = "Vul een naam in")]
        [Display(Name = "Naam")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vul een startdatum in")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd-hh-mm-ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start datum")]
        public DateTime DateStart { get; set; }
        [Required(ErrorMessage = "Vul een einddatum in")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd-hh-mm-ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Eind datum")]
        public DateTime DateEnd { get; set; }
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

        public int EventID { get; set; }

        public EventModel(string name, DateTime dateStart, DateTime dateEnd, string street, string housenumber, string zipcode, string city, string country)
        {
            Name = name;
            DateStart = dateStart;
            DateEnd = dateEnd;
            Street = street;
            HouseNumber = housenumber;
            Zipcode = zipcode;
            City = city;
            Country = country;
        }

        public EventModel()
        {
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}