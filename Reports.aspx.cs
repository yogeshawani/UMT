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

public partial class Reports : System.Web.UI.Page
{
    ClsAssetregister objAR = new ClsAssetregister();
    clsInventory objInventory = new clsInventory();
    Common objCommon = new Common();
    DataTable dbTable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlControl li = (HtmlGenericControl)Page.Master.FindControl("lireport");
        li.Attributes.Add("class", "active");
        if (!Page.IsPostBack)
        {
            objCommon.BindDropdowns(ddlst_circle);
            //lbl_ErrorMessage.Visible = false;
        }
    }
    protected void btn_ExportExcel_Click(object sender,EventArgs e)
    {
        if(ViewState["Assets"] != null)
          {
            Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "AssetReport.xls"));
        Response.ContentType = "application/ms-excel";
        DataTable dt = (DataTable)ViewState["Assets"];
        string str = string.Empty;
        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
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
    protected void btn_SelectSite_Click(object sender, EventArgs e)
    {
        objAR.AssetType = Convert.ToString(ddlst_AssetType.SelectedValue);
        objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
        dbTable = objAR.GetFullAssetDetails();
        ViewState["Assets"] = dbTable;
        if (dbTable.Rows.Count > 0)
        {
            btn_ExportExcel.Visible = true;
            grdview_Report.DataSource = dbTable;
            grdview_Report.DataBind();
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('There is no "+Convert.ToString(ddlst_AssetType.SelectedItem)+" register for this site');", true);
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