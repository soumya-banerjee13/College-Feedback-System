using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class viewDate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        access.Visible=false;
        if(Session["user_admin"]!=null)
        {
            access.Visible = true;
            try
            {
                string qry01 = "select * from set_date where fb=1";
                string qry02 = "select * from set_date where fb=2";
                //string status = "Some error occured";
                DataSet fd1 = dba.fetchData(qry01);
                DataSet fd2 = dba.fetchData(qry02);
                string d1 = fd1.Tables[0].Rows[0].ItemArray[1].ToString();
                string m1 = fd1.Tables[0].Rows[0].ItemArray[2].ToString();
                string y1 = fd1.Tables[0].Rows[0].ItemArray[3].ToString();
                string open1 = fd1.Tables[0].Rows[0].ItemArray[4].ToString();
                string d2 = fd2.Tables[0].Rows[0].ItemArray[1].ToString();
                string m2 = fd2.Tables[0].Rows[0].ItemArray[2].ToString();
                string y2 = fd2.Tables[0].Rows[0].ItemArray[3].ToString();
                string open2 = fd2.Tables[0].Rows[0].ItemArray[4].ToString();
                if (open1 == "0")
                {
                    Label1.Text = d1 + "-" + m1 + "-" + y1+"(Open)";
                }
                else
                {
                    Label1.Text = d1 + "-" + m1 + "-" + y1+"(Closed)";
                }
                if (open2 == "0")
                {
                    Label2.Text = d2 + "-" + m2 + "-" + y2 + "(Open)";
                }
                else
                {
                    Label2.Text = d2 + "-" + m2 + "-" + y2 + "(Closed)";
                }
            }
            catch(Exception)
            {
                msg.ForeColor = Color.Red;
                msg.Text = "Some error occured...";
            }
        }
        else
        {
            Response.Write("<script>confirm('Session Expired! Redirecting to Admin Login page...'); window.location='Default.aspx'</script>");
        }
    }
}