using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class db_generate : System.Web.UI.Page
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
            msg.Visible = true;
            msg.ForeColor = Color.Red;
            msg.Text = "Some error occured while initializing the page...";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        access.Visible = false;
        GridView1.Visible = false;
        msg.Visible = false;
        if(Session["user_admin"]!=null)
        {
            access.Visible = true;
        }
        else
        {
            Response.Write("<script>confirm('Session Expired! Redirecting to Admin Login page...'); window.location='Default.aspx'</script>");
        }
    }
    protected void chng_Click(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        show.Visible = false;
        msg.Visible = false;
        if (DropDownList1.SelectedIndex != 0 && DropDownList2.SelectedIndex != 0)
        {
            try
            {
                string fQry = "select subj_code from subjects where stream='" + DropDownList1.SelectedItem.ToString() + "' and semester='" + DropDownList2.SelectedItem.ToString() + "' order by subj_name";
                DataSet ds = dba.fetchData(fQry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string strSem = (DropDownList1.SelectedItem.ToString() + "_" + DropDownList2.SelectedItem.ToString()).ToLower();
                    string drQry = "drop table if exists " + strSem + "";
                    bool dec = dba.saveData(drQry);
                    if(dec==true)
                    {
                        string crQry = "create table " +strSem + "(id int(11) not null auto_increment,user_id varchar(45) not null,";
                        for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                        {
                            string subj = ds.Tables[0].Rows[i].ItemArray[0].ToString().ToLower();
                            string subj_com = subj + "_com";
                            crQry = crQry + subj + " int(3) not null," + subj_com + " varchar(500),";
                        }
                        crQry = crQry + "date varchar(20),flag int(1),constraint "+strSem+"_fk foreign key(user_id) references student_list(user_id) on delete cascade,primary key(id),unique index id_unq (id ASC));";
                        bool send = dba.saveData(crQry);
                        if (send == true)
                        {
                            msg.Visible = true;
                            msg.ForeColor = Color.Green;
                            msg.Text = "Changes applied to the system...";
                        }
                        else
                        {
                            msg.Visible = true;
                            msg.ForeColor = Color.Red;
                            msg.Text = "Changes not applied...";
                        }
                    }
                    else
                    {
                        msg.Visible = true;
                        msg.ForeColor = Color.Red;
                        msg.Text = "Some error occured. Changes not applied...";
                    }
                }
            }
            catch (Exception ee)
            {
                msg.Visible = true;
                msg.ForeColor = Color.Red;
                msg.Text = ee.Message;
                //msg.Text = "Some error occured...";
            }
        }
    }
    protected void view_Click(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        show.Visible = false;
        msg.Visible = false;
        try
        {
            if (DropDownList1.SelectedIndex != 0 && DropDownList2.SelectedIndex != 0)
            {
                string qry = "select subj_name,subj_code from subjects where stream='" + DropDownList1.SelectedItem.ToString() + "' and semester='" + DropDownList2.SelectedItem.ToString() + "' order by subj_name";
                DataSet ds = dba.fetchData(qry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    show.Visible = true;
                    show.Text = "You are viewing list of current subjects of " + DropDownList1.SelectedItem.ToString() + " " + DropDownList2.SelectedItem.ToString() + " semester";
                }
                else
                {
                    msg.Visible = true;
                    msg.ForeColor = Color.Blue;
                    msg.Text = "No subjects uploaded...";
                }
            }
            else if (DropDownList1.SelectedIndex == 0 && DropDownList2.SelectedIndex == 0)
            {
                msg.Visible = true;
                msg.ForeColor = Color.Blue;
                msg.Text = "Please select stream and semester...";
            }
            else if (DropDownList1.SelectedIndex == 0)
            {
                msg.Visible = true;
                msg.ForeColor = Color.Blue;
                msg.Text = "Please select stream...";
            }
            else if (DropDownList2.SelectedIndex == 0)
            {
                msg.Visible = true;
                msg.ForeColor = Color.Blue;
                msg.Text = "Please select semester...";
            }
        }
        catch (Exception)
        {
            msg.Visible = true;
            msg.ForeColor = Color.Red;
            msg.Text = "Some error occured while showing subject list...";
        }
    }
}