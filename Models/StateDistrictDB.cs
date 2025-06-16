using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MVC_ddbind.Models
{
    public class StateDistrictDB
    {
        string conString = ConfigurationManager.ConnectionStrings["TestCon"].ConnectionString;
        SqlConnection con;
        public StateDistrictDB()
        {
            con = new SqlConnection(conString);
        }
        public List<stclass> Selectstates()
        {
            var getdata = new List<stclass>();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_state", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var o = new stclass
                    {
                        sId = Convert.ToInt32(sdr["StateId"]),
                        sName = sdr["StateName"].ToString()
                    };
                    getdata.Add(o);
                }
                con.Close();
                return getdata;


            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
        public List<Dclass> Selectdistricts(int stateId)
        {
            var getdata = new List<Dclass>();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_districts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@stid", stateId);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var o = new Dclass
                    {
                        dId = Convert.ToInt32(sdr["Did"]),
                        dName = sdr["DName"].ToString()
                    };
                    getdata.Add(o);
                }
                con.Close();
                return getdata;


            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;

            }
        }
    }
}