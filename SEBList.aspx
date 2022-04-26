<%@ Page Title="UMT SEB List" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SEBList.aspx.cs" Inherits="SEBList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">   
    <link href="../css/pagging.css" rel="stylesheet" type="text/css" />
    <div class="container">
        <div class="row">
            <div class="col-sm-12 maincontent">
                <header class="page-header">
                    <h2 class="HeaderText page-title">SEB List</h2>
                </header>
                <div class="row">
                <div class="col-sm-12">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlst_circle" runat="server" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlst_circle_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlst_rowsize" runat="server" AutoPostBack="true" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlst_rowsize_SelectedIndexChanged">
                                    <asp:ListItem Text="No Of Records" Value="init"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="25" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="50" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="100" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:TextBox ID="txt_SearchInventory" runat="server" CssClass="form-control" Placeholder="Search SEB"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-1">
                        <div class="form-group">
                            <%--<a href="#" class="btn btn-primary">View List</a>--%>
                             <a href="seb.aspx" class="btn btn-primary">Add SEB</a>
                        </div>
                        </div>
                        
                    </div>
                    <div class="row table-responsive">
                        <div class="col-sm-12">
                            <asp:GridView ID="grdview_SEBDetails" CssClass="table table-bordered table-striped table-hover"
                                PageSize="10" AllowPaging="true" runat="server" Style="font-size: 12px;" ForeColor="#333333"
                                GridLines="None" AutoGenerateColumns="false" OnPageIndexChanging="grdview_SEBDetails_PageIndexChanging"
                                OnRowDataBound="grdview_SEBDetails_RowDataBound" OnRowCommand="grdview_SEBDetails_RowCommand" OnRowDeleting="grdview_SEBDetails_RowDeleting">
                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="SEB ID" />
                                    <asp:BoundField DataField="SITE_ID" HeaderText="Site ID" />
                                    <asp:BoundField DataField="SITE_NAME" HeaderText="Site Name" />
                                    <asp:BoundField DataField="CIRCLE_NAME" HeaderText="Circle" />
                                    <asp:BoundField DataField="MEPT" HeaderText="MEPT" />
                                    <%--<asp:BoundField DataField="CONSUMER_NO" HeaderText="CONSUMER NO" />--%>
                                    <asp:TemplateField HeaderText="CONSUMER NO">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_ConsumerNo" Text='<%# Eval("CONSUMER_NO") %>' CommandArgument='<%# Eval("ID") %>' CommandName="edit"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER NAME" />
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:Button ID="btn_apply" runat="server" CommandName="Delete" Text="Delete" CommandArgument='<%# Eval("ID") %>'
                                                CssClass="btn btn-primary btn-sm" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

