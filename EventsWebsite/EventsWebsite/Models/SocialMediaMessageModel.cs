using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventsWebsite.Models
{
    public class SocialMediaMessageModel
    {
        [Required(ErrorMessage = "Vul een titel voor je bericht in")]
        [Display(Name = "Titel")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Vul een bericht in")]
        [Display(Name = "Bericht")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name="Bijlage")]
        public HttpPostedFileBase FileUpload { get; set; }

        public DateTime UploadTime { get; set; }
        public int Messageid { get; set; }
        public string Username { get; set; }
        public string Filepath { get; set; }

        public SocialMediaMessageModel()
        {
        }

        public SocialMediaMessageModel(string message, DateTime uploadTime, int messageid, string username)
        {
            Message = message;
            UploadTime = uploadTime;
            Messageid = messageid;
            Username = username;
        }

        public override string ToString()
        {
            return Messageid+"";
        }
    }
}