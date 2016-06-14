using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsWebsite.Models
{
    public class EventModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Start date")]
        public DateTime DateStart { get; set; }
        [Required]
        [Display(Name = "End date")]
        public DateTime DateEnd { get; set; }
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
    }
}