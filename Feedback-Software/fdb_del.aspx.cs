using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
public partial class fdb_del : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
            msg.ForeColor = Color.Purple;
            msg.Text = "Caution! To keep previous records compress first then delete";
            string qry = "select short_name from streams order by short_name";
            DataSet ds = dba.fetchData(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DropDownList1.Items.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString().ToUpper());
                }
            }
            //Response.Write("<script>confirm('Caution! To keep previous records compress first then delete')</script>");
        }
        catch (Exception)
        {
            msg.ForeColor = Color.Red;
            msg.Text = "Some error occured while initializing the paze...";
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
    protected void comp_del_Click(object sender, EventArgs e)
    {
        msg.Text = "";
        if (DropDownList1.SelectedIndex != 0 && DropDownList2.SelectedIndex != 0 && DropDownList3.SelectedIndex != 0)
        {
            try
            {
                string strComp = "compr_" + DropDownList1.SelectedItem.ToString().ToLower();
                if (DropDownList2.SelectedIndex == 1)
                {
                    int err = 0;
                    for (int s = 1; s <= 8; s++)
                    {
                        string seq = "";
                        if (s == 1)
                        {
                            seq = s.ToString() + "st";
                        }
                        else if (s == 2)
                        {
                            seq = s.ToString() + "nd";
                        }
                        else if (s == 3)
                        {
                            seq = s.ToString() + "rd";
                        }
                        else
                        {
                            seq = s.ToString() + "th";
                        }
                        string strSem = DropDownList1.SelectedItem.ToString().ToLower() + "_" + seq;
                        string sem = seq;
                        string subjQry = "select subj_code from subjects where stream='" + DropDownList1.SelectedItem.ToString().ToUpper() + "' and semester='" + seq + "' order by subj_code";
                        DataSet dsSubj = dba.fetchData(subjQry);
                        try 
                        { 
                        if (dsSubj.Tables[0].Rows.Count > 0)
                        {
                            string trns = "start transaction;";
                            string optQry = "select optio,up_limit,low_limit from opt order by low_limit desc";
                            DataSet dsOpt = dba.fetchData(optQry);
                            if (dsOpt.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsSubj.Tables[0].Rows.Count; i++)
                                {
                                    string subj = dsSubj.Tables[0].Rows[i].ItemArray[0].ToString();
                                    string subj_com = subj + "_com";
                                    string marksQry1 = "";
                                    string marksQry2 = "";
                                    for (int j = 0; j < dsOpt.Tables[0].Rows.Count; j++)
                                    {
                                        string optio = dsOpt.Tables[0].Rows[j].ItemArray[0].ToString();
                                        string opt_comp = optio.Replace(" ", "");
                                        string up_lmt = dsOpt.Tables[0].Rows[j].ItemArray[1].ToString();
                                        string low_lmt = dsOpt.Tables[0].Rows[j].ItemArray[2].ToString();
                                        if (DropDownList3.SelectedIndex == 1)
                                        {
                                            marksQry1 = marksQry1 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=0 and a.feedback=1) as " + opt_comp + ",";
                                            marksQry2 = marksQry2 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=1 and a.feedback=2) as " + opt_comp + ",";
                                        }
                                        else if (DropDownList3.SelectedIndex == 2)
                                        {
                                            marksQry1 = marksQry1 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=0 and a.feedback=1) as " + opt_comp + ",";
                                        }
                                        else if (DropDownList3.SelectedIndex == 3)
                                        {
                                            marksQry2 = marksQry2 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=1 and a.feedback=2) as " + opt_comp + ",";
                                        }
                                    }
                                    if (DropDownList3.SelectedIndex == 1)
                                    {
                                        marksQry1 = marksQry1 + "(select sum(a.days_present*p." + subj + ") from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=0 and a.feedback=1)/(select sum(a.days_present) from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=0 and a.feedback=1) as result,";
                                        marksQry1 = marksQry1 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and flag=0 and a.feedback=1) as total";
                                        marksQry2 = marksQry2 + "(select sum(a.days_present*p." + subj + ") from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=1 and a.feedback=2)/(select sum(a.days_present) from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=1 and a.feedback=2) as result,";
                                        marksQry2 = marksQry2 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and flag=1 and a.feedback=2) as total";
                                    }
                                    else if (DropDownList3.SelectedIndex == 2)
                                    {
                                        marksQry1 = marksQry1 + "(select sum(a.days_present*p." + subj + ") from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=0 and a.feedback=1)/(select sum(a.days_present) from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=0 and a.feedback=1) as result,";
                                        marksQry1 = marksQry1 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and flag=0 and a.feedback=1) as total";
                                    }
                                    else if (DropDownList3.SelectedIndex == 3)
                                    {
                                        marksQry2 = marksQry2 + "(select sum(a.days_present*p." + subj + ") from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=1 and a.feedback=2)/(select sum(a.days_present) from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=1 and a.feedback=2) as result,";
                                        marksQry2 = marksQry2 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and flag=1 and a.feedback=2) as total";
                                    }
                                    int indx1 = 0, indx2 = 0;
                                    if (marksQry1.Length > 0)
                                    {
                                        marksQry1 = "select " + marksQry1;
                                        DataSet dsRslt1 = dba.fetchData(marksQry1);
                                        if (dsRslt1.Tables[0].Rows.Count > 0)
                                        {
                                            string addi = "insert into " + strComp + "(semester,subj_code,opt_count,total,marks,status,year) values('" + sem + "','" + subj + "','";
                                            for (int j = 0; j < dsOpt.Tables[0].Rows.Count; j++)
                                            {
                                                string optio = dsOpt.Tables[0].Rows[j].ItemArray[0].ToString();
                                                addi = addi + optio + "-" + dsRslt1.Tables[0].Rows[0].ItemArray[indx1].ToString();
                                                if (j != dsOpt.Tables[0].Rows.Count - 1) { addi = addi + ","; }
                                                indx1++;
                                            }
                                            addi = addi + "','" + dsRslt1.Tables[0].Rows[0].ItemArray[indx1 + 1].ToString() + "','" + dsRslt1.Tables[0].Rows[0].ItemArray[indx1].ToString() + "','0','" + DateTime.Now.Year.ToString() + "');";
                                            trns = trns + addi;
                                        }
                                    }
                                    if (marksQry2.Length > 0)
                                    {
                                        marksQry2 = "select " + marksQry2;
                                        DataSet dsRslt2 = dba.fetchData(marksQry2);
                                        if (dsRslt2.Tables[0].Rows.Count > 0)
                                        {
                                            string addi = "insert into " + strComp + "(semester,subj_code,opt_count,total,marks,status,year) values('" + sem + "','" + subj + "','";
                                            for (int j = 0; j < dsOpt.Tables[0].Rows.Count; j++)
                                            {
                                                string optio = dsOpt.Tables[0].Rows[j].ItemArray[0].ToString();
                                                addi = addi + optio + "-" + dsRslt2.Tables[0].Rows[0].ItemArray[indx2].ToString();
                                                if (j != dsOpt.Tables[0].Rows.Count - 1) { addi = addi + ","; }
                                                indx2++;
                                            }
                                            addi = addi + "','" + dsRslt2.Tables[0].Rows[0].ItemArray[indx2 + 1].ToString() + "','" + dsRslt2.Tables[0].Rows[0].ItemArray[indx2].ToString() + "','1','" + DateTime.Now.Year.ToString() + "');";
                                            trns = trns + addi;
                                        }
                                    }
                                    //msg.Text = marksQry1 +"  *** "+ marksQry2;
                                }
                                //msg.Text = trns;
                                trns = trns + "commit";
                                dba.saveData(trns);
                            }
                            }
                        }
                        catch (Exception er) 
                        {
                            if (er.Message.Contains("doesn't exist"))
                            {
                                continue;
                            }
                            else if (er.Message.Contains("Duplicate entry"))
                            {
                                err = 1;
                                msg.ForeColor = Color.Purple;
                                msg.Text = "You should delete all compressed records of this stream and semester for the year " + DateTime.Now.Year.ToString();
                            }
                            else
                            {
                                err = 1;
                            }
                        }
                    }
                    if(err==0)
                    {
                        if (DropDownList3.SelectedIndex == 1)
                        {
                            msg.ForeColor = Color.Green;
                            msg.Text = msg.Text = "All records of " + DropDownList1.SelectedItem.ToString() + " " + DropDownList2.SelectedItem.ToString() + " semester compressed successfully...";
                        }
                        else if (DropDownList3.SelectedIndex == 2)
                        {
                            msg.ForeColor = Color.Green;
                            msg.Text = "Records of first feedbacks of " + DropDownList1.SelectedItem.ToString() + " " + DropDownList2.SelectedItem.ToString() + " semester compressed successfully...";
                        }
                        else if (DropDownList3.SelectedIndex == 3)
                        {
                            msg.ForeColor = Color.Green;
                            msg.Text = "Records of second feedbacks of " + DropDownList1.SelectedItem.ToString() + " " + DropDownList2.SelectedItem.ToString() + " semester compressed successfully...";
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                    string sem = DropDownList2.SelectedItem.ToString().ToLower();
                    string strSem = DropDownList1.SelectedItem.ToString().ToLower() + "_" + DropDownList2.SelectedItem.ToString().ToLower();
                    string subjQry = "select subj_code from subjects where stream='" + DropDownList1.SelectedItem.ToString().ToUpper() + "' and semester='" + DropDownList2.SelectedItem.ToString().ToLower() + "' order by subj_code";
                    DataSet dsSubj = dba.fetchData(subjQry);
                    if (dsSubj.Tables[0].Rows.Count > 0)
                    {
                        string trns = "start transaction;";
                        string optQry = "select optio,up_limit,low_limit from opt order by low_limit desc";
                        DataSet dsOpt = dba.fetchData(optQry);
                        if (dsOpt.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsSubj.Tables[0].Rows.Count; i++)
                            {
                                string subj = dsSubj.Tables[0].Rows[i].ItemArray[0].ToString();
                                string subj_com = subj + "_com";
                                string marksQry1 = "";
                                string marksQry2 = "";
                                for (int j = 0; j < dsOpt.Tables[0].Rows.Count; j++)
                                {
                                    string optio = dsOpt.Tables[0].Rows[j].ItemArray[0].ToString();
                                    string opt_comp = optio.Replace(" ", "");
                                    string up_lmt = dsOpt.Tables[0].Rows[j].ItemArray[1].ToString();
                                    string low_lmt = dsOpt.Tables[0].Rows[j].ItemArray[2].ToString();
                                    if (DropDownList3.SelectedIndex == 1)
                                    {
                                        marksQry1 = marksQry1 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=0 and a.feedback=1) as " + opt_comp + ",";
                                        marksQry2 = marksQry2 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=1 and a.feedback=2) as " + opt_comp + ",";
                                    }
                                    else if (DropDownList3.SelectedIndex == 2)
                                    {
                                        marksQry1 = marksQry1 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=0 and a.feedback=1) as " + opt_comp + ",";
                                    }
                                    else if (DropDownList3.SelectedIndex == 3)
                                    {
                                        marksQry2 = marksQry2 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and " + subj + ">=" + low_lmt + " and " + subj + "<=" + up_lmt + " and flag=1 and a.feedback=2) as " + opt_comp + ",";
                                    }
                                }
                                if (DropDownList3.SelectedIndex == 1)
                                {
                                    marksQry1 = marksQry1 + "(select sum(a.days_present*p." + subj + ") from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=0 and a.feedback=1)/(select sum(a.days_present) from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=0 and a.feedback=1) as result,";
                                    marksQry1 = marksQry1 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and flag=0 and a.feedback=1) as total";
                                    marksQry2 = marksQry2 + "(select sum(a.days_present*p." + subj + ") from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=1 and a.feedback=2)/(select sum(a.days_present) from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=1 and a.feedback=2) as result,";
                                    marksQry2 = marksQry2 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and flag=1 and a.feedback=2) as total";
                                }
                                else if (DropDownList3.SelectedIndex == 2)
                                {
                                    marksQry1 = marksQry1 + "(select sum(a.days_present*p." + subj + ") from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=0 and a.feedback=1)/(select sum(a.days_present) from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=0 and a.feedback=1) as result,";
                                    marksQry1 = marksQry1 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and flag=0 and a.feedback=1) as total";
                                }
                                else if (DropDownList3.SelectedIndex == 3)
                                {
                                    marksQry2 = marksQry2 + "(select sum(a.days_present*p." + subj + ") from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=1 and a.feedback=2)/(select sum(a.days_present) from attn a," + strSem + " p where p.user_id=a.user_id and a.subj_code='" + subj + "' and p.flag=1 and a.feedback=2) as result,";
                                    marksQry2 = marksQry2 + "(select count(*) from " + strSem + " p,attn a where p.user_id=a.user_id and a.days_present>0 and a.subj_code='" + subj + "' and flag=1 and a.feedback=2) as total";
                                }
                                int indx1 = 0, indx2 = 0;
                                if (marksQry1.Length > 0)
                                {
                                    marksQry1 = "select " + marksQry1;
                                    DataSet dsRslt1 = dba.fetchData(marksQry1);
                                    if (dsRslt1.Tables[0].Rows.Count > 0)
                                    {
                                        string addi = "insert into " + strComp + "(semester,subj_code,opt_count,total,marks,status,year) values('" + sem + "','" + subj + "','";
                                        for (int j = 0; j < dsOpt.Tables[0].Rows.Count; j++)
                                        {
                                            string optio = dsOpt.Tables[0].Rows[j].ItemArray[0].ToString();
                                            addi = addi + optio + "-" + dsRslt1.Tables[0].Rows[0].ItemArray[indx1].ToString();
                                            if (j != dsOpt.Tables[0].Rows.Count - 1) { addi = addi + ","; }
                                            indx1++;
                                        }
                                        addi = addi + "','" + dsRslt1.Tables[0].Rows[0].ItemArray[indx1 + 1].ToString() + "','" + dsRslt1.Tables[0].Rows[0].ItemArray[indx1].ToString() + "','0','" + DateTime.Now.Year.ToString() + "');";
                                        trns = trns + addi;
                                    }
                                }
                                if (marksQry2.Length > 0)
                                {
                                    marksQry2 = "select " + marksQry2;
                                    DataSet dsRslt2 = dba.fetchData(marksQry2);
                                    if (dsRslt2.Tables[0].Rows.Count > 0)
                                    {
                                        string addi = "insert into " + strComp + "(semester,subj_code,opt_count,total,marks,status,year) values('" + sem + "','" + subj + "','";
                                        for (int j = 0; j < dsOpt.Tables[0].Rows.Count; j++)
                                        {
                                            string optio = dsOpt.Tables[0].Rows[j].ItemArray[0].ToString();
                                            addi = addi + optio + "-" + dsRslt2.Tables[0].Rows[0].ItemArray[indx2].ToString();
                                            if (j != dsOpt.Tables[0].Rows.Count - 1) { addi = addi + ","; }
                                            indx2++;
                                        }
                                        addi = addi + "','" + dsRslt2.Tables[0].Rows[0].ItemArray[indx2 + 1].ToString() + "','" + dsRslt2.Tables[0].Rows[0].ItemArray[indx2].ToString() + "','1','" + DateTime.Now.Year.ToString() + "');";
                                        trns = trns + addi;
                                    }
                                }
                                //msg.Text = marksQry1 +"  *** "+ marksQry2;
                            }
                            //msg.Text = trns;
                            trns = trns + "commit";
                            bool conf = dba.saveData(trns);
                            if (conf == true)
                            {
                                if (DropDownList3.SelectedIndex == 1)
                                {
                                    msg.ForeColor = Color.Green;
                                    msg.Text = msg.Text = "All records of " + DropDownList1.SelectedItem.ToString() + " " + DropDownList2.SelectedItem.ToString() + " semester compressed successfully...";
                                }
                                else if (DropDownList3.SelectedIndex == 2)
                                {
                                    msg.ForeColor = Color.Green;
                                    msg.Text = "Records of first feedbacks of " + DropDownList1.SelectedItem.ToString() + " " + DropDownList2.SelectedItem.ToString() + " semester compressed successfully...";
                                }
                                else if (DropDownList3.SelectedIndex == 3)
                                {
                                    msg.ForeColor = Color.Green;
                                    msg.Text = "Records of second feedbacks of " + DropDownList1.SelectedItem.ToString() + " " + DropDownList2.SelectedItem.ToString() + " semester compressed successfully...";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                msg.ForeColor = Color.Red;
                //msg.Text = ee.Message;
                msg.Text = "Some error occured...";
                if (ee.Message.Contains("doesn't exist"))
                {
                    msg.ForeColor = Color.Blue;
                    msg.Text = "No record to compress";
                }
                else if (ee.Message.Contains("Duplicate entry"))
                {
                    msg.ForeColor = Color.Purple;
                    msg.Text = "You should delete all compressed records of this stream and semester for the year " + DateTime.Now.Year.ToString();
                }
            }
        }
        else if (DropDownList1.SelectedIndex == 0 || DropDownList2.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Select stream and semester...";
        }
        else if (DropDownList3.SelectedIndex == 0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select the feedbacks you want to delete...";
        }
    }
    protected void just_del_Click(object sender, EventArgs e)
    {
        msg.Text = "";
        if(DropDownList1.SelectedIndex!=0 && DropDownList2.SelectedIndex!=0 && DropDownList3.SelectedIndex!=0)
        {
            try
            {
                if (DropDownList2.SelectedIndex == 1)
                {
                    int err = 0;
                    for(int i=1;i<=8;i++)
                    {
                        string seq = "";
                        if(i==1)
                        {
                            seq = i.ToString() + "st";
                        }
                        else if (i == 2)
                        {
                            seq = i.ToString() + "nd";
                        }
                        else if (i == 3)
                        {
                            seq = i.ToString() + "rd";
                        }
                        else
                        {
                            seq = i.ToString() + "th";
                        }
                        string strSem = DropDownList1.SelectedItem.ToString().ToLower() + "_" + seq;
                        if (DropDownList3.SelectedIndex == 1)
                        {
                            string delQry = "delete from " + strSem;
                            try
                            {
                                dba.saveData(delQry);
                            }
                            catch (Exception er)
                            {
                                if (er.Message.Contains("doesn't exist"))
                                {
                                    continue;
                                }
                                else
                                {
                                    err = 1;
                                }
                            }
                        }
                        else if (DropDownList3.SelectedIndex == 2) 
                        {
                            string delQry = "delete from " + strSem + " where flag=0";
                            try
                            {
                                dba.saveData(delQry);
                            }
                            catch (Exception er)
                            {
                                if (er.Message.Contains("doesn't exist"))
                                {
                                    continue;
                                }
                                else
                                {
                                    err = 1;
                                }
                            }
                        }
                        else if (DropDownList3.SelectedIndex == 3) 
                        {
                            string delQry = "delete from " + strSem + " where flag=1";
                            try
                            {
                                dba.saveData(delQry);
                            }
                            catch (Exception er)
                            {
                                if (er.Message.Contains("doesn't exist"))
                                {
                                    continue;
                                }
                                else
                                {
                                    err = 1;
                                }
                            }
                        }
                    }
                    if (err == 0)
                    {
                        if (DropDownList3.SelectedIndex == 1)
                        {
                            msg.ForeColor = Color.Green;
                            msg.Text = msg.Text = "All records of " + DropDownList1.SelectedItem.ToString() + " " + DropDownList2.SelectedItem.ToString() + " semester deleted successfully...";
                        }
                        else if(DropDownList3.SelectedIndex==2)
                        {
                            msg.ForeColor = Color.Green;
                            msg.Text = "Records of first feedbacks of " + DropDownList1.SelectedItem.ToString() + " " + DropDownList2.SelectedItem.ToString() + " semester deleted successfully...";
                        }
                        else if (DropDownList3.SelectedIndex == 3)
                        {
                            msg.ForeColor = Color.Green;
                            msg.Text = "Records of second feedbacks of " + DropDownList1.SelectedItem.ToString() + " " + DropDownList2.SelectedItem.ToString() + " semester deleted successfully...";
                        }
                    }
                    else
                    {
                        msg.ForeColor = Color.Red;
                        msg.Text = "Some error occured...";
                    }
                }
                else
                {
                    string strSem = DropDownList1.SelectedItem.ToString().ToLower() + "_" + DropDownList2.SelectedItem.ToString().ToLower();
                    if (DropDownList3.SelectedIndex == 1)
                    {
                        string delQry = "delete from " + strSem;
                        bool b = dba.saveData(delQry);
                        if (b == true)
                        {
                            msg.ForeColor = Color.Green;
                            msg.Text = msg.Text = "All records of " + DropDownList1.SelectedItem.ToString() + " " + DropDownList2.SelectedItem.ToString() + " semester deleted successfully...";
                        }
                        else
                        {
                            msg.ForeColor = Color.Red;
                            msg.Text = "Some error occured.Records not deleted...";
                        }
                    }
                    else if (DropDownList3.SelectedIndex == 2)
                    {
                        string delQry = "delete from " + strSem + " where flag=0";
                        bool b = dba.saveData(delQry);
                        if (b == true)
                        {
                            msg.ForeColor = Color.Green;
                            msg.Text = "Records of first feedbacks of " + DropDownList1.SelectedItem.ToString() + " " + DropDownList2.SelectedItem.ToString() + " semester deleted successfully...";
                        }
                        else
                        {
                            msg.ForeColor = Color.Red;
                            msg.Text = "Some error occured.Records not deleted...";
                        }
                    }
                    else if (DropDownList3.SelectedIndex == 3)
                    {
                        string delQry = "delete from " + strSem + " where flag=1";
                        bool b = dba.saveData(delQry);
                        if (b == true)
                        {
                            msg.ForeColor = Color.Green;
                            msg.Text = msg.Text = "Records of second feedbacks of " + DropDownList1.SelectedItem.ToString() + " " + DropDownList2.SelectedItem.ToString() + " semester deleted successfully...";
                        }
                        else
                        {
                            msg.ForeColor = Color.Red;
                            msg.Text = "Some error occured.Records not deleted...";
                        }
                    }
                }
            }
            catch(Exception ee)
            {
                msg.ForeColor = Color.Red;
                msg.Text = "Some error occured...";
                if(ee.Message.Contains("doesn't exist"))
                {
                    msg.ForeColor = Color.Green;
                    msg.Text = "Records deleted successfully...";
                }
            }
        }
        else if(DropDownList1.SelectedIndex==0 || DropDownList2.SelectedIndex==0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Select stream and semester...";
        }
        else if(DropDownList3.SelectedIndex==0)
        {
            msg.ForeColor = Color.Blue;
            msg.Text = "Please select the feedbacks you want to delete...";
        }
    }
}