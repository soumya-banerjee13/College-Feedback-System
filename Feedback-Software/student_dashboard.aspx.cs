using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class student_dashboard : System.Web.UI.Page
{
    bool linkedRoll = false;
    string topMsg = "";
    bool granted = false;
    protected override void OnInit(EventArgs e)
    {
        try
        {
            access.Visible = false;
            if (Session["stud_user"] != null && Session["stud_stream"] != null && Session["stud_sem"] != null && Session["stud_flag"] != null)
            {
                profilePanel.Visible = false;
                link.Visible = false;
                password.Visible = false;
                giveFeedback.Visible = false;
                string linkedQuery = "select roll_no,last_log,dirty_bit from student_list where user_id='" + Session["stud_user"].ToString() + "'";
                DataSet linkedDs = dba.fetchData(linkedQuery);
                if (DBNull.Value.Equals(linkedDs.Tables[0].Rows[0].ItemArray[0]))
                {
                    topMessage.Text = "Link your roll no. with this profile to give feedback";
                    linkRollPanel.CssClass = "panel-button2";
                    linkedRoll = false;
                }
                else
                {
                    linkedRoll=true;
                }
                if(linkedRoll==true)
                {
                    /*TopMessage generation(if roll no. is linked) starts here*/
                    string last_log = linkedDs.Tables[0].Rows[0].ItemArray[1].ToString();
                    string dirty_bit = linkedDs.Tables[0].Rows[0].ItemArray[2].ToString();
                    string qry01 = "select * from set_date where fb=1";
                    string qry02 = "select * from set_date where fb=2";
                    DataSet fd1 = dba.fetchData(qry01);
                    DataSet fd2 = dba.fetchData(qry02);
                    string open1 = fd1.Tables[0].Rows[0].ItemArray[4].ToString();
                    int day1 = Convert.ToInt32(fd1.Tables[0].Rows[0].ItemArray[1].ToString());
                    int month1 = Convert.ToInt32(fd1.Tables[0].Rows[0].ItemArray[2].ToString());
                    int year1 = Convert.ToInt32(fd1.Tables[0].Rows[0].ItemArray[3].ToString());
                    string open2 = fd2.Tables[0].Rows[0].ItemArray[4].ToString();
                    int day2 = Convert.ToInt32(fd2.Tables[0].Rows[0].ItemArray[1].ToString());
                    int month2 = Convert.ToInt32(fd2.Tables[0].Rows[0].ItemArray[2].ToString());
                    int year2 = Convert.ToInt32(fd2.Tables[0].Rows[0].ItemArray[3].ToString());
                    DateTime pDate1 = new DateTime(year1, month1, day1);
                    DateTime pDate2 = new DateTime(year2, month2, day2);
                    TimeSpan fbInterval1 = CheckDate(pDate1);
                    TimeSpan fbInterval2 = CheckDate(pDate2);
                    if (fbInterval2.Days >= 0)
                    {
                        if (open2 == "0")
                        {
                            if (last_log == "1" || (last_log == "2" && dirty_bit == "1"))
                            {
                                topMsg = "Feedback2 form fill-up pending";
                                topMessage.Text = topMsg;
                                granted = true;
                                giveFeedback.Visible = true;
                                //go_to_feedback button visible here
                            }
                            else
                            {
                                topMsg = "No pending feedback";
                                topMessage.Text = topMsg;
                            }
                        }
                        else
                        {
                            if (last_log == "1" || (last_log == "2" && dirty_bit == "1"))
                            {
                                topMsg = "Feedback2 form fill-up portal is closed now";
                                topMessage.Text = topMsg;
                            }
                            else
                            {
                                topMsg = "No pending feedback";
                                topMessage.Text = topMsg;
                            }
                        }
                    }
                    else if(fbInterval1.Days>=0)
                    {
                        if (open1 == "0")
                        {
                            if (last_log == "0" || (last_log == "1" && dirty_bit == "1"))
                            {
                                topMsg = "Feedback1 form fill-up pending";
                                topMessage.Text = topMsg;
                                granted = true;
                                giveFeedback.Visible = true;
                                //go_to_feedback button visible here
                            }
                            else
                            {
                                topMsg = "Feedback2 form fill-up starts from " + day2.ToString() + "/" + month2.ToString() + "/" + year2.ToString();
                                topMessage.Text = topMsg;
                            }
                        }
                        else
                        {
                            if (last_log == "0" || (last_log == "1" && dirty_bit == "1"))
                            {
                                topMsg = "Feedback1 form fill-up portal is closed now";
                                topMessage.Text = topMsg;
                            }
                            else
                            {
                                topMsg = "Feedback2 form fill-up starts from " + day2.ToString() + "/" + month2.ToString() + "/" + year2.ToString();
                                topMessage.Text = topMsg;
                            }
                        }
                    }
                    /*TopMessage generation(if roll no. is linked) ends here*/
                    linkRollPanel.CssClass = "panelLinked-button2";
                }
                else
                {
                    string last_log = linkedDs.Tables[0].Rows[0].ItemArray[1].ToString();
                    string dirty_bit = linkedDs.Tables[0].Rows[0].ItemArray[2].ToString();
                    string qry01 = "select * from set_date where fb=1";
                    string qry02 = "select * from set_date where fb=2";
                    DataSet fd1 = dba.fetchData(qry01);
                    DataSet fd2 = dba.fetchData(qry02);
                    string open1 = fd1.Tables[0].Rows[0].ItemArray[4].ToString();
                    int day1 = Convert.ToInt32(fd1.Tables[0].Rows[0].ItemArray[1].ToString());
                    int month1 = Convert.ToInt32(fd1.Tables[0].Rows[0].ItemArray[2].ToString());
                    int year1 = Convert.ToInt32(fd1.Tables[0].Rows[0].ItemArray[3].ToString());
                    string open2 = fd2.Tables[0].Rows[0].ItemArray[4].ToString();
                    int day2 = Convert.ToInt32(fd2.Tables[0].Rows[0].ItemArray[1].ToString());
                    int month2 = Convert.ToInt32(fd2.Tables[0].Rows[0].ItemArray[2].ToString());
                    int year2 = Convert.ToInt32(fd2.Tables[0].Rows[0].ItemArray[3].ToString());
                    DateTime pDate1 = new DateTime(year1, month1, day1);
                    DateTime pDate2 = new DateTime(year2, month2, day2);
                    TimeSpan fbInterval1 = CheckDate(pDate1);
                    TimeSpan fbInterval2 = CheckDate(pDate2);
                    if (fbInterval2.Days >= 0)
                    {
                        if (open2 == "0")
                        {
                            if (last_log == "1" || (last_log == "2" && dirty_bit == "1"))
                            {
                                topMsg = "Feedback2 form fill-up pending";
                                granted = true;
                            }
                            else
                            {
                                topMsg = "No pending feedback";
                            }
                        }
                        else
                        {
                            if (last_log == "1" || (last_log == "2" && dirty_bit == "1"))
                            {
                                topMsg = "Feedback2 form fill-up portal is closed now";
                            }
                            else
                            {
                                topMsg = "No pending feedback";
                            }
                        }
                    }
                    else if (fbInterval1.Days >= 0)
                    {
                        if (open1 == "0")
                        {
                            if (last_log == "0" || (last_log == "1" && dirty_bit == "1"))
                            {
                                topMsg = "Feedback1 form fill-up pending";
                                granted = true;
                            }
                            else
                            {
                                topMsg = "Feedback2 form fill-up starts from " + day2.ToString() + "/" + month2.ToString() + "/" + year2.ToString();
                            }
                        }
                        else
                        {
                            if (last_log == "0" || (last_log == "1" && dirty_bit == "1"))
                            {
                                topMsg = "Feedback1 form fill-up portal is closed now";
                            }
                            else
                            {
                                topMsg = "Feedback2 form fill-up starts from " + day2.ToString() + "/" + month2.ToString() + "/" + year2.ToString();
                            }
                        }
                    }
                    /*TopMessage generation(if roll no. is linked) ends here*/
                }
            }
        }
        catch(Exception)
        {
            msg.ForeColor = Color.Red;
            msg.Text = "Some error occured...";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //access.Visible cannot be given here updatePanel updation problem will occur
        if (Session["stud_user"] != null && Session["stud_stream"] != null && Session["stud_sem"] != null && Session["stud_flag"] != null)
        {
            if(linkedRoll==false)
            {
                giveFeedback.Visible = false;
                linkRollPanel.CssClass = "panel-button2";
            }
            else
            {
                if (granted == true)
                {
                    giveFeedback.Visible = true;
                }
                linkRollPanel.CssClass = "panelLinked-button2";
            }
            studentId.Text = "Student ID: " + Session["stud_user"].ToString();
            bool visibility1 = false,visibility2=false,visibility3=false;
            if (profilePanel.Visible)
            {
                visibility1 = true;
            }
            if (link.Visible)
            {
                visibility2 = true;
            }
            if (password.Visible)
            {
                visibility3 = true;
            }
            access.Visible = true;
            if (visibility1 == false)
            {
                profilePanel.Visible = false;
            }
            if (visibility2 == false)
            {
                link.Visible = false;
            }
            if (visibility3 == false)
            {
                password.Visible = false;
            }
        }
        else
        {
            access.Visible = false;
            Response.Write("<script>confirm('You are not logged in. Redirecting to Login page...'); window.location='student_login.aspx'</script>");
        }
    }
    protected void logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("student_login.aspx");
    }
    protected void profileStatusPanel_Click(object sender, EventArgs e)
    {
        profilePanel.Visible = false;
        link.Visible = false;
        password.Visible = false;
        msg.Text = "";
        topMessage.Visible = false;
        if (Session["stud_user"] != null && Session["stud_stream"] != null && Session["stud_sem"] != null && Session["stud_flag"] != null)
        {
            try
            {
                if (linkedRoll == true)
                {
                    topMessage.Visible = true;
                    topMessage.Text = topMsg;
                }
                string studentDetails = "select user_id,stream,semester,roll_no,date,logged_in,dirty_bit from student_list where user_id='" + Session["stud_user"].ToString() + "'";
                DataSet detailsDb = dba.fetchData(studentDetails);
                if (detailsDb.Tables[0].Rows.Count > 0) 
                {
                    profileUser.Text = detailsDb.Tables[0].Rows[0].ItemArray[0].ToString();
                    profileStream.Text = detailsDb.Tables[0].Rows[0].ItemArray[1].ToString();
                    profileSem.Text = detailsDb.Tables[0].Rows[0].ItemArray[2].ToString();

                    //Roll logic
                    if (!(DBNull.Value.Equals(detailsDb.Tables[0].Rows[0].ItemArray[3])))
                    {
                        profileRoll.ForeColor = Color.Green;
                        profileRoll.Text = detailsDb.Tables[0].Rows[0].ItemArray[3].ToString();
                    }
                    else
                    {
                        profileRoll.ForeColor = Color.Red;
                        profileRoll.Text = "Not linked";
                    }

                    //Date(Last login) logic
                    if (!(DBNull.Value.Equals(detailsDb.Tables[0].Rows[0].ItemArray[4])))
                    {
                        profileLastLog.Text = detailsDb.Tables[0].Rows[0].ItemArray[4].ToString();
                    }
                    else
                    {
                        profileLastLog.Text = "Never logged in before";
                    }

                    //Feedback given logic
                    string logged_in = detailsDb.Tables[0].Rows[0].ItemArray[5].ToString();
                    string dirty_bit = detailsDb.Tables[0].Rows[0].ItemArray[6].ToString();
                    int noOfFeedback = Convert.ToInt32(logged_in);
                    if (dirty_bit != "0")
                    {
                        noOfFeedback--;
                    }
                    if(noOfFeedback<=0)
                    {
                        profileNoFeedback.Text = "Never in this semester";
                    }
                    else if(noOfFeedback==1)
                    {
                        profileNoFeedback.Text = "Once";
                    }
                    else if(noOfFeedback==2)
                    {
                        profileNoFeedback.Text = "Twice";
                    }

                    profilePanel.Visible = true;
                }
            }
            catch(Exception)
            {
                msg.ForeColor = Color.Red;
                msg.Text = "Some error occured...";
            }
        }
    }
    protected void linkRollPanel_Click(object sender, EventArgs e)
    {
        profilePanel.Visible = false;
        link.Visible = false;
        password.Visible = false;
        msg.Text = "";
        topMessage.Visible = false;
        if (Session["stud_user"] != null && Session["stud_stream"] != null && Session["stud_sem"] != null && Session["stud_flag"] != null)
        {
            try
            {
                if (linkedRoll == true)
                {
                    topMessage.Visible = true;
                    topMessage.Text = topMsg;
                }
                if (linkedRoll == false) 
                {
                    link.Visible = true;
                }
                else
                {
                    msg.ForeColor=Color.Blue;
                    msg.Text = "You have already linked your roll no.";
                }
            }
            catch (Exception)
            {
                msg.ForeColor = Color.Red;
                msg.Text = "Some error occured...";
            }
        }
    }
    protected void changePwdPanel_Click(object sender, EventArgs e)
    {
        profilePanel.Visible = false;
        link.Visible = false;
        password.Visible = false;
        msg.Text = "";
        topMessage.Visible = false;
        if (Session["stud_user"] != null && Session["stud_stream"] != null && Session["stud_sem"] != null && Session["stud_flag"] != null)
        {
            try
            {
                if (linkedRoll == true)
                {
                    topMessage.Visible = true;
                    topMessage.Text = topMsg;
                }
                password.Visible = true;
            }
            catch (Exception)
            {
                msg.ForeColor = Color.Red;
                msg.Text = "Some error occured...";
            }
        }
    }
    protected void linkButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["stud_user"] != null && Session["stud_stream"] != null && Session["stud_sem"] != null && Session["stud_flag"] != null)
            {
                if (linkRlText.Text == "" && linkPassword.Text == "")
                {
                    msg2.ForeColor = Color.Blue;
                    msg2.Text = "Please provide your roll no. and password";
                }
                else if (linkRlText.Text == "")
                {
                    msg2.ForeColor = Color.Blue;
                    msg2.Text = "Please provide your roll no.";
                }
                else if (linkPassword.Text == "")
                {
                    msg2.ForeColor = Color.Blue;
                    msg2.Text = "Please provide password";
                }
                else
                {
                    string beforeLinkQry = "select password,roll_no from student_list where user_id='" + Session["stud_user"].ToString() + "'";
                    DataSet beforeDs = dba.fetchData(beforeLinkQry);
                    if (linkPassword.Text == beforeDs.Tables[0].Rows[0].ItemArray[0].ToString() && DBNull.Value.Equals(beforeDs.Tables[0].Rows[0].ItemArray[1]))
                    {
                        string linkToRoll = "update student_list set roll_no='" + linkRlText.Text + "' where user_id='" + Session["stud_user"].ToString() + "' and password='" + linkPassword.Text + "'";
                        bool linked = dba.saveData(linkToRoll);
                        if (linked == true)
                        {
                            msg2.ForeColor = Color.Green;
                            msg2.Text = "Your profile successfully linked to roll no. " + linkRlText.Text;
                            linkedRoll = true;
                            linkRollPanel.CssClass = "panelLinked-button2";
                        }
                        else
                        {
                            msg2.ForeColor = Color.Red;
                            msg2.Text = "Some error occured...";
                        }
                    }
                    else if (linkPassword.Text != beforeDs.Tables[0].Rows[0].ItemArray[0].ToString())
                    {
                        msg2.ForeColor = Color.Blue;
                        msg2.Text = "Wrong Password";
                    }
                    else
                    {
                        msg2.ForeColor = Color.Blue;
                        msg2.Text = "You have already linked your roll no.";
                    }
                }
            }
        }
        catch (Exception ee)
        {
            msg2.ForeColor = Color.Red;
            //msg2.Text = ee.Message;
            if (ee.Message.ToLower().Contains("duplicate entry"))
            {
                msg2.Text = "This roll no. is already linked to another profile. If this is your own roll no. contact your system administrator.";
            }
            else
            {
                msg2.Text = "Some error occured...";
            }
        }
    }
    protected void pwdButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["stud_user"] != null && Session["stud_stream"] != null && Session["stud_sem"] != null && Session["stud_flag"] != null)
            {
                if (pwdCurrent.Text != "" && pwdNew.Text != "" && pwdConfirm.Text != "" && pwdNew.Text == pwdConfirm.Text)
                {
                    string beforeChangeQry = "select password from student_list where user_id='"+Session["stud_user"].ToString()+"'";
                    DataSet beforeChangeDs = dba.fetchData(beforeChangeQry);
                    if (pwdCurrent.Text == beforeChangeDs.Tables[0].Rows[0].ItemArray[0].ToString())
                    {
                        string changeQry = "update student_list set password='" + pwdNew.Text + "' where user_id='" + Session["stud_user"].ToString()+"'";
                        bool pwdChanged = dba.saveData(changeQry);
                        if (pwdChanged == true)
                        {
                            msg3.ForeColor = Color.Green;
                            msg3.Text = "Your password changed successfully";
                        }
                        else
                        {
                            msg3.ForeColor = Color.Red;
                            msg3.Text = "Some error occured...";
                        }
                    }
                    else
                    {
                        msg3.ForeColor = Color.Blue;
                        msg3.Text = "Wrong Password";
                    }
                }
                else if(pwdCurrent.Text=="")
                {
                    msg3.ForeColor = Color.Blue;
                    msg3.Text = "Please provide your current password";
                }
                else if (pwdNew.Text == "" || pwdConfirm.Text == "")
                {
                    msg3.ForeColor = Color.Blue;
                    msg3.Text = "Please type new password and re-type to confirm";
                }
                else if (pwdNew.Text != pwdConfirm.Text)
                {
                    msg3.ForeColor = Color.Blue;
                    msg3.Text = "New password and re-typed password do not match";
                }
            }
        }
        catch (Exception)
        {
            msg3.ForeColor = Color.Red;
            msg3.Text = "Some error occured...";
        }
    }
    TimeSpan CheckDate(DateTime pDate)
    {
        int year = Convert.ToInt32(System.DateTime.Now.Year.ToString("0#"));
        int month = Convert.ToInt32(System.DateTime.Now.Month.ToString("0#"));
        int day = Convert.ToInt32(System.DateTime.Now.Day.ToString("0#"));
        DateTime cDate = new DateTime(year, month, day);
        TimeSpan ts = (cDate - pDate);
        return ts;
    }
    protected void giveFeedback_Click(object sender, EventArgs e)
    {
        Session["authenticate"] = "authenticated";
        Response.Redirect("student_feedback_view.aspx");
    }
}