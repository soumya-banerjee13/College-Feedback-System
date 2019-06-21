using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class deleteUser_manually : System.Web.UI.Page
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
    protected void deleteUser_Click(object sender, EventArgs e)
    {
        if (queryBox.Text != "")
        {
            try
            {
                string queryString = "select * from student_list where user_id='" + queryBox.Text.ToString() + "'";
                DataSet ds = dba.fetchData(queryString);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string deleteQuery = "delete from student_list where user_id='" + queryBox.Text.ToString() + "'";
                    bool success = dba.saveData(deleteQuery);
                    if (success == true)
                    {
                        msg.ForeColor = Color.Green;
                        msg.Text = "User Id '" + queryBox.Text .ToString()+ "' and related record deleted successfully";
                    }
                    else
                    {
                        msg.ForeColor = Color.Red;
                        msg.Text = "Some error occured...";
                    }
                }
                else
                {
                    msg.ForeColor = Color.Blue;
                    msg.Text = "User Id not found!";
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
            msg.Text = "Please provide a user id to delete.";
            msg.ForeColor = Color.Blue;
        }
    }
}