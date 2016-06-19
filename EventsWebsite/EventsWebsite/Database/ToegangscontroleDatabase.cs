using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsWebsite.Models;

namespace EventsWebsite.Database
{
    public class ToegangscontroleDatabase : Database
    {
        public bool HasAccess(int barcode)
        {
            int exists = Count("EVENT e, LOCATIE l, PLEK p, PLEK_RESERVERING pr, RESERVERING r, RESERVERING_POLSBANDJE rp, POLSBANDJE pb WHERE e.LocatieID = L.LocatieID AND p.LocatieID = L.LocatieID AND pr.PlekID = p.PlekID AND r.ReserveringID = pr.ReserveringID AND rp.ReserveringID = r.ReserveringID AND pb.PolsbandjeID = rp.PolsbandjeID","*","barcode",barcode.ToString());
            return exists == 1;
        }

        public void UpdateTag(int barcode,string value)
        {
            Dictionary<string,string> dict = new Dictionary<string,string>();
            dict.Add("Actief",value);
            Update("Polsbandje",dict,"barcode",barcode.ToString());
        }

        public List<UserModel> GetAllPeopleOnTerrain()
        {
            List<string> PersonData = new List<string> {"p.voornaam", "p.tussenvoegsel", "p.achternaam"};
            return ReadObjects("persoon p Join reservering_polsbandje rp ON p.accountid = rp.accountid", PersonData, "Aanwezig", "1", "User2").Cast<UserModel>().ToList();
        }
    }
}
