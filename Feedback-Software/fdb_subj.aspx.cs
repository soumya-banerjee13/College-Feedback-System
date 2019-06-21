using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class fdb_subj : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        show.Text = "";
        mark.Visible=false;
        GridView1.Visible=false;
        msg.Text = "";
        if (Session["user_admin"] != null && Session["stream"] != null && Session["semester"] != null && Session["subj_name"]!=null && Session["subj_code"]!=null && Session["fdb"]!=null)
        {
            if (Session["fdb"].ToString().Equals("1st"))
            {
                show.Text = "1st Feedback Summary of " + Session["subj_name"].ToString() + " of " + Session["stream"].ToString().ToUpper() + " " + Session["semester"].ToString() + " Semester";
            }
            else if (Session["fdb"].ToString().Equals("2nd"))
            {
                show.Text = "2nd Feedback Summary of " + Session["subj_name"].ToString() + " of " + Session["stream"].ToString().ToUpper() + " " + Session["semester"].ToString() + " Semester";
            }
            else if (Session["fdb"].ToString().Equals("all"))
            {
                show.Text = "Feedback Summary of " + Session["subj_name"].ToString() + " of " + Session["stream"].ToString().ToUpper() + " " + Session["semester"].ToString() + " Semester";
            }
            try
            {
                string subj = Session["subj_code"].ToString();
                string subj_com = subj + "_com";
                string strSem = Session["stream"].ToString().ToLower() + "_" + Session["semester"].ToString().ToLower();
                string optQry = "select optio,up_limit,low_limit from opt order by low_limit desc";
                DataSet dsOpt = dba.fetchData(optQry);
                if (dsOpt.Tables[0].Rows.Count > 0)
                {
                    string marksQry = "select ";
                    for(int i=0;i<dsOpt.Tables[0].Rows.Count;i++)
                    {
                        string optio = dsOpt.Tables[0].Rows[i].ItemArray[0].ToString();
                        string opt_comp = optio.Replace(" ", "");
                        string up_lmt = dsOpt.Tables[0].Rows[i].ItemArray[1].ToString();
                        string low_lmt = dsOpt.Tables[0].Rows[i].ItemArray[2].ToString();
                        GridView1.Columns.Add(new BoundField { DataField = opt_comp, HeaderText = optio, SortExpression = opt_comp });
                        string othQry = "";
                        if (Session["fdb"].ToString().Equals("1st")) 
                        {
                            marksQry = marksQry + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=0 and a.feedback=1) as " + opt_comp + ","; //upto this portion for mark-sheet
                            othQry = "select " + subj_com + " from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=0 and a.feedback=1";
                        }
                        else if (Session["fdb"].ToString().Equals("2nd"))
                        {
                            marksQry = marksQry + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=1 and a.feedback=2) as " + opt_comp + ","; //upto this portion for mark-sheet
                            othQry = "select " + subj_com + " from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=1 and a.feedback=2";
                        }
                        else if (Session["fdb"].ToString().Equals("all"))
                        {
                            marksQry = marksQry + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + ") as " + opt_comp + ","; //upto this portion for mark-sheet
                            othQry = "select " + subj_com + " from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt;
                        }
                        string commQry = "select comm from comm where optio='" + optio + "' order by comm";
                        DataSet dsComm = dba.fetchData(commQry);
                        DataSet dsOth = dba.fetchData(othQry);
                        string oth_com = "";
                        for (int k = 0; k < dsOth.Tables[0].Rows.Count; k++) 
                        {
                            string oth = dsOth.Tables[0].Rows[k].ItemArray[0].ToString();
                            string[] othArr = oth.Split('.');
                            foreach (string word in othArr)
                            {
                                bool flag = true;
                                if (word.Length > 3)
                                {
                                    for (int j = 0; j < dsComm.Tables[0].Rows.Count; j++)
                                    {
                                        string compare = dsComm.Tables[0].Rows[j].ItemArray[0].ToString();
                                        if (word.Contains(compare))
                                        {
                                            flag = false;
                                        }
                                    }
                                }
                                else
                                {
                                    flag = false;
                                }
                                if(flag==true)
                                {
                                    oth_com = oth_com + word + "***";
                                }
                            }
                        }
                        if (dsComm.Tables[0].Rows.Count > 0)
                        {
                            LiteralControl lc1 = new LiteralControl();
                            lc1.Text = @"<br/><br/>";
                            bdy.Controls.Add(lc1);
                            Label Label1 = new Label();
                            Label1.ID = "opt" + (i + 1).ToString();
                            Label1.Text = "Comments Related to Option " + optio;
                            Label1.CssClass = "lbl";
                            bdy.Controls.Add(Label1);
                            LiteralControl lc2 = new LiteralControl();
                            lc2.Text = @"<br/>";
                            bdy.Controls.Add(lc2);
                            GridView grd1 = new GridView();
                            grd1.ID = "grd" + (i + 1).ToString();
                            grd1.CssClass = "Grid";
                            grd1.AlternatingRowStyle.CssClass = "alt";
                            grd1.PagerStyle.CssClass = "pgr";
                            grd1.AutoGenerateColumns = false;
                            string cntQry = "select ";
                            for (int j = 0; j < dsComm.Tables[0].Rows.Count; j++)
                            {
                                string cmnt = dsComm.Tables[0].Rows[j].ItemArray[0].ToString();
                                grd1.Columns.Add(new BoundField { DataField = cmnt, HeaderText = cmnt,SortExpression = cmnt });
                                if (Session["fdb"].ToString().Equals("1st"))
                                {
                                    cntQry = cntQry + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and instr(" + subj_com + ",'" + cmnt + "') and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=0 and a.feedback=1) as '" + cmnt + "'";
                                }
                                else if (Session["fdb"].ToString().Equals("2nd"))
                                {
                                    cntQry = cntQry + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and instr(" + subj_com + ",'" + cmnt + "') and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=1 and a.feedback=2) as '" + cmnt + "'";
                                }
                                else if (Session["fdb"].ToString().Equals("all"))
                                {
                                    cntQry = cntQry + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and instr(" + subj_com + ",'" + cmnt + "') and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + ") as '" + cmnt + "'";
                                }
                                if(j!=(dsComm.Tables[0].Rows.Count-1))
                                {
                                     cntQry=cntQry+ ",";
                                }
                            }
                            DataSet dsCnt = dba.fetchData(cntQry);
                            grd1.DataSource = dsCnt.Tables[0];
                            grd1.DataBind();
                            bdy.Controls.Add(grd1);
                            Label Label2 = new Label();
                            Label2.ID = "oth" + (i + 1).ToString();
                            Label2.CssClass = "show";
                            Label2.Text = "Other Comments: "+oth_com;
                            bdy.Controls.Add(Label2);
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
                    GridView1.Columns.Add(new BoundField { DataField = "total", HeaderText = "Total Feedbacks", SortExpression = "total" });
                    GridView1.Columns.Add(new BoundField { DataField = "result", HeaderText = "Total Marks", SortExpression = "result" });
                    DataSet dsRslt = dba.fetchData(marksQry);
                    if (dsRslt.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = dsRslt.Tables[0];
                        GridView1.DataBind();
                        GridView1.Visible = true;
                        mark.Visible = true;
                    }
                    else
                    {
                        msg.ForeColor = Color.Blue;
                        msg.Text = "No data available...";
                    }
                }
                else
                {
                    msg.ForeColor = Color.Blue;
                    msg.Text = "Summary Cannot be Produced as No Option and Related Marks is Inserted...";
                }
            }
            catch (Exception)
            {
                msg.ForeColor = Color.Blue;
                //msg.Text = ee.Message;
                msg.Text = "Some error occured while initializing the page...";
            }
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
}