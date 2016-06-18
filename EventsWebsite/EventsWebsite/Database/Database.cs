using System;
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
                if (values.Keys.Last() == v){querUpdateValues += v + " = '" + values[v] + "'";}
                else { querUpdateValues += v + " = '" + values[v] + "', "; }
            }

            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand($"update {table} set {querUpdateValues} where {condition1} = {condition2}", conn))
                   
                {
                    command.BindByName = true;
                    command.Parameters.Add(new OracleParameter(":Condition2", condition2));
                    foreach (string c in values.Keys)
                    {
                        string value = values[c];
                        command.Parameters.Add(new OracleParameter(c, value));
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
                using (OracleCommand command = new OracleCommand("SELECT " + column + " FROM " + table + " WHERE " + ConditionValue1 + " = :Condition2", conn))
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
                                    UserModel user = new UserModel(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), Convert.ToInt32(reader[5]), reader[6].ToString(), Convert.ToInt32(reader[7]), reader[8].ToString(), reader[9].ToString());
                                    ReturnData.Add(user);
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
                                        MaterialModel m = new MaterialModel(reader.GetInt32(0).ToString(), reader.GetInt32(1).ToString(), true, true);
                                        ReturnData.Add(m);
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
                                        EventModel e = new EventModel(reader.GetString(0),reader.GetDateTime(1),reader.GetDateTime(2),reader.GetInt32(3));
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

        public virtual MaterialModel ReadExemplarenModel(string table, List<string> data, string ConditionValue1, string ConditionValue2)
        {
            MaterialModel ReturnData = null;
            string columnNames = GetColumnNames(data);
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("SELECT " + columnNames + " FROM " + table + " WHERE " + ConditionValue1 + " = " + ConditionValue2, conn))
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
                                    MaterialModel m = new MaterialModel(reader.GetInt32(0), reader.GetInt32(1));
                                    ReturnData = m;
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
                                    UserModel user = new UserModel(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), Convert.ToInt32(reader[5]), reader[6].ToString(), Convert.ToInt32(reader[7]), reader[8].ToString(), reader[9].ToString());
                                    ReturnData = user;
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

        public virtual List<string> ReadWithCondition(string table, List<string> data, string ConditionValue1, string ConditionValue2)
        {
            string ColumNames = GetColumnNames(data);
            List<string> ReturnData = new List<string>();
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("SELECT " + ColumNames + " FROM " + table + " WHERE " + ConditionValue1 + " = :Condition2", conn))
                {
                    command.BindByName = true;
                    command.Parameters.Add(new OracleParameter(":Condition2", ConditionValue2));
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
                            "SELECT ExemplaarID FROM VERHUUR v, RESERVERING_POLSBANDJE rp, RESERVERING r, PLEK_RESERVERING pr, Plek p, LOCATIE l, EVENT e WHERE v.Reservering_PolsbandjeID = rp.ID AND rp.ReserveringID = r.ReserveringID AND r. ReserveringID = pr.ReserveringID AND pr.PlekID = p.PlekID AND p.LocatieID = l.LocatieID AND l.LocatieID = e.LocatieID AND EventID = :ei",
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

        public virtual List<int> GetFreeMaterial(int eventid, int exemplaarid)
        {
            List<int> ReturnData = new List<int>();
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (
                    OracleCommand command =
                        new OracleCommand(
                            "SELECT ex.ExemplaarID FROM EXEMPLAAR ex, VERHUUR v, RESERVERING_POLSBANDJE rp, RESERVERING r, PLEK_RESERVERING pr, Plek p, LOCATIE l, EVENT e WHERE ex.ExemplaarID = v.ExemplaarID AND v.Reservering_PolsbandjeID = rp.ID AND rp.ReserveringID = r.ReserveringID AND r. ReserveringID = pr.ReserveringID AND pr.PlekID = p.PlekID AND p.LocatieID = l.LocatieID AND l.LocatieID = e.LocatieID AND EventID = :ei AND :exi NOT IN SELECT ExemplaarID FROM VERHUUR v, RESERVERING_POLSBANDJE rp, RESERVERING r, PLEK_RESERVERING pr, Plek p, LOCATIE l, EVENT e WHERE v.Reservering_PolsbandjeID = rp.ID AND rp.ReserveringID = r.ReserveringID AND r. ReserveringID = pr.ReserveringID AND pr.PlekID = p.PlekID AND p.LocatieID = l.LocatieID AND l.LocatieID = e.LocatieID AND EventID = :ei;",
                            conn)
                    )
                {
                    command.BindByName = true;
                    command.Parameters.Add(":ei", eventid);
                    command.Parameters.Add(":exi", eventid);
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

        public virtual List<int> GetAllMaterial(int eventid)
        {
            List<int> ReturnData = new List<int>();
            using (OracleConnection conn = new OracleConnection(Connectionstring))
            {
                using (
                    OracleCommand command =
                        new OracleCommand(
                            "SELECT ex.ExemplaarID FROM EXEMPLAAR ex, VERHUUR v, RESERVERING_POLSBANDJE rp, RESERVERING r, PLEK_RESERVERING pr, Plek p, LOCATIE l, EVENT e WHERE ex.ExemplaarID = v.ExemplaarID AND v.Reservering_PolsbandjeID = rp.ID AND rp.ReserveringID = r.ReserveringID AND r. ReserveringID = pr.ReserveringID AND pr.PlekID = p.PlekID AND p.LocatieID = l.LocatieID AND l.LocatieID = e.LocatieID AND EventID = 1",
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

        public virtual List<EventModel> GetAllEvents()
        {
            List<EventModel> returnData = new List<EventModel>();
            try
            {
                using (OracleConnection conn = new OracleConnection(Connectionstring))
                {
                    conn.Open();
                    using (
                        OracleCommand command =
                            new OracleCommand(
                                "SELECT eventid, event.naam, datumstart,datumeinde, plaats FROM event, locatie WHERE event.locatieid = locatie.locatieid",
                                conn))
                    {
                        OracleDataReader dr = command.ExecuteReader();
                        while (dr.Read())
                        {
                            EventModel add = new EventModel();
                            add.EventID = dr.GetInt32(0);
                            add.Name = dr.GetString(1);
                            add.DateStart = dr.GetDateTime(2);
                            add.DateEnd = dr.GetDateTime(3);
                            add.City = dr.GetString(4);
                            returnData.Add(add);
                        }
                    }
                }
            }
            catch { }
            return returnData;
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

        public virtual bool ExecuteProcedure(string procedure)
        {
            using (OracleConnection con = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand(procedure,con))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
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
