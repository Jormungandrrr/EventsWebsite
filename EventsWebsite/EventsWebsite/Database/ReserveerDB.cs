using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsWebsite.Models;

namespace EventsWebsite.Database
{
    public class ReserveerDB : Database
    {

        public int InsertReservering(int EventID , int AccountID , int PersoonID , int aantal)
        {
            int ReserveringID = ReserveringNieuw(EventID, AccountID, PersoonID, aantal);
            return ReserveringID;
        }
        public void Insertbandjes(int ReserveringID, string Gebruikersnaam)
        {
            int gelukt = ReserveringToevoegen(ReserveringID, Gebruikersnaam);
        }


    }
}