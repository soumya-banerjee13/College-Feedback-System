using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class user_upload : System.Web.UI.Page
{
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
    protected void stu_upload_Click(object sender, EventArgs e)
    {
        if(FileUpload1.HasFile)
        {
            if(FileUpload1.FileName.ToString().Contains(".csv"))
            {
                try
                {
                    string trns = "start transaction;";
                    trns=trns+"delete from student_list;";
                    string path = string.Concat((Server.MapPath("~/temp/" + FileUpload1.FileName)));
                    FileUpload1.PostedFile.SaveAs(path);
                    int stat = 0, status = 0;
                    using (System.IO.StreamReader sr = System.IO.File.OpenText(path))
                    {
                        int indx = 0;
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            string[] fld = s.Split(',');
                            if (indx == 0 && fld.Length == 4)
                            {
                                if (!(fld[0].ToLower().Contains("user") && fld[1].ToLower().Contains("password") && fld[2].ToLower().Contains("stream") && fld[3].ToLower().Contains("semester")))
                                {
                                    msg.ForeColor = Color.Red;
                                    msg.Text = "File Format not Supported...";
                                    status = 1;
                                    break;
                                }
                                indx++;
                            }
                            else if (indx == 0 && fld.Length != 4)
                            {
                                msg.ForeColor = Color.Red;
                                msg.Text = "File Format not Supported...";
                                status = 1;
                                break;
                            }
                            else if (indx > 0 && fld.Length == 4)
                            {
                                string stream = fld[2].ToUpper();
                                int sm = Convert.ToInt32(fld[3]);
                                string sem = "";
                                if (sm == 1)
                                {
                                    sem = sm.ToString() + "st";
                                }
                                else if (sm == 2)
                                {
                                    sem = sm.ToString() + "nd";
                                }
                                else if (sm == 3)
                                {
                                    sem = sm.ToString() + "rd";
                                }
                                else if (sm > 3 && sm <= 8)
                                {
                                    sem = sm.ToString() + "th";
                                }
                                else
                                {
                                    stat = 1;
                                }
                                trns = trns + "insert into student_list(user_id,password,stream,semester,logged_in,last_log) values('" + fld[0] + "','" + fld[1] + "','" + stream + "','" + sem + "','0','0');";
                            }
                        }
                    }
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    if (status == 1)
                    {
                    }
                    else if (stat == 1)
                    {
                        msg.ForeColor = Color.Red;
                        msg.Text = "Do not try to insert some incorrect data...";
                    }
                    else if (stat == 0)
                    {
                        trns=trns+"commit;";
                        bool tr = dba.saveData(trns);
                        if (tr == true)
                        {
                            msg.ForeColor = Color.Green;
                            msg.Text = "Student list inserted successfully...";
                        }
                        else
                        {
                             msg.ForeColor = Color.Red;
                             msg.Text = "Some error occured...";
                         }
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
        else if (!(FileUpload1.HasFile))
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select the file to upload...";
        }
    }
}