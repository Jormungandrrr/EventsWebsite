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

        public ReservationModel(DateTime startDate, DateTime endDate, bool paid)
        {
            StartDate = startDate;
            EndDate = endDate;
            Paid = paid;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}