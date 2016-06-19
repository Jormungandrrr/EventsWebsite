using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventsWebsite.Models
{
    public class MaterialModel
    {
        public int Number { get; set; }
        public int Barcode { get; set; }

        public MaterialModel(int number, int barcode)
        {
            Number = number;
            Barcode = barcode;
        }
    }
}
 
 
