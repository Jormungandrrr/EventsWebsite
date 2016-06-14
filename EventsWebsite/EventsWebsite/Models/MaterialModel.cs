using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventsWebsite.Models
{
    public class MaterialModel
    {
        [Required(ErrorMessage = "Vul een naam in")]
        [Display(Name = "Naam")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vul een omschrijving in")]
        [Display(Name = "Omschrijving")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Vul in of het object beschadigd is")]
        [Display(Name = "Beschadigd")]
        public bool Damaged { get; set; }
        [Required(ErrorMessage = "Vul in of het object is verhuurd")]
        [Display(Name = "Gehuurd")]
        public bool Rented { get; set; }

        public MaterialModel(string name, string description, bool damaged, bool rented)
        {
            Name = name;
            Description = description;
            Damaged = damaged;
            Rented = rented;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}