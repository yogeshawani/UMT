using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using UMT;
using System.Data;
using System.Net.Mail;

public partial class seb : System.Web.UI.Page
{
    Common objCommon = new Common();
    clsSEB objSEB = new clsSEB();
    ClsAssetregister objAR = new ClsAssetregister();
    clsInventory objInventory = new clsInventory();
    DataTable dbTable = new DataTable();
    DataTable dbInv = new DataTable();
    string result = "";
    double lat2, long2;

    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlControl li = (HtmlGenericControl)Page.Master.FindControl("liseb");
        li.Attributes.Add("class", "active");
        if (!Page.IsPostBack)
        {
            objCommon.BindDropdowns(ddlst_circle);
            if (Session["SebID"] != null)
                GetSebDetails(Session["SebID"].ToString());
        }

    }

    private void GetSebDetails(string SebID)
    {
        objSEB.GetSebDetails(SebID);
        hdn_SebID.Value = objSEB.ID.ToString();
        //txt_.Text = objSEB.INVENTORY_ID;
        txt_ConsumerNo.Text = objSEB.CONSUMER_NO;
        txt_Customer.Text = objSEB.CUSTOMER_NAME;
        txt_EBConnectedDate.Text = objSEB.EB_CONNECTED_DATE.ToString("dd-MM-yyyy");
        txt_MeterNo.Text = objSEB.METER_NO;
        txt_NoOfOperator.Text = objSEB.NO_OF_OPERATOR.ToString();
        txt_InterestEarnedOnDeposite.Text = objSEB.INTEREST_EARNED_ON_DEPOSITE.ToString();
        ddlst_circle.SelectedItem.Text = objSEB.CIRCLE_NAME;
        //txt_.Text = objSEB.MEPT;
        txt_SiteID.Text = objSEB.SITE_ID;
        txt_SiteName.Text = objSEB.SITE_NAME;
        //txt_.Text = objSEB.FAC_ID;
        //txt_.Text = objSEB.SITE_TYPE;
        ddlst_Month.SelectedItem.Text = objSEB.MONTH;
        ddlst_Year.SelectedItem.Text = objSEB.YEAR.ToString();
        txt_SEBStatus.Text = objSEB.SEB_STATUS;
        txt_ProjectedBillCollection.Text = objSEB.PROJECTED_BILL_COLLECTION;
        txt_BillDate.Text = objSEB.ACTUAL_BILL_COLLECTED_DATE.ToString("dd-MM-yyyy");
        //txt_billn.Text = objSEB.BILL_NO;
        //txt_amo.Text = objSEB.AMOUNT_PAYABLE_AFTER_DUE_DATE;
        txt_UnitType.Text = objSEB.UNIT_TYPE;
        //txt_su.Text = objSEB.SURCHARGE;
        //txt_.Text = objSEB.SUNDRY_CHARGES;
        txt_PaymentMode.Text = objSEB.PREFERRED_PAYMENT_MODE;
        txt_ConnectionCategory.Text = objSEB.CONNECTION_CATEGORY;
        ddlst_BillingCycle.SelectedItem.Text = objSEB.BILLING_CYCLE;
        txt_BillDueDate.Text = objSEB.BILL_DUE_DATE.ToString("dd-MM-yyyy");
        txt_RevisedBillDueDate.Text = objSEB.REVISED_BILL_DUE_DATE.ToString("dd-MM-yyyy");
        txt_BillType.Text = objSEB.BILL_TYPE;
        txt_BillFromDate.Text = objSEB.BILL_FROM_DATE.ToString("dd-MM-yyyy");
        txt_BillToDate.Text = objSEB.BILL_TO_DATE.ToString("dd-MM-yyyy");
        txt_CurrentReading.Text = objSEB.CURRENT_READING;
        txt_PrevStartingReading.Text = objSEB.PREVIOUS_READING;
        txt_TotalUnitsConsumed.Text = objSEB.TOTAL_UNITS.ToString();
        txt_TotalBillAmount.Text = objSEB.TOTAL_BILL_AMOUNT.ToString();
        //txt_pe.Text = objSEB.PENALTY;
        txt_AdjustmentAmount.Text = objSEB.ADJUSTMENT_AMOUNT.ToString();
        txt_CurrentMonthAmount.Text = objSEB.CURRENT_MONTH_AMOUNT.ToString();
        txt_ArrearAmount.Text = objSEB.ARREAR_AMOUNT.ToString();
        txt_AreaManagerRemarks.Text = objSEB.AREA_MANAGER_REMARKS;
        txt_AreaManagerApproval.Text = objSEB.AREA_MANAGER_APPROVAL;
        txt_DisconnectedDueToNonPayment.Text = objSEB.DISCONNECTED_DUE_TO_NONPAYMENT;
        txt_ReconnectionDate.Text = objSEB.RECONNECTION_DATE.ToString("dd-MM-yyyy");
        txt_ReconnectionChargesPaid.Text = objSEB.RECONNECTION_CHARGES_PAID.ToString();
        txt_PaymentSubmissionDateByOSE.Text = objSEB.PAYMENTSUBMISSION_DATE_BY_OSE.ToString("dd-MM-yyyy");
        txt_PaymentInstRec.Text = objSEB.PAYMENT_SUBMISSION_RECEIPT_FROM_ELECTRICITY_BOARD;
        txt_ReciptNo.Text = objSEB.RECEIPT_NO;
        txt_PnfSMERemarks.Text = objSEB.P_F_SME_REMARK;
        txt_FinalRemarks.Text = objSEB.FINAL_REMARK;
        txt_UnProtestAmount.Text = objSEB.UNDER_PROTEST_AMOUNT.ToString();
        txt_PaymentMode.Text = objSEB.PAYMENT_MODE;
        txt_DDRTGSNo.Text = objSEB.DD_NO_RTGS_NO;
        txt_FinalPaymentReceiptAmount.Text = objSEB.FINAL_PAYMENT_RECEIPT_AMOUNT.ToString();
        txt_BillSubmittedDate.Text = objSEB.BILL_SUBMITTED_DATE_TO_RCOM.ToString("dd-MM-yyyy");
        txt_PaymentInstRec.Text = objSEB.PAYMENT_INSTRUMENT_RECD_DATE_FROM_RCOM_TO_ERICSSON.ToString("dd-MM-yyyy");
        txt_FinAmount.Text = objSEB.FINAL_AMOUNT.ToString();

        if (objSEB.IsFinalSubmit)
        {
            btn_SebSaveDetails.Visible = false;
            btn_SebFinalSubmit.Visible = false;
        }
    }

    protected void ddlst_circle_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dbTable = new DataTable();
        if (ddlst_circle.SelectedValue != null && ddlst_circle.SelectedValue != "")
        {
            dbTable = objCommon.GetCircleWiseSites(Convert.ToInt32(ddlst_circle.SelectedValue));
            Session["AutoCompleteSite"] = dbTable;
        }
    }
    protected void btn_SebSaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            objSEB.IsFinalSubmit = false;
            SaveSEBDetails();
        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "btn_SaveDGDetails", "Page Level Insert");
        }

    }
    protected void btn_SebFinalSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objSEB.IsFinalSubmit = true;
            SaveSEBDetails();
            objInventory.intInventoryId = Convert.ToInt32(InventoryID.Value);
            dbInv = objInventory.GetSpecificSite();
            if (dbInv.Rows.Count > 0)
            {
                string body = "<html><body><table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='4'><b>SEB Details</b></td> " +
                        " </tr> <tr> <td>Site ID</td> <td>Site Name</td> </tr> " +
                        "<tr> <td>" + Convert.ToString(dbInv.Rows[0]["SiteID"]) + "</td> " +
                        " <td>" + Convert.ToString(dbInv.Rows[0]["SiteName"]) + "</td> </tr></table>" +
                        "<hr>" +
                        "" +
                        "</body></html>";
                List<MailAddress> lst = new List<MailAddress>();
                lst.Add(new MailAddress("vishal.wable17@gmail.com"));
                lst.Add(new MailAddress("subhenduraj@hotmail.com"));
                lst.Add(new MailAddress("yaw.itengnr@gmail.com"));
                lst.Add(new MailAddress("mrb@auropower.in"));
                lst.Add(new MailAddress("nagaraja.venka@auropower.in"));
                objCommon.SendHtmlFormattedEmail(lst, "SEB details for Site ID:- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]), body);
                Common.AddProcessLog("SEB details added for Site ID :- " + Convert.ToString(dbTable.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbTable.Rows[0]["SiteName"]) + "", Convert.ToInt32(Session["UsrID"]));                
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('SEB details final submit done sucessfully');", true);
            }
        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "btn_SaveDGDetails", "Page Level Insert");
        }

    }
    protected void btn_SebResetDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ClearSEBDetails();
        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "btn_SaveDGDetails", "Page Level Insert");
        }

    }
    protected void btn_SelectSite_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();        
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
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Location Match Found at " + Lattitude + "-" + Longitude + "');", true);                         
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Location Match Not  Found at " + Lattitude + "-" + Longitude + "');", true);
                UpdatePanel1.Visible = false;
            }
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Your location services may be not active please check browser settings');", true);
            UpdatePanel1.Visible = false;
        }
    }
    private void SaveSEBDetails()
    {
        objSEB.dtDefaultDt = Convert.ToDateTime("01/01/1900");
        objSEB.ID = Convert.ToInt32(hdn_SebID.Value);
        objSEB.CIRCLE_NAME = ddlst_circle.SelectedItem.Text;
        objSEB.MEPT = "";
        objSEB.SITE_ID = txt_SiteID.Text;
        objSEB.SITE_NAME = txt_SiteName.Text;
        objSEB.FAC_ID = "";
        objSEB.SITE_TYPE = "";

        objSEB.INVENTORY_ID = 0;
        objSEB.CONSUMER_NO = txt_ConsumerNo.Text;
        objSEB.CUSTOMER_NAME = txt_Customer.Text;
        objSEB.EB_CONNECTED_DATE = string.IsNullOrEmpty(txt_EBConnectedDate.Text) ? objSEB.dtDefaultDt : Convert.ToDateTime(txt_EBConnectedDate.Text);
        objSEB.METER_NO = txt_MeterNo.Text;
        objSEB.NO_OF_OPERATOR = Convert.ToInt32(txt_NoOfOperator.Text);
        objSEB.INTEREST_EARNED_ON_DEPOSITE = Convert.ToDouble(txt_InterestEarnedOnDeposite.Text);

        objSEB.MONTH = ddlst_Month.SelectedItem.Text;
        objSEB.YEAR = Convert.ToInt32(ddlst_Year.SelectedItem.Text);
        objSEB.SEB_STATUS = txt_SEBStatus.Text;
        objSEB.PROJECTED_BILL_COLLECTION = txt_ProjectedBillCollection.Text;
        objSEB.ACTUAL_BILL_COLLECTED_DATE = string.IsNullOrEmpty(txt_BillDate.Text) ? objSEB.dtDefaultDt : Convert.ToDateTime(txt_BillDate.Text);
        objSEB.BILL_NO = "";
        objSEB.AMOUNT_PAYABLE_AFTER_DUE_DATE = 0;
        objSEB.UNIT_TYPE = txt_UnitType.Text;
        objSEB.SURCHARGE = 0;
        objSEB.SUNDRY_CHARGES = 0;
        objSEB.PREFERRED_PAYMENT_MODE = txt_PaymentMode.Text;
        objSEB.CONNECTION_CATEGORY = txt_ConnectionCategory.Text;
        objSEB.BILLING_CYCLE = ddlst_BillingCycle.SelectedItem.Text;
        objSEB.BILL_DUE_DATE = string.IsNullOrEmpty(txt_BillDueDate.Text) ? objSEB.dtDefaultDt : Convert.ToDateTime(txt_BillDueDate.Text);
        objSEB.REVISED_BILL_DUE_DATE = string.IsNullOrEmpty(txt_RevisedBillDueDate.Text) ? objSEB.dtDefaultDt : Convert.ToDateTime(txt_RevisedBillDueDate.Text);
        objSEB.BILL_TYPE = txt_BillType.Text;
        objSEB.BILL_FROM_DATE = string.IsNullOrEmpty(txt_BillFromDate.Text) ? objSEB.dtDefaultDt : Convert.ToDateTime(txt_BillFromDate.Text);
        objSEB.BILL_TO_DATE = string.IsNullOrEmpty(txt_BillToDate.Text) ? objSEB.dtDefaultDt : Convert.ToDateTime(txt_BillToDate.Text);
        objSEB.CURRENT_READING = txt_CurrentReading.Text;
        objSEB.PREVIOUS_READING = txt_PrevStartingReading.Text;
        objSEB.TOTAL_UNITS = Convert.ToInt32(txt_TotalUnitsConsumed.Text);
        objSEB.TOTAL_BILL_AMOUNT = Convert.ToDouble(txt_TotalBillAmount.Text);
        objSEB.PENALTY = "";
        objSEB.ADJUSTMENT_AMOUNT = Convert.ToDouble(txt_AdjustmentAmount.Text);
        objSEB.CURRENT_MONTH_AMOUNT = Convert.ToDouble(txt_CurrentMonthAmount.Text);
        objSEB.ARREAR_AMOUNT = Convert.ToDouble(txt_ArrearAmount.Text);
        objSEB.AREA_MANAGER_REMARKS = txt_AreaManagerRemarks.Text;
        objSEB.AREA_MANAGER_APPROVAL = txt_AreaManagerApproval.Text;
        objSEB.DISCONNECTED_DUE_TO_NONPAYMENT = txt_DisconnectedDueToNonPayment.Text;
        objSEB.RECONNECTION_DATE = string.IsNullOrEmpty(txt_ReconnectionDate.Text) ? objSEB.dtDefaultDt : Convert.ToDateTime(txt_ReconnectionDate.Text);
        objSEB.RECONNECTION_CHARGES_PAID = Convert.ToDouble(txt_ReconnectionChargesPaid.Text);
        objSEB.PAYMENTSUBMISSION_DATE_BY_OSE = string.IsNullOrEmpty(txt_PaymentSubmissionDateByOSE.Text) ? objSEB.dtDefaultDt : Convert.ToDateTime(txt_PaymentSubmissionDateByOSE.Text);
        objSEB.PAYMENT_SUBMISSION_RECEIPT_FROM_ELECTRICITY_BOARD = txt_PayRecFromEB.Text;
        objSEB.RECEIPT_NO = txt_ReciptNo.Text;
        objSEB.P_F_SME_REMARK = txt_PnfSMERemarks.Text;
        objSEB.FINAL_REMARK = txt_FinalRemarks.Text;
        objSEB.UNDER_PROTEST_AMOUNT = Convert.ToDouble(txt_UnProtestAmount.Text);
        objSEB.PAYMENT_MODE = txt_PaymentMode.Text;
        objSEB.DD_NO_RTGS_NO = txt_DDRTGSNo.Text;
        objSEB.FINAL_PAYMENT_RECEIPT_AMOUNT = Convert.ToDouble(txt_FinalPaymentReceiptAmount.Text);
        objSEB.BILL_SUBMITTED_DATE_TO_RCOM = string.IsNullOrEmpty(txt_BillSubmittedDate.Text) ? objSEB.dtDefaultDt : Convert.ToDateTime(txt_BillSubmittedDate.Text);
        objSEB.PAYMENT_INSTRUMENT_RECD_DATE_FROM_RCOM_TO_ERICSSON = string.IsNullOrEmpty(txt_PaymentInstRec.Text) ? objSEB.dtDefaultDt : Convert.ToDateTime(txt_PaymentInstRec.Text);
        objSEB.FINAL_AMOUNT = Convert.ToDouble(txt_FinAmount.Text);

        result = objSEB.InsertUpdateSEBDetails();
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + result + "');", true);
        Session["SebID"] = null;       
    }

    #region Clear Control
    public void ClearSEBDetails()
    {
        hdn_SebID.Value = "";
        //txt_.Text = objSEB.INVENTORY_ID;
        txt_ConsumerNo.Text = "";
        txt_Customer.Text = "";
        txt_EBConnectedDate.Text = "";
        txt_MeterNo.Text = "";
        txt_NoOfOperator.Text = "";
        txt_InterestEarnedOnDeposite.Text = "";
        ddlst_circle.SelectedItem.Text = "";
        //txt_.Text = objSEB.MEPT;
        txt_SiteID.Text = "";
        txt_SiteName.Text = "";
        //txt_.Text = objSEB.FAC_ID;
        //txt_.Text = objSEB.SITE_TYPE;
        ddlst_Month.SelectedItem.Text = "";
        ddlst_Year.SelectedItem.Text = "";
        txt_SEBStatus.Text = "";
        txt_ProjectedBillCollection.Text = "";
        txt_BillDate.Text = "";
        //txt_billn.Text = objSEB.BILL_NO;
        //txt_amo.Text = objSEB.AMOUNT_PAYABLE_AFTER_DUE_DATE;
        txt_UnitType.Text = "";
        //txt_su.Text = objSEB.SURCHARGE;
        //txt_.Text = objSEB.SUNDRY_CHARGES;
        txt_PaymentMode.Text = "";
        txt_ConnectionCategory.Text = "";
        ddlst_BillingCycle.SelectedItem.Text = "";
        txt_BillDueDate.Text = "";
        txt_RevisedBillDueDate.Text = "";
        txt_BillType.Text = "";
        txt_BillFromDate.Text = "";
        txt_BillToDate.Text = "";
        txt_CurrentReading.Text = "";
        txt_PrevStartingReading.Text = "";
        txt_TotalUnitsConsumed.Text = "";
        txt_TotalBillAmount.Text = "";
        //txt_pe.Text = objSEB.PENALTY;
        txt_AdjustmentAmount.Text = "";
        txt_CurrentMonthAmount.Text = "";
        txt_ArrearAmount.Text = "";
        txt_AreaManagerRemarks.Text = "";
        txt_AreaManagerApproval.Text = "";
        txt_DisconnectedDueToNonPayment.Text = "";
        txt_ReconnectionDate.Text = "";
        txt_ReconnectionChargesPaid.Text = "";
        txt_PaymentSubmissionDateByOSE.Text = "";
        txt_PaymentInstRec.Text = "";
        txt_ReciptNo.Text = "";
        txt_PnfSMERemarks.Text = "";
        txt_FinalRemarks.Text = "";
        txt_UnProtestAmount.Text = "";
        txt_PaymentMode.Text = "";
        txt_DDRTGSNo.Text = "";
        txt_FinalPaymentReceiptAmount.Text = "";
        txt_BillSubmittedDate.Text = "";
        txt_PaymentInstRec.Text = "";
        txt_FinAmount.Text = "";

    }
    #endregion
}