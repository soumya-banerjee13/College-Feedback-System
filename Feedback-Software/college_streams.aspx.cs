using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

public partial class college_streams : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
            string qry = "select short_name from streams order by short_name";
            DataSet ds = dba.fetchData(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DropDownList1.Items.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString().ToUpper());
                }
            }
        }
        catch (Exception)
        {
            stat.Visible = true;
            stat.ForeColor = Color.Red;
            stat.Text = "Some error occured while initializing the page...";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        access.Visible = false;
        GridView1.Visible = false;
        lst.Visible = false;
        if(Session["user_admin"]!=null)
        {
            access.Visible = true;
            try
            {
                string qry = "select stream_name,short_name from streams";
                DataSet ds = dba.fetchData(qry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    stat.Visible = false;
                    lst.Visible = true;
                }
                else
                {
                    stat.Visible = true;
                    stat.ForeColor = Color.Magenta;
                    stat.Text = "No stream exists...";
                }
            }
            catch (Exception)
            {
            }
        }
        else
        {
            Response.Write("<script>confirm('Session Expired! Redirecting to Admin Login page...'); window.location='Default.aspx'</script>");
        }
    }
    protected void createStream_Click(object sender, EventArgs e)
    {
        msg2.Text = "";
        if (TextBox1.Text != "" && TextBox2.Text != "")
        {
            try
            {
                string compr = "compr_" + TextBox2.Text.ToString().ToLower();
                string qry = "insert into streams(short_name,stream_name) values('" + TextBox2.Text.ToUpper() + "','" + TextBox1.Text + "')";
                qry = qry + "; create table "+compr+"(semester varchar(3),subj_code varchar(10),opt_count varchar(160),total int(5),marks int(3),status int(1),year varchar(5),constraint "+compr+"_pk primary key(subj_code,status,year))";
                bool d = dba.saveData(qry);
                if (d == true)
                {
                    msg1.ForeColor = Color.Green;
                    msg1.Text = "Congratulations! GCETTB get new stream " + TextBox2.Text;
                    GridView1.Visible = false;
                    qry = "select stream_name,short_name from streams order by short_name";
                    DataSet ds = dba.fetchData(qry);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                        GridView1.Visible = true;
                        stat.Visible = false;
                        lst.Visible = true;
                    }
                    else
                    {
                        stat.Visible = true;
                        stat.ForeColor = Color.Magenta;
                        stat.Text = "No stream exists...";
                        lst.Visible = false;
                    }
                    if (DropDownList1.Items.Count >= 1)
                    {
                        for (int i = DropDownList1.Items.Count - 1; i >= 1; i--)
                        {
                            DropDownList1.Items.RemoveAt(i);
                        }
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DropDownList1.Items.Add(ds.Tables[0].Rows[i].ItemArray[1].ToString().ToUpper());
                        }
                    }
                    TextBox1.Text = "";
                    TextBox2.Text = "";
                }
                else
                {
                    msg1.ForeColor = Color.Red;
                    msg1.Text = "New stream not been added";
                }
            }
            catch (Exception)
            {
                msg1.ForeColor = Color.Red;
                msg1.Text = "New stream not been added...";
            }
        }
        else
        {
            msg1.ForeColor = Color.Blue;
            msg1.Text = "Fill the form above...";
        }
    }
    protected void delStream_Click(object sender, EventArgs e)
    {
        msg1.Text = "";
        if(DropDownList1.SelectedIndex!=0)
        {
            try
            {
                string compr = "compr_" + DropDownList1.SelectedItem.ToString().ToLower();
                string qry = "delete from streams where short_name='" + DropDownList1.SelectedItem.ToString() + "';drop table if exists " + compr + "";
                for(int i=1;i<=8;i++)
                {
                    string seq = "";
                    if(i==1)
                    {
                        seq = "_1st";
                    }
                    else if(i==2)
                    {
                        seq = "_2nd";
                    }
                    else if(i==3)
                    {
                        seq = "_3rd";
                    }
                    else
                    {
                        seq = "_"+i.ToString() + "th";
                    }
                    seq = DropDownList1.SelectedItem.ToString().ToLower() + seq;
                    qry += ";drop table if exists " + seq;
                }
                //msg2.Text = qry;
                bool d = dba.saveData(qry);
                if (d == true)
                {
                    msg2.ForeColor = Color.Green;
                    msg2.Text = "Old stream " + DropDownList1.SelectedItem.ToString() + " deleted successfully";
                    GridView1.Visible = false;
                    qry = "select stream_name,short_name from streams order by short_name";
                    DataSet ds = dba.fetchData(qry);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                        GridView1.Visible = true;
                        stat.Visible = false;
                        lst.Visible = true;
                    }
                    else
                    {
                        stat.Visible = true;
                        stat.ForeColor = Color.Magenta;
                        stat.Text = "No stream exists...";
                        lst.Visible = false;
                    }
                    if (DropDownList1.Items.Count > 1)
                    {
                        for (int i = DropDownList1.Items.Count - 1; i >= 1; i--)
                        {
                            DropDownList1.Items.RemoveAt(i);
                        }
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DropDownList1.Items.Add(ds.Tables[0].Rows[i].ItemArray[1].ToString().ToUpper());
                        }
                    }
                    //string qry1 = "select short_name from streams order by short_name";
                    //    DataSet ds = dba.fetchData(qry1);
                    //    if (ds.Tables[0].Rows.Count > 0)
                    //    {
                    //        if (DropDownList1.Items.Count > 1) { }
                    //    }
                }
                else
                {
                    msg2.ForeColor = Color.Red;
                    msg2.Text = "Stream not deleted";
                }
            }
            catch (Exception)
            {
                msg2.ForeColor = Color.Red;
                msg2.Text = "Stream not deleted...";
                //msg2.Text = ee.Message;
            }
        }
        else
        {
            msg2.ForeColor = Color.Blue;
            msg2.Text = "Fill the form above...";
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
    }
}