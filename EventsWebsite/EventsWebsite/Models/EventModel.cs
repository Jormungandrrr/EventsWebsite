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
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vul een startdatum in")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd-hh-mm-ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start date")]
        public DateTime DateStart { get; set; }
        [Required(ErrorMessage = "Vul een einddatum in")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd-hh-mm-ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "End date")]
        public DateTime DateEnd { get; set; }
        [Required(ErrorMessage = "Vul een straat in")]
        [Display(Name = "Straat")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Vul een huisnummer in")]
        [Display(Name = "HouseNumber")]
        public string HouseNumber { get; set; }
        [Required(ErrorMessage = "Vul een postcode in")]
        [Display(Name = "Zipcode")]
        public string Zipcode { get; set; }
        [Required(ErrorMessage = "Vul een stad in")]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Vul een land in")]
        [Display(Name = "Country")]
        public string Country { get; set; }

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

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}