using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using UMT;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Services;
using System.Net.Mail;
using System.Globalization;

public partial class AssetRegister : System.Web.UI.Page
{
    ClsAssetregister objAR = new ClsAssetregister();
    clsInventory objInventory = new clsInventory();
    Common objCommon = new Common();
    DataTable dbTable = new DataTable();
    DataTable dbInv = new DataTable();
    string result = "";
    double lat2, long2;
    protected void Page_Load(object sender, EventArgs e)
    {
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
                    lbl_ErrorMessage.Visible = false;
                }
                else
                {
                    objCommon.BindDropdowns(ddlst_circle);
                    lbl_ErrorMessage.Visible = false;
                }
            }
        }
        HtmlControl li = (HtmlGenericControl)Page.Master.FindControl("liassetregister");
        li.Attributes.Add("class", "active");
    }
    protected void btn_SaveDGDetails_Click(object sender, EventArgs e)
    {
        try
        {
            objAR.dtDefaultDt = Convert.ToDateTime("01/01/1900");
            if (InventoryID.Value == null || InventoryID.Value == String.Empty)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select Inventory');", true);
            }
            else
            {
                if (Convert.ToInt32(ddlst_dgno.SelectedValue) == 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Select correct DG no!!!');", true);
                }
                else
                {
                    objAR.intDGno = Convert.ToInt32(ddlst_dgno.SelectedValue);
                    objAR.strDGModelNo = Convert.ToString(txt_DGModelNo.Text);
                    objAR.strDGManufacturername = Convert.ToString(txt_DGManufacturerName.Text);
                    objAR.strDGRating = Convert.ToString(txt_DGRaring.Text);
                    objAR.strDGUnit = Convert.ToString(txt_DGUnitKVA.Text);
                    objAR.strEngineSrNo = Convert.ToString(txt_DGEngineSrNo.Text);
                    if (string.IsNullOrEmpty(txt_DGMfgDate.Text))
                    {
                        objAR.strDGMfgDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.strDGMfgDate = Convert.ToDateTime(txt_DGMfgDate.Text);
                    }
                    objAR.strDGWorkingStatus = Convert.ToString(txt_DGWorkingStatus.Text);
                    if (string.IsNullOrEmpty(txt_DGInstallationDate.Text))
                    {
                        objAR.strDGInstallationDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.strDGInstallationDate = Convert.ToDateTime(txt_DGInstallationDate.Text);
                    }
                    if (string.IsNullOrEmpty(txt_AlternatorMfgDate.Text))
                    {
                        objAR.strDGAlternatorMfgDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.strDGAlternatorMfgDate = Convert.ToDateTime(txt_AlternatorMfgDate.Text);
                    }
                    objAR.strDGAlternatorModelNo = Convert.ToString(txt_DGAlternatorModelNo.Text);
                    objAR.strDGAlternatorMake = Convert.ToString(txt_DGAlternatorMake.Text);
                    objAR.strDGAlternatorRating = Convert.ToString(txt_DGAlternatorRating.Text);
                    objAR.strDGAlternatorSrNo = Convert.ToString(txt_DGAlternatorSrNo.Text);
                    objAR.strDGAlternatorUnit = Convert.ToString(txt_DGAlternatorUnit.Text);
                    if (string.IsNullOrEmpty(txt_DGAMFCommisionDate.Text))
                    {
                        objAR.strDGAlternatorCommDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        //objAR.strDGAlternatorCommDate = Convert.ToDateTime(txt_DGAMFCommisionDate.Text);
                        objAR.strDGAlternatorCommDate = DateTime.ParseExact(txt_DGAMFCommisionDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                    }
                    objAR.strDGBatteryWorkingStatus = Convert.ToString(txt_DGBatteryworkingStatus.Text);
                    objAR.strDGBatteryCapacity = Convert.ToString(txt_DGBatteryCapacityAH.Text);
                    if (string.IsNullOrEmpty(txt_DGBatteryCommisionDate.Text))
                    {
                        objAR.strDGBatteryCommDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.strDGBatteryCommDate = Convert.ToDateTime(txt_DGBatteryCommisionDate.Text);
                    }
                    objAR.strDGBatteryMake = Convert.ToString(txt_DGBatteryMake.Text);
                    objAR.strDGBatterySrNo = Convert.ToString(txt_DGBatterySrNo.Text);
                    objAR.strDGAMFModelNo = Convert.ToString(txt_DGAMFmodelNo.Text);
                    if (string.IsNullOrEmpty(txt_DGAMFCommisionDate.Text))
                    {
                        objAR.strDGAmfCommisionDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.strDGAmfCommisionDate = Convert.ToDateTime(txt_DGAMFCommisionDate.Text);
                    }
                    objAR.strDGAMFWorkingStatus = Convert.ToString(txt_DGAMFWorkingStatus.Text);
                    if (string.IsNullOrEmpty(txt_DGGCUCommisionDate.Text))
                    {
                        objAR.strDGGCUCommDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.strDGGCUCommDate = Convert.ToDateTime(txt_DGGCUCommisionDate.Text);
                    }
                    objAR.strDGGCUMake = Convert.ToString(txt_DGGCUMake.Text);
                    objAR.strDGGCUModelNo = Convert.ToString(txt_DGGCUModelNo.Text);
                    objAR.strDGGCUWorkingStatus = Convert.ToString(txt_DGGCUWorkingStatus.Text);
                    objAR.strDGHealthStatus = Convert.ToString(txt_DGhealthStatus.Text);
                    objAR.strDGCPH = Convert.ToString(txt_DGCPH.Text);
                    objAR.strDGFuelTank = Convert.ToString(txt_DGFuelTank.Text);
                    objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                    objAR.intUsrId = Convert.ToInt32(Session["UsrID"]);
                    result = objAR.InsertDGDetails();
                    if (result == "1")
                    {
                        //dbTable = objAR.GetDGDetails(Convert.ToInt32(InventoryID.Value), Convert.ToInt32(ddlst_dgno.SelectedValue));
                        objAR.AssetType = "DG";
                        objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                        objAR.intAssetNo = Convert.ToInt32(ddlst_dgno.SelectedValue);
                        dbTable = objAR.GetAssetDetails();
                        if (dbTable.Rows.Count > 0)
                        {

                            string body = "<html><body><table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='4'><b>New Asset Registered</b></td> " +
                             " </tr> <tr> <td>Site ID</td> <td>Site Name</td> <td>Asset Type</td> <td>Asset No</td> </tr> <tr>" +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "</td> " +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "</td> " +
                             " <td>Diesel Generator</td> <td>" + Convert.ToString(dbTable.Rows[0]["dgnumber"]) + "</td> </tr></table>" +
                             "<hr>" +
                             "" +
                             "</body></html>";                           
                            objCommon.SendHtmlFormattedEmail(objCommon.GetMailerList(), "New Asset Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]), body);
                            Common.AddProcessLog("New DG Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "/ DG No:" + Convert.ToString(ddlst_dgno.SelectedValue) + ".", Convert.ToInt32(Session["UsrID"]));
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('DG Details Saved Succesfully for Site ID " + txt_SiteID.Text + " !!!');", true);
                            ClearDGDetails();
                        }

                    }
                    else if (result == "EXIST")
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Asset for site with asset no " + Convert.ToString(ddlst_dgno.SelectedItem) + " is exist');", true);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('There is some error: " + result + "!!!');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message,"btn_SaveDGDetails","Page Level Insert");
        }

    }
    protected void btn_ResetDGDetails_Click(object sender, EventArgs e)
    {
        ClearDGDetails();
    }
    protected void btn_SavePACDetails_Click(object sender, EventArgs e)
    {
        try
        {
            objAR.dtDefaultDt = Convert.ToDateTime("01/01/1900");
            if (InventoryID.Value == null || InventoryID.Value == String.Empty)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select Inventory');", true);
            }
            else
            {
                if (Convert.ToInt32(ddlst_PACno.SelectedValue) == 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Select correct PAC no!!!');", true);
                }
                else
                {   
                    objAR.strPACNo = Convert.ToString(ddlst_PACno.SelectedValue);
                    objAR.strPACModelNo = Convert.ToString(txt_PACModelNo.Text);
                    objAR.strPACMake = Convert.ToString(txt_PACMake.Text);
                    objAR.strPACRating = Convert.ToString(txt_PACRating.Text);
                    objAR.strPACSrNo = Convert.ToString(txt_PACSerialNo.Text);
                    objAR.strPACCompressorSrNo = Convert.ToString(txt_PACComressorSrNo.Text);
                    if (string.IsNullOrEmpty(txt_PACManufacturingDate.Text))
                    {
                        objAR.strPACMfgDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.strPACMfgDate = DateTime.ParseExact(txt_PACManufacturingDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                    }
                    if (string.IsNullOrEmpty(txt_PACInstallationDate.Text))
                    {
                        objAR.strPACInstallationDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.strPACInstallationDate = DateTime.ParseExact(txt_PACInstallationDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                    }
                    objAR.strPACWorkingStatus = Convert.ToString(txt_PACWorkingStatus.Text);
                    objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                    objAR.intUsrId = Convert.ToInt32(Session["UsrID"]);
                    result = objAR.InsertPACDetails();
                    if (result == "1")
                    {
                        //dbTable = objAR.GetPACDetails(Convert.ToInt32(InventoryID.Value), Convert.ToInt32(ddlst_PACno.SelectedValue));
                        objAR.AssetType = "PAC";
                        objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                        objAR.intAssetNo = Convert.ToInt32(ddlst_PACno.SelectedValue);
                        dbTable = objAR.GetAssetDetails();
                        if (dbTable.Rows.Count > 0)
                        {
                            string body = "<html><body><table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='4'><b>New Asset Registered</b></td> " +
                             " </tr> <tr> <td>Site ID</td> <td>Site Name</td> <td>Asset Type</td> <td>Asset No</td> </tr> <tr>" +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "</td> " +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "</td> " +
                             " <td>PAC</td> <td>" + Convert.ToString(dbTable.Rows[0]["pacnumber"]) + "</td> </tr></table>" +
                             "<hr>" +
                             "" +
                             "</body></html>";
                            //List<MailAddress> lst = new List<MailAddress>();
                            //lst.Add(new MailAddress("vishal.wable17@gmail.com"));
                            //lst.Add(new MailAddress("subhenduraj@hotmail.com"));
                            //lst.Add(new MailAddress("yaw.itengnr@gmail.com"));
                            ////lst.Add(new MailAddress("mrb@auropower.in"));
                            ////lst.Add(new MailAddress("nagaraja.venka@auropower.in"));
                            objCommon.SendHtmlFormattedEmail(objCommon.GetMailerList(), "New Asset Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]), body);
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('PAC Details Saved Succesfully for Site ID " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + " !!!');", true);
                            Common.AddProcessLog("New PAC Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "/ PAC No:" + Convert.ToString(ddlst_PACno.SelectedValue) + ".", Convert.ToInt32(Session["UsrID"]));
                            ClearPACDetails();
                        }
                    }
                    else if (result == "EXIST")
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Asset for site with asset no " + Convert.ToString(ddlst_PACno.SelectedItem) + " is exist');", true);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('There is some error: " + result + "!!!');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "btn_SavePACDetails", "Page Level Insert");
        }

    }
    protected void btn_ResetPACDetails_Click(object sender, EventArgs e)
    {
        ClearPACDetails();
    }
    protected void btn_SaveSMPSDetails_Click(object sender, EventArgs e)
    {
        try
        {
            objAR.dtDefaultDt = Convert.ToDateTime("01/01/1900");
            if (InventoryID.Value == null || InventoryID.Value == String.Empty)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select Inventory');", true);
            }
            else
            {
                if (Convert.ToInt32(ddlst_SMPSNo.SelectedValue) == 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Select correct SMPS no!!!');", true);
                }
                else
                {
                    objAR.strSMPSNo = Convert.ToString(ddlst_SMPSNo.SelectedValue);
                    objAR.strSMPSModelNo = Convert.ToString(txt_SMPSModelno.Text);
                    objAR.strSMPSMake = Convert.ToString(txt_SMPSMake.Text);
                    objAR.strSMPSSerialNo = Convert.ToString(txt_SMPSSrNo.Text);
                    objAR.strSMPSRating = Convert.ToString(txt_SMPSRating.Text);
                    objAR.strSMPSNoOfRectifierSlot = Convert.ToString(txt_SMPSNoOfRectifierSlot.Text);
                    if (string.IsNullOrEmpty(txt_SMPSMfgDate.Text))
                    {
                        objAR.strSMPSMfgDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.strSMPSMfgDate = DateTime.ParseExact(txt_SMPSMfgDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                    }
                    if (string.IsNullOrEmpty(txt_SMPSCommisionDate.Text))
                    {
                        objAR.strSMPSCommDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.strSMPSCommDate = DateTime.ParseExact(txt_SMPSCommisionDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                    }
                    objAR.strSMPSWorkingStatus = Convert.ToString(txt_SMPSWorkingStatus.Text);
                    objAR.strSMPSLVDRating = Convert.ToString(txt_SMPSLVDRating.Text);
                    if (string.IsNullOrEmpty(txt_SMPSLVDCommisionDate.Text))
                    {
                        objAR.strSMPSLVDCommisionDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.strSMPSLVDCommisionDate = DateTime.ParseExact(txt_SMPSLVDCommisionDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                    }
                    if (string.IsNullOrEmpty(txt_SMPSControllerCommisionDate.Text))
                    {
                        objAR.strSMPSControllerCommisionDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.strSMPSControllerCommisionDate = DateTime.ParseExact(txt_SMPSControllerCommisionDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                    }
                    objAR.strNoOfRectifierModuleInstalled = Convert.ToString(txt_SMPSInstalledRectifierModule.Text);
                    objAR.strNoOfModuleWorking = Convert.ToString(txt_SMPSNoOfWorkingModule.Text);
                    objAR.strModule1SrNo = Convert.ToString(txt_SMPSModule1SrNo.Text);
                    objAR.strModule2SrNo = Convert.ToString(txt_SMPSModule2SrNo.Text);
                    objAR.strModule3SrNo = Convert.ToString(txt_SMPSModule3SrNo.Text);
                    objAR.strModule4SrNo = Convert.ToString(txt_SMPSModule4SrNo.Text);
                    objAR.strModule5SrNo = Convert.ToString(txt_SMPSModule5SrNo.Text);
                    objAR.strModule6SrNo = Convert.ToString(txt_SMPSModule6SrNo.Text);
                    objAR.strModule7SrNo = Convert.ToString(txt_SMPSModule7SrNo.Text);
                    objAR.strModule8SrNo = Convert.ToString(txt_SMPSModule8SrNo.Text);
                    objAR.strModule9SrNo = Convert.ToString(txt_SMPSModule9SrNo.Text);
                    objAR.strModule10SrNo = Convert.ToString(txt_SMPSModule10SrNo.Text);
                    objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                    objAR.intUsrId = Convert.ToInt32(Session["UsrID"]);
                    result = objAR.InsertSMPSDetails();
                    if (result == "1")
                    {
                        objAR.AssetType = "SMPS";
                        objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                        objAR.intAssetNo = Convert.ToInt32(ddlst_SMPSNo.SelectedValue);
                        dbTable = objAR.GetAssetDetails();
                        if (dbTable.Rows.Count > 0)
                        {
                            string body = "<html><body><table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='4'><b>New Asset Registered</b></td> " +
                             " </tr> <tr> <td>Site ID</td> <td>Site Name</td> <td>Asset Type</td> <td>Asset No</td> </tr> <tr>" +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "</td> " +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "</td> " +
                             " <td>SMPS</td> <td>" + Convert.ToString(dbTable.Rows[0]["SMPSNo"]) + "</td> </tr></table>" +
                             "<hr>" +
                             "" +
                             "</body></html>";
                            //List<MailAddress> lst = new List<MailAddress>();
                            //lst.Add(new MailAddress("vishal.wable17@gmail.com"));
                            //lst.Add(new MailAddress("subhenduraj@hotmail.com"));
                            //lst.Add(new MailAddress("yaw.itengnr@gmail.com"));
                            ////lst.Add(new MailAddress("mrb@auropower.in"));
                            ////lst.Add(new MailAddress("nagaraja.venka@auropower.in"));
                            objCommon.SendHtmlFormattedEmail(objCommon.GetMailerList(), "New Asset Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]), body);
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('SMPS Details Saved Succesfully for Site ID " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + " !!!');", true);
                            Common.AddProcessLog("New SMPS Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "/ SMPS No:" + Convert.ToString(ddlst_SMPSNo.SelectedValue) + ".", Convert.ToInt32(Session["UsrID"]));
                            ClearSMPSDetails();
                        }
                    }
                    else if (result == "EXIST")
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Asset for site with asset no " + Convert.ToString(ddlst_SMPSNo.SelectedItem) + " is exist');", true);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('There is some error: " + result + "!!!');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "btn_SaveSMPSDetails", "Page Level Insert");
        }
    }
    protected void btn_ResetSMPSDetails_Click(object sender, EventArgs e)
    {
        ClearSMPSDetails();
    }
    protected void btn_BBSaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (InventoryID.Value == null || InventoryID.Value == String.Empty)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select Inventory');", true);
            }
            else
            {
                if (Convert.ToInt32(ddlst_BBNumber.SelectedValue) == 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Select correct Battery Bank no!!!');", true);
                }
                else
                {
                    objAR.dtDefaultDt = Convert.ToDateTime("01/01/1900");
                    objAR.strBBNumber = Convert.ToString(ddlst_BBNumber.SelectedValue);
                    objAR.strBBModel = Convert.ToString(txt_BBModelNo.Text);
                    objAR.strBBCapacity = Convert.ToString(txt_BBCapacity.Text);
                    objAR.strBBSrNo = Convert.ToString(txt_BBSrNo.Text);
                    objAR.strBBNoOfCell = Convert.ToString(txt_BBNoOfCells.Text);
                    objAR.strBBBackupInHrs = Convert.ToString(txt_BBBackupInHrs.Text);
                    objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                    if (string.IsNullOrEmpty(txt_BBMfgDate.Text))
                    {
                        objAR.strBBMfgDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.strBBMfgDate = DateTime.ParseExact(txt_BBMfgDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                    }
                    if (string.IsNullOrEmpty(txt_BBCommisionDate.Text))
                    {
                        objAR.strBBCommisionDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.strBBCommisionDate = DateTime.ParseExact(txt_BBCommisionDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                    }
                    objAR.strBBNoOfFaultyCell = Convert.ToString(txt_BBNoOfFaultyCells.Text);
                    objAR.intUsrId = Convert.ToInt32(Session["UsrID"]);
                    result = objAR.InsertBBDetails();
                    if (result == "1")
                    {
                        //dbTable = objAR.GetDGDetails(Convert.ToInt32(InventoryID.Value), Convert.ToInt32(ddlst_dgno.SelectedValue));
                        objAR.AssetType = "BB";
                        objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                        objAR.intAssetNo = Convert.ToInt32(ddlst_BBNumber.SelectedValue);
                        dbTable = objAR.GetAssetDetails();
                        if (dbTable.Rows.Count > 0)
                        {

                            string body = "<html><body><table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='4'><b>New Asset Registered</b></td> " +
                             " </tr> <tr> <td>Site ID</td> <td>Site Name</td> <td>Asset Type</td> <td>Asset No</td> </tr> <tr>" +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "</td> " +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "</td> " +
                             " <td>Battery Bank</td> <td>" + Convert.ToString(dbTable.Rows[0]["BBNo"]) + "</td> </tr></table>" +
                             "<hr>" +
                             "" +
                             "</body></html>";
                            //List<MailAddress> lst = new List<MailAddress>();
                            //lst.Add(new MailAddress("vishal.wable17@gmail.com"));
                            //lst.Add(new MailAddress("subhenduraj@hotmail.com"));
                            //lst.Add(new MailAddress("yaw.itengnr@gmail.com"));
                            ////lst.Add(new MailAddress("mrb@auropower.in"));
                            ////lst.Add(new MailAddress("nagaraja.venka@auropower.in"));
                            //"vishal.wable17@gmail.com,subhenduraj@hotmail.com,yaw.itengnr@gmail.com,mrb@auropower.in,nagaraja.venka@auropower.in"
                            objCommon.SendHtmlFormattedEmail(objCommon.GetMailerList(), "New Asset Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]), body);
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Battery Bank Saved Succesfully for Site ID " + txt_SiteID.Text + " !!!');", true);
                            Common.AddProcessLog("New Battery Bank Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "/ Battery Bank No:" + Convert.ToString(ddlst_BBNumber.SelectedValue) + ".", Convert.ToInt32(Session["UsrID"]));
                            ClearBBDetails();
                        }

                    }
                    else if (result == "EXIST")
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Asset for site with asset no " + Convert.ToString(ddlst_BBNumber.SelectedItem) + " is exist');", true);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('There is some error: " + result + "!!!');", true);
                    }
                }
            }

        }
        catch(Exception ex)
        {
            Common.LogException(ex.Message, "btn_SaveBBDetails", "Page Level Insert");
        }
    }
    protected void btn_BBReset_Click(object sender, EventArgs e)
    {
        ClearBBDetails();
    }
    protected void btn_TowerSaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (InventoryID.Value == null || InventoryID.Value == String.Empty)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select Inventory');", true);
            }
            else
            {
                if (Convert.ToInt32(ddlst_TowerNo.SelectedValue) == 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Select correct Tower no!!!');", true);
                }
                else
                {

                    objAR.dtDefaultDt = Convert.ToDateTime("01/01/1900");
                    objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                    objAR.intTWRNo = Convert.ToInt32(ddlst_TowerNo.SelectedValue);
                    objAR.strTWRSiteType = Convert.ToString(txt_TowerSiteType.Text);
                    objAR.strTWRAge = Convert.ToString(txt_TowerAge.Text);
                    objAR.strTWRCapacity = Convert.ToString(txt_TowerCapacity.Text);
                    objAR.strTWRNoOfTenancy = Convert.ToString(txt_TowerNoOfTenancy.Text);
                    objAR.strTWRNameOfTenant1 = Convert.ToString(txt_TowerNameOfTenant1.Text);
                    objAR.strTWRNameOfTenant2 = Convert.ToString(txt_TowerNameOfTenant2.Text);
                    objAR.strTWRNameOfTenant3 = Convert.ToString(txt_TowerNameOfTenant3.Text);
                    objAR.strTWRNameOfTenant4 = Convert.ToString(txt_TowerNameOfTenant4.Text);
                    objAR.strTWRNameOfTenant5 = Convert.ToString(txt_TowerNameOfTenant5.Text);
                    objAR.strTWRVacantSlots = Convert.ToString(txt_TowerCurrentVacantSlot.Text);
                    objAR.strTWRUnderAviationZone = Convert.ToString(ddlst_TowerAviationZoneYN.SelectedValue);
                    objAR.strTWRHeight = Convert.ToString(txt_TowerHeight.Text);
                    objAR.strTWRGalvanized = Convert.ToString(ddlst_TowerGalvanisation.SelectedValue);
                    objAR.strTWRAviationLampWorkingCondition = Convert.ToString(txt_TowerAviationLampStatus.Text);
                    objAR.intUsrId = Convert.ToInt32(Session["UsrID"]);
                    result = objAR.InsertTowerDetails();
                    if (result == "1")
                    {
                        //dbTable = objAR.GetDGDetails(Convert.ToInt32(InventoryID.Value), Convert.ToInt32(ddlst_dgno.SelectedValue));
                        objAR.AssetType = "Tower";
                        objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                        objAR.intAssetNo = Convert.ToInt32(ddlst_TowerNo.SelectedValue);
                        dbTable = objAR.GetAssetDetails();
                        if (dbTable.Rows.Count > 0)
                        {

                            string body = "<html><body><table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='4'><b>New Asset Registered</b></td> " +
                             " </tr> <tr> <td>Site ID</td> <td>Site Name</td> <td>Asset Type</td> <td>Asset No</td> </tr> <tr>" +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "</td> " +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "</td> " +
                             " <td>Tower</td> <td>" + Convert.ToString(dbTable.Rows[0]["TowerNo"]) + "</td> </tr></table>" +
                             "<hr>" +
                             "" +
                             "</body></html>";
                            //List<MailAddress> lst = new List<MailAddress>();
                            //lst.Add(new MailAddress("vishal.wable17@gmail.com"));
                            //lst.Add(new MailAddress("subhenduraj@hotmail.com"));
                            //lst.Add(new MailAddress("yaw.itengnr@gmail.com"));
                            ////lst.Add(new MailAddress("mrb@auropower.in"));
                            ////lst.Add(new MailAddress("nagaraja.venka@auropower.in"));
                            ////"vishal.wable17@gmail.com,subhenduraj@hotmail.com,yaw.itengnr@gmail.com,mrb@auropower.in,nagaraja.venka@auropower.in"
                            objCommon.SendHtmlFormattedEmail(objCommon.GetMailerList(), "New Asset Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]), body);
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Tower Saved Succesfully for Site ID " + txt_SiteID.Text + " !!!');", true);
                            Common.AddProcessLog("New Tower Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "/ Tower No:" + Convert.ToString(ddlst_TowerNo.SelectedValue) + ".", Convert.ToInt32(Session["UsrID"]));
                            ClearTowerDetails();
                        }

                    }
                    else if (result == "EXIST")
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Asset for site with asset no " + Convert.ToString(ddlst_TowerNo.SelectedItem) + " is exist');", true);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('There is some error: " + result + "!!!');", true);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "btn_SaveBBDetails", "Page Level Insert");
        }
    }
    protected void btn_TowerReset_Click(object sender, EventArgs e)
    {
        ClearTowerDetails();
    }    
    protected void btn_EarthingSaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (InventoryID.Value == null || InventoryID.Value == String.Empty)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select Inventory');", true);
            }
            else
            {
                objAR.dtDefaultDt = Convert.ToDateTime("01/01/1900");
                objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                objAR.intErNoOfEarthPit = Convert.ToInt32(txt_EarthingNoOfEarthPit.Text);
                objAR.intErNoOfIGB = Convert.ToInt32(txt_EarthingNoOfIGB.Text);
                objAR.intErNoOfOGB = Convert.ToInt32(txt_EarthingNoOfOGB.Text);
                objAR.intErNoOfEarthPitOKCond = Convert.ToInt32(txt_EarthingNoOfEarthPitOKCOndition.Text);
                objAR.intUsrId = Convert.ToInt32(Session["UsrID"]);
                result = objAR.InsertEarthingDetails();
                if (result == "1")
                {
                    //dbTable = objAR.GetDGDetails(Convert.ToInt32(InventoryID.Value), Convert.ToInt32(ddlst_dgno.SelectedValue));
                    objAR.AssetType = "Earthing";
                    objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                    objAR.intAssetNo = 0;
                    dbTable = objAR.GetAssetDetails();
                    if (dbTable.Rows.Count > 0)
                    {

                        string body = "<html><body><table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='4'><b>New Asset Registered</b></td> " +
                         " </tr> <tr> <td>Site ID</td> <td>Site Name</td> <td>Asset Type</td> <td>Asset No</td> </tr> <tr>" +
                         " <td>" + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "</td> " +
                         " <td>" + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "</td> " +
                         " <td>Earthing</td> <td>&nbsp;</td> </tr></table>" +
                         "<hr>" +
                         "" +
                         "</body></html>";
                        //List<MailAddress> lst = new List<MailAddress>();
                        //lst.Add(new MailAddress("vishal.wable17@gmail.com"));
                        //lst.Add(new MailAddress("subhenduraj@hotmail.com"));
                        //lst.Add(new MailAddress("yaw.itengnr@gmail.com"));
                        ////lst.Add(new MailAddress("mrb@auropower.in"));
                        ////lst.Add(new MailAddress("nagaraja.venka@auropower.in"));
                        ////"vishal.wable17@gmail.com,subhenduraj@hotmail.com,yaw.itengnr@gmail.com,mrb@auropower.in,nagaraja.venka@auropower.in"
                        objCommon.SendHtmlFormattedEmail(objCommon.GetMailerList(), "Earthing Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + " / Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]), body);
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Earthing Saved Succesfully for Site ID " + txt_SiteID.Text + " !!!');", true);
                        Common.AddProcessLog("New Earthing Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + " / Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]), Convert.ToInt32(Session["UsrID"]));
                        ClearEarthingDetails();
                    }

                }
                else if (result == "EXIST")
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Earthing for this site is exist');", true);
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('There is some error: " + result + "!!!');", true);
                }
            }

        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "btn_SaveBBDetails", "Page Level Insert");
        }
    }
    protected void btn_EarthingReset_Click(object sender, EventArgs e)
    {
        ClearEarthingDetails();
    }
    protected void btn_AVRSaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (InventoryID.Value == null || InventoryID.Value == String.Empty)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select Inventory');", true);
            }
            else
            {
                if (Convert.ToInt32(ddlst_NoOfAVR.SelectedValue) == 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Select correct AVR no!!!');", true);
                }
                else
                {
                    objAR.dtDefaultDt = Convert.ToDateTime("01/01/1900");
                    objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                    objAR.intAVRNo = Convert.ToInt32(ddlst_NoOfAVR.SelectedValue);
                    objAR.strAVRMake = Convert.ToString(txt_AVRMake.Text);
                    objAR.strAVRModelNo = Convert.ToString(txt_AVRModelNo.Text);
                    objAR.strAVRNoOfModelInstalled = Convert.ToString(txt_AVRNoOfInstalled.Text);
                    objAR.strAVRSrNo = Convert.ToString(txt_AVRSrNo.Text);
                    if (string.IsNullOrEmpty(txt_AVRInstalledDate.Text))
                    {
                        objAR.strAVRInstallationDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.strAVRInstallationDate = Convert.ToDateTime(txt_AVRInstalledDate.Text);
                    }
                    objAR.strAVRworkingStatus = Convert.ToString(txt_AVRWorkingStatus.Text);
                    objAR.intUsrId = Convert.ToInt32(Session["UsrID"]);
                    result = objAR.InsertAVRDetails();
                    if (result == "1")
                    {
                        //dbTable = objAR.GetDGDetails(Convert.ToInt32(InventoryID.Value), Convert.ToInt32(ddlst_dgno.SelectedValue));
                        objAR.AssetType = "AVR";
                        objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                        objAR.intAssetNo = Convert.ToInt32(ddlst_NoOfAVR.SelectedValue);
                        dbTable = objAR.GetAssetDetails();
                        if (dbTable.Rows.Count > 0)
                        {

                            string body = "<html><body><table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='4'><b>New Asset Registered</b></td> " +
                             " </tr> <tr> <td>Site ID</td> <td>Site Name</td> <td>Asset Type</td> <td>Asset No</td> </tr> <tr>" +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "</td> " +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "</td> " +
                             " <td>AVR</td> <td>" + Convert.ToString(dbTable.Rows[0]["AVRNo"]) + "</td> </tr></table>" +
                             "<hr>" +
                             "" +
                             "</body></html>";
                            //List<MailAddress> lst = new List<MailAddress>();
                            //lst.Add(new MailAddress("vishal.wable17@gmail.com"));
                            //lst.Add(new MailAddress("subhenduraj@hotmail.com"));
                            //lst.Add(new MailAddress("yaw.itengnr@gmail.com"));
                            ////lst.Add(new MailAddress("mrb@auropower.in"));
                            ////lst.Add(new MailAddress("nagaraja.venka@auropower.in"));
                            ////"vishal.wable17@gmail.com,subhenduraj@hotmail.com,yaw.itengnr@gmail.com,mrb@auropower.in,nagaraja.venka@auropower.in"
                            objCommon.SendHtmlFormattedEmail(objCommon.GetMailerList(), "New Asset Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + " / Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]), body);
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('AVR Saved Succesfully for Site ID " + txt_SiteID.Text + " !!!');", true);
                            Common.AddProcessLog("New AVR Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + " / Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "/ AVR No:" + Convert.ToString(ddlst_NoOfAVR.SelectedValue) + ".", Convert.ToInt32(Session["UsrID"]));
                            ClearAVRDetails();
                        }

                    }
                    else if (result == "EXIST")
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('AVR for site with asset no " + Convert.ToString(ddlst_NoOfAVR.SelectedItem) + " is exist');", true);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('There is some error: " + result + "!!!');", true);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "btn_SaveBBDetails", "Page Level Insert");
        }
    }
    protected void btn_AVRReset_Click(object sender, EventArgs e)
    {
        ClearAVRDetails();
    }
    protected void btn_ShelterSaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (InventoryID.Value == null || InventoryID.Value == String.Empty)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select Inventory');", true);
            }
            else
            {
                objAR.dtDefaultDt = Convert.ToDateTime("01/01/1900");
                objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                objAR.intShelterNo = Convert.ToInt32(ddlst_ShelterNo.SelectedValue);
                objAR.strShelterType = Convert.ToString(ddlst_ShelterType.SelectedValue);
                objAR.strShelterMake = Convert.ToString(txt_ShelterMake.Text);
                if (string.IsNullOrEmpty(txt_ShelterMfgDate.Text))
                {
                    objAR.strShelterMfgDt = objAR.dtDefaultDt;
                }
                else
                {
                    objAR.strShelterMfgDt = Convert.ToDateTime(txt_ShelterMfgDate.Text);
                }
                if (string.IsNullOrEmpty(txt_ShelterCommisionDate.Text))
                {
                    objAR.strShelterCommDt = objAR.dtDefaultDt;
                }
                else
                {
                    objAR.strShelterCommDt = Convert.ToDateTime(txt_ShelterCommisionDate.Text);
                }
                objAR.strShelterDim = Convert.ToString(txt_ShelterDimension.Text);
                objAR.intUsrId = Convert.ToInt32(Session["UsrID"]);
                result = objAR.InsertShelterDetails();
                if (result == "1")
                {
                    //dbTable = objAR.GetDGDetails(Convert.ToInt32(InventoryID.Value), Convert.ToInt32(ddlst_dgno.SelectedValue));
                    objAR.AssetType = "Shelter";
                    objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                    objAR.intAssetNo = Convert.ToInt32(ddlst_ShelterNo.SelectedValue);
                    dbTable = objAR.GetAssetDetails();
                    if (dbTable.Rows.Count > 0)
                    {

                        string body = "<html><body><table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='4'><b>New Asset Registered</b></td> " +
                         " </tr> <tr> <td>Site ID</td> <td>Site Name</td> <td>Asset Type</td> <td>Asset No</td> </tr> <tr>" +
                         " <td>" + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "</td> " +
                         " <td>" + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "</td> " +
                         " <td>Shelter</td> <td>" + Convert.ToString(dbTable.Rows[0]["ShelterNo"]) + "</td> </tr></table>" +
                         "<hr>" +
                         "" +
                         "</body></html>";
                        //List<MailAddress> lst = new List<MailAddress>();
                        //lst.Add(new MailAddress("vishal.wable17@gmail.com"));
                        //lst.Add(new MailAddress("subhenduraj@hotmail.com"));
                        //lst.Add(new MailAddress("yaw.itengnr@gmail.com"));
                        ////lst.Add(new MailAddress("mrb@auropower.in"));
                        ////lst.Add(new MailAddress("nagaraja.venka@auropower.in"));
                        ////"vishal.wable17@gmail.com,subhenduraj@hotmail.com,yaw.itengnr@gmail.com,mrb@auropower.in,nagaraja.venka@auropower.in"
                        objCommon.SendHtmlFormattedEmail(objCommon.GetMailerList(), "New Asset Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]), body);
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Shelter Saved Succesfully for Site ID " + txt_SiteID.Text + " !!!');", true);
                        Common.AddProcessLog("New Shelter Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "/ Shelter No:" + Convert.ToString(ddlst_ShelterNo.SelectedValue) + ".", Convert.ToInt32(Session["UsrID"]));
                        ClearShelterDetails();
                    }

                }
                else if (result == "EXIST")
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Asset for site with asset no " + Convert.ToString(ddlst_ShelterNo.SelectedItem) + " is exist');", true);
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('There is some error: " + result + "!!!');", true);
                }
            }

        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "btn_ShelterSaveDetails_Click", "Page Level Insert");
        }
    }
    protected void btn_ShelterReset_Click(object sender, EventArgs e)
    {
        ClearShelterDetails();
    }
    protected void btn_TransformerSaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (InventoryID.Value == null || InventoryID.Value == String.Empty)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select Inventory');", true);
            }
            else
            {
                objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                objAR.strTfmHTLT = Convert.ToString(ddlst_TransformerHTLT.SelectedValue);
                objAR.strTfmLineCapacity = Convert.ToString(txt_TransformerLineCapacity.Text);
                objAR.strTfmOwnSEB = Convert.ToString(txt_TransformerOwnSEB.Text);
                objAR.strTfmSiteLoad = Convert.ToString(txt_TransformerSiteLoadCurrent.Text);
                objAR.intUsrId = Convert.ToInt32(Session["UsrID"]);
                result = objAR.InsertTransformerDetails();
                if (result == "1")
                {
                    //dbTable = objAR.GetDGDetails(Convert.ToInt32(InventoryID.Value), Convert.ToInt32(ddlst_dgno.SelectedValue));
                    objAR.AssetType = "Transformer";
                    objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                    objAR.intAssetNo = Convert.ToInt32(ddlst_ShelterNo.SelectedValue);
                    dbTable = objAR.GetAssetDetails();
                    if (dbTable.Rows.Count > 0)
                    {

                        string body = "<html><body><table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='4'><b>New Asset Registered</b></td> " +
                         " </tr> <tr> <td>Site ID</td> <td>Site Name</td> <td>Asset Type</td> <td>Asset No</td> </tr> <tr>" +
                         " <td>" + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "</td> " +
                         " <td>" + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "</td> " +
                         " <td>Transformer</td> <td>&nbsp;</td> </tr></table>" +
                         "<hr>" +
                         "" +
                         "</body></html>";
                        //List<MailAddress> lst = new List<MailAddress>();
                        //lst.Add(new MailAddress("vishal.wable17@gmail.com"));
                        //lst.Add(new MailAddress("subhenduraj@hotmail.com"));
                        //lst.Add(new MailAddress("yaw.itengnr@gmail.com"));
                        ////lst.Add(new MailAddress("mrb@auropower.in"));
                        ////lst.Add(new MailAddress("nagaraja.venka@auropower.in"));
                        ////"vishal.wable17@gmail.com,subhenduraj@hotmail.com,yaw.itengnr@gmail.com,mrb@auropower.in,nagaraja.venka@auropower.in"
                        objCommon.SendHtmlFormattedEmail(objCommon.GetMailerList(), "New Asset Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]), body);
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Transformer Saved Succesfully for Site ID " + txt_SiteID.Text + " !!!');", true);
                        Common.AddProcessLog("New Transformer Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]) + ".", Convert.ToInt32(Session["UsrID"]));
                        ClearTransformerDetails();
                    }

                }
                else if (result == "EXIST")
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Asset for site with asset no " + Convert.ToString(ddlst_ShelterNo.SelectedItem) + " is exist');", true);
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('There is some error: " + result + "!!!');", true);
                }
            }

        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "btn_ShelterSaveDetails_Click", "Page Level Insert");
        }
    }
    protected void btn_TransformerReset_Click(object sender, EventArgs e)
    {
        ClearTransformerDetails();
    }
    protected void btn_FireSystemSave_Click(object sender, EventArgs e)
    {
        try
        {
            objAR.dtDefaultDt = Convert.ToDateTime("01/01/1900");
            if (InventoryID.Value == null || InventoryID.Value == String.Empty)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select Inventory');", true);
            }
            else
            {
                if (Convert.ToInt32(ddlst_FireSystemNo.SelectedValue) == 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Select correct Fire System no!!!');", true);
                }
                else
                {
                    objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                    objAR.FSNo = Convert.ToInt32(ddlst_FireSystemNo.SelectedValue);
                    objAR.FSMake = Convert.ToString(txt_FireSystemMake.Text);
                    objAR.FSModel = Convert.ToString(txt_FireSystemModel.Text);
                    objAR.FSStatus = Convert.ToString(txt_FireSystemStatus.Text);
                    if (string.IsNullOrEmpty(txt_FireSystemCommDate.Text))
                    {
                        objAR.FSInstallDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.FSInstallDate = Convert.ToDateTime(txt_FireSystemCommDate.Text);
                    }
                    objAR.ExtraInfo1 = Convert.ToString(txt_FireSystemExt1.Text);
                    objAR.ExtraInfo2 = Convert.ToString(txt_FireSystemExt2.Text);
                    objAR.ExtraInfo3 = Convert.ToString(txt_FireSystemExt3.Text);
                    objAR.intUsrId = Convert.ToInt32(Session["UsrID"]);
                    result = objAR.InsertFireSystemDetails();
                    if (result == "1")
                    {
                        objAR.AssetType = "Fire System";
                        objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                        objAR.intAssetNo = Convert.ToInt32(ddlst_FireSystemNo.SelectedValue);
                        dbTable = objAR.GetAssetDetails();
                        if (dbTable.Rows.Count > 0)
                        {

                            string body = "<html><body><table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='4'><b>New Asset Registered</b></td> " +
                             " </tr> <tr> <td>Site ID</td> <td>Site Name</td> <td>Asset Type</td> <td>Asset No</td> </tr> <tr>" +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "</td> " +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "</td> " +
                             " <td> Fire System </td> <td>" + Convert.ToString(dbTable.Rows[0]["FSNo"]) + "</td> </tr></table>" +
                             "<hr>" +
                             "" +
                             "</body></html>";
                            //List<MailAddress> lst = new List<MailAddress>();
                            //lst.Add(new MailAddress("vishal.wable17@gmail.com"));
                            //lst.Add(new MailAddress("subhenduraj@hotmail.com"));
                            //lst.Add(new MailAddress("yaw.itengnr@gmail.com"));
                            objCommon.SendHtmlFormattedEmail(objCommon.GetMailerList(), "New Asset Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]), body);
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Shelter Saved Succesfully for Site ID " + txt_SiteID.Text + " !!!');", true);
                            Common.AddProcessLog("New Shelter Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "/ Fire System No:" + Convert.ToString(ddlst_FireSystemNo.SelectedValue) + ".", Convert.ToInt32(Session["UsrID"]));
                            ClearFireSystemDetails();
                        }

                    }
                    else if (result == "EXIST")
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Asset for site with asset no " + Convert.ToString(ddlst_FireSystemNo.SelectedItem) + " is exist');", true);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('There is some error: " + result + "!!!');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "btn_FireSystemSave_Click", "Page Level Insert");
        }
    }
    protected void btn_FireSystemReset_Click(object sender, EventArgs e)
    {
        ClearFireSystemDetails();
    }
    protected void btn_CoolSystemSave_Click(object sender, EventArgs e)
    {
        try
        {
            objAR.dtDefaultDt = Convert.ToDateTime("01/01/1900");
            if (InventoryID.Value == null || InventoryID.Value == String.Empty)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select Inventory');", true);
            }
            else
            {
                if (Convert.ToInt32(ddlst_CoolingSystemNo.SelectedValue) == 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Select correct Cooling System no!!!');", true);
                }
                else
                {
                    objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                    objAR.CSNo = Convert.ToInt32(ddlst_CoolingSystemNo.SelectedValue);
                    objAR.CSMake = Convert.ToString(txt_CoolSysMake.Text);
                    objAR.CSModel = Convert.ToString(txt_CoolSysModel.Text);
                    objAR.CSStatus = Convert.ToString(txt_CoolSysStatus.Text);
                    if (string.IsNullOrEmpty(txt_CoolSysCommDate.Text))
                    {
                        objAR.CSInstallDate = objAR.dtDefaultDt;
                    }
                    else
                    {
                        objAR.CSInstallDate = Convert.ToDateTime(txt_CoolSysCommDate.Text);
                    }
                    objAR.ExtraInfo1 = Convert.ToString(txt_FireSystemExt1.Text);
                    objAR.ExtraInfo2 = Convert.ToString(txt_FireSystemExt2.Text);
                    objAR.ExtraInfo3 = Convert.ToString(txt_FireSystemExt3.Text);
                    objAR.intUsrId = Convert.ToInt32(Session["UsrID"]);
                    result = objAR.InsertFireSystemDetails();
                    if (result == "1")
                    {
                        objAR.AssetType = "Fire System";
                        objAR.intInventoryId = Convert.ToInt32(InventoryID.Value);
                        objAR.intAssetNo = Convert.ToInt32(ddlst_CoolingSystemNo.SelectedValue);
                        dbTable = objAR.GetAssetDetails();
                        if (dbTable.Rows.Count > 0)
                        {

                            string body = "<html><body><table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='4'><b>New Asset Registered</b></td> " +
                             " </tr> <tr> <td>Site ID</td> <td>Site Name</td> <td>Asset Type</td> <td>Asset No</td> </tr> <tr>" +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "</td> " +
                             " <td>" + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "</td> " +
                             " <td> Cooling System </td> <td>" + Convert.ToString(dbTable.Rows[0]["CSNo"]) + "</td> </tr></table>" +
                             "<hr>" +
                             "" +
                             "</body></html>";
                            //List<MailAddress> lst = new List<MailAddress>();
                            //lst.Add(new MailAddress("vishal.wable17@gmail.com"));
                            //lst.Add(new MailAddress("subhenduraj@hotmail.com"));
                            //lst.Add(new MailAddress("yaw.itengnr@gmail.com"));
                            objCommon.SendHtmlFormattedEmail(objCommon.GetMailerList(), "New Asset Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]), body);
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Shelter Saved Succesfully for Site ID " + txt_SiteID.Text + " !!!');", true);
                            Common.AddProcessLog("New Shelter Added to Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "/ Cooling System No:" + Convert.ToString(ddlst_CoolingSystemNo.SelectedValue) + ".", Convert.ToInt32(Session["UsrID"]));
                            ClearCoolingSystemDetails();
                        }

                    }
                    else if (result == "EXIST")
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Asset for site with asset no " + Convert.ToString(ddlst_CoolingSystemNo.SelectedItem) + " is exist');", true);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('There is some error: " + result + "!!!');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "btn_CoolSystemSave_Click", "Page Level Insert");
        }
    }
    protected void btn_CoolSystemReset_Click(object sender, EventArgs e)
    {
        ClearCoolingSystemDetails();
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
        try
        {
            DataTable dt = new DataTable();
            dt = objAR.GetDGDetails(Convert.ToInt32(InventoryID.Value));
            //BindGrid(grdview_dg, dt);
            var Lattitude = latitudeI.Value;
            var Longitude = longitudeId.Value;
            DataTable dbTable = new DataTable();
            dbTable = objCommon.GetLatLong(Convert.ToInt32(InventoryID.Value));
            double lat1 = Convert.ToDouble(dbTable.Rows[0]["lattitude"]);
            double long1 = Convert.ToDouble(dbTable.Rows[0]["longitude"]);
            txt_TowerLattitude.Text = Convert.ToString(dbTable.Rows[0]["lattitude"]);
            txt_TowerLongitude.Text = Convert.ToString(dbTable.Rows[0]["longitude"]);
            if (latitudeI.Value != "" && longitudeId.Value != "")
            {
                lat2 = Convert.ToDouble(latitudeI.Value);
                long2 = Convert.ToDouble(longitudeId.Value);
                if (objCommon.Distance(lat1, long1, lat2, long2, 1) < 300)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Location Match Found at " + Lattitude + "-" + Longitude + "');", true);

                    dt = null;
                    objInventory.intInventoryId = Convert.ToInt32(InventoryID.Value);
                    dt = objInventory.GetSpecificSite();
                    dg.Visible = true;
                    allinputs.Attributes.Add("disabled", "false");
                    if (Convert.ToString(dt.Rows[0]["dgnondgys"]) == "1")
                    {
                        dg.Attributes.Add("disabled", "false");
                        dg_UpdatePanel.Visible = true;
                        lbl_ErrorMessage.Visible = false;
                    }
                    else
                    {
                        dg.Attributes.Add("disabled", "true");
                        lbl_ErrorMessage.Visible = true;
                        lbl_ErrorMessage.Text = "Diesel Generator for this site is not exist.";
                        dg_UpdatePanel.Visible = false;
                    }
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Location Match Not  Found at " + Lattitude + "-" + Longitude + "');", true);
                    dg.Visible = false;
                    allinputs.Visible = false;
                }
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Your location services may be not active please check browser settings');", true);
            }
        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "Select site", "Asset Register page Issue while selection of site");
        }
    }
    public void CreateExcelFile(DataTable Excel)
    {
        //Clears all content output from the buffer stream.  
        Response.ClearContent();
        //Adds HTTP header to the output stream  
        Response.AddHeader("content-disposition", string.Format("attachment; filename=NewAssetRegister.xls"));
        // Gets or sets the HTTP MIME type of the output stream  
        Response.ContentType = "application/vnd.ms-excel";
        string space = "";
        foreach (DataColumn dcolumn in Excel.Columns)
        {
            Response.Write(space + dcolumn.ColumnName);
            space = "\t";
        }
        Response.Write("\n");
        int countcolumn;
        foreach (DataRow dr in Excel.Rows)
        {
            space = "";
            for (countcolumn = 0; countcolumn < Excel.Columns.Count; countcolumn++)
            {
                Response.Write(space + dr[countcolumn].ToString());
                space = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
    public void BindGrid(GridView gv, DataTable dt)
    {
        gv.DataSource = dt;
        gv.DataBind();
    }    
    #region Clear Controls
    public void ClearPACDetails()
    {
        txt_PACComressorSrNo.Text = "";
        txt_PACInstallationDate.Text = "";
        ddlst_PACno.SelectedIndex = 0;
        txt_PACMake.Text = "";
        txt_PACManufacturingDate.Text = "";
        txt_PACModelNo.Text = "";
        txt_PACRating.Text = "";
        txt_PACSerialNo.Text = "";
        txt_PACWorkingStatus.Text = "";
    }
    public void ClearDGDetails()
    {

    }
    public void ClearSMPSDetails()
    {
        ddlst_SMPSNo.SelectedIndex = 0;
        txt_SMPSCommisionDate.Text = "";
        txt_SMPSControllerCommisionDate.Text = "";
        txt_SMPSInstalledRectifierModule.Text = "";
        txt_SMPSLVDCommisionDate.Text = "";
        txt_SMPSLVDRating.Text = "";
        txt_SMPSMake.Text = "";
        txt_SMPSMfgDate.Text = "";
        txt_SMPSModelno.Text = "";
        txt_SMPSModule10SrNo.Text = "";
        txt_SMPSModule1SrNo.Text = "";
        txt_SMPSModule2SrNo.Text = "";
        txt_SMPSModule3SrNo.Text = "";
        txt_SMPSModule4SrNo.Text = "";
        txt_SMPSModule5SrNo.Text = "";
        txt_SMPSModule6SrNo.Text = "";
        txt_SMPSModule7SrNo.Text = "";
        txt_SMPSModule8SrNo.Text = "";
        txt_SMPSModule9SrNo.Text = "";
        txt_SMPSNoOfRectifierSlot.Text = "";
        txt_SMPSNoOfWorkingModule.Text = "";
        txt_SMPSRating.Text = "";
        txt_SMPSSrNo.Text = "";
        txt_SMPSWorkingStatus.Text = "";
    }
    public void ClearBBDetails()
    {
        ddlst_BBNumber.SelectedIndex = 0;
        txt_BBBackupInHrs.Text = "";
        txt_BBCapacity.Text = "";
        txt_BBCommisionDate.Text = "";
        txt_BBMfgDate.Text = "";
        txt_BBModelNo.Text = "";
        txt_BBNoOfCells.Text = "";
        txt_BBNoOfFaultyCells.Text = "";
        txt_BBSrNo.Text = "";
    }
    public void ClearTowerDetails()
    {
        ddlst_TowerNo.SelectedIndex = 0;
        txt_TowerAge.Text = "";
        txt_TowerAviationLampStatus.Text = "";
        txt_TowerCapacity.Text = "";
        txt_TowerCurrentVacantSlot.Text = "";
        txt_TowerHeight.Text = "";
        txt_TowerLattitude.Text = "";
        txt_TowerLongitude.Text = "";
        txt_TowerNameOfTenant1.Text = "";
        txt_TowerNameOfTenant2.Text = "";
        txt_TowerNameOfTenant3.Text = "";
        txt_TowerNameOfTenant4.Text = "";
        txt_TowerNameOfTenant5.Text = "";
        txt_TowerNoOfTenancy.Text = "";
        txt_TowerSiteType.Text = "";
        ddlst_TowerAviationZoneYN.SelectedIndex = 0;
        ddlst_TowerGalvanisation.SelectedIndex = 0;
    }
    public void ClearEarthingDetails()
    {
        txt_EarthingNoOfEarthPit.Text = "";
        txt_EarthingNoOfEarthPitOKCOndition.Text = "";
        txt_EarthingNoOfIGB.Text = "";
        txt_EarthingNoOfOGB.Text = "";
    }
    public void ClearAVRDetails()
    {
        ddlst_NoOfAVR.SelectedIndex = 0;
        txt_AVRInstalledDate.Text = "";
        txt_AVRMake.Text = "";
        txt_AVRModelNo.Text = "";
        txt_AVRNoOfInstalled.Text = "";
        txt_AVRSrNo.Text = "";
        txt_AVRWorkingStatus.Text = "";
    }
    public void ClearShelterDetails()
    {
        ddlst_ShelterNo.SelectedIndex = 0;
        ddlst_ShelterType.SelectedIndex = 0;
        txt_ShelterCommisionDate.Text = "";
        txt_ShelterDimension.Text = "";
        txt_ShelterMake.Text = "";
        txt_ShelterMfgDate.Text = "";
    }
    public void ClearTransformerDetails()
    {
        ddlst_TransformerHTLT.SelectedIndex = 0;
        txt_TransformerLineCapacity.Text = "";
        txt_TransformerOwnSEB.Text = "";
        txt_TransformerSiteLoadCurrent.Text = "";
    }
    public void ClearFireSystemDetails()
    {
        ddlst_FireSystemNo.SelectedIndex = 0;
        txt_FireSystemExt1.Text = "";
        txt_FireSystemExt2.Text = "";
        txt_FireSystemExt3.Text = "";
        txt_FireSystemCommDate.Text = "";
        txt_FireSystemMake.Text = "";
        txt_FireSystemModel.Text = "";
        txt_FireSystemStatus.Text = "";
    }
    public void ClearCoolingSystemDetails()
    {
        ddlst_CoolingSystemNo.SelectedIndex = 0;
        txt_CoolSysStatus.Text = "";
        txt_CoolSysCommDate.Text = "";
        txt_CoolSysExt1.Text = "";
        txt_CoolSysExt2.Text = "";
        txt_CoolSysExt3.Text = "";
        txt_CoolSysMake.Text = "";
        txt_CoolSysModel.Text = "";
    }
    #endregion

}