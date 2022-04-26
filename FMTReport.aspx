<%@ Page Title="FMT Report" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="FMTReport.aspx.cs" Inherits="FMTReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(function () {
            $("[id$=txt_SiteID]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("FMTReport.aspx/GetSites") %>',
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
    <div class="container">
        <div class="row">
            <header class="page-header">
                <h3 class="page-title">FMT Report</h3>
            </header>
        </div>
        <div class="row">
            <div class="col-sm-4">
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
            <div class="col-sm-4">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="red" runat="server"
                    Display="Dynamic" ErrorMessage="Please Select Site" ControlToValidate="txt_SiteID"
                    Text="*" ValidationGroup="SiteSelect"></asp:RequiredFieldValidator>
                <div class="form-group">
                    <asp:TextBox ID="txt_SiteID" runat="server" CssClass="form-control" Placeholder="Enter Site ID"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-3">
                <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="SiteSelect" DisplayMode="List"
                    ShowMessageBox="true" ShowSummary="false" runat="server" />
                <div class="form-group">
                    <asp:HiddenField ID="InventoryID" runat="server" />
                    <asp:Button ID="btn_ShowReport" runat="server" CssClass="btn btn-primary" Text="Show Report"
                        ValidationGroup="SiteSelect" OnClick="btn_ShowReport_Click" />
                    <asp:Button ID="btn_ExportExcel" runat="server" CssClass="btn-sm btn btn-success"
                        Visible="false" Text="Export to Excel" />
                </div>
            </div>
            <div class="col-sm-1">
                <div class="form-group">
                    <a href="FuelDetails.aspx" class="btn btn-default">Add FMT</a>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <asp:Label ID="lbl_SiteID" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                    <asp:Label ID="lbl_SiteName" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 table-responsive">
                <asp:GridView ID="grdview_FMTReport" CssClass="table table-bordered table-striped table-hover"
                    PageSize="10" AllowPaging="true" runat="server" Style="font-size: 12px;" ForeColor="#333333"
                    GridLines="None" AutoGenerateColumns="true">
                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
