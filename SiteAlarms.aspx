<%@ Page Title="Site Alarms" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="SiteAlarms.aspx.cs" Inherits="SiteAlarms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function blinker() {
            $('.blinking').fadeOut(500);
            $('.blinking').fadeIn(500);
        }
        setInterval(blinker, 1000);
    </script>
    <div class="container">
        <div class="row">
            <div class="col-sm-12 maincontent">
                <header class="page-header">
                    <h3 class="page-title">Site Alarms</h3>
                </header>
                <div class="row">
                    <%--<div class="col-sm-2">
                        <table class="table bg-warning">
                            <tr style="border-bottom: 1px solid #efefef !important;" class="bg-success text-center">
                                <td>
                                    <label>
                                        Search</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Circle</label>
                                    <asp:DropDownList ID="ddlst_Circle" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlst_Circle_SelectedIndexChanged">
                                        <asp:ListItem Text="Select Circle" Value="0">--Select Circle --</asp:ListItem>
                                        <asp:ListItem Text="Mumbai" Value="1">Mumbai</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        MEPT</label>
                                    <asp:DropDownList ID="ddlst_MEPT" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Select MEPT" Value="0">--Select MEPT --</asp:ListItem>
                                        <asp:ListItem Text="Mumbai" Value="1">Mumbai</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Site</label>
                                    <asp:DropDownList ID="ddlst_Site" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Select Site" Value="0">--Select Site --</asp:ListItem>
                                        <asp:ListItem Text="VAFS00213" Value="1">VAFS00213</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_submitsearch" runat="server" CssClass="btn btn-warning" Text="Search" />
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                    <div id="div_table" runat="server" class="col-sm-12 table-responsive">
                       <%-- <asp:GridView ID="grdview_SiteAlarms" CssClass="table table-responsive" PageSize="10"
                            runat="server" Style="font-size: 12px;" ForeColor="#333333" GridLines="None"
                            AutoGenerateColumns="false" 
                            onrowdatabound="grdview_SiteAlarms_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="siteid" HeaderText="Site Id" />
                                <asp:BoundField DataField="sitename" HeaderText="Site Name" />
                                <asp:TemplateField HeaderText="ACMF">
                                <ItemTemplate>
                                <%# Eval("A1") %>
                                </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="A2" HeaderText="BOL" />
                                <asp:BoundField DataField="A3" HeaderText="DOL" />
                                <asp:BoundField DataField="A4" HeaderText="PACA" />
                                <asp:BoundField DataField="A5" HeaderText="Fire SMS" />                                
                            </Columns>
                        </asp:GridView>--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <asp:Button ID="btn_save" runat="server" Text="Save Details" CssClass="btn btn-success" />
                        <asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="btn btn-warning" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        &nbsp;</div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
