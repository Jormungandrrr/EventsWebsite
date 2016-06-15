using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsWebsite.Models;

namespace EventsWebsite.Database
{
    public class MateriaalverhuurDB : Database
    {
        public void ReserveMaterial(int id, string date)
        {
            Dictionary<string, string> ReserveData = new Dictionary<string, string>();
            ReserveData.Add("VerhuurID", id.ToString());
            ReserveData.Add("Reservering_PolsbandjeID", "1");
            ReserveData.Add("ExemplaarID", "1");
            ReserveData.Add("datumuit", date);
            ReserveData.Add("prijs", "60");
            ReserveData.Add("betaald", "1");
            Insert("VERHUUR", ReserveData);
        }

        public void ReturnMaterial(int id, string date)
        {
            Dictionary<string, string> ReturnData = new Dictionary<string, string>();
            ReturnData.Add("datumin", date);
            Update("VERHUUR", ReturnData, "VerhuurID", id.ToString());
        }

        public List<MaterialModel> GetAllFreeMaterial(int eventid)
        {
            List<string> all = new List<string> { "*" };
            List<MaterialModel> materials = new List<MaterialModel>();
            foreach (string s in ReadWithCondition("EXEMPLAAR", all, "ExemplaarID", "ExemplaarID","VERHUUR"))
            {
                all.Add(s);
            }
            //MaterialModel m = new MaterialModel();
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