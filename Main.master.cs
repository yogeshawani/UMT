using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UMT;
using System.Data;

public partial class Main : System.Web.UI.MasterPage
{
    DataTable dbTable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["usrDetails"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            dbTable = (DataTable)Session["usrDetails"];
            username.InnerText = Convert.ToString(dbTable.Rows[0]["FIRSTNAME"]);
            if (Convert.ToString(dbTable.Rows[0]["USERTYPE"]) == "0")
            {
                Session["ISDemoUsr"] = Convert.ToInt32(dbTable.Rows[0]["ISDEMOUSR"]);
                Session["DemoStatus"] = Convert.ToInt32(dbTable.Rows[0]["USRDEMOSTATUS"]);
                Session["DemoEndDate"] = Convert.ToString(dbTable.Rows[0]["USRDEMOENDDATE"]);
                if (Convert.ToInt32(Session["ISDemoUsr"]) == 1 && Convert.ToInt32(Session["DemoStatus"]) == 1 && String.Format("{0:M/d/yyyy}", Convert.ToDateTime(Session["DemoEndDate"])) == String.Format("{0:M/d/yyyy}", Convert.ToDateTime(DateTime.Now)))
                {
                    Session.Abandon();
                    Session.Clear();
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Demo not available for this user');", true);
                }
                //liadmin.Visible = true;
            }
        }

    }
}
