using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsWebsite.Models
{
    public class MaterialModel
    {
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        public bool Beschadigd { get; set; }
        public bool Gehuurd { get; set; }

        public MaterialModel(string naam, string omschrijving, bool beschadigd, bool gehuurd)
        {
            Naam = naam;
            Omschrijving = omschrijving;
            Beschadigd = beschadigd;
            Gehuurd = gehuurd;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}