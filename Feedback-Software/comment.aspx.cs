using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class comment : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
            GridView1.Visible = false;
            for(int i=-9;i<10;i++)
            {
                DropDownList2.Items.Add(i.ToString());
            }
            string qry = "select optio from opt order by low_limit";
            DataSet ds = dba.fetchData(qry);
            if(ds.Tables[0].Rows.Count>0)
            {
                for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                {
                    DropDownList1.Items.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString());
                    DropDownList3.Items.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString());
                }
            }
            else
            {
                stat.Visible = true;
                stat.ForeColor = Color.Magenta;
                stat.Text = "No option added...";
                HyperLink h = new HyperLink();
                h.NavigateUrl = "option.aspx";
                h.Text = "Go to Add/Remove Option Page to add some options";
                hyp.Controls.Add(h);
            }
        }
        catch(Exception)
        {
            stat.Visible = true;
            stat.ForeColor = Color.Red;
            //stat.Text = ee.Message;
            stat.Text = "Some error occured while initializing the page...";
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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedIndex != 0)
        {
            try
            {
                GridView1.Visible = false;
                lst.Text = "";
                string qry = "select comm,comm_marks from comm where optio='" + DropDownList1.SelectedItem.ToString() + "'";
                DataSet ds = dba.fetchData(qry);
                if(ds.Tables[0].Rows.Count>0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    stat.Visible = false;
                    lst.Text = "Comments List Related to Option '" + DropDownList1.SelectedItem.ToString()+"'";
                }
            }
            catch (Exception)
            {
                stat.Visible = true;
                stat.ForeColor = Color.Red;
                stat.Text = "Some error occured...";
            }
        }
        else
        {
            GridView1.Visible = false;
            lst.Text = "";
            stat.Visible = false;
        }
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList3.SelectedIndex != 0)
        {
            try
            {
                GridView1.Visible = false;
                lst.Text = "";
                string qry = "select comm,comm_marks from comm where optio='" + DropDownList3.SelectedItem.ToString() + "'";
                DataSet ds = dba.fetchData(qry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    stat.Visible = false;
                    lst.Text = "Comments List Related to Option '" + DropDownList3.SelectedItem.ToString()+"'";
                    if(DropDownList4.Items.Count>1)
                    {
                        for(int i=DropDownList4.Items.Count-1;i>0;i--)
                        {
                            DropDownList4.Items.RemoveAt(i);
                        }
                    }
                    for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                    {
                        DropDownList4.Items.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString());
                    }
                }
            }
            catch (Exception)
            {
                stat.Visible = true;
                stat.ForeColor = Color.Red;
                stat.Text = "Some error occured...";
            }
        }
        else
        {
            GridView1.Visible = false;
            lst.Text = "";
            stat.Visible = false;
            for (int i = DropDownList4.Items.Count - 1; i >= 1; i--)
            {
                DropDownList4.Items.RemoveAt(i);
            }
        }
    }
    protected void addComment_Click(object sender, EventArgs e)
    {
        msg2.Text = "";
        DropDownList3.SelectedIndex = 0;
        if(TextBox1.Text!="" && DropDownList1.SelectedIndex!=0 && DropDownList2.SelectedIndex!=0 && TextBox1.Text.Length<=50)
        {
            try
            {
                string qry = "insert into comm(optio,comm,comm_marks) values('" + DropDownList1.SelectedItem.ToString() + "','" + TextBox1.Text.ToString() + "','" + DropDownList2.SelectedItem.ToString() + "')";
                bool b = dba.saveData(qry);
                if(b==true)
                {
                    msg1.ForeColor = Color.Green;
                    msg1.Text = "New Comment '"+TextBox1.Text.ToString()+"' Related to Option '"+DropDownList1.SelectedItem.ToString()+"' Added Successfully...";
                    qry="select comm,comm_marks from comm where optio='" + DropDownList1.SelectedItem.ToString() + "'";
                    GridView1.Visible = false;
                    DataSet ds = dba.fetchData(qry);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                        GridView1.Visible = true;
                        stat.Visible = false;
                        lst.Text = "Comments List Related to Option '" + DropDownList1.SelectedItem.ToString()+"'";
                    }
                    else
                    {
                        stat.Visible = true;
                        stat.ForeColor = Color.Magenta;
                        stat.Text = "There is No Comment Related to Option '" + DropDownList1.SelectedItem.ToString() + "'";
                        lst.Text = "";
                    }
                }
                else
                {
                    msg1.ForeColor = Color.Red;
                    msg1.Text = "Some error occured";
                }
            }
            catch(Exception ee)
            {
                msg1.ForeColor = Color.Red;
                //msg1.Text = ee.Message;
                msg1.Text = "Some error occured.New comment not been added...";
                if (ee.Message.Contains("Duplicate entry"))
                {
                    msg1.Text = "You can not add an an existing comment for the same option";
                }
            }
        }
        else if (TextBox1.Text.Length > 50)
        {
            msg1.ForeColor = Color.Blue;
            msg1.Text = "Try a comment within 50 letters...";
        }
        else
        {
            msg1.ForeColor = Color.Blue;
            msg1.Text = "Please fill the form above...";
        }
    }
    protected void remComment_Click(object sender, EventArgs e)
    {
        msg1.Text = "";
        if(DropDownList3.SelectedIndex!=0 && DropDownList4.SelectedIndex!=0)
        {
           try
           {
               string qry = "delete from comm where optio='" + DropDownList3.SelectedItem.ToString() + "' and comm='" + DropDownList4.SelectedItem.ToString() + "'";
               bool b = dba.saveData(qry);
               if (b == true)
               { 
                   msg2.ForeColor = Color.Green;
                   msg2.Text = "Old Comment '" + DropDownList4.SelectedItem.ToString() + "' Related to Option " + DropDownList3.SelectedItem.ToString() + " Removed Successfully...";
                   string qry1 = "select comm,comm_marks from comm where optio='" + DropDownList3.SelectedItem.ToString() + "'";
                   GridView1.Visible = false;
                   DataSet ds1 = dba.fetchData(qry1);
                   if (ds1.Tables[0].Rows.Count > 0)
                   {
                       GridView1.DataSource = ds1.Tables[0];
                       GridView1.DataBind();
                       GridView1.Visible = true;
                       stat.Visible = false;
                       lst.Text = "Comments List Related to Option '" + DropDownList3.SelectedItem.ToString() + "'";
                   }
                   else
                   {
                       stat.Visible = true;
                       stat.ForeColor = Color.Magenta;
                       stat.Text = "There is No Comment Related to Option '" + DropDownList3.SelectedItem.ToString() + "'";
                       lst.Text = "";
                   }
                   DropDownList3.SelectedIndex = 0;
               }
               else
               {
                   msg2.ForeColor = Color.Red;
                   msg2.Text = "Some error occured";
               }
               
           }
           catch(Exception)
           {
               msg2.ForeColor = Color.Red;
               //msg1.Text = ee.Message;
               msg2.Text = "Some error occured.Old comment not been deleted...";
           }
        }
        else
        {
            msg2.ForeColor = Color.Blue;
            msg2.Text = "Please fill the form above...";
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
    }
}