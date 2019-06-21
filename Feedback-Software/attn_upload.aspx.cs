using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class attn_upload : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        GridView1.Visible = false;
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
        if (Session["user_admin"] != null)
        {
            access.Visible = true;
        }
        else
        {
            Response.Write("<script>confirm('Session Expired! Redirecting to Admin Login page...'); window.location='Default.aspx'</script>");
        }
    }
    protected void attn_upl_Click(object sender, EventArgs e)
    {
        show.Text = "";
        GridView1.Visible = false;
        if (DropDownList1.SelectedIndex != 0 && DropDownList2.SelectedIndex != 0 && DropDownList3.SelectedIndex != 0 && FileUpload1.HasFile)
        {
            if (FileUpload1.FileName.ToString().Contains(".csv"))
            {
                try
                {
                    string fQry = "select subj_code from subjects where stream='" + DropDownList1.SelectedItem.ToString().ToUpper() + "' and semester='" + DropDownList2.SelectedItem.ToString() + "' order by subj_code";
                    DataSet fieldSubject = dba.fetchData(fQry);
                    if (fieldSubject.Tables[0].Rows.Count > 0)
                    {
                        bool allInsert = true;
                        string trns = "start transaction;";
                        if (DropDownList3.Text == "1st")
                        {
                            trns = trns + "delete from attn where user_id in(select user_id from student_list where stream='" + DropDownList1.SelectedItem.ToString() + "' and semester='" + DropDownList2.SelectedItem.ToString() + "') and feedback=1;";
                        }
                        else if (DropDownList3.Text == "2nd")
                        {
                            trns = trns + "delete from attn where user_id in(select user_id from student_list where stream='" + DropDownList1.SelectedItem.ToString() + "' and semester='" + DropDownList2.SelectedItem.ToString() + "') and feedback=2;";
                        }
                        else
                        {
                            trns = trns + "delete from attn where user_id in(select user_id from student_list where stream='" + DropDownList1.SelectedItem.ToString() + "' and semester='" + DropDownList2.SelectedItem.ToString() + "');";
                        }
                        string path = string.Concat((Server.MapPath("~/temp/" + FileUpload1.FileName)));
                        FileUpload1.PostedFile.SaveAs(path);
                        int status = 0;
                        using (System.IO.StreamReader sr = System.IO.File.OpenText(path))
                        {
                            int indx = 0;
                            string s = "";
                            while ((s = sr.ReadLine()) != null)
                            {
                                string[] field = s.Split(',');
                                if (indx == 0 && field.Length == (fieldSubject.Tables[0].Rows.Count+1))
                                {
                                    if(field[0].ToLower().Contains("roll"))
                                    {
                                        int stat = 0;
                                        for(int i=0;i<fieldSubject.Tables[0].Rows.Count;i++)
                                        {
                                            if(!(field[i+1].ToLower().Contains(fieldSubject.Tables[0].Rows[i].ItemArray[0].ToString().ToLower())))
                                            {
                                                stat = 1;
                                            }
                                        }
                                        if(stat==1)
                                        {
                                            msg.ForeColor = Color.Red;
                                            msg.Text = "Subject Code fields are not properly given...";
                                            status = 1;
                                            break;
                                        }
                                    }
                                    else if (!(field[0].ToLower().Contains("roll"))) 
                                    {
                                        msg.ForeColor = Color.Red;
                                        msg.Text = "File Format not Supported...";
                                        status = 1;
                                        break;
                                    }
                                    indx++;
                                }
                                else if (indx == 0 && field.Length != (fieldSubject.Tables[0].Rows.Count + 1))
                                {
                                    msg.ForeColor = Color.Red;
                                    msg.Text = "File Format not Supported...";
                                    status = 1;
                                    break;
                                }
                                else if (indx > 0 && field.Length == (fieldSubject.Tables[0].Rows.Count + 1))
                                {
                                    string queryString = "select user_id from student_list where roll_no='" + field[0] + "'";
                                    DataSet queryDs = dba.fetchData(queryString);
                                    if (queryDs.Tables[0].Rows.Count == 1)
                                    {
                                        if (DropDownList3.Text == "1st")
                                        {
                                            for (int cnt = 0; cnt < fieldSubject.Tables[0].Rows.Count; cnt++)
                                            {
                                                trns = trns + "insert into attn(user_id,subj_code,days_present,feedback) values('" + queryDs.Tables[0].Rows[0].ItemArray[0].ToString() + "','" + fieldSubject.Tables[0].Rows[cnt].ItemArray[0] + "','" + field[cnt + 1] + "',1);";
                                            }
                                        }
                                        else if (DropDownList3.Text == "2nd")
                                        {
                                            for (int cnt = 0; cnt < fieldSubject.Tables[0].Rows.Count; cnt++)
                                            {
                                                trns = trns + "insert into attn(user_id,subj_code,days_present,feedback) values('" + queryDs.Tables[0].Rows[0].ItemArray[0].ToString() + "','" + fieldSubject.Tables[0].Rows[cnt].ItemArray[0] + "','" + field[cnt + 1] + "',2);";
                                            }
                                        }
                                        else
                                        {
                                            for (int cnt = 0; cnt < fieldSubject.Tables[0].Rows.Count; cnt++)
                                            {
                                                trns = trns + "insert into attn(user_id,subj_code,days_present,feedback) values('" + queryDs.Tables[0].Rows[0].ItemArray[0].ToString() + "','" + fieldSubject.Tables[0].Rows[cnt].ItemArray[0] + "','" + field[cnt + 1] + "',1);";
                                                trns = trns + "insert into attn(user_id,subj_code,days_present,feedback) values('" + queryDs.Tables[0].Rows[0].ItemArray[0].ToString() + "','" + fieldSubject.Tables[0].Rows[cnt].ItemArray[0] + "','" + field[cnt + 1] + "',2);";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        allInsert = false;
                                    }
                                }
                            }
                        }
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        if (status == 0)
                        {
                            trns = trns + "commit;";
                            bool dr = dba.saveData(trns);
                            if (dr == true)
                            {
                                msg.ForeColor = Color.Green;
                                msg.Text = "Attendance list inserted successfully...";
                                if (allInsert == false)
                                {
                                    msg.Text = "Some records are not inserted as users with these roll numbers have not given any feedback yet. " + msg.Text;
                                }
                            }
                            else
                            {
                                msg.ForeColor = Color.Red;
                                msg.Text = "Some error occured...";
                            }
                        }
                    }
                    else
                    {
                        msg.ForeColor = Color.Blue;
                        msg.Text = "No subject uploaded for " + DropDownList1.SelectedItem.ToString().ToUpper() + " " + DropDownList2.SelectedItem.ToString() + " semester";
                    }

                }
                catch (Exception ee)
                {
                    Array.ForEach(System.IO.Directory.GetFiles((Server.MapPath("~/temp/"))), System.IO.File.Delete);
                    msg.ForeColor = Color.Red;
                    msg.Text = ee.Message;
                    //msg.Text = "File not uploaded. Some error occured...";
                }
            }
            else
            {
                msg.ForeColor = Color.Blue;
                msg.Text = "File extension should be .csv";
            }
        }
        else if (DropDownList1.SelectedIndex == 0 && DropDownList2.SelectedIndex == 0 && DropDownList3.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select stream,semester and feedback...";
        }
        else if (DropDownList1.SelectedIndex == 0 && DropDownList2.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select stream and semester...";
        }
        else if (DropDownList2.SelectedIndex == 0 && DropDownList3.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select semester and feedback...";
        }
        else if (DropDownList1.SelectedIndex == 0 && DropDownList3.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select stream and feedback...";
        }
        else if (DropDownList1.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select stream...";
        }
        else if (DropDownList2.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select semester...";
        }
        else if (DropDownList3.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select feedback...";
        }
        else if (!(FileUpload1.HasFile))
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select the file to upload...";
        }
    }
    protected void reqOrder_Click(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        show.Text = "";
        msg.Text = "";
        if (DropDownList1.SelectedIndex != 0 && DropDownList2.SelectedIndex != 0)
        {
            try
            {
                string qry = "select subj_code from subjects where stream='" + DropDownList1.SelectedItem.ToString() + "' and semester='" + DropDownList2.SelectedItem.ToString() + "' order by subj_code";
                DataSet ds = dba.fetchData(qry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    show.Visible = true;
                    show.Text = "Fields needed after user_name field to upload attendance list of " + DropDownList1.SelectedItem.ToString() + " " + DropDownList2.SelectedItem.ToString() + " semester";
                }
                else
                {
                    msg.Visible = true;
                    msg.ForeColor = Color.Blue;
                    msg.Text = "No subjects uploaded...";
                }
            }
            catch (Exception)
            {
                msg.ForeColor = Color.Red;
                msg.Text = "Some error occured...";
            }
        }
        else if (DropDownList1.SelectedIndex == 0 && DropDownList2.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select stream and semester...";
        }
        else if (DropDownList1.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select stream...";
        }
        else if (DropDownList2.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select semester...";
        }
    }
}