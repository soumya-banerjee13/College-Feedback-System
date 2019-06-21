using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for dba
/// </summary>
public class dba
{
    public static bool saveData(string qur)
    {
        string cs = WebConfigurationManager.ConnectionStrings["feedback"].ConnectionString;
        MySqlConnection con = new MySqlConnection(cs);
        con.Open();
        MySqlCommand cmd = new MySqlCommand(qur, con);
        cmd.ExecuteNonQuery();
        con.Close();
        return true;
    }
    public static DataSet fetchData(string qry)
    {
        string cs1 = WebConfigurationManager.ConnectionStrings["feedback"].ConnectionString;
        MySqlConnection con2 = new MySqlConnection(cs1);
        con2.Open();
        MySqlDataAdapter da = new MySqlDataAdapter(qry, con2);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con2.Close();
        return ds;
    }
}