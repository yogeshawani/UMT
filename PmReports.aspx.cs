using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using UMT;
using System.Data;
using System.Xml;

public partial class pm : System.Web.UI.Page
{
    Common objCommon = new Common();
    clsInventory objInventory = new clsInventory();
    PMClass objPM = new PMClass();
    double lat2, long2;
    string HTML = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlControl li = (HtmlGenericControl)Page.Master.FindControl("lireport");
        li.Attributes.Add("class", "active");        
        if (!Page.IsPostBack)
        {
            objCommon.BindDropdowns(ddlst_circle);
            objPM.Ddlst_PMMaster(ddlst_PMMaster);
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
    protected void ddlst_PMMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
       // BindGrid(Convert.ToInt32(ddlst_PMMaster.SelectedValue));
    }    
    
    protected void btn_ShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            objPM.InventoryID = Convert.ToInt32(InventoryID.Value);
            objPM.PM_ID = Convert.ToInt32(ddlst_PMMaster.SelectedValue);
            dt = objPM.GetPMTranList();
            if (dt.Rows.Count > 0)
            {
                lbl_PMName.Text = "PM For :" + Convert.ToString(ddlst_PMMaster.SelectedItem);
                lbl_SiteID.Text = "Site ID :" + Convert.ToString(dt.Rows[0]["SiteID"]);
                lbl_SiteName.Text = "Site Name :" + Convert.ToString(dt.Rows[0]["SiteName"]);
                grdview_PMReport.DataSource = dt;
                grdview_PMReport.DataBind();
                GetPMImages(Convert.ToInt32(ddlst_PMMaster.SelectedValue),Convert.ToInt32(InventoryID.Value));
            }
            else
            {
                grdview_PMReport.DataSource = null;
                grdview_PMReport.DataBind();
                lbl_SiteName.Visible = false;
                lbl_PMName.Visible = false;
                lbl_SiteID.Visible = false;
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Preventive maintenance not done ');", true);
            }
        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "btn_ShowReport_Click", "PM Report Page issue"); 
        }
    }
    private void BindGrid()
    {
        DataTable dt = new DataTable();
        objPM.InventoryID = Convert.ToInt32(InventoryID.Value);
        objPM.PM_ID = Convert.ToInt32(ddlst_PMMaster.SelectedValue);
        dt = objPM.GetPMTranList();
        grdview_PMReport.DataSource = dt;
        grdview_PMReport.DataBind();
    }
    private void GetPMImages(int PMID,int InventoryID)
    {
        DataTable dt = new DataTable();
        objPM.InventoryID = InventoryID;
        objPM.PM_ID = PMID;
        dt = objPM.GetPMImages();
        if (dt.Rows.Count > 0)
        {
            HTML += "<div class='row'>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HTML += "<div class='col-sm-3'>";
                if (Convert.ToString(dt.Rows[i]["PMimg"]) != string.Empty)
                {
                    byte[] bytes;
                    string b = string.Empty;
                    bytes = (byte[])dt.Rows[i]["PMimg"];
                    b = Convert.ToBase64String(bytes, 0, bytes.Length);
                    HTML += "<img class='img img-responsive' src='data:image/jpg;base64," + b + "' width='256' height='256' alt=''/>";
                }
                else
                {
                    HTML += "<div class='alert alert-warning'>There are no images uploaded for Preventive Maintenance</div>";
                }
                HTML += "</div>";
            }
            HTML += "</div>";
        }
        else
        {
            HTML += "<div class='alert alert-warning'>There are no images uploaded for Preventive Maintenance</div>";
        }
        pmImages.InnerHtml = HTML;
    }
}