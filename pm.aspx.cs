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
using System.IO;
using System.Net.Mail;

public partial class pm : System.Web.UI.Page
{
    Common objCommon = new Common();
    clsInventory objInventory = new clsInventory();
    ClsAssetregister objAR = new ClsAssetregister();
    PMClass objPM = new PMClass();
    public string ImgFilePath, ImgName = "";
    double lat2, long2;
    DataTable dbTable = new DataTable();
    DataTable dbInv = new DataTable();
    byte[] raw;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objPM.Ddlst_PMMaster(ddlst_PMMaster);
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
        HtmlControl li = (HtmlGenericControl)Page.Master.FindControl("lipm");
        li.Attributes.Add("class", "active");
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
    protected void btn_SelectSite_Click(object sender, EventArgs e)
    {
        try
        {
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
                    ddlst_PMMaster.Enabled = false;
                    PMImg1.Visible = false;
                    PMImg2.Visible = false;
                    PMImg3.Visible = false;
                    btn_SaveDetails.Visible = false;
                    btn_Reset.Visible = false;
                }
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Your location services may be not active please check browser settings');", true);
            }
        }
        catch (Exception ex)
        {
            Common.LogException(ex.Message, "Select site", "PM page Issue while selection of site");
        }
    }
    protected void btn_SaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            int InvID = Convert.ToInt32(InventoryID.Value);
            DataTable dt = new DataTable();
            dt.Columns.Add("PM_Desc_ID", typeof(int));
            dt.Columns.Add("PM_ID", typeof(int));
            dt.Columns.Add("InventoryID", typeof(int));
            dt.Columns.Add("Frequency", typeof(string));
            dt.Columns.Add("PMDate", typeof(DateTime));
            dt.Columns.Add("NextPMDate", typeof(DateTime));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("StatusRemarks", typeof(string));
            dt.Columns.Add("UpdatedBy", typeof(int));
            dt.Columns.Add("UpdateDate", typeof(DateTime));
            Label lbl_PMDescID = new Label();
            Label lbl_PMID = new Label();
            int Img;
            DropDownList ddlst_Frequency = new DropDownList();
            DropDownList ddlst_Status = new DropDownList();
            TextBox txt_StatusRemarks = new TextBox();
            if (objPM.GetPMDescExist(Convert.ToInt32(ddlst_PMMaster.SelectedValue)) == objPM.GetPMTransExist(Convert.ToInt32(InventoryID.Value), Convert.ToInt32(ddlst_PMMaster.SelectedValue)))
            {
                //Update PM for selected Inventory            
                foreach (GridViewRow row in grdview_PMDetails.Rows)
                {
                    lbl_PMDescID = (Label)row.Cells[0].FindControl("lbl_PMDescID");
                    int PM_Desc_ID = Convert.ToInt32(lbl_PMDescID.Text);
                    lbl_PMID = (Label)row.Cells[1].FindControl("lbl_PMID");
                    int PM_ID = Convert.ToInt32(lbl_PMID.Text);
                    int IV_ID = Convert.ToInt32(InvID);
                    ddlst_Frequency = (DropDownList)row.Cells[4].FindControl("ddlst_PMFrequency");
                    string Frequency = Convert.ToString(ddlst_Frequency.SelectedValue);
                    DateTime dtPMDate = DateTime.Now;
                    DateTime PMNextDate = DateTime.Now;
                    if (Convert.ToString(ddlst_Frequency.SelectedValue) == "MONTHLY")
                    {
                        PMNextDate = PMNextDate.AddDays(30);
                    }
                    else if (Convert.ToString(ddlst_Frequency.SelectedValue) == "WEEKLY")
                    {
                        PMNextDate = PMNextDate.AddDays(7);
                    }
                    else if (Convert.ToString(ddlst_Frequency.SelectedValue) == "QUATERLY")
                    {
                        PMNextDate = PMNextDate.AddDays(90);
                    }
                    else if (Convert.ToString(ddlst_Frequency.SelectedValue) == "ANNUALY")
                    {
                        PMNextDate = PMNextDate.AddDays(365);
                    }
                    ddlst_Status = (DropDownList)row.Cells[5].FindControl("ddlst_PMStatus");
                    string PM_Status = Convert.ToString(ddlst_Status.SelectedValue);
                    txt_StatusRemarks = (TextBox)row.Cells[6].FindControl("txt_StatusDescription");
                    string PM_StatusRemarks = Convert.ToString(txt_StatusRemarks.Text);
                    int UpdatedBy = Convert.ToInt32(Session["UsrID"]);
                    DateTime dtUpdateDate = DateTime.Now;
                    dt.Rows.Add(PM_Desc_ID, PM_ID, IV_ID, Frequency, dtPMDate, PMNextDate, PM_Status, PM_StatusRemarks, UpdatedBy, dtUpdateDate);
                }
                objPM.UpdatePM(dt);
                if (PMImg1.PostedFile != null && !string.IsNullOrEmpty(PMImg1.PostedFile.FileName))
                {
                    //Img = PMImg1.PostedFile.ContentLength;
                    //byte[] msdata = new byte[Img];
                    //PMImg1.PostedFile.InputStream.Read(msdata, 0, Img);
                    string extension = Path.GetExtension(PMImg1.PostedFile.FileName);
                    if (((extension == ".jpg") || (extension == ".JPG") || ((extension == ".gif") || (extension == ".png"))))
                    {
                        PMImg1.SaveAs(Server.MapPath(ImgFilePath + PMImg1.PostedFile.FileName));
                        FileStream fs = new FileStream(Server.MapPath(ImgFilePath + PMImg1.PostedFile.FileName), FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        raw = new byte[fs.Length];
                        fs.Read(raw, 0, Convert.ToInt32(fs.Length));
                    }
                    objPM.PM_ID = Convert.ToInt32(ddlst_PMMaster.SelectedValue);
                    objPM.InventoryID = Convert.ToInt32(InventoryID.Value);
                    objPM.img = raw;
                    objPM.SavePMImage();
                }
                if (PMImg2.PostedFile != null && !string.IsNullOrEmpty(PMImg2.PostedFile.FileName))
                {
                    //Img = PMImg2.PostedFile.ContentLength;
                    //byte[] msdata = new byte[Img];
                    //PMImg1.PostedFile.InputStream.Read(msdata, 0, Img);
                    string extension = Path.GetExtension(PMImg2.PostedFile.FileName);
                    if (((extension == ".jpg") || (extension == ".JPG") || ((extension == ".gif") || (extension == ".png"))))
                    {
                        PMImg2.SaveAs(Server.MapPath(ImgFilePath + PMImg2.PostedFile.FileName));
                        FileStream fs = new FileStream(Server.MapPath(ImgFilePath + PMImg2.PostedFile.FileName), FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        raw = new byte[fs.Length];
                        fs.Read(raw, 0, Convert.ToInt32(fs.Length));
                    }
                    objPM.PM_ID = Convert.ToInt32(ddlst_PMMaster.SelectedValue);
                    objPM.InventoryID = Convert.ToInt32(InventoryID.Value);
                    objPM.img = raw;
                    objPM.SavePMImage();
                }
                if (PMImg3.PostedFile != null && !string.IsNullOrEmpty(PMImg3.PostedFile.FileName))
                {
                    //Img = PMImg3.PostedFile.ContentLength;
                    //byte[] msdata = new byte[Img];
                    //PMImg1.PostedFile.InputStream.Read(msdata, 0, Img);
                    string extension = Path.GetExtension(PMImg3.PostedFile.FileName);
                    if (((extension == ".jpg") || (extension == ".JPG") || ((extension == ".gif") || (extension == ".png"))))
                    {
                        PMImg3.SaveAs(Server.MapPath(ImgFilePath + PMImg3.PostedFile.FileName));
                        FileStream fs = new FileStream(Server.MapPath(ImgFilePath + PMImg3.PostedFile.FileName), FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        raw = new byte[fs.Length];
                        fs.Read(raw, 0, Convert.ToInt32(fs.Length));
                    }
                    objPM.PM_ID = Convert.ToInt32(ddlst_PMMaster.SelectedValue);
                    objPM.InventoryID = Convert.ToInt32(InventoryID.Value);
                    objPM.img = raw;
                    objPM.SavePMImage();
                }
                objInventory.intInventoryId = Convert.ToInt32(InventoryID.Value);
                dbInv = objInventory.GetSpecificSite();
                if (dbInv.Rows.Count > 0)
                {
                    string body = "<html><body><table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='4'><b>Preventive Maintenance Update Done</b></td> " +
                            " </tr> <tr> <td>Site ID</td> <td>Site Name</td> <td>Asset Type</td>  </tr> <tr>" +
                            " <td>" + Convert.ToString(dbInv.Rows[0]["SiteID"]) + "</td> " +
                            " <td>" + Convert.ToString(dbInv.Rows[0]["SiteName"]) + "</td> " +
                            " <td>" + Convert.ToString(ddlst_PMMaster.SelectedItem) + "</td> </tr></table>" +
                            "<hr>" +
                            "" +
                            "</body></html>";
                    objCommon.SendHtmlFormattedEmail(objCommon.GetMailerList(), "Preventive maintenance update done for Site ID:- " + Convert.ToString(dbInv.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbInv.Rows[0]["SiteName"]), body);
                    Common.AddProcessLog("PM Updated for Site ID :- " + Convert.ToString(dbInv.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbInv.Rows[0]["SiteName"]) + "", Convert.ToInt32(Session["UsrID"]));
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Preventive maintenance updated succesfully');", true);
                }
            }
            else
            {
                // Insert PM for selected Inventory
                foreach (GridViewRow row in grdview_PMDetails.Rows)
                {
                    lbl_PMDescID = (Label)row.Cells[0].FindControl("lbl_PMDescID");
                    int PM_Desc_ID = Convert.ToInt32(lbl_PMDescID.Text);
                    lbl_PMID = (Label)row.Cells[1].FindControl("lbl_PMID");
                    int PM_ID = Convert.ToInt32(lbl_PMID.Text);
                    int IV_ID = Convert.ToInt32(InvID);
                    ddlst_Frequency = (DropDownList)row.Cells[4].FindControl("ddlst_PMFrequency");
                    string Frequency = Convert.ToString(ddlst_Frequency.SelectedValue);
                    DateTime dtPMDate = DateTime.Now;
                    DateTime PMNextDate = DateTime.Now;
                    if (Convert.ToString(ddlst_Frequency.SelectedValue) == "MONTHLY")
                    {
                        PMNextDate = PMNextDate.AddDays(30);
                    }
                    else if (Convert.ToString(ddlst_Frequency.SelectedValue) == "WEEKLY")
                    {
                        PMNextDate = PMNextDate.AddDays(7);
                    }
                    else if (Convert.ToString(ddlst_Frequency.SelectedValue) == "QUATERLY")
                    {
                        PMNextDate = PMNextDate.AddDays(90);
                    }
                    else if (Convert.ToString(ddlst_Frequency.SelectedValue) == "ANNUALY")
                    {
                        PMNextDate = PMNextDate.AddDays(365);
                    }
                    ddlst_Status = (DropDownList)row.Cells[5].FindControl("ddlst_PMStatus");
                    string PM_Status = Convert.ToString(ddlst_Status.SelectedValue);
                    txt_StatusRemarks = (TextBox)row.Cells[6].FindControl("txt_StatusDescription");
                    string PM_StatusRemarks = Convert.ToString(txt_StatusRemarks.Text);
                    int UpdatedBy = Convert.ToInt32(Session["UsrID"]);
                    DateTime dtUpdateDate = DateTime.Now;
                    dt.Rows.Add(PM_Desc_ID, PM_ID, IV_ID, Frequency, dtPMDate, PMNextDate, PM_Status, PM_StatusRemarks, UpdatedBy, dtUpdateDate);
                }
                objPM.SavePM(dt);
                if (PMImg1.PostedFile != null && !string.IsNullOrEmpty(PMImg1.PostedFile.FileName))
                {
                    Img = PMImg1.PostedFile.ContentLength;
                    byte[] msdata = new byte[Img];
                    PMImg1.PostedFile.InputStream.Read(msdata, 0, Img);
                    objPM.PM_ID = Convert.ToInt32(ddlst_PMMaster.SelectedValue);
                    objPM.InventoryID = Convert.ToInt32(InventoryID.Value);
                    objPM.img = msdata;
                    objPM.SavePMImage();
                }
                if (PMImg2.PostedFile != null && !string.IsNullOrEmpty(PMImg2.PostedFile.FileName))
                {
                    Img = PMImg2.PostedFile.ContentLength;
                    byte[] msdata = new byte[Img];
                    PMImg1.PostedFile.InputStream.Read(msdata, 0, Img);
                    objPM.PM_ID = Convert.ToInt32(ddlst_PMMaster.SelectedValue);
                    objPM.InventoryID = Convert.ToInt32(InventoryID.Value);
                    objPM.img = msdata;
                    objPM.SavePMImage();
                }
                if (PMImg3.PostedFile != null && !string.IsNullOrEmpty(PMImg3.PostedFile.FileName))
                {
                    Img = PMImg3.PostedFile.ContentLength;
                    byte[] msdata = new byte[Img];
                    PMImg1.PostedFile.InputStream.Read(msdata, 0, Img);
                    objPM.PM_ID = Convert.ToInt32(ddlst_PMMaster.SelectedValue);
                    objPM.InventoryID = Convert.ToInt32(InventoryID.Value);
                    objPM.img = msdata;
                    objPM.SavePMImage();
                }
                objInventory.intInventoryId = Convert.ToInt32(InventoryID.Value);
                dbInv = objInventory.GetSpecificSite();
                if (dbInv.Rows.Count > 0)
                {
                    string body = "<html><body><table border='1' style='border-collapse: collapse;' cellpadding='2'> <tr> <td colspan='4'><b>New Preventive Maintenance Done</b></td> " +
                            " </tr> <tr> <td>Site ID</td> <td>Site Name</td> <td>Asset Type</td>  </tr> <tr>" +
                            " <td>" + Convert.ToString(dbInv.Rows[0]["SiteID"]) + "</td> " +
                            " <td>" + Convert.ToString(dbInv.Rows[0]["SiteName"]) + "</td> " +
                            " <td>" + Convert.ToString(ddlst_PMMaster.SelectedItem) + "</td> </tr></table>" +
                            "<hr>" +
                            "" +
                            "</body></html>";
                    objCommon.SendHtmlFormattedEmail(objCommon.GetMailerList(), "Preventive Maintenance done for Site ID:- " + Convert.ToString(dbInv.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbInv.Rows[0]["SiteName"]), body);
                    Common.AddProcessLog("New PM Done for Site ID :- " + Convert.ToString(dbInv.Rows[0]["SiteID"]) + "/ Site Name:- " + Convert.ToString(dbInv.Rows[0]["SiteName"]) + "", Convert.ToInt32(Session["UsrID"]));
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Preventive maintenance saved succesfully');", true);
                }
                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Preventive maintenance saved succesfully');", true);
            }
        }
        catch (Exception ex) 
        {
            Common.LogException(ex.Message, "btn_SaveDetails_Click", "PM Page Wise Insert"); 
        }
    }
    protected void btn_Reset_Click(object sender, EventArgs e)
    {

    }
    protected void ddlst_PMMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid(Convert.ToInt32(ddlst_PMMaster.SelectedValue));
    }
    protected void grdview_PMDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdview_PMDetails.PageIndex = e.NewPageIndex;
        BindGrid(Convert.ToInt32(ddlst_PMMaster.SelectedValue));
    }
    private void BindGrid(int PMID)
    {
        grdview_PMDetails.DataSource = objPM.GetPM(PMID);
        grdview_PMDetails.DataBind();
    }
    private void UploadImage()
    {
        //if (FileUpload1.PostedFile != null && !string.IsNullOrEmpty(FileUpload1.PostedFile.FileName))
        //{
        //    //create byte array with size corresponding to the currently selected file
        //    byte[] imgBin = new byte[FileUpload1.PostedFile.ContentLength];
        //    //store the currently selected file in memory
        //    HttpPostedFile img = FileUpload1.PostedFile;
        //    //store the image binary data of the selected file in the imgBin byte array
        //    img.InputStream.Read(imgBin, 0, (int)FileUpload1.PostedFile.ContentLength);

        //    //connect to the db
        //    //SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        //    //sql command to send all of our img data to the db
        //    //SqlCommand cmd = new SqlCommand("INSERT INTO Images(ImgBin, ImgType, ImgSize) VALUES (@ImgBin, @ImgType, @ImgSize)", conn);
        //    //cmd.CommandType = CommandType.Text;

        //    //add the image binary data to the sql command
        //    //cmd.Parameters.Add("@ImgBin", SqlDbType.Image, imgBin.Length).Value = imgBin;
        //    //add the image type to the sql command
        //    //cmd.Parameters.AddWithValue("@ImgType", FileUpload1.PostedFile.ContentType);
        //    //add the image size to the sql command
        //    //cmd.Parameters.AddWithValue("@ImgSize", FileUpload1.PostedFile.ContentLength);

        //    using (conn)
        //    {
        //        //open the connection
        //        conn.Open();
        //        //send the sql query to store the data
        //        cmd.ExecuteNonQuery();
        //    }
        //}

    }


}