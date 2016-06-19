using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventsWebsite.Models
{
    public class MaterialModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Damaged { get; set; }
        public bool Rented { get; set; }
        public int Number { get; set; }
        public int Barcode { get; set; }


        public MaterialModel(string name, string description, bool damaged, bool rented)
        {
            Name = name;
            Description = description;
            Damaged = damaged;
            Rented = rented;
        }



        public MaterialModel(int number, int barcode)
        {
            Number = number;
            Barcode = barcode;
        }
    }
}
 
 
