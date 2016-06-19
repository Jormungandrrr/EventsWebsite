using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using EventsWebsite.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Web.UI.WebControls;

namespace EventsWebsite.Database
{
    class SocialMediaDB : Database
    {
        public bool AddMessage(SocialMediaMessageModel model, int uid)
        {
            return AddMessage("BijdrageBericht", model.Title, model.Message, uid);
        }

        public bool AddAttachment(SocialMediaMessageModel model, int i, string fileurl)
        {
            return AddFile("BijdrageBestand", fileurl, model.FileUpload.ContentLength, i);
        }

        public List<SocialMediaMessageModel> getallposts()
        {
            return AllPosts();
        }

        public List<SocialMediaMessageModel> GetDetailView(int i)
        {
            return DetailPost(i);
        }
        public bool Reply(SocialMediaMessageModel model, int uid)
        {
            return AddReply("BijdrageReactie", model.Title, model.Message, uid, model.Messageid);
        }

        public bool AddMessage(string procedure, string titel, string inhoud, int userid)
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
        public bool AddFile(string procedure, string floc, int fsize, int userid)
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

        public bool AddReply(string procedure, string titel, string inhoud, int userid, int messageid)
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
                        command.Parameters.Add("t_reactieop", OracleDbType.Int32, messageid, ParameterDirection.Input);
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
        public  List<SocialMediaMessageModel> AllPosts()
        {
            List<SocialMediaMessageModel> ret = new List<SocialMediaMessageModel>();
            using (OracleConnection con = new OracleConnection(Connectionstring))
            {
                using (OracleCommand command = new OracleCommand("SELECT b.titel,a.gebruikersnaam, b.bijdrageid FROM bericht b, bijdrage bd, account a WHERE b.bijdrageid = bd.bijdrageID AND bd.accountid = a.accountid AND b.bijdrageID NOT IN(SELECT berichtid FROM bijdrage_bericht) ORDER BY b.bijdrageID DESC", con))
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
                        command.CommandText =
                            "SELECT b.bestandslocatie, b.bijdrageid,a.gebruikersnaam FROM bestand b, bijdrage bd, account a WHERE b.bijdrageid = bd.bijdrageid AND bd.accountID = a.accountid ORDER BY b.bijdrageID DESC";
                        OracleDataReader dr2 = command.ExecuteReader();
                        while (dr2.Read())
                        {
                            SocialMediaMessageModel add = new SocialMediaMessageModel
                            {
                                Title = "Mediapost",
                                Username = dr2.GetString(2),
                                Messageid = dr2.GetInt32(1),
                                Filepath = dr2.GetString(0)
                            };
                            ret.Add(add);
                        }
                    }
                    catch { }
                }
            }
            return ret;
        }
        public List<SocialMediaMessageModel> DetailPost(int messageid)
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
                        command.CommandText = $"SELECT berichtid FROM bijdrage_bericht WHERE bijdrageid ={messageid}";
                           
                        OracleDataReader dr = command.ExecuteReader();
                        while (dr.Read())
                        {
                            command.CommandText =
                                $"SELECT b.titel, b.inhoud, a.gebruikersnaam FROM bericht b, bijdrage bd, account a WHERE b.bijdrageid = bd.bijdrageID AND bd.accountid = a.accountid AND b.bijdrageid ={dr.GetInt32(0)}";
                            OracleDataReader dr2 = command.ExecuteReader();
                            dr2.Read();
                            SocialMediaMessageModel add = new SocialMediaMessageModel
                            {
                                Title = dr2.GetString(0),
                                Username = dr2.GetString(2),
                                Messageid = dr.GetInt32(0),
                                Message = dr2.GetString(1)
                            };
                            ret.Add(add);
                        }
                    }
                    catch { }
                }
            }
            return ret;
        }
    }
}
