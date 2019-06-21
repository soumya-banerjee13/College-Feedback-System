using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;

public partial class setdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        access.Visible = false;
        if (Session["user_admin"] != null)
        {
            access.Visible = true;
        }
        else
        {
            Response.Write("<script>confirm('Session Expired! Redirecting to Admin Login page...'); window.location='Default.aspx'</script>");
        }
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        int d = Calendar1.SelectedDate.Day;
        int m = Calendar1.SelectedDate.Month;
        int y = Calendar1.SelectedDate.Year;
        Label1.Text = d.ToString() + "/" + m.ToString() + "/" + y.ToString();
       //Label1.Text = Calendar1.SelectedDate.ToString("MM/dd/yyyy");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if ((Label1.Text.ToString() != "") && (Label2.Text.ToString() != ""))
            {
                int d1 = Calendar1.SelectedDate.Day;
                int m1 = Calendar1.SelectedDate.Month;
                int y1 = Calendar1.SelectedDate.Year;
                int d2 = Calendar2.SelectedDate.Day;
                int m2 = Calendar2.SelectedDate.Month;
                int y2 = Calendar2.SelectedDate.Year;
                string dt1 = m1.ToString() + "-" + d1.ToString() + "-" + y1.ToString();
                string dt2 = m2.ToString() + "-" + d2.ToString() + "-" + y2.ToString();
                DateTime date1 = Convert.ToDateTime(dt1);//ToString(),"m-d-yyyy",null);
                DateTime date2 = Convert.ToDateTime(dt2);
                TimeSpan ts = date2 - date1;
                if (ts.Days <= 0)
                {
                    msg.ForeColor = Color.Magenta;
                    msg.Text = "Start date of feedback-I can not be after start date of feedback-II";
                }
                else
                {
                    //string qry1 = "insert into set_date values(1,'" + d1.ToString() + "','" + m1.ToString() + "','" + y1.ToString() + "')";
                    //string qry2 = "insert into set_date values(2,'" + d2.ToString() + "','" + m2.ToString() + "','" + y2.ToString() + "')";
                    string qry1 = "update set_date set dt='" + d1.ToString() + "', mt='" + m1.ToString() + "',yr='" + y1.ToString() + "' where fb=1";
                    string qry2 = "update set_date set dt='" + d2.ToString() + "', mt='" + m2.ToString() + "',yr='" + y2.ToString() + "' where fb=2";
                    bool sd1 = dba.saveData(qry1);
                    bool sd2 = dba.saveData(qry2);
                    if ((sd1 == true) && (sd2 == true))
                    {
                        msg.ForeColor = Color.Green;
                        msg.Text = "New values has been set";
                    }
                    else
                    {
                        msg.ForeColor = Color.Red;
                        msg.Text = "New values has not been set successfully";
                    }
                }
            }
            else
            {
                msg.ForeColor = Color.Blue;
                msg.Text = "Please select start date for all feedbacks";
            }
        }
        catch(Exception ee)
        {
            msg.ForeColor = Color.Red;
            msg.Text = ee.Message.ToString();
        }
    }
    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        {
            int d = Calendar2.SelectedDate.Day;
            int m = Calendar2.SelectedDate.Month;
            int y = Calendar2.SelectedDate.Year;
            Label2.Text = d.ToString() + "/" + m.ToString() + "/" + y.ToString();
            //Label2.Text = Calendar2.SelectedDate.ToString("MM/dd/yyyy");
        }
    }
}