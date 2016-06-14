using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebsite.Database
{
    public class ToegangscontroleDatabase : Database
    {
        public bool HasAccess(int barcode)
        {
            int exists = Count("Event","EventID","LocatieID",Convert.ToInt32(ReadStringWithCondition("Locatie","LocatieID","LocatieID",ReadStringWithCondition("Plek","LocatieID","PlekID",ReadStringWithCondition("Plek_Reservering","PlekID","ReserveringID",ReadStringWithCondition("Reservering","ReserveringID","ReserveringsID",ReadStringWithCondition("Reservering_Polsbandje","ReserveringID","PolsbandjeID",ReadStringWithCondition("Polsbandje","PolsbandjeID","barcode",barcode.ToString()))))))));
            return exists == 1;
        }

        public void UpdateTag(int barcode,string value)
        {
            Dictionary<string,string> dict = new Dictionary<string,string>();
            dict.Add("Actief",value);
            Update("Polsbandje",dict,"barcode",barcode.ToString());
        }
    }
}
