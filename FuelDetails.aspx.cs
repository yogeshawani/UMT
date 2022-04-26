using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using UMT;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Net.Mail;

public partial class FuelDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    clsInventory objInventory = new clsInventory();
    ClsFuelDetails objFuel = new ClsFuelDetails();
    DataTable dbTable = new DataTable();
    DataTable dbInv = new DataTable();
    DataTable dbFuelDt = new DataTable();
    double lat2, long2;
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlControl li = (HtmlGenericControl)Page.Master.FindControl("lifuel");
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
    protected void btn_SelectSite_Click(object sender, EventArgs e)
    {
        try
        {
            objFuel.InventoryID = Convert.ToInt32(InventoryID.Value);
            dbFuelDt = objFuel.GetFuelDetails();
            GetFuelDetails(dbFuelDt);
            var Lattitude = latitudeI.Value;
            var Longitude = longitudeId.Value;
            DataTable dbTable = new DataTable();
            dbTable = objCommon.GetLatLong(Convert.ToInt32(InventoryID.Value));
            double lat1 = Convert.ToDouble(dbTable.Rows[0]["lattitude"]);
            double long1 = Convert.ToDouble(dbTable.Rows[0]["longitude"]);

            if (latitudeI.Value != "" && longitudeId.Value != "")
            {
                lat2 = Convert.ToDouble(latitudeI.Value);
                long2 = Convert.ToDouble(longitudeId.Value);
                if (objCommon.Distance(lat1, long1, lat2, long2, 1) < 300)
                {
                    //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Location Match Found at " + Lattitude + "-" + Longitude + "');", true);
                    objFuel.InventoryID = Convert.ToInt32(InventoryID.Value);
                    dbFuelDt = objFuel.GetFuelDetails();
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Location Match Not  Found at " + Lattitude + "-" + Longitude + "');", true);                  
                }
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Your location services may be not active please check browser settings');", true);
            }
        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "Select site", "FMT page Issue while selection of site");
        }
    }
    protected void txt_SMRClosing_TextChanged(object sender, EventArgs e)
    {
        txt_SMRUnitConsumed.Text = objFuel.CalcSEBMeterReading(Convert.ToDouble(txt_SMROpening.Text), Convert.ToDouble(txt_SMRClosing.Text)).ToString();
    }
    protected void txt_GRClosing_TextChanged(object sender, EventArgs e)
    {
        txt_GRUnitConsumed.Text = objFuel.CalcSEBMeterReading(Convert.ToDouble(txt_GROpening.Text), Convert.ToDouble(txt_GRClosing.Text)).ToString();
    }
    protected void txt_GHMRClosing_TextChanged(object sender, EventArgs e)
    {
        txt_GHMRRunhr.Text = objFuel.CalcSEBMeterReading(Convert.ToDouble(txt_GHMROpening.Text), Convert.ToDouble(txt_GHMRClosing.Text)).ToString();
    }
    protected void txt_DHMRClosing_TextChanged(object sender, EventArgs e)
    {
        txt_DHMRRunhr.Text = objFuel.CalcSEBMeterReading(Convert.ToDouble(txt_DHMROpening.Text), Convert.ToDouble(txt_DHMRClosing.Text)).ToString();
    }
    protected void txt_DRDieselFilled_TextChanged(object sender, EventArgs e)
    {
        txt_DRDieselConsumed.Text = objFuel.CalcDCDuringMonth(Convert.ToDouble(txt_DROpeningStock.Text), 
            Convert.ToDouble(txt_DRClosingStock.Text), Convert.ToDouble(txt_DRDieselFilled.Text)).ToString();
        txt_DRActualCPH.Text = objFuel.CalcActualCPH(Convert.ToDouble(txt_DRDieselConsumed.Text), Convert.ToDouble(txt_GHMRRunhr.Text)).ToString();
        txt_DRDieselComsumptionDeviation.Text = objFuel.CalcDCDeviation(Convert.ToDouble(txt_DRActualCPH.Text),
            Convert.ToDouble(txt_DHMROpening.Text), Convert.ToDouble(txt_GHMRRunhr.Text)).ToString();
    }
    protected void btn_SaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            objFuel.InventoryID = Convert.ToInt32(InventoryID.Value);
            objFuel.SMROpening = Convert.ToDouble(txt_SMROpening.Text);
            objFuel.SMRClosing = Convert.ToDouble(txt_SMRClosing.Text);
            objFuel.SMRUnitConsumed = Convert.ToDouble(txt_SMRUnitConsumed.Text);
            objFuel.GROpening = Convert.ToDouble(txt_GROpening.Text);
            objFuel.GRClosing = Convert.ToDouble(txt_GRClosing.Text);
            objFuel.GRUnitConsumed = Convert.ToDouble(txt_GRUnitConsumed.Text);
            objFuel.GHMROpening = Convert.ToDouble(txt_GHMROpening.Text);
            objFuel.GHMRClosing = Convert.ToDouble(txt_GHMRClosing.Text);
            objFuel.GHMRTotalDGRunHr = Convert.ToDouble(txt_GHMRRunhr.Text);
            objFuel.DHMROpening = Convert.ToDouble(txt_DHMROpening.Text);
            objFuel.DHMRClosing = Convert.ToDouble(txt_DHMRClosing.Text);
            objFuel.DHMRTotalDGRunHr = Convert.ToDouble(txt_DHMRRunhr.Text);
            objFuel.DROpeningStock = Convert.ToDouble(txt_DROpeningStock.Text);
            objFuel.DRClosingStock = Convert.ToDouble(txt_DRClosingStock.Text);
            objFuel.DRDieselFilled = Convert.ToDouble(txt_DRDieselFilled.Text);
            objFuel.DRDieselConsumed = Convert.ToDouble(txt_DRDieselConsumed.Text);
            objFuel.DRActualCPH = Convert.ToDouble(txt_DRActualCPH.Text);
            objFuel.DRDieselConsumptionDev = Convert.ToDouble(txt_DRDieselComsumptionDeviation.Text);
            objFuel.UserID = Convert.ToInt32(Session["UsrID"]);
            objFuel.InsertFuelDetails();
            objInventory.intInventoryId = Convert.ToInt32(InventoryID.Value);
            dbInv = objInventory.GetSpecificSite();
            if (dbInv.Rows.Count > 0)
            {
                string body = "<html><body><table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='4'><b>Fuel Details</b></td> " +
                        " </tr> <tr> <td>Site ID</td> <td>Site Name</td> </tr> " +
                        "<tr> <td>" + Convert.ToString(dbInv.Rows[0]["SiteID"]) + "</td> " +
                        " <td>" + Convert.ToString(dbInv.Rows[0]["SiteName"]) +"</td> </tr></table>" +
                        "<hr>" +
                        "" +
                        "</body></html>";
                List<MailAddress> lst = new List<MailAddress>();
                lst.Add(new MailAddress("vishal.wable17@gmail.com"));
                lst.Add(new MailAddress("subhenduraj@hotmail.com"));
                lst.Add(new MailAddress("yaw.itengnr@gmail.com"));
                lst.Add(new MailAddress("mrb@auropower.in"));
                lst.Add(new MailAddress("nagaraja.venka@auropower.in"));
                objCommon.SendHtmlFormattedEmail(lst, "Fuel details for Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]), body);
                Common.AddProcessLog("Fuel details added for Site ID :- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "", Convert.ToInt32(Session["UsrID"]));
                ClearControls();
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Fuel details added sucessfully');", true);
            }
        }
        catch (Exception ex)
        {

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
    protected void GetFuelDetails(DataTable dbTable)
    {
        if (dbTable.Rows.Count > 0)
        {
            txt_SMROpening.Text = Convert.ToString(dbTable.Rows[0]["SMRClosing"]);
            txt_GROpening.Text = Convert.ToString(dbTable.Rows[0]["GRClosing"]);
            txt_GHMROpening.Text = Convert.ToString(dbTable.Rows[0]["GHMRClosing"]);
            txt_DHMROpening.Text = Convert.ToString(dbTable.Rows[0]["DHMRClosing"]);
            txt_DROpeningStock.Text = Convert.ToString(dbTable.Rows[0]["DRClosingStock"]);
        }
    }
    protected void ClearControls()
    {
        txt_DHMRClosing.Text = "";
        txt_DHMROpening.Text = "";
        txt_DHMRRunhr.Text = "";
        txt_DRActualCPH.Text = "";
        txt_DRClosingStock.Text = "";
        txt_DRDieselComsumptionDeviation.Text = "";
        txt_DRDieselConsumed.Text = "";
        txt_DRDieselFilled.Text = "";
        txt_DROpeningStock.Text = "";
        txt_GHMRClosing.Text = "";
        txt_GHMROpening.Text = "";
        txt_GHMRRunhr.Text = "";
        txt_SMROpening.Text = "";
        txt_SMRUnitConsumed.Text = "";
        txt_SMRClosing.Text = "";
    }
}