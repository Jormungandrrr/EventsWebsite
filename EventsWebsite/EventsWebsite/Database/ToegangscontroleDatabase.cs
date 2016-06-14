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
            int exists = Count("Reservering", "ReserveringID", "ReserveringID", barcode);
            return exists == 1;
        }
    }
}
