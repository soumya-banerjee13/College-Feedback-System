using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
public partial class del_compr : System.Web.UI.Page
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
            for(int i=2016;i<=2100;i++)
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
    protected void del_Click(object sender, EventArgs e)
    {
        msg.Text = "";
        if (DropDownList1.SelectedIndex != 0 && DropDownList2.SelectedIndex != 0 && DropDownList3.SelectedIndex != 0 && DropDownList4.SelectedIndex != 0 && DropDownList5.SelectedIndex != 0)
        {
            try
            {
                string compr = "compr_" + DropDownList1.SelectedItem.ToString().ToLower();
                string delQry = "delete from "+compr;
                string addi = "";
                if(DropDownList2.SelectedIndex!=1)
                {
                    addi=addi+ "semester='" + DropDownList2.SelectedItem.ToString() + "'";
                }
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
                if(addi.Length>0)
                {
                    delQry = delQry + " where " + addi;
                }
                bool b = dba.saveData(delQry);
                if(b==true)
                {
                    msg.ForeColor = Color.Green;
                    msg.Text = "Compressed Records deleted for Stream-"+DropDownList1.SelectedItem.ToString()+", Semester-"+DropDownList2.SelectedItem.ToString()+", Subject-"+DropDownList3.SelectedItem.ToString()+", Feedback-"+DropDownList4.SelectedItem.ToString()+", Year-"+DropDownList5.SelectedItem.ToString();
                }
            }
            catch(Exception ee)
            {
                msg.ForeColor = Color.Red;
                //msg.Text = "Some error occured...";
                msg.Text = ee.Message;
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
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList3.Items.Count > 2)
        {
            for (int i = DropDownList3.Items.Count - 1; i >= 2; i--)
            {
                DropDownList3.Items.RemoveAt(i);
            }
        }
        if(DropDownList1.SelectedIndex!=0)
        {
            if (DropDownList2.SelectedIndex != 0 && DropDownList2.SelectedIndex != 1)
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
}