using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Data;
using System.Drawing;

public partial class student_feedback_view : System.Web.UI.Page
{
    bool subjectsUploaded = false, optionsUploaded = false;
    DataSet subjDs;
    DataSet optDs;
    DataSet[] commForOptDs;
    TableCell[] td3;
    CheckBoxList[] commentCheckList;
    RadioButtonList[] radioOption;
    TextBox[] commentBox;

    protected override void OnInit(EventArgs e)
    {
        try
        {
            msg.Text = "";
            subjViewTable.Visible = false;
            if (Session["stud_user"] != null && Session["stud_stream"] != null && Session["stud_sem"] != null && Session["stud_flag"] != null && Session["authenticate"] == "authenticated")
            {
                formName.Text = "Feedback Form(" + Session["stud_stream"].ToString().ToUpper() + " " + Session["stud_sem"].ToString().ToLower() + " Semester)";
                string qry = "select subj_name,subj_code from subjects where stream='" + Session["stud_stream"].ToString() + "' and semester='" + Session["stud_sem"].ToString() + "' order by subj_name";
                subjDs = dba.fetchData(qry);
                if (subjDs.Tables[0].Rows.Count > 0) 
                {
                    subjectsUploaded = true;
                    string optQry = "select * from opt order by marks desc";
                    optDs = dba.fetchData(optQry);
                    if (optDs.Tables[0].Rows.Count > 0) 
                    {
                        optionsUploaded = true;
                        //Creating RadioButtonList of options for all subjects
                        radioOption = new RadioButtonList[subjDs.Tables[0].Rows.Count];

                        //Number of DataSets for comments equals to the number of options
                        commForOptDs = new DataSet[optDs.Tables[0].Rows.Count];

                        //Number of TableCells & CheckBoxLists containing comments equals to the number of subjects
                        td3 = new TableCell[subjDs.Tables[0].Rows.Count];
                        commentCheckList = new CheckBoxList[subjDs.Tables[0].Rows.Count];
                        feedbackSubmit.Attributes.Add("onclick", "return validateRadios(" + subjDs.Tables[0].Rows.Count + ")");

                        //Number of TextBoxes for comments equals to the number of subjects
                        commentBox = new TextBox[subjDs.Tables[0].Rows.Count];

                        for (int optCount = 0; optCount < optDs.Tables[0].Rows.Count; optCount++)
                        {
                            string commQry = "select comm,comm_marks from comm where optio='"+optDs.Tables[0].Rows[optCount].ItemArray[0]+"' order by comm_marks desc";
                            commForOptDs[optCount] = dba.fetchData(commQry);
                        }
                        for (int subjCount = 0; subjCount < subjDs.Tables[0].Rows.Count; subjCount++)
                        {
                            radioOption[subjCount] = new RadioButtonList();
                            radioOption[subjCount].ID = "Option" + subjCount.ToString();
                            radioOption[subjCount].RepeatDirection = RepeatDirection.Horizontal;
                            radioOption[subjCount].RepeatLayout = RepeatLayout.Flow;
                            radioOption[subjCount].AutoPostBack = true;
                            radioOption[subjCount].SelectedIndexChanged += new System.EventHandler(this.OptionList_SelectedIndexChanged);

                            for (int optCount = 0; optCount < optDs.Tables[0].Rows.Count; optCount++)
                            {
                                string Option = optDs.Tables[0].Rows[optCount].ItemArray[0].ToString();
                                radioOption[subjCount].Items.Add(new ListItem(Option, optCount.ToString()));
                                //radioOption[subjCount].EnableViewState = true;
                                //radioOption[subjCount].ViewStateMode = ViewStateMode.Enabled;
                            }

                            TableRow tr = new TableRow();
                            tr.Style.Value = "margin-bottom:50px;";

                            string subject = (subjCount + 1).ToString() + ". " + subjDs.Tables[0].Rows[subjCount].ItemArray[0].ToString() + "(" + subjDs.Tables[0].Rows[subjCount].ItemArray[1].ToString()+")";
                            //formName.Text = subject;
                            TableCell td1 = new TableCell();
                            td1.Style.Value = "padding-right:20px;";
                            td1.Text = subject;

                            TableCell td2 = new TableCell();
                            td2.Style.Value = "padding-right:20px;";
                            td2.Controls.Add(radioOption[subjCount]);

                            td3[subjCount] = new TableCell();
                            //div1
                            HtmlGenericControl commAround = new HtmlGenericControl("div");
                            commAround.Attributes.Add("class", "commentAround");

                            //div2
                            HtmlGenericControl commSecHeader = new HtmlGenericControl("div");
                            commSecHeader.Attributes.Add("class", "commentSecHeader");
                            LiteralControl lc1 = new LiteralControl();
                            lc1.Text = "<center>Why</center>";
                            commSecHeader.Controls.Add(lc1);

                            //div3
                            HtmlGenericControl commSec = new HtmlGenericControl("div");
                            commSec.ID = "commSec" + subjCount.ToString();
                            commSec.Attributes.Add("class", "commentSec");

                            HtmlGenericControl commCheck = new HtmlGenericControl("div");
                            commCheck.ID = "commCheck" + subjCount.ToString();
                            commentCheckList[subjCount] = new CheckBoxList();
                            commentCheckList[subjCount].AutoPostBack = true;
                            commentCheckList[subjCount].SelectedIndexChanged += new System.EventHandler(this.CheckList_SelectedIndexChanged);
                            commCheck.Controls.Add(commentCheckList[subjCount]);
                            commSec.Controls.Add(commCheck);

                            LiteralControl lc2 = new LiteralControl();
                            lc2.Text = "<br/>Remark:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            commSec.Controls.Add(lc2);

                            commentBox[subjCount] = new TextBox();
                            commentBox[subjCount].TextMode = TextBoxMode.MultiLine;
                            commSec.Controls.Add(commentBox[subjCount]);

                            td3[subjCount].Controls.Add(commAround);
                            td3[subjCount].Controls.Add(commSecHeader);
                            td3[subjCount].Controls.Add(commSec);
                            td3[subjCount].Visible = false;
                            
                            tr.Cells.Add(td1);
                            tr.Cells.Add(td2);
                            tr.Cells.Add(td3[subjCount]);

                            subjViewTable.Rows.Add(tr);

                        }
                        subjViewTable.Visible = true;
                        formName.Text = "";
                    }
                    else 
                    {
                        access.Visible = false;
                        formName.Text = "Kindly contact your system administrator to add some options for teachers performance evolution";
                    }
                }
                else
                {
                    access.Visible = false;
                    formName.Text = "Kindly contact your system administrator to upload routine for " + Session["stud_stream"].ToString().ToUpper() + " " + Session["stud_sem"].ToString().ToLower() + " semester";
                }
            }
        }
        catch (Exception)
        {
            access.Visible = false;
            formName.Text = "Some error occured...";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        msg.Text = "";
        if (Session["stud_user"] != null && Session["stud_stream"] != null && Session["stud_sem"] != null && Session["stud_flag"] != null && Session["authenticate"] == "authenticated")
        {
            studentId.Text = "Student ID: " + Session["stud_user"].ToString();
            if (subjectsUploaded == true && optionsUploaded == true)
            {
                access.Visible = true;
                if (formName.Text=="")
                {
                    formName.Text = "Feedback Form(" + Session["stud_stream"].ToString().ToUpper() + " " + Session["stud_sem"].ToString().ToLower() + " Semester)";
                }
            }
        }
        else
        {
            access.Visible = false;
            Response.Write("<script>confirm('You are not logged in. Redirecting to Login page...'); window.location='student_login.aspx'</script>");
        }
        //this.Title=
    }

    private void OptionList_SelectedIndexChanged(object sender,EventArgs e)
    {
        try
        {
            msg.Text = "";
            RadioButtonList optionList = (RadioButtonList)sender;
            string optId = optionList.ID;
            int subjIndex = Convert.ToInt32(optId.Substring((optId.Length) - 1, 1));
            int optIndex = Convert.ToInt32(optionList.SelectedValue);

            td3[subjIndex].Controls.Remove(commentCheckList[subjIndex]);
            commentCheckList[subjIndex].Items.Clear();
            for(int i=0;i<commForOptDs[optIndex].Tables[0].Rows.Count;i++)
            {
                string comment = commForOptDs[optIndex].Tables[0].Rows[i].ItemArray[0].ToString();
                commentCheckList[subjIndex].Items.Add(new ListItem(comment,i.ToString()));
            }

            td3[subjIndex].FindControl("commCheck" + subjIndex.ToString()).Controls.Add(commentCheckList[subjIndex]);
            //td3[subjIndex].Controls[2].Add(commentCheckList[subjIndex]);
            td3[subjIndex].Visible = true;

            //msg.ForeColor = Color.Blue;
            //msg.Text = "No. of items in this checkboxlist is: " + commentCheckList[subjIndex].Items.Count.ToString();
        }
        catch (Exception)
        {
            access.Visible = false;
            formName.Text = "Some error occured";

        }
    }

    private void CheckList_SelectedIndexChanged(object sender,EventArgs e)
    {
        //CheckBoxList checkList = (CheckBoxList)sender;
        //string subjId = checkList.ID;
        //int subjIndex = Convert.ToInt32(subjId.Substring((subjId.Length)-1,1));
        //foreach(ListItem li in commentCheckList[subjIndex].Items)
        //{
        //    if(li.Selected==true)
        //    {
        //        li.Selected = true;
        //    }
        //}
    }

    protected void feedbackSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            msg.Text = "";
            if (Session["stud_user"] != null && Session["stud_stream"] != null && Session["stud_sem"] != null && Session["stud_flag"] != null && Session["authenticate"] == "authenticated")
            {
                string strSem = Session["stud_stream"].ToString().ToLower() + "_" + Session["stud_sem"].ToString().ToLower();
                string transaction = "start transaction;";
                transaction = transaction + "update student_list set dirty_bit=0 where user_id='" + Session["stud_user"].ToString() + "';";
                //transaction = transaction + "insert into "+strSem+"(user_id,";
                string attributesList = "";
                string valuesList = "";
                for (int subjCount = 0; subjCount < subjDs.Tables[0].Rows.Count; subjCount++)
                {
                    /*adding attributes to attributesList*/
                    if (subjCount > 0)
                    {
                        attributesList = attributesList + ",";
                        valuesList = valuesList + ",";
                    }
                    attributesList = attributesList + subjDs.Tables[0].Rows[subjCount].ItemArray[1].ToString().ToLower();
                    attributesList = attributesList + "," + subjDs.Tables[0].Rows[subjCount].ItemArray[1].ToString().ToLower() + "_com";

                    /*adding values to valuesList*/
                    int selectedOptionIndex = Convert.ToInt32(radioOption[subjCount].SelectedValue.ToString());
                    //assigning base mark from selected option from radioButtonList
                    int baseMark = Convert.ToInt32(optDs.Tables[0].Rows[selectedOptionIndex].ItemArray[1].ToString());
                    int upperLimit = Convert.ToInt32(optDs.Tables[0].Rows[selectedOptionIndex].ItemArray[2].ToString());
                    int lowerLimit = Convert.ToInt32(optDs.Tables[0].Rows[selectedOptionIndex].ItemArray[3].ToString());
                    //adding extra marks from checkBoxList selected items
                    int extraMarks = 0;
                    int totalMarks = 0;
                    string comments = "";
                    for (int comm = 0; comm < commentCheckList[subjCount].Items.Count; comm++)
                    {
                        if (commentCheckList[subjCount].Items[comm].Selected == true)
                        {
                            extraMarks += Convert.ToInt32(commForOptDs[selectedOptionIndex].Tables[0].Rows[comm].ItemArray[1].ToString());
                            if (comments != "")
                            {
                                comments = comments + ".";
                            }
                            comments = comments + commentCheckList[subjCount].Items[comm].Text.ToString();
                        }
                    }
                    totalMarks = baseMark + extraMarks;
                    if (totalMarks > upperLimit)
                    {
                        totalMarks = upperLimit;
                    }
                    else if (totalMarks < lowerLimit)
                    {
                        totalMarks = lowerLimit;
                    }
                    if (commentBox[subjCount].Text != "")
                    {
                        if (comments != "")
                        {
                            comments = comments + ".";
                        }
                        comments = comments + commentBox[subjCount].Text.ToString();
                    }

                    valuesList = valuesList + totalMarks;
                    valuesList = valuesList + ",'" + comments.ToString() + "'";
                }
                transaction = transaction + "insert into " + strSem + "(user_id," + attributesList + ",date,flag) values('" + Session["stud_user"].ToString() + "'," + valuesList + ",'" + DateTime.Now.ToString("dd/MM/yyyy") + "'," + Session["stud_flag"].ToString() + ");";
                transaction = transaction + "commit;";
                //msg.ForeColor = Color.Green;
                //msg.Text = transaction;
                bool confirm = dba.saveData(transaction);
                if (confirm == true)
                {
                    Response.Write("<script>confirm('Your feedback submitted successfully. Redirecting to dashboard page...'); window.location='student_dashboard.aspx'</script>");
                }
            }
        }
        catch (Exception)
        {
            msg.ForeColor = Color.Red;
            msg.Text = "Some error occured...";
            //msg.Text = ee.Message;
        }
    }

    protected void resetButton_Click(object sender, EventArgs e)
    {
        try
        {
            msg.Text = "";
            ViewState.Clear();
            for (int i = 0; i < radioOption.Length; i++)
            {
                radioOption[i].ClearSelection();
            }
            for (int i = 0; i < td3.Length; i++)
            {
                td3[i].Visible = false;
            }
        }
        catch (Exception)
        {
            access.Visible = false;
            formName.Text = "Some error occured...";
        }
    }

    protected void logout_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Abandon();
            Response.Redirect("student_login.aspx");
        }
        catch (Exception)
        {

        }
    }
}