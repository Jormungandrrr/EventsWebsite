﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.DynamicData;
using EventsWebsite.Models;
using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;

namespace EventsWebsite.Database
{
    public abstract class Database
    { 
        static string Connectionstring = @"Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = fhictora01.fhict.local)(PORT = 1521)))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = fhictora))); User ID = dbi331842; PASSWORD =CZSKUvxUUs;";

        public virtual void Insert(string table, Dictionary<string, string> values)
        {
            string columnNames = GetColumnParameter(values, false);
            string parametercolumns = GetColumnParameter(values, true);

            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("INSERT INTO " + table + " (" + columnNames + ") VALUES (" + parametercolumns + ")", conn))
                {
                    foreach (string r in values.Keys)
                    {
                        string value = values[r];
                        command.Parameters.Add(new OracleParameter(r, value));
                    }

                    command.Connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        public virtual void Update(string table, Dictionary<string, string> values,string condition1, string condition2)
        {
            string querUpdateValues = "";
            foreach (string v in values.Keys)
            {
                if (values.Keys.Last() == v) { querUpdateValues += v + " = :" + v; }
                else { querUpdateValues += v + " = :" + v + ", "; }
            }

            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand($"update {table} set {querUpdateValues} where {condition1} = {condition2}", conn))
                   
                {
                    command.BindByName = true;
                    command.Parameters.Add(new OracleParameter(":Condition2", condition2));
                    foreach (string c in values.Keys)
                    {
                        command.Parameters.Add(new OracleParameter(":"+c, values[c]));
                    }

                    command.Connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch
                    {

                    }

                }
            }
        }

    public virtual string ReadStringWithCondition(string table, string column, string ConditionValue1, string ConditionValue2)
        {
            string ReturnData = "";
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                string query = ("SELECT " + column + " FROM " + table + " WHERE " + ConditionValue1 + " = " + "'" + ConditionValue2 + "'");
                
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.BindByName = true;
                    command.Parameters.Add(new OracleParameter(":Condition2", ConditionValue2));
                    try
                    { 
                        command.Connection.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReturnData = (Convert.ToString(reader[column]));
                            }
                            return ReturnData;
                        }
                    }
                    catch
                    {

                    }
                    return ReturnData;
                }

            }
        }

        public virtual string ReadStringWith2Conditions(string table, string column, string ConditionValue1, string ConditionValue2, string ConditionValue3)
        {
            string ReturnData = "";
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                string query = ("SELECT " + column + " FROM " + table + " WHERE " + ConditionValue1 + " = " + ConditionValue2 + " AND " + ConditionValue3 + " IS NULL");

                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.BindByName = true;
                    command.Parameters.Add(new OracleParameter(":Condition2", ConditionValue2));
                    try
                    {
                        command.Connection.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReturnData = (Convert.ToString(reader[column]));
                            }
                            return ReturnData;
                        }
                    }
                    catch
                    {

                    }
                    return ReturnData;
                }

            }
        }

        public virtual List<int> Read(string table, string column)
        {
            List<int> ReturnData = new List<int>();
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("SELECT " + column + " FROM " + table, conn))
                {
                    command.BindByName = true;
                    try
                    {
                        command.Connection.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReturnData.Add(Convert.ToInt32(reader[column]));
                            }
                            return ReturnData;
                        }
                    }
                    catch
                    {

                    }
                    return ReturnData;
                }
            }
        }

        public virtual List<object> ReadObjects(string table, List<string> data,string type)
        {
            List<object> ReturnData = new List<object>();
            string columnNames = GetColumnNames(data);
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("SELECT " + columnNames + " FROM " + table, conn))
                {
                    try{
                        command.Connection.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (type == "User")
                                {
                                    UserModel user = new UserModel(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), Convert.ToInt32(reader[5]), reader[6].ToString(), Convert.ToInt32(reader[7]), reader[8].ToString(), reader[9].ToString(),reader.GetInt32(10));
                                    ReturnData.Add(user);
                                }
                                else if (type == "Event")
                                {
                                    EventModel e = new EventModel(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetInt32(4), reader.GetInt32(5));
                                    ReturnData.Add(e);
                                }
                            }
                            return ReturnData;
                        }
                    }

                    catch{}
                    return ReturnData;
                }

            }
        }

        public virtual List<object> ReadObjects(string table, List<string> data, string ConditionValue1, string ConditionValue2, string type)
        {
            List<object> ReturnData = new List<object>();
            string columnNames = GetColumnNames(data);
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("SELECT " + columnNames + " FROM " + table + " WHERE " + ConditionValue1 +" = " + ConditionValue2, conn))
                {
                    command.BindByName = true;
                    try
                    {
                        command.Connection.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            try
                            {
                                while (reader.Read())
                                {
                                    if (type == "Materiaal")
                                    {
                                        MaterialModel m = new MaterialModel(reader.GetInt32(0), reader.GetInt32(1));
                                        ReturnData.Add(m);
                                    }
                                    else if (type == "thumbnail")
                                    {
                                        Thumbnail add = new Thumbnail(reader.GetInt32(0), reader.GetString(1),reader.GetString(2), reader.GetString(3), reader.GetString(4),reader.GetString(5),reader.GetInt32(6));
                                        ReturnData.Add(add);
                                    }
                                    
                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
                            }
                            return ReturnData;
                        }
                    }

                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    return ReturnData;
                }

            }
        }

        public virtual List<object> ReadObjects(string table, List<string> data, string Where, string type)
        {
            List<object> ReturnData = new List<object>();
            string columnNames = GetColumnNames(data);
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("SELECT " + columnNames + " FROM " + table + " WHERE " + Where, conn))
                {
                    command.BindByName = true;
                    try
                    {
                        command.Connection.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            try
                            {
                                while (reader.Read())
                                {
                                    if (type == "Materiaal")
                                    {
                                        MaterialModel m = new MaterialModel(reader.GetInt32(0).ToString(), reader.GetInt32(1).ToString(), true, true);
                                        ReturnData.Add(m);
                                    }
                                    else if (type == "Event")
                                    {
                                        EventModel e = new EventModel(reader.GetInt32(0),reader.GetString(1),reader.GetDateTime(2),reader.GetDateTime(3),reader.GetInt32(4), reader.GetInt32(5));
                                        ReturnData.Add(e);
                                    }

                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
                            }
                            return ReturnData;
                        }
                    }

                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    return ReturnData;
                }

            }
        }

        public virtual List<Thumbnail> GetThumbnails(string table, List<string> data, string ConditionValue1, string ConditionValue2, string type)
        {
            List<Thumbnail> ReturnData = new List<Thumbnail>();
            string columnNames = GetColumnNames(data);
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("SELECT " + columnNames + " FROM " + table + " WHERE " + ConditionValue1 + " <= " + ConditionValue2, conn))
                {
                    command.BindByName = true;
                    try
                    {
                        command.Connection.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            try
                            {
                                while (reader.Read())
                                {
                                    Thumbnail add = new Thumbnail(reader.GetInt32(0), reader.GetString(1),
                                        reader.GetString(2), reader.GetString(3), reader.GetString(4),
                                        reader.GetString(5),
                                        reader.GetInt32(6));
                                    ReturnData.Add(add);
                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
                            }
                            return ReturnData;
                        }
                    }

                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    return ReturnData;
                }

            }
        }


        public virtual object ReadObjectWithCondition(string table, List<string> data, string ConditionValue1, string ConditionValue2, string type)
        {
            object ReturnData = new object();
            string columnNames = GetColumnNames(data);
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("SELECT " + columnNames + " FROM " + table + " WHERE "+ ConditionValue1 + " = :Condition2", conn))
                {
                    command.BindByName = true;
                    command.Parameters.Add(new OracleParameter(":Condition2", ConditionValue2));
                    try
                    {
                        command.Connection.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (type == "User")
                                {
                                    UserModel user = new UserModel(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), Convert.ToInt32(reader[5]), reader[6].ToString(), Convert.ToInt32(reader[7]), reader[8].ToString(), reader[9].ToString(),reader.GetInt32(10));
                                    ReturnData = user;
                                }
                                else if (type == "Exemplaar")
                                {
                                    MaterialModel m = new MaterialModel(reader.GetInt32(0), reader.GetInt32(1));
                                    ReturnData = m;
                                }
                                else if (type == "Event")
                                {
                                    EventModel e = new EventModel(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetInt32(4), reader.GetInt32(5));
                                    ReturnData = e;
                                }

                            }
                            return ReturnData;
                        }
                    }

                    catch { }
                    return ReturnData;
            } 

        }
        }

        public virtual List<string> ReadWithCondition(string table, List<string> data, string ConditionValue1, string ConditionValue2, string ConditionValue3)
        {
            string ColumNames = GetColumnNames(data);
            List<string> ReturnData = new List<string>();
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("SELECT " + ColumNames + " FROM " + table + " WHERE " + ConditionValue1 + " IN( SELECT :Condition2 FROM :Condition3)", conn))
                {
                    command.BindByName = true;
                    command.Parameters.Add(new OracleParameter(":Condition2", ConditionValue2));
                    command.Parameters.Add(new OracleParameter(":Condition3", ConditionValue3));
                    command.Connection.Open();
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            foreach (string v in data)
                            {
                                ReturnData.Add(Convert.ToString(reader[v]));
                            }
                        }
                        return ReturnData;
                    }
                }
            }
        }

        public virtual List<string> ReadWithConditionNotIN(string table, List<string> data, string ConditionValue1, string ConditionValue2, string ConditionValue3)
        {
            string ColumNames = GetColumnNames(data);
            List<string> ReturnData = new List<string>();
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("SELECT " + ColumNames + " FROM " + table + " WHERE " + ConditionValue1 + " NOT IN( SELECT " + ConditionValue2  + " FROM " + ConditionValue3 + ")", conn))
                {
                    command.BindByName = true;
                    command.Parameters.Add(new OracleParameter("Condition1", ConditionValue1));
                    command.Parameters.Add(new OracleParameter("Condition2", ConditionValue2));
                    command.Parameters.Add(new OracleParameter("Condition3", ConditionValue3));
                    command.Connection.Open();
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            foreach (string v in data)
                            {
                                ReturnData.Add(Convert.ToString(reader[0]));
                            }
                        }
                        return ReturnData;
                    }
                }
            }
        }

        public virtual int Count(string table, string column, string condition1,string condition2)
        {
            int ReturnData = 0;
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("SELECT COUNT(" + column + ") as Aantal FROM " + table + " WHERE " + condition1 + " = :Condition2", conn))
                {
                    command.BindByName = true;
                    command.Parameters.Add(new OracleParameter(":Condition2", condition2));
                    try
                    {
                        command.Connection.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReturnData = reader.GetInt32(0);
                            }
                        }
                    }
                    catch { }
                    return ReturnData;
                }
            }
        }

        public virtual int CountAccess(int barcode)
        {
            int ReturnData = 0;
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (
                    OracleCommand command =
                        new OracleCommand(
                            "SELECT COUNT(*) FROM EVENT e, LOCATIE l, PLEK p, PLEK_RESERVERING pr, RESERVERING r, RESERVERING_POLSBANDJE rp, POLSBANDJE pb WHERE e.LocatieID = L.LocatieID AND p.LocatieID = L.LocatieID AND pr.PlekID = p.PlekID AND r.ReserveringID = pr.ReserveringID AND rp.ReserveringID = r.ReserveringID AND pb.PolsbandjeID = rp.PolsbandjeID AND barcode = :bc",
                            conn)
                    )
                {
                    command.BindByName = true;
                    command.Parameters.Add(":bc", barcode);
                    try
                    {
                        conn.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReturnData = reader.GetInt32(0);
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

        public virtual List<int> GetMaterial(int eventid)
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

        public virtual bool Delete(string table, string where, string equals)
        {
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand($"DELETE FROM {table} WHERE {where}  = :value",conn))
                {
                    command.BindByName = true;
                    try
                    {
                        conn.Open();
                        command.Parameters.Add("value", equals);
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
        public virtual bool AddMessage(string procedure, string titel, string inhoud, int userid)
        {
            using (OracleConnection con = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand(procedure, con))
                {
                    try
                    {
                        con.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.BindByName = true;
                        command.Parameters.Add("t_title", OracleDbType.Varchar2, titel, ParameterDirection.Input);
                        command.Parameters.Add("t_message", OracleDbType.Varchar2, inhoud, ParameterDirection.Input);
                        command.Parameters.Add("t_sender", OracleDbType.Int32, userid, ParameterDirection.Input);
                        command.Parameters.Add("return", OracleDbType.Int32, ParameterDirection.ReturnValue);
                        command.ExecuteNonQuery();
                        string rt = command.Parameters["return"].Value.ToString();
                        int ret;
                        if (int.TryParse(rt, out ret))
                        {
                            return ret == 1;
                        }
                        return false;

                    }
                    catch
                    {
                        return false;
                    }
                }

            }
        }
        public virtual bool AddFile(string procedure, string floc, int fsize, int userid)
        {
            using (OracleConnection con = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand(procedure, con))
                {
                    try
                    {
                        con.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.BindByName = true;
                        command.Parameters.Add("t_bestandlocatie", OracleDbType.Varchar2, floc, ParameterDirection.Input);
                        command.Parameters.Add("t_message", OracleDbType.Int32, fsize, ParameterDirection.Input);
                        command.Parameters.Add("t_sender", OracleDbType.Int32, userid, ParameterDirection.Input);
                        command.Parameters.Add("return", OracleDbType.Int32, ParameterDirection.ReturnValue);
                        command.ExecuteNonQuery();
                        string rt = command.Parameters["return"].Value.ToString();
                        int ret;
                        if (int.TryParse(rt, out ret))
                        {
                            return ret == 1;
                        }
                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                }

            }
        }
        public virtual int ReserveringNieuw(int EventID, int AccountID, int PersoonID, int aantal)
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
                            if(ret > 0)
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

        public virtual int ReserveringToevoegen(int ReserveringID, string Gebruikersnaam)
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

        public virtual
            bool AddReply(string procedure, string titel, string inhoud, int userid, int messageid)
        {
            using (OracleConnection con = new OracleConnection())
            {
                using (OracleCommand command = new OracleCommand(procedure, con))
                {
                    try
                    {
                        con.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.BindByName = true;
                        command.Parameters.Add("t_title", OracleDbType.Varchar2, titel, ParameterDirection.Input);
                        command.Parameters.Add("t_message", OracleDbType.Varchar2, inhoud, ParameterDirection.Input);
                        command.Parameters.Add("t_sender", OracleDbType.Int32, userid, ParameterDirection.Input);
                        command.Parameters.Add("bijdrage", OracleDbType.Int32, messageid, ParameterDirection.Input);
                        command.Parameters.Add("return", OracleDbType.Int32, ParameterDirection.ReturnValue);
                        command.ExecuteNonQuery();
                        string rt = command.Parameters["return"].Value.ToString();
                        int ret;
                        if (int.TryParse(rt, out ret))
                        {
                            return ret == 1;
                        }
                        return false;

                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
        public virtual List<SocialMediaMessageModel> AllPosts()
        {
            List<SocialMediaMessageModel> ret = new List<SocialMediaMessageModel>();
            using (OracleConnection con = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("SELECT b.titel,a.gebruikersnaam, b.bijdrageid FROM bericht b, bijdrage bd, account a WHERE b.bijdrageid = bd.bijdrageID AND bd.accountid = a.accountid ORDER BY b.bijdrageID DESC", con))
                {
                    try
                    {
                        con.Open();
                        OracleDataReader dr = command.ExecuteReader();
                        while (dr.Read())
                        {
                            SocialMediaMessageModel add = new SocialMediaMessageModel
                            {
                                Title = dr.GetString(0),
                                Username = dr.GetString(1),
                                Messageid = dr.GetInt32(2)
                            };
                            ret.Add(add);
                        }
                    }
                    catch { }
                }
            }
            return ret;
            }
        public virtual List<SocialMediaMessageModel> DetailPost(int messageid)
        {
            List<SocialMediaMessageModel> ret = new List<SocialMediaMessageModel>();
            using (OracleConnection con = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand())
                {
                    try
                    {
                        command.Connection = con;
                        con.Open();
                        command.CommandText =
                           $"SELECT b.titel, b.inhoud,a.gebruikersnaam, b.bijdrageid FROM bericht b, bijdrage bd, account a WHERE b.bijdrageid = bd.bijdrageID AND bd.accountid = a.accountid AND b.bijdrageid = {messageid}";
                        OracleDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            SocialMediaMessageModel add = new SocialMediaMessageModel
                            {
                                Title = reader.GetString(0),
                                Username = reader.GetString(2),
                                Messageid = reader.GetInt32(3),
                                Message = reader.GetString(1)
                            };
                            ret.Add(add);
                        }
                        command.CommandText =
                            $"SELECT b.titel, b.inhoud,a.gebruikersnaam, b.bijdrageid FROM bericht b, bijdrage bd, account a, bijdrage_bericht bb WHERE b.bijdrageid = bd.bijdrageID AND bd.accountid = a.accountid AND bb.bijdrageid = b.bijdrageid AND b.bijdrageid ={messageid} OR bb.bijdrageid={messageid} ORDER BY b.bijdrageID DESC";
                        OracleDataReader dr = command.ExecuteReader();
                        while (dr.Read())
                        {
                            SocialMediaMessageModel add = new SocialMediaMessageModel
                            {
                                Title = dr.GetString(0),
                                Username = dr.GetString(2),
                                Messageid = dr.GetInt32(3),
                                Message = dr.GetString(1)
                            };
                            ret.Add(add);
                        }
                    }
                    catch { }
                }
            }
            return ret;
        }
        protected string GetColumnParameter(Dictionary<string, string> values, bool Parameter)
        {
            if (!Parameter)
            {
                string rows = "";
                foreach (string c in values.Keys)
                {
                    if (values.Keys.Last() == c) { rows += c; }
                    else { rows += c + ","; }
                }
                return rows;
            }
            else if (Parameter)
            {
                string ParameterRows = "";
                foreach (string c in values.Keys)
                {
                    if (values.Keys.Last() == c) { ParameterRows += ":" + c; }
                    else { ParameterRows += ":" + c + ","; }
                }
                return ParameterRows;
            }
            else return "error";
        }

        protected string GetColumnNames(List<string> data)
        {
            string rows = "";
            foreach (string c in data)
            {
                if (data.Last() == c) { rows += c; }
                else { rows += c + ","; }
            }
            return rows;
        }
    }
}