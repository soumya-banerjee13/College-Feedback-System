using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class fdb_allSubj : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        show.Text = "";
        mark.Visible=false;
        GridView1.Visible=false;
        msg.Text = "";
        if (Session["user_admin"] != null && Session["stream"] != null && Session["semester"] != null && Session["fdb"] != null)
        {
            if (Session["fdb"].ToString().Equals("1st"))
            {
                show.Text = "1st Feedback Summary of " + Session["stream"].ToString().ToUpper() + " " + Session["semester"].ToString() + " Semester";
            }
            else if (Session["fdb"].ToString().Equals("2nd"))
            {
                show.Text = "2nd Feedback Summary of " + Session["stream"].ToString().ToUpper() + " " + Session["semester"].ToString() + " Semester";
            }
            else if (Session["fdb"].ToString().Equals("all"))
            {
                show.Text = "Feedback Summary of " + Session["stream"].ToString().ToUpper() + " " + Session["semester"].ToString() + " Semester";
            }
        }
        try
        {
            string subjQry = "select subj_code,subj_name from subjects where stream='" + Session["stream"].ToString().ToUpper() + "' and semester='" + Session["semester"] .ToString().ToLower()+ "' order by subj_code";
            DataSet dsSubj = dba.fetchData(subjQry);
            if(dsSubj.Tables[0].Rows.Count>0)
            {
                string optQry = "select optio,up_limit,low_limit from opt order by low_limit desc";
                DataSet dsOpt = dba.fetchData(optQry);
                if (dsOpt.Tables[0].Rows.Count > 0)
                {
                    string strSem = Session["stream"].ToString().ToLower() + "_" + Session["semester"].ToString().ToLower();
                    GridView1.Columns.Add(new BoundField { DataField = "sname", HeaderText = "Subject Name", SortExpression = "sname" });
                    for (int j = 0; j < dsOpt.Tables[0].Rows.Count; j++)
                    {
                        string optio = dsOpt.Tables[0].Rows[j].ItemArray[0].ToString();
                        string opt_comp = optio.Replace(" ", "");
                        string up_lmt = dsOpt.Tables[0].Rows[j].ItemArray[1].ToString();
                        string low_lmt = dsOpt.Tables[0].Rows[j].ItemArray[2].ToString();
                        GridView1.Columns.Add(new BoundField { DataField = opt_comp, HeaderText = optio, SortExpression = opt_comp });
                    }
                    GridView1.Columns.Add(new BoundField { DataField = "total", HeaderText = "Total Feedbacks", SortExpression = "total" });
                    GridView1.Columns.Add(new BoundField { DataField = "result", HeaderText = "Total Marks", SortExpression = "result" });
                    DataSet dsSummary = new DataSet();
                    for (int i = 0; i < dsSubj.Tables[0].Rows.Count; i++)
                    {
                        string subj = dsSubj.Tables[0].Rows[i].ItemArray[0].ToString();
                        string subj_com = subj + "_com";
                        string marksQry = "select (select subj_name from subjects where subj_code='" + subj + "' and stream='" + Session["stream"].ToString().ToUpper() + "' and semester='" + Session["semester"].ToString().ToLower() + "') as sname,";
                        for(int j=0;j<dsOpt.Tables[0].Rows.Count;j++)
                        {
                            string optio = dsOpt.Tables[0].Rows[j].ItemArray[0].ToString();
                            string opt_comp = optio.Replace(" ", "");
                            string up_lmt = dsOpt.Tables[0].Rows[j].ItemArray[1].ToString();
                            string low_lmt = dsOpt.Tables[0].Rows[j].ItemArray[2].ToString();
                            if (Session["fdb"].ToString().Equals("1st"))
                            {
                                marksQry = marksQry + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='"+subj+"' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=0 and a.feedback=1) as " + opt_comp + ",";
                            }
                            else if (Session["fdb"].ToString().Equals("2nd"))
                            {
                                marksQry = marksQry + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=1 and a.feedback=2) as " + opt_comp + ",";
                            }
                            else if (Session["fdb"].ToString().Equals("all"))
                            {
                                marksQry = marksQry + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + ") as " + opt_comp + ",";
                            }
                        }
                        if (Session["fdb"].ToString().Equals("1st"))
                        {
                            marksQry = marksQry + "(select sum(a.days_present*p." + subj + ") from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=0 and a.feedback=1)/(select sum(a.days_present) from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=0 and a.feedback=1) as result,";
                            marksQry = marksQry + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and flag=0 and a.feedback=1) as total";
                        }
                        else if (Session["fdb"].ToString().Equals("2nd"))
                        {
                            marksQry = marksQry + "(select sum(a.days_present*p." + subj + ") from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=1 and a.feedback=2)/(select sum(a.days_present) from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=1 and a.feedback=2) as result,";
                            marksQry = marksQry + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and flag=1 and a.feedback=2) as total";
                        }
                        else if (Session["fdb"].ToString().Equals("all"))
                        {
                            marksQry = marksQry + "(select sum(a.days_present*p." + subj + ") from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "')/(select sum(a.days_present) from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "') as result,";
                            marksQry = marksQry + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "') as total";
                        }
                        DataSet dsRslt = dba.fetchData(marksQry);
                        if (dsRslt.Tables[0].Rows.Count > 0)
                        {
                            dsSummary.Merge(dsRslt);
                        }
                    }
                    if (dsSummary.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = dsSummary.Tables[0];
                        GridView1.DataBind();
                        GridView1.Visible = true;
                        mark.Visible = true;
                    }
                    else
                    {
                        msg.ForeColor = Color.Blue;
                        msg.Text = "No records available...";
                    }
                }
                else
                {
                    msg.ForeColor = Color.Blue;
                    msg.Text = "Summary Cannot be Produced as No Option and Related Marks is Inserted...";
                }
            }
            else
            {
                msg.ForeColor = Color.Blue;
                msg.Text = "No subject existing for " + Session["stream"].ToString().ToUpper() + " " + Session["semester"].ToString().ToLower() + " semester";
            }
        }
        catch (Exception ee)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = ee.Message;
            //msg.Text = "Some error occured while initializing the page...";
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
}