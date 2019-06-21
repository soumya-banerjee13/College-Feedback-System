using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class fdb_gateway : System.Web.UI.Page
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
        if(Session["user_admin"]!=null)
        {
            access.Visible = true;
        }
        else
        {
            Response.Write("<script>confirm('Session Expired! Redirecting to Admin Login page...'); window.location='Default.aspx'</script>");
        }
    }
    protected void go_Click(object sender, EventArgs e)
    {
        if(DropDownList1.SelectedIndex!=0 && DropDownList2.SelectedIndex!=0)
        {
            Session["stream"] = DropDownList1.SelectedItem.ToString();
            Session["semester"] = DropDownList2.SelectedItem.ToString();
            Response.Redirect("fdb_general.aspx");
        }
        else
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select stream and semester...";
        }
    }
}