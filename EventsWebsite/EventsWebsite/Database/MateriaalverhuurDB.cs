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
            //ReserveData.Add("VerhuurID", id.ToString());
            ReserveData.Add("Reservering_PolsbandjeID", "1");
            ReserveData.Add("ExemplaarID", ReadStringWithCondition("EXEMPLAAR", "ExemplaarID", "Volgnummer" , id.ToString()));
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

        public List<MaterialModel> GetAllFreeMaterial()
        {
            List<string> values = new List<string> { "DISTINCT(E.ExemplaarID)" };
            List<string> all = new List<string>();
            all.Add("volgnummer");
            all.Add("barcode");
            List<MaterialModel> materials = new List<MaterialModel>();
            List<int> ids = new List<int>();
            MaterialModel material;
            List<string> idsfree = ReadWithConditionNotIN("EXEMPLAAR E, VERHUUR V", values, " V.datumin = null OR E.ExemplaarID", "ExemplaarID", "VERHUUR");
            foreach (string id in idsfree)
            {
                int a = Convert.ToInt32(id);
                ids.Add(a);
            }
            foreach (int id in ids)
            {
                material = (MaterialModel)ReadObjectWithCondition("EXEMPLAAR", all, "ExemplaarID", id.ToString(),"Exemplaar");
                materials.Add(material);
            }
            return materials;

        }

        public List<MaterialModel> GetAllHiredMaterial(int eventid)
        {
            List<string> values = new List<string> { "ExemplaarID" };
            List<string> all = new List<string>();
            all.Add("volgnummer");
            all.Add("barcode");
            List<MaterialModel> materials = new List<MaterialModel>();
            List<int> ids = GetMaterial(eventid);
            foreach (int i in ids)
            {
                foreach(MaterialModel m in ReadObjects("EXEMPLAAR", all, "ExemplaarID", i.ToString(), "Materiaal"))
                {
                    materials.Add(m);
                }
            }
            return materials;
        }
    }
}
