using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class userName_query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        access.Visible = true;
        if (Session["user_admin"] != null)
        {
            access.Visible = true;
        }
        else
        {
            Response.Write("<script>confirm('Session Expired! Redirecting to Admin Login page...'); window.location='Default.aspx'</script>");
        }
    }
    protected void query_Click(object sender, EventArgs e)
    {
        if (queryBox.Text != "")
        {
            try
            {
                string queryString = "select user_id from student_list where roll_no='" + queryBox.Text.ToString() + "'";
                DataSet ds = dba.fetchData(queryString);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    msg.ForeColor = Color.Green;
                    msg.Text = "Found! User Id for this roll no. is '" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "'";
                }
                else
                {
                    msg.ForeColor = Color.Blue;
                    msg.Text = "No user id matched for roll no. " + queryBox.Text.ToString();
                }
            }
            catch (Exception)
            {
                msg.ForeColor = Color.Red;
                msg.Text = "Some error occured...";
            }
        }
        else
        {
            msg.Text = "Please provide a roll no. to verify user id.";
            msg.ForeColor = Color.Blue;
        }
    }
}