﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    catch
                    {

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
                using (OracleCommand command = new OracleCommand("UPDATE " + table + " SET " + querUpdateValues + " WHERE " + condition1 + " = :Condition2", conn))
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
                using (OracleCommand command = new OracleCommand("SELECT " + columnNames + " FROM " + table + " WHERE " + ConditionValue1 + " = :Condition2", conn))
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

                            }
                            return ReturnData;
                        }
                    }

                    catch { }
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
