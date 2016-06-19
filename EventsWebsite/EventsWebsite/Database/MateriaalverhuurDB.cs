using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsWebsite.Models;
using Oracle.ManagedDataAccess.Client;

namespace EventsWebsite.Database
{
    public class MateriaalverhuurDB : Database
    {
        public void ReserveMaterial(int id, string date)
        {
            Dictionary<string, string> ReserveData = new Dictionary<string, string>();
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
            List<string> values = new List<string> { "DISTINCT(V.ExemplaarID)" };
            List<string> all = new List<string>();
            all.Add("volgnummer");
            all.Add("barcode");
            List<MaterialModel> materials = new List<MaterialModel>();
            List<int> ids = new List<int>();
            MaterialModel material;
            List<string> idsfree = ReadWithConditionNotIN("EXEMPLAAR E, VERHUUR V", values, " V.ExemplaarID NOT IN(SELECT ExemplaarID FROM VERHUUR WHERE datumin is null) OR E.ExemplaarID ", "ExemplaarID", "VERHUUR");
            foreach (string id in idsfree)
            {
                int a = Convert.ToInt32(id);
                ids.Add(a);
            }
            foreach (int id in ids)
            {
                material = (MaterialModel)ReadObjectWithCondition("EXEMPLAAR", all, "ExemplaarID","=", id.ToString(),"Exemplaar");
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

        public List<int> GetMaterial(int eventid)
        {
            List<int> ReturnData = new List<int>();
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (
                    OracleCommand command =
                        new OracleCommand(
                            "SELECT DISTINCT(ExemplaarID) FROM VERHUUR v, RESERVERING_POLSBANDJE rp, RESERVERING r, PLEK_RESERVERING pr, Plek p, LOCATIE l, EVENT e WHERE v.Reservering_PolsbandjeID = rp.ID AND rp.ReserveringID = r.ReserveringID AND r. ReserveringID = pr.ReserveringID AND pr.PlekID = p.PlekID AND p.LocatieID = l.LocatieID AND l.LocatieID = e.LocatieID AND v.datumin IS NULL",
                            conn)
                    )
                {
                    command.BindByName = true;
                    command.Parameters.Add(":ei", eventid);
                    try
                    {
                        conn.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReturnData.Add(reader.GetInt32(0));
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return ReturnData;
        }
    }
}
