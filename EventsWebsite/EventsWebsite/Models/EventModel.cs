using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventsWebsite.Models
{
    public class EventModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string Zipcode { get; set; }

        public string City { get; set; }

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