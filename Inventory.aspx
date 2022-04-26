<%@ Page Title="UMT : Inventory" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true"
    CodeFile="Inventory.aspx.cs" Inherits="Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/pagging.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
            function EndRequestHandler(sender, args) {
                $("[id$=txt_InventoryDate]").datepicker({ dateFormat: 'mm-dd-yy' });
            }
        });
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div id="Background" class=""></div>
            <div id="Progress">
                <img alt="" src="../img/ajax-loader_circle.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <!-- Article main content -->
                    <div class="col-sm-12 maincontent">
                        <header class="page-header">
                            <h2 class="page-title">Site Master</h2>
                            <small>All fields are marked with <span style="color: Red">*</span> are mandatory.</small>
                        </header>
                        <h3 style="margin-top:0 !important;font-weight:300;">Site Details</h3>
                        <div class="row" id="editDetails" runat="server" visible="false">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label for="technology">
                                        Create Date: </label>
                                    <asp:Label ID="lbl_CreateDate" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label for="technology">
                                        Created By: </label>
                                    <asp:Label ID="lbl_CreatedBy" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label for="technology">
                                        Site Name</label>
                                    <span style="color: Red">*</span>
                                    <asp:TextBox ID="txt_SiteName" runat="server" CssClass="form-control" ViewStateMode="Enabled" placeholder="Site Name"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label for="circle">
                                        Site ID</label>
                                    <span style="color: Red">*</span>
                                    <asp:TextBox ID="txt_SiteId" runat="server" CssClass="form-control" placeholder="Site ID"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label for="technology">
                                        Select Circle</label>
                                    <span style="color: Red">*</span>
                                    <asp:DropDownList ID="ddlst_circle" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label for="circle">
                                        Longitude</label><span style="color: Red">*</span>
                                    <asp:TextBox ID="txt_Longitude" runat="server" CssClass="form-control" placeholder="Longitude"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label for="technology">
                                        Latitude</label>
                                    <span style="color: Red">*</span>
                                    <asp:TextBox ID="txt_Latitude" runat="server" CssClass="form-control" placeholder="Latitude"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label for="circle">
                                        MEPT</label>
                                    <span style="color: Red">*</span>
                                    <asp:TextBox ID="txt_MEPT" runat="server" CssClass="form-control" placeholder="MEPT"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label for="technology">
                                        Select Article Type</label>
                                    <span style="color: Red">*</span>
                                    <asp:DropDownList ID="ddlst_articleType" runat="server" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="0" Text="-- Select Circle --">-- Select Article --</asp:ListItem>
                                        <asp:ListItem Value="MSC" Text="MSC">MSC</asp:ListItem>
                                        <asp:ListItem Value="IS" Text="IS">IS</asp:ListItem>
                                        <asp:ListItem Value="BTS" Text="BTS">BTS</asp:ListItem>
                                        <asp:ListItem Value="BAN" Text="BAN">BAN</asp:ListItem>
                                        <asp:ListItem Value="POI" Text="POI">POI</asp:ListItem>
                                        <asp:ListItem Value="Repeater" Text="Repeater">Repeater</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label for="circle">
                                        DG Availability</label>
                                    <span style="color: Red">*</span>
                                    <asp:DropDownList ID="ddlst_dgyn" runat="server" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="select" Text="select">Select</asp:ListItem>
                                        <asp:ListItem Value="1" Text="YES">Yes</asp:ListItem>
                                        <asp:ListItem Value="0" Text="NO">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label for="technology">
                                        SEB Availability</label><span style="color: Red">*</span>
                                    <asp:DropDownList ID="ddlst_sebyn" runat="server" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="select" Text="select">Select</asp:ListItem>
                                        <asp:ListItem Value="1" Text="YES">Yes</asp:ListItem>
                                        <asp:ListItem Value="0" Text="NO">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label for="circle">
                                        Article Status</label>
                                    <span style="color: Red">*</span>
                                    <asp:DropDownList ID="ddlst_articlestatus" runat="server" CssClass="form-control"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="0" Text="--Select Status--">--Select Status--</asp:ListItem>
                                        <asp:ListItem Value="1" Text="Operational">Operational</asp:ListItem>
                                        <asp:ListItem Value="2" Text="Non-Operational">Non-Operational</asp:ListItem>
                                        <asp:ListItem Value="3" Text="Infra Retained">Infra Retained</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label for="technology">
                                        Inventory Status</label>
                                    <span style="color: Red">*</span>
                                    <asp:DropDownList ID="ddlst_inventorystatus" runat="server" CssClass="form-control"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="0" Text="IN">-- Select Inventory Status --</asp:ListItem>
                                        <asp:ListItem Value="1" Text="IN">IN</asp:ListItem>
                                        <asp:ListItem Value="2" Text="OUT">OUT</asp:ListItem>
                                        <asp:ListItem Value="3" Text="ADDED">ADDED</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label for="circle">
                                        Inventory Date (MM/dd/yyyy)</label>
                                    <span style="color: Red">*</span>
                                    <asp:TextBox ID="txt_InventoryDate" runat="server" CssClass="form-control" placeholder="Inventory Date"
                                        ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label for="technology">
                                        Facility ID</label>
                                    <span style="color: Red">*</span>
                                    <asp:TextBox ID="txt_FacID" runat="server" CssClass="form-control" placeholder="Facility ID"></asp:TextBox>
                                </div>
                            </div>


                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:Button ID="btn_SaveDetails" runat="server" Text="Save Details" CssClass="btn btn-primary"
                                        OnClick="btn_SaveDetails_Click" />
                                    <asp:Button ID="btn_resetDetails" runat="server" Text="Reset" CssClass="btn btn-warning"
                                        OnClick="btn_resetDetails_Click" />
                                </div>
                            </div>
                            <div class="col-sm-8">
                                <div class="form-group">
                                    &nbsp;
                                </div>
                            </div>
                        </div>


                    </div>

                    <!-- /Sidebar -->
                </div>
                <hr />
                <div id="overallreport" class="row">                    
                    <div class="col-sm-12 table-responsive">
                        <h3 style="margin-top:0 !important;font-weight:300;" >Sites List</h3>
                        <asp:GridView ID="grdview_InventoryDetails" CssClass="table table-bordered table-striped table-hover"
                            PageSize="10" AllowPaging="true" runat="server" Style="font-size: 12px;" ForeColor="#333333"
                            GridLines="None" OnPageIndexChanging="grdview_InventoryDetails_PageIndexChanging"
                            AutoGenerateColumns="false" OnRowCommand="grdview_InventoryDetails_RowCommand"
                            OnDataBound="grdview_InventoryDetails_DataBound">
                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_InventoryID" runat="server" Text='<%# Eval("InventoryID") %>'
                                            Visible="false"></asp:Label>
                                        <asp:CheckBox ID="chk_Selector" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SiteID" HeaderText="Site ID" />
                                <asp:BoundField DataField="SiteName" HeaderText="Site Name" />
                                <asp:BoundField DataField="CircleName" HeaderText="Circle" />
                                <%-- <asp:BoundField DataField="MEPT" HeaderText="MEPT" />--%>
                                <asp:BoundField DataField="Lat" HeaderText="Lattitude" />
                                <asp:BoundField DataField="Long" HeaderText="Longitude" />
                                <%-- <asp:BoundField DataField="FACID" HeaderText="FACID" />--%>
                                <asp:BoundField DataField="articletype" HeaderText="Site Type" />
                                <%-- <asp:BoundField DataField="DGStatus" HeaderText="DG" />
                                <asp:BoundField DataField="SEBStatus" HeaderText="SEB" />--%>
                                <asp:BoundField DataField="InventoryStatus" HeaderText="Site Status" />
                                <asp:BoundField DataField="ArticleStatus" HeaderText="Article Status" />
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnedit" runat="server" CommandName="edit" CommandArgument='<%# Eval("InventoryID") %>'><i style="color: green" class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btndelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("InventoryID") %>'><i style="color:red;" class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
