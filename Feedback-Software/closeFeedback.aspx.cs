using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class closeFeedback : System.Web.UI.Page
{
    bool open1 = false;
    bool open2 = false;
    protected override void OnInit(EventArgs e)
    {
        try
        {
            access.Visible = false;
            if (Session["user_admin"] != null)
            {
                string qry1 = "select open from set_date where fb=1";
                DataSet ds1 = dba.fetchData(qry1);
                string qry2 = "select open from set_date where fb=2";
                DataSet ds2 = dba.fetchData(qry2);
                if (ds1.Tables[0].Rows[0].ItemArray[0].ToString() == "0")
                {
                    open1 = true;
                }
                else
                {
                    open1 = false;
                }
                if (ds2.Tables[0].Rows[0].ItemArray[0].ToString() == "0")
                {
                    open2 = true;
                }
                else
                {
                    open2 = false;
                }
            }
        }
        catch (Exception)
        {
            msg.ForeColor = Color.Red;
            msg.Text = "Some error occured...";
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_admin"] != null)
        {
            access.Visible = true;
        }
        else
        {
            Response.Write("<script>confirm('Session Expired! Redirecting to Admin Login page...'); window.location='Default.aspx'</script>");
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList1.SelectedIndex != 0)
            {
                if (DropDownList1.SelectedIndex == 1)
                {
                    openClose.Visible = true;
                    if (open1 == true)
                    {
                        openClose.Text = "Close";
                    }
                    else
                    {
                        openClose.Text = "Open";
                    }
                }
                else
                {
                    openClose.Visible = true;
                    if (open2 == true)
                    {
                        openClose.Text = "Close";
                    }
                    else
                    {
                        openClose.Text = "Open";
                    }
                }
            }
            else
            {
                openClose.Text = "Open/Close";
            }
        }
        catch (Exception)
        {
            msg.ForeColor = Color.Red;
            msg.Text = "Some error occured...";
        }
    }
    protected void openClose_Click(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList1.SelectedIndex != 0)
            {
                if (DropDownList1.SelectedIndex == 1)
                {
                    string updateQry = "";
                    if (openClose.Text == "Open")
                    {
                        updateQry = "update set_date set open=0 where fb=1";
                    }
                    else if (openClose.Text == "Close")
                    {
                        updateQry = "update set_date set open=1 where fb=1";
                    }
                    bool updated = dba.saveData(updateQry);
                    if (updated == true)
                    {
                        if (open1 == true)
                        {
                            open1 = false;
                            msg.ForeColor = Color.Blue;
                            msg.Text = "Feedback1 closed successfully";
                            openClose.Text = "Open";
                        }
                        else 
                        { 
                            open1 = true;
                            msg.ForeColor = Color.Green;
                            msg.Text = "Feedback1 is opened now";
                            openClose.Text = "Close";
                        }
                    }
                }
                if (DropDownList1.SelectedIndex == 2)
                {
                    string updateQry = "";
                    if (openClose.Text == "Open")
                    {
                        updateQry = "update set_date set open=0 where fb=2";
                    }
                    else if (openClose.Text == "Close")
                    {
                        updateQry = "update set_date set open=1 where fb=2";
                    }
                    bool updated = dba.saveData(updateQry);
                    if (updated == true)
                    {
                        if (open2 == true)
                        {
                            open2 = false;
                            msg.ForeColor = Color.Blue;
                            msg.Text = "Feedback2 closed successfully";
                            openClose.Text = "Open";
                        }
                        else
                        {
                            open2 = true;
                            msg.ForeColor = Color.Green;
                            msg.Text = "Feedback2 is opened now";
                            openClose.Text = "Close";
                        }
                    }
                }
            }
            else
            {
                msg.ForeColor = Color.Blue;
                msg.Text = "Please select a feedback";
            }
        }
        catch (Exception)
        {
            msg.ForeColor = Color.Red;
            msg.Text = "Some error occured...";
        }
    }
}