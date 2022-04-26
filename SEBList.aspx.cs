using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UMT;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class SEBList : System.Web.UI.Page
{
    clsSEB objSEB = new clsSEB();
    Common objCommon = new Common();
    DataTable dbTable = new DataTable();
    string InventoryID, ULId, UserID, CreatedByUserID = "";
    int ApprovalStage = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlControl li = (HtmlGenericControl)Page.Master.FindControl("liseb");
        li.Attributes.Add("class", "active");
        if (!Page.IsPostBack)
        {
            objCommon.BindDropdowns(ddlst_circle);
            BindGrid(Convert.ToInt32(ddlst_circle.SelectedValue));
        }
    }
    private void BindGrid(int circleId)
    {
        //objclsInventory.intCircle = circleId;
        dbTable = objSEB.GetSebList();
        if (dbTable.Rows.Count > 0)
        {
            grdview_SEBDetails.DataSource = dbTable;
            grdview_SEBDetails.DataBind();
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('There is no SEB present in this circle');", true);
        }
    }
    protected void grdview_SEBDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdview_SEBDetails.PageIndex = e.NewPageIndex;
        BindGrid(Convert.ToInt32(ddlst_circle.SelectedValue));
    }
    protected void ddlst_rowsize_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlst_rowsize.SelectedIndex == 1)
        {
            grdview_SEBDetails.PageSize = 5;
        }
        else if (ddlst_rowsize.SelectedIndex == 2)
        {
            grdview_SEBDetails.PageSize = 10;
        }
        else if (ddlst_rowsize.SelectedIndex == 3)
        {
            grdview_SEBDetails.PageSize = 25;
        }
        else if (ddlst_rowsize.SelectedIndex == 4)
        {
            grdview_SEBDetails.PageSize = 50;
        }
        else if (ddlst_rowsize.SelectedIndex == 5)
        {
            grdview_SEBDetails.PageSize = 100;
        }
    }
    protected void grdview_SEBDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DropDownList ddList = (DropDownList)e.Row.FindControl("ddlst_UserType");
        //    Button btn_Apply = (Button)e.Row.FindControl("btn_apply");
        //    ddList.DataSource = objCommon.GetUserTypes(1);
        //    ddList.DataTextField = "LVLNAME";
        //    ddList.DataValueField = "LVLID";
        //    ddList.DataBind();
        //    ddList.Items.Insert(0, new ListItem("-- Select User Type --"));
        //}
    }
    protected void ddlst_circle_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid(Convert.ToInt32(ddlst_circle.SelectedValue));
    }
    protected void grdview_SEBDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("edit"))
        {
            Session["SebID"] = e.CommandArgument;
            Response.Redirect("/seb.aspx");
        }
        if (e.CommandName.Equals("Delete"))
        {
            string result = objSEB.DeleteSebDetails(e.CommandArgument.ToString());
            BindGrid(Convert.ToInt32(ddlst_circle.SelectedValue));
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('SEB Deleted Sucessfully');", true);
        }
        //if (e.CommandName.Equals("AllocateInventory"))
        //{
        //    string result = objclsInventory.AllocateInventory(Convert.ToString(ViewState["InventoryID"]),
        //                                                       Convert.ToString(ViewState["UserLevelId"]),
        //                                                       Convert.ToString(ViewState["ApproverID"]),
        //                                                       Convert.ToInt32(Session["UsrID"]));
        //    if (result == "1")
        //    {
        //        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Site Allocated Sucessfully');", true);
        //    }
        //    else if (result == "ALLOCATED")
        //    {
        //        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Site Allocation Completed');", true);
        //    }
        //    else if (result == "PARTIALLY ALLOCATED")
        //    {
        //        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Site Allocated Partially');", true);
        //    }
        //}
    }
    protected void grdview_SEBDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}