using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        mastercon.Visible = false;
        Head1.Visible = false;
        if(Session["user_admin"]!=null)
        {
            Head1.Visible = true;
            mastercon.Visible = true;
        }
    }
}
