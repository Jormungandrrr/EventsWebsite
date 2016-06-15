using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsWebsite.Models;

namespace EventsWebsite.Database
{
    public class MateriaalverhuurDB : Database
    {
        public void ReserveMaterial(MaterialModel material)
        {
            Dictionary<string, string> ReserveData = new Dictionary<string, string>();
            //ReserveData.Add("datumuit");
            //ReserveData.Add("prijs");
            //ReserveData.Add("betaald");
            Insert("Verhuur", ReserveData);
        }

        public void ReturnMaterial(MaterialModel material)
        {
            Dictionary<string, string> ReturnData = new Dictionary<string, string>();
            //ReturnData.Add("datumin");
            Insert("Verhuur", ReturnData);
        }

        public List<MaterialModel> GetAllFreeMaterial(int eventid)
        {
            List<string> all = new List<string> { "*" };
            List<MaterialModel> materials = new List<MaterialModel>();
            //materials = (List<MaterialModel>)ReadObjects("Product", all, "eventid", eventid.ToString(), "MaterialModel").Cast<MaterialModel>();
            return materials;
        }

        public List<MaterialModel> GetAllLoanedMaterial(int eventid)
        {
            List<string> all = new List<string> { "*" };
            List<MaterialModel> materials = new List<MaterialModel>();
            //materials = ;
            return materials;
        }
    }
}