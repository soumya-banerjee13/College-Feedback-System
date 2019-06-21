using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class subj_upload : System.Web.UI.Page
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
            //try
            //{
            //    string qry = "select short_name from streams order by short_name";
            //    DataSet ds = dba.fetchData(qry);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        if (DropDownList1.Items.Count > 1)
            //        {
            //            //int[] statD = new int[DropDownList1.Items.Count - 1];
            //            //int[] statTb = new int[ds.Tables[0].Rows.Count];
            //            //for (int i = 0; i < statD.Length; i++)
            //            //{
            //            //    statD[i] = 0;
            //            //}
            //            //for (int i = 0; i < statTb.Length; i++)
            //            //{
            //            //    statTb[i] = 0;
            //            //}
            //            //for (int j = 0; j < statD.Length; j++)
            //            //{
            //            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //            //    {
            //            //        string match = ds.Tables[0].Rows[i].ItemArray[0].ToString().ToUpper();
            //            //        if (DropDownList1.Items[j + 1].Equals(match))
            //            //        {
            //            //            statTb[i] = 1;
            //            //            statD[j] = 1;
            //            //        }
            //            //    }
            //            //}
            //            //for (int i = DropDownList1.Items.Count - 1; i >= 1; i--)
            //            //{
            //            //    if (statD[i-1] == 0) { DropDownList1.Items.RemoveAt(i); }
            //            //}
            //            //for (int i = 0; i < statTb.Length; i++)
            //            //{
            //            //    if (statTb[i] == 0) { DropDownList1.Items.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString().ToUpper()); }
            //            //}
            //        }
            //        else
            //        {
            //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //            {
            //                DropDownList1.Items.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString().ToUpper());
            //            }
            //        }
            //    }
            //    else if (DropDownList1.Items.Count > 1)
            //    {
            //        for (int i = DropDownList1.Items.Count - 1; i >= 1; i--)
            //        {
            //            DropDownList1.Items.RemoveAt(i);
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    msg.ForeColor = Color.Red;
            //    //msg.Text = ee.Message;
            //    msg.Text = "Some error occured while loading...";
            //}
        }
        else
        {
            Response.Write("<script>confirm('Session Expired! Redirecting to Admin Login page...'); window.location='Default.aspx'</script>");
        }
    }
    protected void subj_upl_Click(object sender, EventArgs e)
    {
        if(DropDownList1.SelectedIndex!=0 && DropDownList2.SelectedIndex!=0 && FileUpload1.HasFile)
        {
            if(FileUpload1.FileName.ToString().Contains(".csv"))
            {
                try
                {
                    string trns = "start transaction;";
                    trns =trns+ "delete from subjects where stream='" + DropDownList1.SelectedItem.ToString() + "' and semester='" + DropDownList2.SelectedItem.ToString() + "';";
                    string path = string.Concat((Server.MapPath("~/temp/" + FileUpload1.FileName)));
                    FileUpload1.PostedFile.SaveAs(path);
                    int status = 0;
                    using (System.IO.StreamReader sr = System.IO.File.OpenText(path))
                    {
                        int indx = 0;
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            string[] fld = s.Split(',');
                            if (indx == 0 && fld.Length == 2)
                            {
                                if (!(fld[0].ToLower().Contains("name") && fld[1].ToLower().Contains("code")))
                                {
                                    msg.ForeColor = Color.Red;
                                    msg.Text = "File Format not Supported...";
                                    status = 1;
                                    break;
                                }
                                indx++;
                            }
                            else if (indx == 0 && fld.Length != 2)
                            {
                                msg.ForeColor = Color.Red;
                                msg.Text = "File Format not Supported...";
                                status = 1;
                                break;
                            }
                            else if (indx > 0)
                            {
                                fld[1] = fld[1].Replace("(", "_");
                                fld[1] = fld[1].Replace(")", "");
                                fld[1] = fld[1].Replace(" ", "");
                                trns = trns + "insert into subjects(stream,semester,subj_name,subj_code) values('" + DropDownList1.SelectedItem.ToString() + "','" + DropDownList2.SelectedItem.ToString() + "','" + fld[0] + "','" + fld[1] + "');";
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
                    else
                    {
                        string strSem = DropDownList1.SelectedItem.ToString().ToLower() + "_" + DropDownList2.SelectedItem.ToString();
                        trns = trns + "drop table if exists " + strSem + ";";
                        trns = trns + "commit;";
                        bool dr = dba.saveData(trns);
                        if (dr == true)
                        {
                            msg.ForeColor = Color.Green;
                            msg.Text = "Subject list inserted successfully...";
                            change.Visible = true;
                            go_change.Visible = true;
                        }
                        else
                        {
                            msg.ForeColor = Color.Red;
                            msg.Text = "Some error occured...";
                        }
                    }
                //}
                //else
                //{
                //    msg.ForeColor = Color.Red;
                //    msg.Text = "Some error occured...";
                //}
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
        else if(DropDownList1.SelectedIndex==0 && DropDownList2.SelectedIndex==0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select stream and semester...";
        }
        else if(DropDownList1.SelectedIndex==0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select stream...";
        }
        else if (DropDownList2.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select semester...";
        }
        else if(!(FileUpload1.HasFile))
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select the file to upload...";
        }
    }
}