using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class option : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
            for(int i=1;i<=100;i++)
            {
                DropDownList1.Items.Add(i.ToString());
            }
            for(int i=1;i<=91;i+=10)
            {
                DropDownList2.Items.Add(i.ToString());
            }
            for (int i = 10; i <= 100; i += 10)
            {
                DropDownList3.Items.Add(i.ToString());
            }
            string qry = "select * from opt order by low_limit";
            DataSet ds = dba.fetchData(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    stat.Visible = false;
                    lst.Visible = true;
                    DropDownList4.Items.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString());
                }
            }
            else
            {
                stat.Visible = true;
                stat.ForeColor = Color.Magenta;
                stat.Text = "No option exists...";
            }
        }
        catch (Exception)
        {
            stat.Visible = true;
            stat.ForeColor = Color.Red;
            //stat.Text = ee.Message;
            stat.Text = "Some error occured while initializing the page...";
            lst.Visible = false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        access.Visible = false;
        if(Session["user_admin"]!=null)
        {
            access.Visible = true;
        }
        else
        {
            Response.Write("<script>confirm('Session Expired! Redirecting to Admin Login page...'); window.location='Default.aspx'</script>");
        }
    }
    protected void addOption_Click(object sender, EventArgs e)
    {
        msg2.Text = "";
        if(TextBox1.Text!="" && DropDownList1.SelectedIndex!=0 && DropDownList2.SelectedIndex!=0 && DropDownList3.SelectedIndex!=0)
        {
            try
            {
                int marks = Convert.ToInt32(DropDownList1.SelectedItem.ToString());
                int low = Convert.ToInt32(DropDownList2.SelectedItem.ToString());
                int up = Convert.ToInt32(DropDownList3.SelectedItem.ToString());
                if(low>up)
                {
                    msg1.ForeColor = Color.Blue;
                    msg1.Text = "Lower limit must be less than the upper limit";
                }
                else if (marks <= up && marks >= low)
                {
                    string chk = "select marks,low_limit,up_limit from opt";
                    DataSet ds = dba.fetchData(chk);
                    int status = 0;
                    for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                    {
                        if (low > Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]) && low > Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2])) status = 0;
                        else if (up < Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]) && up < Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2])) status = 0;
                        else status=1;
                    }
                    if(status==0)
                    {
                        string qry = "insert into opt(optio,marks,low_limit,up_limit) values('" + TextBox1.Text + "','" + DropDownList1.SelectedItem.ToString() + "','" + DropDownList2.SelectedItem.ToString() + "','" + DropDownList3.SelectedItem.ToString() + "')";
                        bool b = dba.saveData(qry);
                        if (b == true)
                        {
                            msg1.ForeColor = Color.Green;
                            msg1.Text = "New option added successfully...";
                            GridView1.Visible = false;
                            string qry1 = "select * from opt order by low_limit";
                            DataSet ds1 = dba.fetchData(qry1);
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                GridView1.DataSource = ds1.Tables[0];
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
                            if (DropDownList4.Items.Count > 1)
                            {
                                for (int i = DropDownList4.Items.Count - 1; i >= 1; i--)
                                {
                                    DropDownList4.Items.RemoveAt(i);
                                }
                            }
                            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                            {
                                DropDownList4.Items.Add(ds1.Tables[0].Rows[i].ItemArray[0].ToString().ToUpper());
                            }
                            TextBox1.Text = "";
                        }
                    }
                    else
                    {
                        msg1.ForeColor = Color.Red;
                        msg1.Text = "Marks for new option should be unique and its range can not be in range of any existing option...";
                    }
                }
                else
                {
                    msg1.ForeColor = Color.Blue;
                    msg1.Text = "Marks for option should be in the range between lower and upper limit";
                }
            }
            catch(Exception ee)
            {
                msg1.ForeColor = Color.Red;
                //msg1.Text = ee.Message;
                msg1.Text = "New option not been added...";
                if(ee.Message.Contains("Duplicate entry"))
                {
                    msg1.Text = "Do not try to enter an existing option name without deleting it";
                }
            }
        }
        else
        {
            msg1.ForeColor = Color.Blue;
            msg1.Text = "Fill the form above...";
        }
    }
    protected void remOption_Click(object sender, EventArgs e)
    {
        msg1.Text = "";
        if (DropDownList4.SelectedIndex != 0)
        {
            try
            {
                string qry = "delete from opt where optio='" + DropDownList4.SelectedItem.ToString() + "'";
                bool b = dba.saveData(qry);
                if (b == true)
                {
                    msg2.ForeColor = Color.Green;
                    msg2.Text = "Old option " + DropDownList4.SelectedItem.ToString() + " removed successfully...";
                    string qry1 = "select * from opt order by low_limit";
                    GridView1.Visible = false;
                    DataSet ds1 = dba.fetchData(qry1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds1.Tables[0];
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
                    if (DropDownList4.Items.Count > 1)
                    {
                        for (int i = DropDownList4.Items.Count - 1; i >= 1; i--)
                        {
                            DropDownList4.Items.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        DropDownList4.Items.Add(ds1.Tables[0].Rows[i].ItemArray[0].ToString().ToUpper());
                    }
                }
                else
                {
                    msg2.ForeColor = Color.Red;
                    msg2.Text = "Option not deleted";
                }
            }
            catch (Exception)
            {
                msg2.ForeColor = Color.Red;
                msg2.Text = "Some error occured.Option not deleted...";
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