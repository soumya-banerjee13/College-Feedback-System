using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Web.Configuration;


    public partial class Default : System.Web.UI.Page
    {
        //MySqlConnection con = new MySqlConnection();
        protected override void OnInit(EventArgs e)
        {
            Labelstate.Text = "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Labelstate.Text = "";
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            if (tbname.Text != "" && tbpass.Text != "")
            {
                try
                {
                    DataSet ds = dba.fetchData("select * from log_table where username collate latin1_general_cs ='" + tbname.Text + "' and password collate latin1_general_cs ='" + tbpass.Text + "'");
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        Session["user_admin"] = tbname.Text;
                        Response.Redirect("adminfunction.aspx");
                    }
                    else
                    {
                        Labelstate.ForeColor = Color.White;
                        Labelstate.Text = "Invalid username or password...";
                    }
                }
                catch (Exception)
                {
                    Labelstate.ForeColor = Color.Red;
                    Labelstate.Text = "Unable to connect to the server...";
                }
            }
            else
            {
                Labelstate.ForeColor = Color.White;
                Labelstate.Text = "Please enter Username and Password";
            }
        }
}
