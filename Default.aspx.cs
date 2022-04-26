using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UMT;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    Common objCommon = new Common();
    DataTable dbTable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btn_Login_Click(object sender, EventArgs e)
    {
        try
        {
            objCommon.usrname = Convert.ToString(txt_Username.Text);
            objCommon.pwd = Convert.ToString(txt_Password.Text);
            dbTable = objCommon.ValidateUser();
            if (dbTable.Rows.Count > 0)
            {
                if (Convert.ToString(dbTable.Rows[0][0]) == "1")
                {
                    Session["usrDetails"] = dbTable;
                    Session["UsrID"] = Convert.ToString(dbTable.Rows[0]["USRID"]);
                    //Server.Execute("~/Inventory.aspx");
                    Response.Redirect("~/Inventory.aspx",false);
                  //if (CompareDates(Convert.ToDateTime(dbTable.Rows[0]["USRDEMOENDDATE"]),DateTime.Now) != 1)
                    //{
                      //  Session["usrDetails"] = dbTable;
                        //Session["UsrID"] = Convert.ToString(dbTable.Rows[0]["USRID"]);
                        //Response.Redirect("~/AssetRegister.aspx");
                    //}
                    //else
                    //{}
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('User validation failed please check user credentials');", true);
                }
            }

        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "btn_Login_Click", "User Validation");
            HttpContext.Current.Response.End();
            //throw new ExceptionTranslater(ex);
        }
    }
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        Session.Clear();
        txt_Password.Text = "";
        txt_Username.Text = "";
    }
  //private int CompareDates(DateTime date1,DateTime date2)
    //{
    //    DateTime dt1 = DateTime.ParseExact(date1, "dd-MM-yyyy", null);
    //    DateTime dt2 = DateTime.ParseExact(date2, "dd-MM-yyyy", null);

    //    int cmp = dt1.CompareTo(dt2);

    //    if (cmp > 0)
    //    {
    //        // date1 is greater means date1 is comes after date2
    //        return 1; // 1 Means Demo Expired
    //    }
    //    else if (cmp < 0)
    //    {
    //        // date2 is greater means date1 is comes after date1
    //        return 2; 
    //    }
    //    else {
    //        // date1 is same as date2
    //        return 0;
    //    }
    //}
}