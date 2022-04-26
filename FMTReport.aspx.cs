using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using UMT;
using System.Web.Services;
using System.Web.Script.Services;
public partial class FMTReport : System.Web.UI.Page
{
    DataTable dbTable = new DataTable();
    Common objCommon = new Common();
    clsInventory objInv = new clsInventory();
    DataTable dbInv = new DataTable();
    ClsFuelDetails objFuel = new ClsFuelDetails();
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlControl li = (HtmlGenericControl)Page.Master.FindControl("lireport");
        li.Attributes.Add("class", "active");
        if (!Page.IsPostBack)
        {
            if (Session["usrDetails"] != null)
            {
                dbTable = (DataTable)Session["usrDetails"];
                if (Convert.ToString(dbTable.Rows[0]["USERTYPE"]) == "0")
                {
                    objCommon.BindDropdowns(ddlst_circle);
                    ddlst_circle.SelectedValue = Convert.ToString(dbTable.Rows[0]["CIRCLEID"]);
                    dbInv = objCommon.GetCircleWiseSites(Convert.ToInt32(ddlst_circle.SelectedValue));
                    Session["AutoCompleteSite"] = dbInv;
                    //ddlst_circle.Enabled = false;
                }
                else
                {
                    objCommon.BindDropdowns(ddlst_circle);
                }
            }
        }       
    }
    protected void ddlst_circle_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dbTable = new DataTable();
        Session["AutoCompleteSite"] = null;
        if (ddlst_circle.SelectedValue != null && ddlst_circle.SelectedValue != "")
        {
            dbTable = objCommon.GetCircleWiseSites(Convert.ToInt32(ddlst_circle.SelectedValue));
            Session["AutoCompleteSite"] = dbTable;
        }
    }
    protected void btn_ShowReport_Click(object sender, EventArgs e)
    {
        DataTable dbTable = new DataTable();
        objFuel.InventoryID = Convert.ToInt32(InventoryID.Value);
        objInv.intInventoryId = Convert.ToInt32(InventoryID.Value);
        dbInv = objInv.GetSpecificSite();
        dbTable = objFuel.GetFuelReport();
        if (dbTable.Rows.Count > 0)
        {
            lbl_SiteID.Text = "Site ID : "+Convert.ToString(dbInv.Rows[0]["SiteID"]);
            lbl_SiteName.Text = "Site Name : "+Convert.ToString(dbInv.Rows[0]["SiteName"]);
            grdview_FMTReport.DataSource = dbTable;
            grdview_FMTReport.DataBind();
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Fuel details are not exist for selected site');", true);
        }
    }
    [WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string[] GetSites(string prefix)
    {
        List<string> sites = new List<string>();
        DataTable dtSites = (DataTable)HttpContext.Current.Session["AutoCompleteSite"];
        DataRow[] dr = dtSites.Select("SiteID LIKE '%" + prefix + "%'");
        if (dr.Length > 0)
        {
            dtSites = dr.CopyToDataTable();
            if (dtSites.Rows.Count > 0)
            {
                for (int i = 0; i < dtSites.Rows.Count; i++)
                {
                    sites.Add(string.Format("{0}-{1}-{2}", dtSites.Rows[i]["SiteID"], dtSites.Rows[i]["InventoryID"], dtSites.Rows[i]["SiteName"]));
                }
            }
            else
            {

            }
        }
        else
        {

        }
        return sites.ToArray();
    }
}