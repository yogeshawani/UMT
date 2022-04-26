<%@ Page Title="UMT : Fuel Details" Language="C#" MasterPageFile="~/Main.master"
    AutoEventWireup="true" CodeFile="FuelDetails.aspx.cs" Inherits="FuelDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/FieldsetStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id$=txt_SiteID]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("FuelDetails.aspx/GetSites") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1],
                                    sval: item.split('-')[2]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=InventoryID]").val(i.item.val);
                    $("[id$=txt_SiteName]").val(i.item.sval);
                },
                minLength: 1
            });
        });
    </script>
    <script type="text/javascript">
        function showPosition() {
            if (navigator.geolocation) {
                var location_timeout = setTimeout("geolocFail()", 10000);
                navigator.geolocation.getCurrentPosition(function (position) {
                    clearTimeout(location_timeout);
                    $("[id$=latitudeI]").val(position.coords.latitude);
                    $("[id$=longitudeId]").val(position.coords.longitude);
                });
            } else {
                alert("Sorry, your browser does not support HTML5 geolocation.");
            }
        }
        document.addEventListener("DOMContentLoaded", function (event) {
            showPosition();
        });

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
        <div class="row">
            <div class="col-sm-12 maincontent">
                <header class="page-header">
                    <h3 class="page-title">Fuel Details</h3>
                </header>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:RequiredFieldValidator ID="rqfv_CircleSelect" runat="server" InitialValue="0"
                                        ForeColor="red" Display="Dynamic" ErrorMessage="Please Select Circle" ControlToValidate="ddlst_circle"
                                        Text="*" ValidationGroup="SiteSelect"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlst_circle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlst_circle_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="-- Select Circle --">-- Select Circle --</asp:ListItem>
                                        <asp:ListItem Value="1" Text="Mumbai">Mumbai</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="red" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Select Site" ControlToValidate="txt_SiteID"
                                    Text="*" ValidationGroup="SiteSelect"></asp:RequiredFieldValidator>
                                <div class="form-group">
                                    <asp:TextBox ID="txt_SiteID" runat="server" CssClass="form-control" Placeholder="Enter Site ID"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txt_SiteName" runat="server" CssClass="form-control" Placeholder="Site Name"
                                        Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="SiteSelect" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" runat="server" />
                                <div class="form-group">
                                    <asp:HiddenField ID="InventoryID" runat="server" />
                                    <asp:HiddenField ID="latitudeI" runat="server" />
                                    <asp:HiddenField ID="longitudeId" runat="server" />
                                    <asp:Button ID="btn_SelectSite" runat="server" CssClass="btn btn-primary" Text="Select Site"
                                        ValidationGroup="SiteSelect" OnClick="btn_SelectSite_Click" />
                                    <asp:Button ID="btn_ViewReport" runat="server" PostBackUrl="~/FMTReport.aspx" CssClass="btn btn-success" Text="View Fuel Report" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <h5>
                                            <asp:Label ID="lbl_SiteID" runat="server"></asp:Label>
                                        </h5>
                                    </div>
                                    <div class="col-sm-6">
                                        <h5>
                                            <asp:Label ID="lbl_SiteName" runat="server"></asp:Label></h5>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">SEB Meter Reading</legend>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            Opening
                                                        </label>
                                                        <asp:TextBox ID="txt_SMROpening" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            Closing</label>
                                                        <asp:TextBox ID="txt_SMRClosing" runat="server" CssClass="form-control" OnTextChanged="txt_SMRClosing_TextChanged"
                                                            AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            Unit Consumed</label>
                                                        <asp:TextBox ID="txt_SMRUnitConsumed" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="col-sm-6">
                                        <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">GCU kWH Reading</legend>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            Opening
                                                        </label>
                                                        <asp:TextBox ID="txt_GROpening" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            Closing
                                                        </label>
                                                        <asp:TextBox ID="txt_GRClosing" runat="server" CssClass="form-control" OnTextChanged="txt_GRClosing_TextChanged"
                                                            AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            Unit Consumed</label>
                                                        <asp:TextBox ID="txt_GRUnitConsumed" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">GCU Hour Meter Reading</legend>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            Opening
                                                        </label>
                                                        <asp:TextBox ID="txt_GHMROpening" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            Closing</label>
                                                        <asp:TextBox ID="txt_GHMRClosing" runat="server" CssClass="form-control" OnTextChanged="txt_GHMRClosing_TextChanged"
                                                            AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            <small>Total DG Run Hrs</small></label>
                                                        <asp:TextBox ID="txt_GHMRRunhr" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="col-sm-6">
                                        <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">DG Hour Meter Reading</legend>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            Opening (A)
                                                        </label>
                                                        <asp:TextBox ID="txt_DHMROpening" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            Closing (B)</label>
                                                        <asp:TextBox ID="txt_DHMRClosing" runat="server" CssClass="form-control" OnTextChanged="txt_DHMRClosing_TextChanged"
                                                            AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            <small>Total DG Run Hrs</small></label>
                                                        <asp:TextBox ID="txt_DHMRRunhr" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">Diesel Record</legend>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            Opening Stock
                                                        </label>
                                                        <asp:TextBox ID="txt_DROpeningStock" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            Closing Stock
                                                        </label>
                                                        <asp:TextBox ID="txt_DRClosingStock" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            Diesel Filled</label>
                                                        <asp:TextBox ID="txt_DRDieselFilled" runat="server" CssClass="form-control" OnTextChanged="txt_DRDieselFilled_TextChanged"
                                                            AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            <small style="font-size: 10px !important;">Diesel Consumed During Month</small>
                                                        </label>
                                                        <asp:TextBox ID="txt_DRDieselConsumed" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            Actual CPH
                                                        </label>
                                                        <asp:TextBox ID="txt_DRActualCPH" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group">
                                                        <label for="circle">
                                                            <small style="font-size: 10px !important;">Diesel Comsumption Deviation</small></label>
                                                        <asp:TextBox ID="txt_DRDieselComsumptionDeviation" Enabled="false" runat="server"
                                                            CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <asp:Button ID="btn_SaveDetails" runat="server" Text="Save Details" CssClass="btn btn-primary"
                                                OnClick="btn_SaveDetails_Click" />
                                            <asp:Button ID="btn_ResetDetails" runat="server" Text="Reset" CssClass="btn btn-warning" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </ContentTemplate> </asp:UpdatePanel>
            </div>
        </div>
    </div>
    
    </div>
</asp:Content>
