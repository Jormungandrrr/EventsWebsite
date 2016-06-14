using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebsite.Database
{
    class ToegangscontroleDatabase : Database
    {
        public bool HasAccess(int barcode)
        {
            return false;
        }
    }
}
