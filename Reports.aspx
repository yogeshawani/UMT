<%@ Page Title="UMT: Reports" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Reports.aspx.cs" Inherits="Reports" %>
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
    <div class="container">
        <div class="row">
            <div class="col-sm-12 maincontent">
                <header class="page-header">
                    <h3 class="page-title">Asset Register Reports</h3>
                </header>
            </div>
        </div>
         <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group">
                         <asp:RequiredFieldValidator ID="rqfv_CircleSelect" runat="server" InitialValue="0" ForeColor="red" Display="Dynamic" ErrorMessage="Please Select Circle" ControlToValidate="ddlst_circle" Text="*" ValidationGroup="SiteSelect"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlst_circle" runat="server" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlst_circle_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="-- Select Circle --">-- Select Circle --</asp:ListItem>
                                <asp:ListItem Value="1" Text="Mumbai">Mumbai</asp:ListItem>
                            </asp:DropDownList>
                           
                        </div>
                    </div>
                    <div class="col-sm-3">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="Please Select Site" ControlToValidate="txt_SiteID" Text="*" ValidationGroup="SiteSelect"></asp:RequiredFieldValidator>
                        <div class="form-group">
                            <asp:TextBox ID="txt_SiteID" runat="server" CssClass="form-control" Placeholder="Enter Site ID"></asp:TextBox>                            
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlst_AssetType" runat="server" CssClass="form-control">
                            <asp:ListItem Text="-- Select Asset Type --" Value="-- Select Asset Type"></asp:ListItem>
                            <asp:ListItem Text="Diesel Generator" Value="DG"></asp:ListItem>
                            <asp:ListItem Text="PAC" Value="PAC"></asp:ListItem>
                            <asp:ListItem Text="SMPS" Value="SMPS"></asp:ListItem>
                            <asp:ListItem Text="Battery Bank" Value="BB"></asp:ListItem>
                            <asp:ListItem Text="Tower" Value="Tower"></asp:ListItem>
                            <asp:ListItem Text="Earthing" Value="Earthing"></asp:ListItem>
                            <asp:ListItem Text="AVR" Value="AVR"></asp:ListItem>
                            <asp:ListItem Text="Shelter" Value="Shelter"></asp:ListItem>
                            <asp:ListItem Text="Transformer" Value="Transformer"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-3">
                    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="SiteSelect" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" runat="server" />
                        <div class="form-group">
                            <asp:HiddenField ID="InventoryID" runat="server" />
                            <asp:HiddenField ID="latitudeI" runat="server" />
                            <asp:HiddenField ID="longitudeId" runat="server" />
                            <asp:Button ID="btn_SelectSite" runat="server" CssClass="btn btn-primary" 
                                Text="Show Report" ValidationGroup="SiteSelect" onclick="btn_SelectSite_Click"/>
                                <asp:Button ID="btn_ExportExcel" runat="server" CssClass="btn btn-success" onclick="btn_ExportExcel_Click" Visible="false" 
                                Text="Export to Excel"/>
                        </div>                        
                    </div>
                </div>
         <div class="row">
         <div class="col-sm-12 table-responsive">
         <asp:GridView ID="grdview_Report" CssClass="table table-bordered table-striped table-hover"
                    PageSize="10" AllowPaging="true" runat="server" Style="font-size: 12px;" ForeColor="#333333"
                    GridLines="None" AutoGenerateColumns="true" RowStyle-Wrap="true">
                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" Font-Size="10" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    </asp:GridView>         
         </div>
         </div>
    </div>
</asp:Content>
