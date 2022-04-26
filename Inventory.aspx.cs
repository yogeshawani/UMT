using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using UMT;
using System.Net.Mail;
using System.Data;


public partial class Inventory : System.Web.UI.Page
{
    private string ActionResult = "";
    clsInventory objclsInventory = new clsInventory();
    Common objCommon = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlControl li = (HtmlGenericControl)Page.Master.FindControl("liassetregister");
        li.Attributes.Add("class", "active");
        if (!Page.IsPostBack)
        {
            BindGrid();
            objCommon.BindDropdowns(ddlst_circle);
            if(Request.QueryString["id"] != null)
            { 
                string id = Convert.ToString(Request.QueryString["id"]);
                EditInventory(Convert.ToInt32(Common.Decrypt(id)));
            }
        }
    }
    protected void btn_SaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            objclsInventory.strSiteID = Convert.ToString(txt_SiteId.Text);
            objclsInventory.strSiteName = Convert.ToString(txt_SiteName.Text);
            objclsInventory.strFacilityID = Convert.ToString(txt_FacID.Text);
            objclsInventory.strMEPT = Convert.ToString(txt_MEPT.Text);
            objclsInventory.strInventoryDate = Convert.ToDateTime(txt_InventoryDate.Text);
            objclsInventory.intDgornongd = Convert.ToInt32(ddlst_dgyn.SelectedIndex);
            objclsInventory.intSEBnonSEB = Convert.ToInt32(ddlst_sebyn.SelectedIndex);
            objclsInventory.strInventoryStatus = Convert.ToInt32(ddlst_inventorystatus.SelectedIndex);
            objclsInventory.intCircle = Convert.ToInt32(ddlst_circle.SelectedValue);
            objclsInventory.strArticleType = Convert.ToString(ddlst_articleType.SelectedItem);
            objclsInventory.intArticleStatus = Convert.ToInt32(ddlst_articlestatus.SelectedIndex);
            objclsInventory.strLattitude = Convert.ToString(txt_Latitude.Text);
            objclsInventory.strLongitude = Convert.ToString(txt_Longitude.Text);
            ActionResult = objclsInventory.InsertInventory();
            //if (ActionResult == "1")
            //{

            //    string body = "<html><body>";
            //    body += "<table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='2'><b>New Inventory Added</b></td> </tr>";
            //    body += "<tr> <td>Site ID : " + Convert.ToString(txt_SiteId.Text) + "</td> <td>Site Name:" + Convert.ToString(txt_SiteName.Text) + "</td></tr>";
            //    body += "<tr> <td>Lat : " + Convert.ToString(txt_Latitude.Text) + "</td> <td>Long:" + Convert.ToString(txt_Longitude.Text) + "</td></tr>";
            //    body += "</body></html>";
            //    objCommon.SendHtmlFormattedEmail(objCommon.GetMailerList(), "New Asset Added to Site ID:- " + Convert.ToString(txt_SiteId.Text) + "/ Site Name:- " + Convert.ToString(txt_SiteName.Text), body);
            //    Common.AddProcessLog("New Site Added Site ID :- " + Convert.ToString(txt_SiteId.Text) + "/ Site Name:- " + Convert.ToString(txt_SiteName.Text) + "", Convert.ToInt32(Session["UsrID"]));
            //    ResetDetails();
            //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Inventory Details Saved Succesfully !!!');", true);
            //    BindGrid();
            //}
            //else if (ActionResult == "Exist")
            if (ActionResult == "Exist")
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Inventory Details Already Exist !!!');", true);
                BindGrid();
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('There is some error " + ActionResult + "!!!');", true);
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "btn_SaveDetails_Click", "Inventory Page Insert");
        }
    }
    protected void btn_resetDetails_Click(object sender, EventArgs e)
    {
        ResetDetails();
    }
    private void ResetDetails()
    {
        txt_FacID.Text = "";
        txt_InventoryDate.Text = "";
        txt_MEPT.Text = "";
        txt_SiteId.Text = "";
        txt_SiteName.Text = "";
        ddlst_articlestatus.SelectedIndex = 0;
        ddlst_articleType.SelectedIndex = 0;
        ddlst_circle.SelectedIndex = 0;
        ddlst_dgyn.SelectedIndex = 0;
        ddlst_inventorystatus.SelectedIndex = 0;
        ddlst_sebyn.SelectedIndex = 0;
        txt_Longitude.Text = "";
        txt_Latitude.Text = "";
        lbl_CreateDate.Text = "";
        lbl_CreatedBy.Text = "";
        editDetails.Visible = false;

    }
    private void BindGrid()
    {
        grdview_InventoryDetails.DataSource = objclsInventory.GetInventoryList();
        grdview_InventoryDetails.DataBind();
    }

    protected void grdview_InventoryDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdview_InventoryDetails.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void grdview_InventoryDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName == "edit")
        {
            string item = Convert.ToString(e.CommandArgument);
            Response.Redirect("~/Inventory.aspx?id=" + Common.Encrypt(item));
        }
        else if (e.CommandName == "delete")
        {
            string item = Convert.ToString(e.CommandArgument);
            objclsInventory.intInventoryId = Convert.ToInt32(item);
            objclsInventory.deleteInventory();
            BindGrid();
        }
    }
    protected void grdview_InventoryDetails_DataBound(object sender, EventArgs e)
    {
        //grdview_InventoryDetails.FooterRow.Cells[0].Text = String.Format("Total: {0}",grdview_InventoryDetails.Rows.Count.ToString());
        //grdview_InventoryDetails.FooterRow.Cells[0].ColumnSpan = grdview_InventoryDetails.FooterRow.Cells.Count;
        //for (int i = 1; i < grdview_InventoryDetails.FooterRow.Cells.Count; i++)
        //{
        //    grdview_InventoryDetails.FooterRow.Cells[i].Visible = false;
        //}

    }
    private void EditInventory(int inventoryId)
    {
        DataTable dbTable = new DataTable();
        objclsInventory.intInventoryId = inventoryId;
        dbTable = objclsInventory.GetSpecificSite();
        if (dbTable.Rows.Count > 0)
        {
            editDetails.Visible = true;
            btn_SaveDetails.Text = "Update Details";
            btn_resetDetails.Text = "Clear Edit";
            lbl_CreateDate.Text = string.Format(Convert.ToString(dbTable.Rows[0]["createdate"]), "dd-mm-yyyy");
            txt_SiteId.Text = Convert.ToString(dbTable.Rows[0]["siteid"]);
            txt_SiteName.Text = Convert.ToString(dbTable.Rows[0]["sitename"]);
            txt_FacID.Text = Convert.ToString(dbTable.Rows[0]["facid"]);
            txt_MEPT.Text = Convert.ToString(dbTable.Rows[0]["mept"]);
            txt_Latitude.Text = Convert.ToString(dbTable.Rows[0]["lattitude"]);
            txt_Longitude.Text = Convert.ToString(dbTable.Rows[0]["longitude"]);
            txt_InventoryDate.Text = string.Format(Convert.ToString(dbTable.Rows[0]["createdate"]), "dd-mm-yyyy");
            ddlst_dgyn.SelectedValue = Convert.ToString(dbTable.Rows[0]["dgnondgys"]);
            ddlst_sebyn.SelectedValue = Convert.ToString(dbTable.Rows[0]["sebnonsebys"]);
            ddlst_inventorystatus.SelectedValue = Convert.ToString(dbTable.Rows[0]["inventorystatus"]);
            ddlst_circle.SelectedValue = Convert.ToString(dbTable.Rows[0]["circleid"]);
            ddlst_articleType.SelectedValue = Convert.ToString(dbTable.Rows[0]["articletype"]);
            ddlst_articlestatus.SelectedValue = Convert.ToString(dbTable.Rows[0]["articlestatus"]);
        }
        
    }
}