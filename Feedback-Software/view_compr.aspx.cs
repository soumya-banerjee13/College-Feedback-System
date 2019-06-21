using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class view_compr : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
            show.Text = "";
            string qry = "select short_name from streams order by short_name";
            DataSet ds = dba.fetchData(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DropDownList1.Items.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString().ToUpper());
                }
            }
            for (int i = 2016; i <= 2100; i++)
            {
                DropDownList5.Items.Add(i.ToString());
            }
        }
        catch (Exception)
        {
            msg.ForeColor = Color.Red;
            msg.Text = "Some error occured while initializing the page...";
        }
    }
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
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList3.Items.Count > 2)
        {
            for (int i = DropDownList3.Items.Count - 1; i >= 2; i--)
            {
                DropDownList3.Items.RemoveAt(i);
            }
        }
        if (DropDownList1.SelectedIndex != 0)
        {
            if (DropDownList2.SelectedIndex != 0)
            {
                try
                {
                    string qry = "select subj_name,subj_code from subjects where stream='" + DropDownList1.SelectedItem.ToString() + "' and semester='" + DropDownList2.SelectedItem.ToString() + "'";
                    DataSet ds = dba.fetchData(qry);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string name = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                            string code = ds.Tables[0].Rows[i].ItemArray[1].ToString().ToLower();
                            DropDownList3.Items.Add(new ListItem(name, code));
                        }
                    }
                }
                catch
                {
                    msg.ForeColor = Color.Red;
                    msg.Text = "Some error occured...";
                }
            }
        }
    }
    protected void view_Click(object sender, EventArgs e)
    {
        msg.Text = "";
        show.Text = "";
        GridView1.Visible = false;
        if (DropDownList1.SelectedIndex != 0 && DropDownList2.SelectedIndex != 0 && DropDownList3.SelectedIndex != 0 && DropDownList4.SelectedIndex != 0 && DropDownList5.SelectedIndex != 0)
        {
            try
            {
                if (GridView1.Columns.Count > 4)
                {
                    for (int i = GridView1.Columns.Count - 1; i >= 4; i--)
                    {
                        GridView1.Columns.RemoveAt(i);
                    }
                }
                string compr = "compr_" + DropDownList1.SelectedItem.ToString().ToLower();
                string stream=DropDownList1.SelectedItem.ToString().ToUpper();
                string sem=DropDownList2.SelectedItem.ToString().ToLower();
                string selQry = "select (select subj_name from subjects s where c.subj_code=s.subj_code and s.stream='"+stream+"' and s.semester='"+sem+"') as subj_name,opt_count,total,marks,(select f.indx from fb_view f where f.numb=c.status) as status,year from "+compr+" c where semester='"+sem+"'";
                string addi = "";
                if (DropDownList3.SelectedIndex != 1)
                {
                    if (addi.Length > 0) { addi = addi + " and "; }
                    addi = addi + "subj_code='" + DropDownList3.SelectedValue.ToString() + "'";
                }
                if (DropDownList4.SelectedIndex != 1)
                {
                    if (addi.Length > 0) { addi = addi + " and "; }
                    addi = addi + "status=(select numb from fb_view where indx='" + DropDownList4.SelectedItem.ToString() + "')";
                }
                if (DropDownList5.SelectedIndex != 1)
                {
                    if (addi.Length > 0) { addi = addi + " and "; }
                    addi = addi + "year='" + DropDownList5.SelectedItem.ToString() + "'";
                }
                if (addi.Length > 0)
                {
                    selQry = selQry + " and " + addi;
                }
                if(DropDownList6.SelectedIndex != 0)
                {
                    selQry = selQry + " order by c." + DropDownList6.SelectedValue.ToString();
                }
                if(DropDownList4.SelectedIndex==1)
                {
                    GridView1.Columns.Add(new BoundField { DataField = "status", HeaderText = "Feedback", SortExpression = "status" });
                }
                if (DropDownList5.SelectedIndex == 1)
                {
                    GridView1.Columns.Add(new BoundField { DataField = "year", HeaderText = "Year", SortExpression = "year" });
                    //GridView1.PageSize = 2;
                }
                int itemNumberPerPage = Convert.ToInt32(DropDownList7.SelectedItem.ToString());
                DataSet ds = dba.fetchData(selQry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.PageSize = itemNumberPerPage;
                    GridView1.Visible = true;
                    show.Text = "View of Compressed Records of Stream-" + DropDownList1.SelectedItem.ToString() + ", Semester-" + DropDownList2.SelectedItem.ToString() + ", Subject-" + DropDownList3.SelectedItem.ToString() + ", Feedback-" + DropDownList4.SelectedItem.ToString() + ", Year-" + DropDownList5.SelectedItem.ToString();
                }
                else
                {
                    msg.ForeColor = Color.Blue;
                    msg.Text = "No data available...";
                }
            }
            catch (Exception)
            {
                msg.ForeColor = Color.Red;
                msg.Text = "Some error occured...";
            }
        }
        else if (DropDownList1.SelectedIndex == 0 || DropDownList2.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select stream and semester...";
        }
        else if (DropDownList3.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select subject...";
        }
        else if (DropDownList4.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select feedback...";
        }
        else if (DropDownList5.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select year...";
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
    }
}