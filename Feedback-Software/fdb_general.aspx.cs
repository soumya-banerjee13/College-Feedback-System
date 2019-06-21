using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class fdb_general : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
            if(Session["user_admin"]!=null && Session["stream"]!=null && Session["semester"]!=null)
            {
                string qry = "select subj_name,subj_code from subjects where stream='" + Session["stream"].ToString() + "' and semester='" + Session["semester"].ToString() + "' order by subj_code";
                DataSet ds = dba.fetchData(qry);
                if(ds.Tables[0].Rows.Count>0)
                {
                    GridView1.Columns.Add(new BoundField { DataField = "user_id", HeaderText = "User ID", SortExpression = "user_id" });
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string name = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                        string code = ds.Tables[0].Rows[i].ItemArray[1].ToString().ToLower();
                        RadioButtonList1.Items.Add(new ListItem(name,code));
                        GridView1.Columns.Add(new BoundField { DataField = code, HeaderText = name, SortExpression = code });
                        GridView1.Columns.Add(new BoundField { DataField = code + "_com", HeaderText = "Comment", SortExpression = code + "_com" });
                    }
                    GridView1.Columns.Add(new BoundField { DataField = "date", HeaderText = "Date", SortExpression = "date" });
                    GridView1.Columns.Add(new BoundField { DataField = "indx", HeaderText = "Feedback", SortExpression = "indx" });
                }
                else
                {
                    Label1.ForeColor = Color.Red;
                    Label1.Text = "No subjects are uploaded for" + Session["stream"].ToString().ToUpper() + " " + Session["semester"].ToString().ToLower() + " semester";
                }
            }
        }
        catch(Exception)
        {
            msg.ForeColor = Color.Red;
            msg.Text = "Some error occured while initializing the page...";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        access.Visible = false;
        if(Session["user_admin"]!=null && Session["stream"]!=null && Session["semester"]!=null)
        {
            access.Visible = true;
            strSem.Text = "VIEW FEEDBACKS OF " + Session["stream"].ToString().ToUpper() + " " + Session["semester"].ToString().ToLower() + " SEMESTER";
        }
        else
        {
            Response.Write("<script>confirm('Session Expired! Redirecting to Admin Login page...'); window.location='Default.aspx'</script>");
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
    }
    protected void btnf1_Click(object sender, EventArgs e)
    {
        try
        {
            GridView1.Visible = false;
            lblstat.Visible = false;
            msg.Visible = false;
            Label1.Visible = false;
            string strSem = Session["stream"].ToString().ToLower() + "_" + Session["semester"].ToString().ToLower();
            string qry = "select user_id,";
            for (int i = 1; i < RadioButtonList1.Items.Count; i++)
            {
                string subj = RadioButtonList1.Items[i].Value;
                string comm = subj + "_com";
                qry += "(select optio from opt where p." + subj + ">=low_limit and p." + subj + "<=up_limit) as "+subj+"," + comm + ",";
            }
            qry += "date,indx from " + strSem + " p,fb_view f where f.numb=p.flag and p.flag=0 order by user_id";
            DataSet ds = dba.fetchData(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                int count = GridView1.Rows[0].Cells.Count;
                GridView1.Visible = true;
                if(count>0)
                {
                    GridView1.Columns[count - 1].Visible = false;
                }
                lblstat.Text = "You are viewing 1st feedback of " + Session["stream"].ToString().ToUpper() + " " + Session["semester"].ToString().ToLower() + " semester";
                lblstat.Visible = true;
            }
            else
            {
                msg.Visible = true;
                msg.ForeColor = Color.Blue;
                msg.Text = "No data available...";
            }
        }
        catch (Exception ee)
        {
            msg.Visible = true;
            msg.ForeColor = Color.Red;
            if (ee.Message.ToLower().Contains("doesn't exist"))
            {
                msg.Text = "Subjects not uploaded";
            }
            else
            {
                msg.Text = "Some error occured..."; 
            }
        }
    }
    protected void btnf2_Click(object sender, EventArgs e)
    {
        try
        {
            GridView1.Visible = false;
            lblstat.Visible = false;
            msg.Visible = false;
            Label1.Visible = false;
            string strSem = Session["stream"].ToString().ToLower() + "_" + Session["semester"].ToString().ToLower();
            string qry = "select user_id,";
            for (int i = 1; i < RadioButtonList1.Items.Count; i++)
            {
                string subj = RadioButtonList1.Items[i].Value;
                string comm = subj + "_com";
                qry += "(select optio from opt where p." + subj + ">=low_limit and p." + subj + "<=up_limit) as " + subj + "," + comm + ",";
            }
            qry += "date,indx from " + strSem + " p,fb_view f where f.numb=p.flag and p.flag=1 order by user_id";
            DataSet ds = dba.fetchData(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                int count = GridView1.Rows[0].Cells.Count;
                GridView1.Visible = true;
                if (count > 0)
                {
                    GridView1.Columns[count - 1].Visible = false;
                }
                lblstat.Text = "You are viewing 2nd feedback of " + Session["stream"].ToString().ToUpper() + " " + Session["semester"].ToString().ToLower() + " semester";
                lblstat.Visible = true;
            }
            else
            {
                msg.Visible = true;
                msg.ForeColor = Color.Blue;
                msg.Text = "No data available...";
            }
        }
        catch (Exception ee)
        {
            msg.Visible = true;
            msg.ForeColor = Color.Red;
            if (ee.Message.ToLower().Contains("doesn't exist"))
            {
                msg.Text = "Subjects not uploaded";
            }
            else
            {
                msg.Text = "Some error occured...";
            }
        }
    }
    protected void btnfall_Click(object sender, EventArgs e)
    {
        try
        {
            GridView1.Visible = false;
            lblstat.Visible = false;
            msg.Visible = false;
            Label1.Visible = false;
            string strSem = Session["stream"].ToString().ToLower() + "_" + Session["semester"].ToString().ToLower();
            string qry = "select user_id,";
            for (int i = 1; i < RadioButtonList1.Items.Count; i++)
            {
                string subj = RadioButtonList1.Items[i].Value;
                string comm = subj + "_com";
                qry += "(select optio from opt where p." + subj + ">=low_limit and p." + subj + "<=up_limit) as " + subj + "," + comm + ",";
            }
            qry += "date,indx from " + strSem + " p,fb_view f where f.numb=p.flag order by user_id";
            DataSet ds = dba.fetchData(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                int count = GridView1.Rows[0].Cells.Count;
                GridView1.Visible = true;
                if (count > 0)
                {
                    GridView1.Columns[count - 1].Visible = true;
                }
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                lblstat.Text = "You are viewing all feedbacks of " + Session["stream"].ToString().ToUpper() + " " + Session["semester"].ToString().ToLower() + " semester";
                lblstat.Visible = true;
            }
            else
            {
                msg.Visible = true;
                msg.ForeColor = Color.Blue;
                msg.Text = "No data available...";
            }
        }
        catch (Exception ee)
        {
            msg.Visible = true;
            msg.ForeColor = Color.Red;
            if (ee.Message.ToLower().Contains("doesn't exist"))
            {
                msg.Text = "Subjects not uploaded";
            }
            else
            {
                msg.Text = "Some error occured...";
            }
        }
    }
    protected void hide_Click(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        lblstat.Visible = false;
        msg.Visible = false;
        Label1.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        lblstat.Visible = false;
        msg.Visible = false;
        Label1.Visible = false;
        if (RadioButtonList1.SelectedItem == null)
        {
            Label1.Visible = true;
            Label1.Text = "Please select an option";
        }
        else if(RadioButtonList1.SelectedIndex==0)
        {
            Session["fdb"] = "all";
            Response.Redirect("fdb_allSubj.aspx");
        }
        else
        {
            Session["subj_name"] = RadioButtonList1.SelectedItem.ToString();
            Session["subj_code"] = RadioButtonList1.SelectedValue.ToString();
            Session["fdb"] = "all";
            Response.Redirect("fdb_subj.aspx");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        lblstat.Visible = false;
        msg.Visible = false;
        Label1.Visible = false;
        if (RadioButtonList1.SelectedItem == null)
        {
            Label1.Visible = true;
            Label1.Text = "Please select an option";
        }
        else if (RadioButtonList1.SelectedIndex == 0)
        {
            Session["fdb"] = "1st";
            Response.Redirect("fdb_allSubj.aspx");
        }
        else
        {
            Session["subj_name"] = RadioButtonList1.SelectedItem.ToString();
            Session["subj_code"] = RadioButtonList1.SelectedValue.ToString();
            Session["fdb"] = "1st";
            Response.Redirect("fdb_subj.aspx");
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        lblstat.Visible = false;
        msg.Visible = false;
        Label1.Visible = false;
        if (RadioButtonList1.SelectedItem == null)
        {
            Label1.Visible = true;
            Label1.Text = "Please select an option";
        }
        else if (RadioButtonList1.SelectedIndex == 0)
        {
            Session["fdb"] = "2nd";
            Response.Redirect("fdb_allSubj.aspx");
        }
        else
        {
            Session["subj_name"] = RadioButtonList1.SelectedItem.ToString();
            Session["subj_code"] = RadioButtonList1.SelectedValue.ToString();
            Session["fdb"] = "2nd";
            Response.Redirect("fdb_subj.aspx");
        }
    }
}