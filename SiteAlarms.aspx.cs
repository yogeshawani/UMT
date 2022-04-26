using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UMT;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Drawing;

public partial class SiteAlarms : System.Web.UI.Page
{
    Common objCommon = new Common();
    string HTML = "";
    DataTable dbTable = new DataTable();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlControl li = (HtmlGenericControl)Page.Master.FindControl("liSiteAlarms");
        li.Attributes.Add("class", "active");
        GenerateGrid(1, 1);        
    }
    protected void ddlst_Circle_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public void GenerateGrid(int intCircleID,int intUserId)
    {
        dbTable = objCommon.GetSitesForAlarm(intCircleID);
        HTML = "<table class='table table-responsive table-striped'>";
        HTML += "<thead class=\"thead-inverse\">";
        HTML += "<th>Site Name</th>" +
               "<th> Site ID </th>" +
               "<th> ACMF </th>" +
               "<th> BOL </th>" +
               "<th> DOL </th>" +
               "<th> PACA </th>" +
               "<th> Fire SMS </th>";
        HTML += "<th> Site Down</th>";
        HTML += "</thead>";

        for (int i = 0; i < dbTable.Rows.Count; i++)
        {
            HTML += "<tr>";
            HTML += "<td>" + dbTable.Rows[i]["SiteID"].ToString() + "</td>";
            HTML += "<td>" + dbTable.Rows[i]["SiteName"].ToString() + "</td>";
            HTML += "<td>";
            int A1 = Convert.ToInt32(dbTable.Rows[i]["A1"].ToString());
            if (A1 > 0)
            {
                HTML += "<div class='col-sm-1 blinking'> <img src='img\\Red_Circle.png'/></div>";
            }
            else
            {
                HTML += "<div class='col-sm-1'> <img src='img\\oval.png'/></div>";
            }
            HTML += "</td>";
            HTML += "<td>";
            int A2 = Convert.ToInt32(dbTable.Rows[i]["A2"].ToString());
            if (A2 > 0)
            {
                HTML += "<div class='col-sm-1 blinking'> <img src='img\\Red_Circle.png'/></div>";
            }
            else
            {
                HTML += "<div class='col-sm-1'> <img src='img\\oval.png'/></div>";
            }
            HTML += "</td>";
            HTML += "<td>";
            int A3 = Convert.ToInt32(dbTable.Rows[i]["A3"].ToString());
            if (A3 > 0)
            {
                HTML += "<div class='col-sm-1 blinking'> <img src='img\\Red_Circle.png'/></div>";
            }
            else
            {
                HTML += "<div class='col-sm-1'> <img src='img\\oval.png'/></div>";
            }
            HTML += "</td>";
            HTML += "<td>";
            int A4 = Convert.ToInt32(dbTable.Rows[i]["A4"].ToString());
            if (A4 > 0)
            {
                HTML += "<div class='col-sm-1 blinking'> <img src='img\\Red_Circle.png'/></div>";
            }
            else
            {
                HTML += "<div class='col-sm-1'> <img src='img\\oval.png'/></div>";
            }
            HTML += "</td>";
            HTML += "<td>";
            int A5 = Convert.ToInt32(dbTable.Rows[i]["A5"].ToString());
            if (A5 > 0)
            {
                HTML += "<div class='col-sm-1 blinking'> <img src='img\\Red_Circle.png'/></div>";
            }
            else
            {
                HTML += "<div class='col-sm-1'> <img src='img\\oval.png'/></div>";
            }
            HTML += "</td>";
            HTML += "<td><div class='col-sm-1'> <img src='img\\oval.png'/></div></td>";
            HTML += "</tr>";
        }
        HTML += "</table>";
        div_table.InnerHtml = HTML;
    }
}