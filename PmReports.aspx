<%@ Page Title="UMT : PM" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="PmReports.aspx.cs" Inherits="pm" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
        <div class="row">
            <header class="page-header">
                <h3 class="page-title">Preventive Maintenance Report</h3>
            </header>
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
                    <asp:DropDownList ID="ddlst_PMMaster" runat="server" CssClass="form-control" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlst_PMMaster_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-2">
                <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="SiteSelect" DisplayMode="List"
                    ShowMessageBox="true" ShowSummary="false" runat="server" />
                <div class="form-group">
                    <asp:HiddenField ID="InventoryID" runat="server" />
                    <asp:Button ID="btn_ShowReport" runat="server" CssClass="btn btn-primary" Text="Show Report"
                        ValidationGroup="SiteSelect" OnClick="btn_ShowReport_Click" />
                    <asp:Button ID="btn_ExportExcel" runat="server" CssClass="btn-sm btn btn-success" Visible="false"
                        Text="Export to Excel" />
                </div>
            </div>
            <div class="col-sm-1">
            <div class="form-group">
            <a href="pm.aspx" class="btn btn-default">Add PM</a>
            </div>
            </div>
        </div>
        <hr style="margin-top: 0px; margin-bottom: 0px;" />
        <div class="row">
            <div class="col-sm-3">
                <div class="form-group">
                    <h5>
                        <asp:Label ID="lbl_SiteID" runat="server"></asp:Label> </h5>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-group">
                    <h5>
                        <asp:Label ID="lbl_SiteName" runat="server"></asp:Label></h5>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-group">
                    <h5>
                         <asp:Label ID="lbl_PMName" runat="server"></asp:Label></h5>
                </div>
            </div>
             <div class="col-sm-3">
                <div class="form-group">
                    <a  href="#" class="btn btn-primary">Edit PM</a>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 table-responsive">
                <asp:GridView ID="grdview_PMReport" CssClass="table table-bordered table-striped table-hover"
                     AllowPaging="false" runat="server" Style="font-size: 12px;" ForeColor="#333333"
                    GridLines="None" AutoGenerateColumns="false">
                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="PM_Description" HeaderText="Description" />
                        <asp:BoundField DataField="Frequency" HeaderText="Frequency" />
                        <asp:BoundField DataField="PMDate" HeaderText="PM Date" />
                        <asp:BoundField DataField="NextPMDate" HeaderText="Next PM Date" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="StatusRemarks" HeaderText="Status Remarks" /> 
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-sm-12" id="pmImages" runat="server">
           
            </div>
        </div>
    </div>
</asp:Content>
