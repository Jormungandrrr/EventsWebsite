using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventsWebsite.Models
{
    public class ReservationModel
    {
        [Required(ErrorMessage = "Reserveringid")]
        [Display(Name = "Reserveringid")]
        public int Reservationid { get; set; }
        [Required(ErrorMessage = "Vul een start datum in")]
        [Display(Name = "Start datum")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Vul een eind datum in")]
        [Display(Name = "Eind datum")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Vul een betalingsstatus in")]
        [Display(Name = "Betalingsstatus")]
        public bool Paid { get; set; }

        public ReservationModel(DateTime startDate, DateTime endDate, bool paid, int reservationid)
        {
            StartDate = startDate;
            EndDate = endDate;
            Paid = paid;
            Reservationid = reservationid;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
        public class ReserveringUsers
        {
            [Display(Name = "Gebruikersnaam")]
            public string Gebruikersnaam1 { get; set; }

            [Display(Name = "Gebruikersnaam")]
            public string Gebruikersnaam2 { get; set; }

            [Display(Name = "Gebruikersnaam")]
            public string Gebruikersnaam3 { get; set; }
        
            [Display(Name = "Gebruikersnaam")]
            public string Gebruikersnaam4 { get; set; }

            [Display(Name = "Gebruikersnaam")]
            public string Gebruikersnaam5 { get; set; }

            [Display(Name = "Gebruikersnaam")]
            public string Gebruikersnaam6 { get; set; }

            [Display(Name = "Gebruikersnaam")]
            public string Gebruikersnaam7 { get; set; }
        }
}