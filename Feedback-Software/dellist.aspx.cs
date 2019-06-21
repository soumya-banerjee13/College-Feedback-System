using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using System.IO;

public partial class dellist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["user_admin"]==null)
        {
            Response.Write("<script>confirm('Session Expired! Redirecting to Admin Login page...'); window.location='Default.aspx'</script>");
        }
    }
    protected void btndel_Click(object sender, EventArgs e)
    {
        try
        {
            string qur = "delete from student_list";
            bool dec = dba.saveData(qur);
            if (dec == true)
            {
                lblstate.Text = "Records deleted successfully...";
            }
            else
            {
                lblstate.Text = "Records not deleted...";
            }
        }
        catch(Exception)
        {
            lblstate.Text = "Some error occured...";
        }
    }
}