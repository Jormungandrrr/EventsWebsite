using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventsWebsite.Models
{
    public class SocialMediaMessageModel
    {
        [Required(ErrorMessage = "Vul een bericht in")]
        [Display(Name = "Bericht")]
        public string Message { get; set; }
        [Required(ErrorMessage = "Vul een datum in")]
        [Display(Name = "Datum")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd-hh-mm-ss}", ApplyFormatInEditMode = true)]
        public DateTime UploadTime { get; set; }
        [Required(ErrorMessage = "Vul een berichtid in")]
        [Display(Name = "Berichtid")]
        public int Messageid { get; set; }
        [Required(ErrorMessage = "Vul een gebruikernaam in")]
        [Display(Name = "Gebruikernaam")]
        public string Username { get; set; }

        public SocialMediaMessageModel(string message, DateTime uploadTime, int messageid, string username)
        {
            Message = message;
            UploadTime = uploadTime;
            Messageid = messageid;
            Username = username;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}