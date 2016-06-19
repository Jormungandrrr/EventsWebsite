using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventsWebsite.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

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

        public int ReserveringToevoegen(int ReserveringID, string Gebruikersnaam)
        {
            using (OracleConnection con = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("ReserveringToevoegen", con))
                {
                    try
                    {
                        con.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.BindByName = true;
                        command.Parameters.Add("t_reserveringID", OracleDbType.Int32, ReserveringID, ParameterDirection.Input);
                        command.Parameters.Add("t_gebruikersnaam", OracleDbType.Varchar2, Gebruikersnaam, ParameterDirection.Input);
                        command.Parameters.Add("return", OracleDbType.Int32, ParameterDirection.ReturnValue);
                        command.ExecuteNonQuery();
                        string rt = command.Parameters["return"].Value.ToString();
                        int ret;
                        if (int.TryParse(rt, out ret))
                        {
                            if (ret == 1)
                                return ret;
                        }
                        return 0;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }

        public int ReserveringNieuw(int EventID, int AccountID, int PersoonID, int aantal)
        {
            using (OracleConnection con = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("ReserveringNieuw", con))
                {
                    try
                    {
                        con.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.BindByName = true;
                        command.Parameters.Add("t_eventID", OracleDbType.Int32, EventID, ParameterDirection.Input);
                        command.Parameters.Add("t_accountID", OracleDbType.Int32, AccountID, ParameterDirection.Input);
                        command.Parameters.Add("t_persoonID", OracleDbType.Int32, PersoonID, ParameterDirection.Input);
                        command.Parameters.Add("n_aantal", OracleDbType.Int32, aantal, ParameterDirection.Input);
                        command.Parameters.Add("return", OracleDbType.Int32, ParameterDirection.ReturnValue);
                        command.ExecuteNonQuery();
                        string rt = command.Parameters["return"].Value.ToString();
                        int ret;
                        if (int.TryParse(rt, out ret))
                        {
                            if (ret > 0)
                                return ret;
                        }
                        return 0;
                    }
                    catch
                    {
                        return 0;
                    }
                }

            }
        }
    }
}