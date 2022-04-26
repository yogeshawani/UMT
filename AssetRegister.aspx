<%@ Page Title="UMT : Asset Register" Language="C#" MasterPageFile="~/Main.master"
    AutoEventWireup="true" CodeFile="AssetRegister.aspx.cs" Inherits="AssetRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(function () {
            $("[id$=txt_SiteID]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("AssetRegister.aspx/GetSites") %>',
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
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
            function EndRequestHandler(sender, args) {
                $("[id$=txt_DGInstallationDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_AlternatorMfgDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_AlternatorCommisionDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_DGBatteryCommisionDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_DGAMFCommisionDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_DGGCUCommisionDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_DGMfgDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_PACManufacturingDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_PACInstallationDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_SMPSMfgDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_SMPSCommisionDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_SMPSLVDCommisionDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_SMPSControllerCommisionDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_BBMfgDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_BBCommisionDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_AVRInstalledDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_ShelterMfgDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_ShelterCommisionDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_FireSystemCommDate]").datepicker({ dateFormat: 'mm-dd-yy' });
                $("[id$=txt_CoolSysCommDate]").datepicker({ dateFormat: 'mm-dd-yy' });
            }
        });
    </script>
    <script type="text/javascript">
        function showPosition() {
            if (navigator.geolocation) {
                //var location_timeout = setTimeout("geolocFail()", 10000);
                navigator.geolocation.getCurrentPosition(function (position) {
                    //clearTimeout(location_timeout);
                    $("[id$=latitudeI]").val(position.coords.latitude);
                    $("[id$=longitudeId]").val(position.coords.longitude);
                    //var positionInfo = "Your current position is <br/>" + "<b>Latitude:<b/> " + position.coords.latitude + "<br/> " + "<b>Longitude:<b/> " + position.coords.longitude + "";
                    //document.getElementById("result").innerHTML = positionInfo;
                });
            } else {
                alert("Sorry, your browser does not support HTML5 geolocation.");
            }
        }


        document.addEventListener("DOMContentLoaded", function (event) {
            showPosition();
        });

    </script>
    <style type="text/css">
        #Background
        {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: 0px;
            background-color: Gray;
            z-index: 999; /*filter:alpha(opacity=40);*/
            opacity: 0.9;
        }
        
        #Progress
        {
            position: fixed;
            top: 40%;
            left: 35%;
            width: 400px;
            text-align: center;
            background-color: #efefef;
            padding: 15px;
            z-index: 1000;
            border: solid 1px #000;
            font-weight: 600;
        }
    </style>
    <div class="container">
        <div class="row">
            <div class="col-sm-12 maincontent">
                <div class="row">
                    <div class="col-sm-12">
                        <header class="page-header">
                <div class="row">
                <div class="col-sm-4">
                    <h3 class="page-title">Asset Register</h3>
                    </div>
                    <div class="col-sm-8">
                    <%--<div class="alert alert-warning alert-dismissible fade in page-title" role="alert"> <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>  </div>--%>
                <h5 class="page-title">
                <asp:Label ID="lbl_ErrorMessage" runat="server" CssClass="myalert"></asp:Label> 
                    </h5>
                    </div>
                    </div>                     
                </header>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <asp:RequiredFieldValidator ID="rqfv_CircleSelect" runat="server" InitialValue="0"
                                ForeColor="red" Display="Dynamic" ErrorMessage="Please Select Circle" ControlToValidate="ddlst_circle"
                                Text="*" ValidationGroup="SiteSelect"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlst_circle" runat="server" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlst_circle_SelectedIndexChanged">
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
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-10" id="allinputs" runat="server" style="border-right:solid 1px #efefef">
                        <ul id="tabs" class="nav nav-tabs" data-tabs="tabs">
                            <li class="active"><a href="#ContentPlaceHolder1_dg" data-toggle="tab">DG</a></li>
                            <li><a href="#ContentPlaceHolder1_pac" data-toggle="tab">PAC</a></li>
                            <li><a href="#ContentPlaceHolder1_smps" data-toggle="tab">SMPS</a></li>
                            <li><a href="#ContentPlaceHolder1_batterybank" data-toggle="tab">Battery Bank</a></li>
                            <li><a href="#ContentPlaceHolder1_tower" data-toggle="tab">Tower</a></li>
                            <li><a href="#ContentPlaceHolder1_earthing" data-toggle="tab">Earthing</a></li>
                            <li><a href="#ContentPlaceHolder1_avr" data-toggle="tab">AVR</a></li>
                            <li><a href="#ContentPlaceHolder1_shelter" data-toggle="tab">Shelter</a></li>
                            <li><a href="#ContentPlaceHolder1_transformer" data-toggle="tab">Transformer</a></li>
                            <li><a href="#firesystem" data-toggle="tab">Fire System</a></li>
                            <li><a href="#coolingsystem" data-toggle="tab">Cooling System</a></li>
                        </ul>
                        <div id="my-tab-content" class="tab-content">
                            <div class="tab-pane active" id="dg" runat="server">
                                <div class="clearfix">
                                </div>
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="dg_UpdatePanel">
                                    <ProgressTemplate>
                                        <div id="Background" class="">
                                        </div>
                                        <div id="Progress">
                                            Loading please wait...
                                            <img alt="" src="img/ajax-loader.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:UpdatePanel ID="dg_UpdatePanel" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="technology">
                                                        Select DG Number</label>
                                                    <asp:DropDownList ID="ddlst_dgno" runat="server" CssClass="form-control" AutoPostBack="true">
                                                        <asp:ListItem Value="0" Text="-- Select D G No--">-- Select D G No--</asp:ListItem>
                                                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                        <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                        <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                        <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Model No</label>
                                                    <asp:TextBox ID="txt_DGModelNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Manufacturer Name</label>
                                                    <asp:TextBox ID="txt_DGManufacturerName" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="technology">
                                                        Raring</label>
                                                    <asp:TextBox ID="txt_DGRaring" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        UNIT (KVA)</label>
                                                    <asp:TextBox ID="txt_DGUnitKVA" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Engine Sr. No.</label>
                                                    <asp:TextBox ID="txt_DGEngineSrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="technology">
                                                        Mfg Date</label>
                                                    <asp:TextBox ID="txt_DGMfgDate" runat="server" CssClass="form-control" Placeholder="MM-DD-YYYY"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Installation Date</label>
                                                    <asp:TextBox ID="txt_DGInstallationDate" runat="server" CssClass="form-control" Placeholder="MM-DD-YYYY"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Alternator Model No</label>
                                                    <asp:TextBox ID="txt_DGAlternatorModelNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="technology">
                                                        Alternator Make</label>
                                                    <asp:TextBox ID="txt_DGAlternatorMake" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Alternator Rating</label>
                                                    <asp:TextBox ID="txt_DGAlternatorRating" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Alternator Unit</label>
                                                    <asp:TextBox ID="txt_DGAlternatorUnit" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="technology">
                                                        Alternator Sr No</label>
                                                    <asp:TextBox ID="txt_DGAlternatorSrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Alternator Mfg Date</label>
                                                    <asp:TextBox ID="txt_AlternatorMfgDate" runat="server" CssClass="form-control" Placeholder="MM-DD-YYYY"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Alternator Commision Date</label>
                                                    <asp:TextBox ID="txt_AlternatorCommisionDate" runat="server" CssClass="form-control"
                                                        Placeholder="MM-DD-YYYY"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="technology">
                                                        DG Working Status</label>
                                                    <asp:TextBox ID="txt_DGWorkingStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        DG Battery Make</label>
                                                    <asp:TextBox ID="txt_DGBatteryMake" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        DG Battery Capacity (AH)</label>
                                                    <asp:TextBox ID="txt_DGBatteryCapacityAH" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="technology">
                                                        DG Battery Serial No</label>
                                                    <asp:TextBox ID="txt_DGBatterySrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        DG Battery Commision Date</label>
                                                    <asp:TextBox ID="txt_DGBatteryCommisionDate" runat="server" CssClass="form-control"
                                                        Placeholder="MM-DD-YYYY"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        DG Battery Working Status</label>
                                                    <asp:TextBox ID="txt_DGBatteryworkingStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="technology">
                                                        AMF Model No</label>
                                                    <asp:TextBox ID="txt_DGAMFmodelNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        DG GCU Make</label>
                                                    <asp:TextBox ID="txt_DGGCUMake" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        AMF Commision Date</label>
                                                    <asp:TextBox ID="txt_DGAMFCommisionDate" runat="server" CssClass="form-control" Placeholder="MM-DD-YYYY"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="technology">
                                                        AMF Working Status</label>
                                                    <asp:TextBox ID="txt_DGAMFWorkingStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        GCU Model No</label>
                                                    <asp:TextBox ID="txt_DGGCUModelNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        GCU Commision Date</label>
                                                    <asp:TextBox ID="txt_DGGCUCommisionDate" runat="server" CssClass="form-control" Placeholder="MM-DD-YYYY"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="technology">
                                                        GCU Working Status</label>
                                                    <asp:TextBox ID="txt_DGGCUWorkingStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        DG Health Status</label>
                                                    <asp:TextBox ID="txt_DGhealthStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        CPH</label>
                                                    <asp:TextBox ID="txt_DGCPH" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="technology">
                                                        DG Fuel Tank</label>
                                                    <asp:TextBox ID="txt_DGFuelTank" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-8">
                                                &nbsp;
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <asp:Button ID="btn_SaveDGDetails" runat="server" CssClass="btn btn-primary" Text="Save Details"
                                                        OnClick="btn_SaveDGDetails_Click" />
                                                    <asp:Button ID="btn_ResetDGDetails" runat="server" CssClass="btn btn-warning" Text="Reset"
                                                        OnClick="btn_ResetDGDetails_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:AsyncPostBackTrigger ControlID="btn_SaveDGDetails" />--%>
                                        <asp:PostBackTrigger ControlID="btn_SaveDGDetails" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="pac" runat="server">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="PAC Number">
                                                Select PAC Number</label>
                                            <asp:DropDownList ID="ddlst_PACno" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="-- Select PAC No--">-- Select PAC No--</asp:ListItem>
                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="PACModelNo">
                                                PAC Model No</label>
                                            <asp:TextBox ID="txt_PACModelNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="PACMake">
                                                PAC Make</label>
                                            <asp:TextBox ID="txt_PACMake" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="PACRating">
                                                Rating</label>
                                            <asp:TextBox ID="txt_PACRating" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="PACSrNo">
                                                Serial No</label>
                                            <asp:TextBox ID="txt_PACSerialNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="CompresserSrNo">
                                                Compressor Serial No</label>
                                            <asp:TextBox ID="txt_PACComressorSrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="ManufacturingDate">
                                                Manufacturing Date</label>
                                            <asp:TextBox ID="txt_PACManufacturingDate" runat="server" Placeholder="MM-DD-YYYY"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="PACInstallationDate">
                                                Installation Date</label>
                                            <asp:TextBox ID="txt_PACInstallationDate" runat="server" Placeholder="MM-DD-YYYY"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="PACWorkingStatus">
                                                PAC Working Status</label>
                                            <asp:TextBox ID="txt_PACWorkingStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <asp:Button ID="btn_SavePACDetails" runat="server" CssClass="btn btn-primary" Text="Save Details"
                                                OnClick="btn_SavePACDetails_Click" />
                                            <asp:Button ID="btn_ResetPACDetails" runat="server" CssClass="btn btn-warning" Text="Reset"
                                                OnClick="btn_ResetPACDetails_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="smps" runat="server">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Select SMPS Number</label>
                                            <asp:DropDownList ID="ddlst_SMPSNo" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="-- Select SMPS No--">-- Select SMPS No--</asp:ListItem>
                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Model No</label>
                                            <asp:TextBox ID="txt_SMPSModelno" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                SMPS Make</label>
                                            <asp:TextBox ID="txt_SMPSMake" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                SMPS Serial No</label>
                                            <asp:TextBox ID="txt_SMPSSrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Rating</label>
                                            <asp:TextBox ID="txt_SMPSRating" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                No Of Rectifier Slot</label>
                                            <asp:TextBox ID="txt_SMPSNoOfRectifierSlot" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Mfg Date</label>
                                            <asp:TextBox ID="txt_SMPSMfgDate" runat="server" Placeholder="MM-DD-YYYY" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Commision Date</label>
                                            <asp:TextBox ID="txt_SMPSCommisionDate" runat="server" Placeholder="MM-DD-YYYY" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Working Status</label>
                                            <asp:TextBox ID="txt_SMPSWorkingStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                LVD Rating</label>
                                            <asp:TextBox ID="txt_SMPSLVDRating" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                LVD Commision Date</label>
                                            <asp:TextBox ID="txt_SMPSLVDCommisionDate" runat="server" Placeholder="MM-DD-YYYY"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Controller Commision Date</label>
                                            <asp:TextBox ID="txt_SMPSControllerCommisionDate" runat="server" Placeholder="MM-DD-YYYY"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                No Of Rectifier Module Installed</label>
                                            <asp:TextBox ID="txt_SMPSInstalledRectifierModule" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                No Of Working Module</label>
                                            <asp:TextBox ID="txt_SMPSNoOfWorkingModule" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Module 1 Serial No</label>
                                            <asp:TextBox ID="txt_SMPSModule1SrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Module 2 Serial No</label>
                                            <asp:TextBox ID="txt_SMPSModule2SrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Module 3 Serial No</label>
                                            <asp:TextBox ID="txt_SMPSModule3SrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Module 4 Serial No</label>
                                            <asp:TextBox ID="txt_SMPSModule4SrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Module 5 Serial No</label>
                                            <asp:TextBox ID="txt_SMPSModule5SrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Module 6 Serial No</label>
                                            <asp:TextBox ID="txt_SMPSModule6SrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Module 7 Serial No</label>
                                            <asp:TextBox ID="txt_SMPSModule7SrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Module 8 Serial No</label>
                                            <asp:TextBox ID="txt_SMPSModule8SrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Module 9 Serial No</label>
                                            <asp:TextBox ID="txt_SMPSModule9SrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Module 10 Serial No</label>
                                            <asp:TextBox ID="txt_SMPSModule10SrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <asp:Button ID="btn_SaveSMPSDetails" runat="server" Text="Save Details" CssClass="btn btn-primary"
                                                OnClick="btn_SaveSMPSDetails_Click" />
                                            <asp:Button ID="btn_ResetSMPSDetails" runat="server" Text="Reset" CssClass="btn btn-warning"
                                                OnClick="btn_ResetSMPSDetails_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="batterybank" runat="server">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Select Battery Bank Number</label>
                                            <asp:DropDownList ID="ddlst_BBNumber" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="-- Select SMPS No--">-- Select Battery Bank No--</asp:ListItem>
                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Model No</label>
                                            <asp:TextBox ID="txt_BBModelNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                BB Capacity</label>
                                            <asp:TextBox ID="txt_BBCapacity" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                BB Serial No</label>
                                            <asp:TextBox ID="txt_BBSrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                No Of Cells</label>
                                            <asp:TextBox ID="txt_BBNoOfCells" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                BB Backup In Hrs</label>
                                            <asp:TextBox ID="txt_BBBackupInHrs" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                BB Mfg Date</label>
                                            <asp:TextBox ID="txt_BBMfgDate" runat="server" Placeholder="MM-DD-YYYY" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                BB Commision Date</label>
                                            <asp:TextBox ID="txt_BBCommisionDate" runat="server" Placeholder="MM-DD-YYYY" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                No Of Faulty Cells</label>
                                            <asp:TextBox ID="txt_BBNoOfFaultyCells" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <asp:Button ID="btn_BBSaveDetails" runat="server" Text="Save Details" CssClass="btn btn-primary"
                                                OnClick="btn_BBSaveDetails_Click" />
                                            <asp:Button ID="btn_BBReset" runat="server" Text="Reset" CssClass="btn btn-warning"
                                                OnClick="btn_BBReset_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="tower" runat="server">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Select Tower Number</label>
                                            <asp:DropDownList ID="ddlst_TowerNo" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="-- Select SMPS No--">-- Select Tower No--</asp:ListItem>
                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Site Type (GBT/ RTT/ RTP)</label>
                                            <asp:TextBox ID="txt_TowerSiteType" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Age Of Tower (Yrs)</label>
                                            <asp:TextBox ID="txt_TowerAge" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Lattitude</label>
                                            <asp:TextBox ID="txt_TowerLattitude" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Longitude</label>
                                            <asp:TextBox ID="txt_TowerLongitude" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Capacity (No Of Slots)</label>
                                            <asp:TextBox ID="txt_TowerCapacity" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                No Of Tenancy</label>
                                            <asp:TextBox ID="txt_TowerNoOfTenancy" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Name Of Tenant 1</label>
                                            <asp:TextBox ID="txt_TowerNameOfTenant1" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Name Of Tenant 2</label>
                                            <asp:TextBox ID="txt_TowerNameOfTenant2" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Name Of Tenant 3</label>
                                            <asp:TextBox ID="txt_TowerNameOfTenant3" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Name Of Tenant 4</label>
                                            <asp:TextBox ID="txt_TowerNameOfTenant4" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Name Of Tenant 5</label>
                                            <asp:TextBox ID="txt_TowerNameOfTenant5" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Current Vacant Slot Available</label>
                                            <asp:TextBox ID="txt_TowerCurrentVacantSlot" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Under Aviation Zone</label>
                                            <asp:DropDownList ID="ddlst_TowerAviationZoneYN" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="YES" Text="YES">YES</asp:ListItem>
                                                <asp:ListItem Value="NO" Text="NO">NO</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Tower Height</label>
                                            <asp:TextBox ID="txt_TowerHeight" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Galvanisation</label>
                                            <asp:DropDownList ID="ddlst_TowerGalvanisation" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="GALVANISED" Text="Galvanised">Galvanised</asp:ListItem>
                                                <asp:ListItem Value="NON-GALVANISED" Text="Non-Galvanised">Non-Galvanised</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Aviation Lamp Working Status</label>
                                            <asp:TextBox ID="txt_TowerAviationLampStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btn_TowerSaveDetails" runat="server" Text="Save Details" CssClass="btn btn-primary"
                                            OnClick="btn_TowerSaveDetails_Click" />
                                        <asp:Button ID="btn_TowerReset" runat="server" Text="Reset" CssClass="btn btn-warning"
                                            OnClick="btn_TowerReset_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="earthing" runat="server">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                No Of Earth Pit</label>
                                            <asp:TextBox ID="txt_EarthingNoOfEarthPit" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                No Of IGB</label>
                                            <asp:TextBox ID="txt_EarthingNoOfIGB" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                No Of OGB</label>
                                            <asp:TextBox ID="txt_EarthingNoOfOGB" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                No Of Earth Pit OK Condition</label>
                                            <asp:TextBox ID="txt_EarthingNoOfEarthPitOKCOndition" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btn_EarthingSaveDetails" runat="server" Text="Save Details" CssClass="btn btn-primary"
                                            OnClick="btn_EarthingSaveDetails_Click" />
                                        <asp:Button ID="btn_EarthingReset" runat="server" Text="Reset" CssClass="btn btn-warning"
                                            OnClick="btn_EarthingReset_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="avr" runat="server">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Select AVR Number</label>
                                            <asp:DropDownList ID="ddlst_NoOfAVR" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="-- Select SMPS No--">-- Select AVR No--</asp:ListItem>
                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                AVR Make</label>
                                            <asp:TextBox ID="txt_AVRMake" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                AVR Model No</label>
                                            <asp:TextBox ID="txt_AVRModelNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                No Of AVR Installed</label>
                                            <asp:TextBox ID="txt_AVRNoOfInstalled" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Serial No</label>
                                            <asp:TextBox ID="txt_AVRSrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                AVR Install Date</label>
                                            <asp:TextBox ID="txt_AVRInstalledDate" runat="server" Placeholder="MM-DD-YYYY" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Working Status Of AVR</label>
                                            <asp:TextBox ID="txt_AVRWorkingStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btn_AVRSaveDetails" runat="server" Text="Save Details" CssClass="btn btn-primary"
                                            OnClick="btn_AVRSaveDetails_Click" />
                                        <asp:Button ID="btn_AVRReset" runat="server" Text="Reset" CssClass="btn btn-warning"
                                            OnClick="btn_AVRReset_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="shelter" runat="server">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Select Shelter Number</label>
                                            <asp:DropDownList ID="ddlst_ShelterNo" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="-- Select Shelter No--">-- Select Shelter No--</asp:ListItem>
                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Select Shelter Type</label>
                                            <asp:DropDownList ID="ddlst_ShelterType" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="-- Select Shelter Type --">-- Select Shelter Type --</asp:ListItem>
                                                <asp:ListItem Value="INDOOR" Text="Indoor">Indoor</asp:ListItem>
                                                <asp:ListItem Value="OUTDOOR" Text="Outdoor">Outdoor</asp:ListItem>
                                                <asp:ListItem Value="ROOM" Text="Room">Room</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Shelter Make</label>
                                            <asp:TextBox ID="txt_ShelterMake" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Shelter Mfg Date</label>
                                            <asp:TextBox ID="txt_ShelterMfgDate" runat="server" Placeholder="MM-DD-YYYY" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Shelter Commision Date</label>
                                            <asp:TextBox ID="txt_ShelterCommisionDate" runat="server" Placeholder="MM-DD-YYYY"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Shelter Dimension</label>
                                            <asp:TextBox ID="txt_ShelterDimension" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btn_ShelterSaveDetails" runat="server" Text="Save Details" CssClass="btn btn-primary"
                                            OnClick="btn_ShelterSaveDetails_Click" />
                                        <asp:Button ID="btn_ShelterReset" runat="server" Text="Reset" CssClass="btn btn-warning"
                                            OnClick="btn_ShelterReset_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="transformer" runat="server">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                HT / LT</label>
                                            <asp:DropDownList ID="ddlst_TransformerHTLT" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="-- Select Shelter No--">-- Select HT/LT No--</asp:ListItem>
                                                <asp:ListItem Value="HT" Text="HT">HT</asp:ListItem>
                                                <asp:ListItem Value="LT" Text="LT">LT</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Line Capacity</label>
                                            <asp:TextBox ID="txt_TransformerLineCapacity" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="circle">
                                                Own/SEB</label>
                                            <asp:TextBox ID="txt_TransformerOwnSEB" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Site Load Current</label>
                                            <asp:TextBox ID="txt_TransformerSiteLoadCurrent" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btn_TransformerSaveDetails" runat="server" Text="Save Details" CssClass="btn btn-primary"
                                            OnClick="btn_TransformerSaveDetails_Click" />
                                        <asp:Button ID="btn_TransformerReset" runat="server" Text="Reset" CssClass="btn btn-warning"
                                            OnClick="btn_TransformerReset_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="firesystem">
                                <div class="row">
                                <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Select Fire System</label>
                                            <asp:DropDownList ID="ddlst_FireSystemNo" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="-- Select Shelter No--">-- Select Fire System No--</asp:ListItem>
                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="technology">
                                                        Make</label>
                                                    <asp:TextBox ID="txt_FireSystemMake" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Model No</label>
                                                    <asp:TextBox ID="txt_FireSystemModel" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                </div>
                                <div class="row">
                                <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Commision Date</label>
                                                    <asp:TextBox ID="txt_FireSystemCommDate" runat="server" CssClass="form-control" Placeholder="MM-DD-YYYY"></asp:TextBox>
                                                </div>
                                            </div>
                                <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="technology">
                                                        Status</label>
                                                    <asp:TextBox ID="txt_FireSystemStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                </div>
                                <div class="row">
                                         <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Extra Info 1</label>
                                                    <asp:TextBox ID="txt_FireSystemExt1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Extra Info 2</label>
                                                    <asp:TextBox ID="txt_FireSystemExt2" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Extra Info 3</label>
                                                    <asp:TextBox ID="txt_FireSystemExt3" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                    <div class="form-group">
                                        <asp:Button ID="btn_FireSystemSave" runat="server" Text="Save Details" CssClass="btn btn-primary"
                                            OnClick="btn_FireSystemSave_Click" />
                                        <asp:Button ID="btn_FireSystemReset" runat="server" Text="Reset" CssClass="btn btn-warning"
                                            OnClick="btn_FireSystemReset_Click" />
                                            </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="coolingsystem">
                                <div class="row">
                                 <div class="col-sm-4">
                                        <div class="form-group">
                                            <label for="technology">
                                                Select Cooling System</label>
                                            <asp:DropDownList ID="ddlst_CoolingSystemNo" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="-- Select Shelter No--">-- Select Cooling System No--</asp:ListItem>
                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="technology">
                                                        Make</label>
                                                    <asp:TextBox ID="txt_CoolSysMake" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Model No</label>
                                                    <asp:TextBox ID="txt_CoolSysModel" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            
                                        </div>
                                <div class="row">
                                <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Commision Date</label>
                                                    <asp:TextBox ID="txt_CoolSysCommDate" runat="server" CssClass="form-control" Placeholder="MM-DD-YYYY"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="technology">
                                                        Status</label>
                                                    <asp:TextBox ID="txt_CoolSysStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                           
                                        </div>
                                <div class="row">
                                         <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Extra Info 1</label>
                                                    <asp:TextBox ID="txt_CoolSysExt1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Extra Info 2</label>
                                                    <asp:TextBox ID="txt_CoolSysExt2" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label for="circle">
                                                        Extra Info 3</label>
                                                    <asp:TextBox ID="txt_CoolSysExt3" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                    <div class="form-group">
                                        <asp:Button ID="btn_CoolSystemSave" runat="server" Text="Save Details" CssClass="btn btn-primary"
                                            OnClick="btn_CoolSystemSave_Click" />
                                        <asp:Button ID="btn_CoolSystemReset" runat="server" Text="Reset" CssClass="btn btn-warning"
                                            OnClick="btn_CoolSystemReset_Click" />
                                            </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    Reports</h3>
                            </div>
                            <div class="panel-body">
                                <ul class="list-unstyled list-spaces">
                                <li><a href="#">Inventories Report</a></li>
                                <li><a href="Reports.aspx">Assets Report</a></li>                              
                            </ul>
                            </div>
                        </div>
                    </div>
                    <script type="text/javascript">
                        jQuery(document).ready(function ($) {
                            $('#tabs').tab();
                        });
                    </script>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
