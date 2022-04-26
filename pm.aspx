<%@ Page Title="UMT : PM" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="pm.aspx.cs" Inherits="pm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <link href="css/pagging.css" rel="stylesheet" type="text/css" />
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
        function showPosition() {
            if (navigator.geolocation) {
                var location_timeout = setTimeout("geolocFail()", 10000);
                navigator.geolocation.getCurrentPosition(function (position) {
                    clearTimeout(location_timeout);
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
    <script type="text/javascript">
        $(document).ready(function () {
            $("div.bhoechie-tab-menu>div.list-group>a").click(function (e) {
                e.preventDefault();
                $(this).siblings('a.active').removeClass("active");
                $(this).addClass("active");
                var index = $(this).index();
                $("div.bhoechie-tab>div.bhoechie-tab-content").removeClass("active");
                $("div.bhoechie-tab>div.bhoechie-tab-content").eq(index).addClass("active");
            });
        });
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
        <div class="col-sm-12 maincontent">
            <header class="page-header">
                <h3 class="page-title">Preventive Maintenance</h3>
            </header>
            <div class="row">
                <div class="col-sm-3">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlst_circle" runat="server" CssClass="form-control" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlst_circle_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="-- Select Circle --">-- Select Circle --</asp:ListItem>
                            <asp:ListItem Value="1" Text="Mumbai">Mumbai</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3">
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
                    <div class="form-group">
                        <asp:HiddenField ID="InventoryID" runat="server" />
                        <asp:HiddenField ID="latitudeI" runat="server" />
                        <asp:HiddenField ID="longitudeId" runat="server" />
                        <asp:Button ID="btn_SelectSite" runat="server" CssClass="btn btn-primary" Text="Select Site"
                            OnClick="btn_SelectSite_Click" />
                        <a href="PmReports.aspx" class="btn btn-success">View PM Report</a>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                <div class="form-group">
                    <asp:DropDownList ID="ddlst_PMMaster" runat="server" CssClass="form-control" 
                        AutoPostBack="true" 
                        onselectedindexchanged="ddlst_PMMaster_SelectedIndexChanged">
                    </asp:DropDownList>
                    </div>
                </div>                
            </div>
            <div class="row">
                <div class="col-sm-12 table-responsive">
                    <asp:GridView ID="grdview_PMDetails" CssClass="table table-bordered table-striped table-hover"
                         AllowPaging="false" runat="server" Style="font-size: 12px;" ForeColor="#333333"
                        GridLines="None" AutoGenerateColumns="false"
                        onpageindexchanging="grdview_PMDetails_PageIndexChanging">
                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                        <Columns>
                         <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_PMDescID" runat="server" Text='<%# Eval("PMDescID") %>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                             <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_PMID" runat="server" Text='<%# Eval("PM_ID") %>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                            <asp:BoundField DataField="PM_Name" HeaderText="PM For" />
                            <asp:BoundField DataField="PM_Description" HeaderText="Preventive Maintenance" />
                            <asp:TemplateField HeaderText="PM Frequency">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlst_PMFrequency" runat="server" AutoPostBack="true" CssClass="form-control form-group-sm">
                                    <asp:ListItem Text="Weekly" Value="WEEKLY"></asp:ListItem>
                                    <asp:ListItem Text="Monthly" Value="MONTHLY"></asp:ListItem>
                                    <asp:ListItem Text="Quaterly" Value="QUATERLY"></asp:ListItem>
                                    <asp:ListItem Text="Annualy" Value="ANNUALY"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PM Status">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlst_PMStatus" runat="server" AutoPostBack="true" CssClass="form-control form-group-sm">
                                    <asp:ListItem Text="OK" Value="OK"></asp:ListItem>
                                    <asp:ListItem Text="NOT-OK" Value="NOT-OK"></asp:ListItem>
                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_StatusDescription" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-4">
                
                <div class="well well-sm">
                        <div class="form-group">
                    <label>Preventive Maintenance Image 1</label>
                        <asp:FileUpload ID="PMImg1" runat="server" CssClass="input-sm"/>
                    </div>
                      </div>
                </div>
               
                <div class="col-sm-4">
            
                <div class="well well-sm">
                
                    <div class="form-group">
                    <label>Preventive Maintenance Image 2</label>
                        <asp:FileUpload ID="PMImg2" runat="server" CssClass="input-sm"/>
                    </div>
                    </div>
           
                </div>
                <div class="col-sm-4">
                
                <div class="well well-sm">
                <div class="form-group">
                    <label>Preventive Maintenance Image 3</label>
                        <asp:FileUpload ID="PMImg3" runat="server" CssClass="input-sm"/>
                    </div>
                </div>
                </div>
           
            </div>
            <div class="row">
                <div class="col-sm-12">
                <div class="form-group">
                    <asp:Button ID="btn_SaveDetails" runat="server" CssClass="btn btn-primary" Text="Save Details"
                        OnClick="btn_SaveDetails_Click" />
                    <asp:Button ID="btn_Reset" runat="server" CssClass="btn btn-warning" Text="Reset Details"
                        OnClick="btn_Reset_Click" />
                        </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
