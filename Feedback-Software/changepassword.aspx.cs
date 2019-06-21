using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Web.Configuration;

public partial class changepassword : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        access.Visible = false;
        if(Session["user_admin"]!=null)
        {
            access.Visible = true;
            Label1.Text = Session["user_admin"].ToString();
        }
        else
        {
            Response.Write("<script>confirm('Session Expired! Redirecting to Admin Login page...'); window.location='Default.aspx'</script>");
        }
    }
    
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        con.ConnectionString = WebConfigurationManager.ConnectionStrings["feedback"].ConnectionString;

        string tuid = TextBoxuid.Text.Trim();
        string pass = TextBoxpwd.Text.Trim();

        try
        {
            string sqlquery1 = "SELECT username, password FROM log_table WHERE username='" + tuid + "' AND " + "password='" + pass + "'";
            MySqlCommand com1 = new MySqlCommand(sqlquery1, con);

            string str1 = "", str2 = "";

            con.Open();
            MySqlDataReader rdr = com1.ExecuteReader();

            while (rdr.Read())
            {
                str1 = rdr["username"].ToString();
                str2 = rdr["password"].ToString();
            }
            con.Close();

            if (str1 != "" && str2 != "")
            {
                string query2 = "UPDATE log_table SET password=@uidpass WHERE username=@uid ";
                MySqlCommand cmd2 = new MySqlCommand(query2, con);

                string newpass = TextBoxnewpwd.Text.Trim();
                // string cnfpass = TextBoxcnfpwd.Text.Trim();

                cmd2.Parameters.AddWithValue("@uidpass", newpass);
                cmd2.Parameters.AddWithValue("@uid", str1);
                //cmd2.Parameters.AddWithValue("@password",str2);
                con.Open();
                cmd2.ExecuteNonQuery();
                con.Close();
                Labelstate.Text = "Password changed successfully.";
            }
            else
            {
                Labelstate.Text = "Wrong userid and password";
            }
        }
        catch (Exception mex)
        {
            Labelstate.Text = mex.Message;
        }
        finally
        {

        }
    }
}