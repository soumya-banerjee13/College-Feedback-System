using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Web.Configuration;

public partial class student_login : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        Labelstate.Text = "";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Session.Abandon();
        //}
        Labelstate.Text = "";
    }
    protected void login_button_Click(object sender, EventArgs e)
    {
        if (user.Text != "" && password.Text != "")
        {
            try
            {
                DataSet ds = dba.fetchData("select * from student_list where user_id collate latin1_general_cs='" + user.Text + "' and password collate latin1_general_cs='" + password.Text + "'");
                if (ds.Tables[0].Rows.Count == 1)
                {
                    string stream = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                    string sem = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                    string status = LoginControl(ds);
                    if(status=="done0")
                    {
                        Session["stud_user"] = user.Text;
                        Session["stud_stream"] = stream;
                        Session["stud_sem"] = sem;
                        Session["stud_flag"] = 0;
                        //Labelstate.Text = "user=" + user.Text + ", stream=" + stream + ", sem=" + sem + ", flag=0";
                        Response.Redirect("student_dashboard.aspx");
                    }
                    else if(status=="done1")
                    {
                        Session["stud_user"] = user.Text;
                        Session["stud_stream"] = stream;
                        Session["stud_sem"] = sem;
                        Session["stud_flag"] = 1;
                        //Labelstate.Text = "user=" + user.Text + ", stream=" + stream + ", sem=" + sem + ", flag=1";
                        Response.Redirect("student_dashboard.aspx");
                    }
                    else
                    {
                        Labelstate.ForeColor = Color.White;
                        Labelstate.Text = status;
                    }
                }
                else
                {
                    Labelstate.ForeColor = Color.White;
                    Labelstate.Text = "Invalid username or password...";
                }
            }
            catch (Exception)
            {
                Labelstate.ForeColor = Color.Red;
                Labelstate.Text = "Unable to connect to the server...";
            }
        }
        else
        {
            Labelstate.ForeColor = Color.White;
            Labelstate.Text = "Please enter Username and Password";
        }
    }
    string LoginControl(DataSet ds)
    {
        //dirty_bit is to test whether already logged in user has submitted feedback or not  
        int dirty_bit = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[7]);
        int logged_in = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[4]);
        int last_log = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[5]);

        string status = "Something is not right";

        //fetching date from setDate table
        string qry01 = "select * from set_date where fb=1";
        string qry02 = "select * from set_date where fb=2";

        DataSet fd1 = dba.fetchData(qry01);
        DataSet fd2 = dba.fetchData(qry02);
        int d1 = Convert.ToInt32(fd1.Tables[0].Rows[0].ItemArray[1].ToString());
        int m1 = Convert.ToInt32(fd1.Tables[0].Rows[0].ItemArray[2].ToString());
        int y1 = Convert.ToInt32(fd1.Tables[0].Rows[0].ItemArray[3].ToString());
        int d2 = Convert.ToInt32(fd2.Tables[0].Rows[0].ItemArray[1].ToString());
        int m2 = Convert.ToInt32(fd2.Tables[0].Rows[0].ItemArray[2].ToString());
        int y2 = Convert.ToInt32(fd2.Tables[0].Rows[0].ItemArray[3].ToString());

        DateTime pDate1 = new DateTime(y1, m1, d1);
        DateTime pDate2 = new DateTime(y2, m2, d2);

        if(dirty_bit==0)
        {
            if(last_log==0)
            {
                if (CheckDate(pDate2).Days >= 0)
                {
                    bool r = dba.saveData("update student_list set logged_in=1,last_log=2,dirty_bit=1,date='" + DateTime.Now.ToString("dd/MM/yyyy") + "' where user_id='" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "'");
                    if (r == true)
                    {
                        status = "doneNow1";
                    }
                }
                else if (CheckDate(pDate1).Days >= 0)
                {
                    bool r = dba.saveData("update student_list set logged_in=1,last_log=1,dirty_bit=1,date='" + DateTime.Now.ToString("dd/MM/yyyy") + "' where user_id='" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "'");
                    if (r == true)
                    {
                        status = "doneNow0";
                    }
                }
            }
            else if(last_log==1)
            {
                if (CheckDate(pDate2).Days >= 0)
                {
                    bool r = dba.saveData("update student_list set logged_in=2,last_log=2,dirty_bit=1,date='" + DateTime.Now.ToString("dd/MM/yyyy") + "' where user_id='" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "'");
                    if (r == true)
                    {
                        status = "doneNow1";
                    }
                }
                else
                {
                    status = "You can login after " + pDate2.ToString("dd/MM/yy");
                }
            }
            else if(last_log==2)
            {
                if (logged_in==2)
                {
                    status = "You have already given feedback twice in this semester";
                }
                else
                {
                    status = "You have already submitted feedback-II in this semester";
                }
            }
        }
        else if(dirty_bit==1)
        {
            if(last_log==1)
            {
                if (CheckDate(pDate2).Days >= 0)
                {
                    bool r = dba.saveData("update student_list set last_log=2,date='" + DateTime.Now.ToString("dd/MM/yyyy") + "' where user_id='" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "'");
                    if (r == true)
                    {
                        status = "doneBefore1";
                    }
                }
                else if (CheckDate(pDate1).Days >= 0)
                {
                    bool r = dba.saveData("update student_list set date='" + DateTime.Now.ToString("dd/MM/yyyy") + "' where user_id='" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "'");
                    if (r == true)
                    {
                        status = "doneBefore0";
                    }
                }
            }
            else if(last_log==2)
            {
                if (CheckDate(pDate2).Days >= 0)
                {
                    bool r = dba.saveData("update student_list set date='" + DateTime.Now.ToString("dd/MM/yyyy") + "' where user_id='" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "'");
                    if (r == true)
                    {
                        status = "doneBefore1";
                    }
                }
                else
                {
                    status = "You can login after " + pDate2.ToString("dd/MM/yy");
                }
            }
        }
        if((status=="doneNow0")||(status=="doneBefore0"))
        {
            status = "done0";
        }
        else if ((status == "doneNow1") || (status == "doneBefore1"))
        {
            status = "done1";
        }
        return status;
    }
    TimeSpan CheckDate(DateTime pDate)
    {
        int year = Convert.ToInt32( System.DateTime.Now.Year.ToString("0#"));
        int month = Convert.ToInt32(System.DateTime.Now.Month.ToString("0#"));
        int day = Convert.ToInt32(System.DateTime.Now.Day.ToString("0#"));
        DateTime cDate = new DateTime(year, month, day);
        TimeSpan ts = (cDate - pDate);
        return ts;
    }
}