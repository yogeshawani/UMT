<%@ Page Title="UMT: SEB" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="seb.aspx.cs" Inherits="seb" %>

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
                $("[id$=txt_EBConnectedDate]").datepicker({ dateFormat: 'dd-mm-yy' });
                $("[id$=txt_BillDate]").datepicker({ dateFormat: 'dd-mm-yy' });
                $("[id$=txt_BillFromDate]").datepicker({ dateFormat: 'dd-mm-yy' });
                $("[id$=txt_BillToDate]").datepicker({ dateFormat: 'dd-mm-yy' });
                $("[id$=txt_BillDueDate]").datepicker({ dateFormat: 'dd-mm-yy' });
                $("[id$=txt_AccountableBillRecievedDate]").datepicker({ dateFormat: 'dd-mm-yy' });
                $("[id$=txt_EBCordinatorBillReceivedDate]").datepicker({ dateFormat: 'dd-mm-yy' });

                $("[id$=txt_PaymentSubmissionDateByOSE]").datepicker({ dateFormat: 'dd-mm-yy' });
                $("[id$=txt_ReconnectionDate]").datepicker({ dateFormat: 'dd-mm-yy' });
                $("[id$=txt_BillSubmittedDate]").datepicker({ dateFormat: 'dd-mm-yy' });
                $("[id$=txt_PaymentInstRec]").datepicker({ dateFormat: 'dd-mm-yy' });
                $("[id$=txt_RevisedBillDueDate]").datepicker({ dateFormat: 'dd-mm-yy' });
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
    <div class="container">
        <div class="row">
            <div class="col-sm-12 maincontent">
                <header class="page-header">
                    <h3 class="page-title">SEB</h3>
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
                            <asp:Button ID="btn_SelectSite" runat="server" CssClass="btn btn-primary" OnClick="btn_SelectSite_Click" Text="Select Site"
                                ValidationGroup="SiteSelect" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<hr style="margin-top: 0px; margin-bottom: 5px;" />--%>
        <div class="row">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="col-sm-10" style="border-right: solid 1px #efefef">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <b>Payment Initiation</b></div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="circle">
                                                Consumer No</label>
                                            <asp:HiddenField runat="server" ID="hdn_SebID" Value="0" />
                                            <asp:TextBox ID="txt_ConsumerNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="circle">
                                                Customer</label>
                                            <asp:TextBox ID="txt_Customer" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <small>
                                                <label for="circle">
                                                    EB Connected Date (MM/dd/yyyy)</label></small>
                                            <asp:TextBox ID="txt_EBConnectedDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Meter No</label>
                                            <asp:TextBox ID="txt_MeterNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                No Of Operator</label>
                                            <asp:TextBox ID="txt_NoOfOperator" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Intrest Earned on Deposite</label>
                                            <asp:TextBox ID="txt_InterestEarnedOnDeposite" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                SEB Status</label>
                                            <asp:TextBox ID="txt_SEBStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Projected Bill Collection</label>
                                            <asp:TextBox ID="txt_ProjectedBillCollection" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Month</label>
                                            <asp:DropDownList ID="ddlst_Month" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Select Month" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="JAN" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="FEB" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="MAR" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="APR" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="MAY" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="JUN" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="JUL" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="AUG" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="SEP" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="OCT" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="NOV" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="DEC" Value="12"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Year</label>
                                            <asp:DropDownList ID="ddlst_Year" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Select Year" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="2014" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="2015" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="2016" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="2017" Value="4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Actual Bill Date (MM/dd/yyyy)</label>
                                            <asp:TextBox ID="txt_BillDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Current Reading</label>
                                            <asp:TextBox ID="txt_CurrentReading" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Previous Reading</label>
                                            <asp:TextBox ID="txt_PrevStartingReading" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Total Units Reading</label>
                                            <asp:TextBox ID="txt_TotalUnitsConsumed" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Bill From date(MM/dd/yyyy)
                                            </label>
                                            <asp:TextBox ID="txt_BillFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Bill To Date (MM/dd/yyyy)</label>
                                            <asp:TextBox ID="txt_BillToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Bill Due Date (MM/dd/yyyy)</label>
                                            <asp:TextBox ID="txt_BillDueDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                <small>Revised Bill Due Date (MM/dd/yyyy)</small></label>
                                            <asp:TextBox ID="txt_RevisedBillDueDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Bill Type</label>
                                            <asp:TextBox ID="txt_BillType" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Connection Category
                                            </label>
                                            <asp:TextBox ID="txt_ConnectionCategory" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Billing Cycle
                                            </label>
                                            <asp:DropDownList ID="ddlst_BillingCycle" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Monthly" Value="MONTHLY"></asp:ListItem>
                                                <asp:ListItem Text="Quaterly" Value="QUATERLY"></asp:ListItem>
                                                <asp:ListItem Text="Yearly" Value="YEARLY"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Unit Type
                                            </label>
                                            <asp:TextBox ID ="txt_UnitType" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Current Month Amount
                                            </label>
                                            <asp:TextBox ID="txt_CurrentMonthAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Adjustment Amount</label>
                                            <asp:TextBox ID="txt_AdjustmentAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Total Bill Amount</label>
                                            <asp:TextBox ID="txt_TotalBillAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Final Amount</label>
                                            <asp:TextBox ID="txt_FinalAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Arrear Amount
                                            </label>
                                            <asp:TextBox ID="txt_ArrearAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Payment Batch No</label>
                                            <asp:TextBox ID="txt_PaymentBatchNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <small>
                                                <label for="circle">
                                                    Accountable Bill Recieved Date</label></small>
                                            <asp:TextBox ID="txt_AccountableBillRecievedDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Sent to Bank for Processing</label>
                                            <asp:DropDownList ID="ddlst_BankProsessingYesNo" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="-- Select --" Value="0">-- Select --</asp:ListItem>
                                                <asp:ListItem Text="YES" Value="YES">YES</asp:ListItem>
                                                <asp:ListItem Text="NO" Value="NO">NO</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="circle">
                                                Area Manager Remarks</label>
                                            <asp:TextBox ID="txt_AreaManagerRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="circle">
                                                Area Manager Approval</label>
                                            <asp:TextBox ID="txt_AreaManagerApproval" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <b>SME Approval</b></div>
                            <div class="panel-body">
                             <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                P & F SME Remarks</label>
                                            <asp:TextBox ID="txt_PnfSMERemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Final Remarks</label>
                                            <asp:TextBox ID="txt_FinalRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Underprotest Amount</label>
                                            <asp:TextBox ID="txt_UnProtestAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Payment Mode</label>
                                            <asp:TextBox ID="txt_PaymentMode" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-warning">
                            <div class="panel-heading">
                                <b>Payment Receipt</b></div>
                            <div class="panel-body">
                            <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                DD/RTGS No</label>
                                            <asp:TextBox ID="txt_DDRTGSNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                <small>Final Payment Receipt Amount</small></label>
                                            <asp:TextBox ID="txt_FinalPaymentReceiptAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Bill Submitted Date</label>
                                            <asp:TextBox ID="txt_BillSubmittedDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Payment Instrument Received</label>
                                            <asp:TextBox ID="txt_PaymentInstRec" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            <div class="row">
                            <div class="col-sm-3">
                            <div class="form-group">
                                            <label for="circle">
                                                Final Amount</label>
                                            <asp:TextBox ID="txt_FinAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                            </div>
                            </div>
                            </div>
                        </div>
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <b>Payment Submission</b></div>
                            <div class="panel-body">
                            <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                               <small>Disconnected due to non-payment</small></label>
                                            <asp:TextBox ID="txt_DisconnectedDueToNonPayment" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                <small>Reconnection Date</small></label>
                                            <asp:TextBox ID="txt_ReconnectionDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Reconnection Charges Paid</label>
                                            <asp:TextBox ID="txt_ReconnectionChargesPaid" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                              <small>Payment Submission Date By OSE</small></label>
                                            <asp:TextBox ID="txt_PaymentSubmissionDateByOSE" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            <div class="row">
                             <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                               <small>Payment Receipt from EB</small></label>
                                            <asp:TextBox ID="txt_PayRecFromEB" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                <small>Receipt No</small></label>
                                            <asp:TextBox ID="txt_ReciptNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <label for="circle">
                                                Upload Receipt</label>
                                            <asp:FileUpload ID="flupload_PaymentReceipt" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                            </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <asp:Button ID="btn_SebSaveDetails" runat="server" Text="Save Partial" OnClick="btn_SebSaveDetails_Click" CssClass="btn btn-info" />
                                    <asp:Button ID="btn_SebFinalSubmit" runat="server" Text="Final Submission" OnClick="btn_SebFinalSubmit_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btn_SebResetDetails" runat="server" Text="Reset" OnClick="btn_SebResetDetails_Click" CssClass="btn btn-warning" />
                                    <asp:HyperLink  ID="btn_back" runat="server" Text="Back" NavigateUrl="~/SEBList.aspx"  CssClass="btn btn-warning" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="col-sm-2">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Reports</h3>
                    </div>
                    <div class="panel-body">
                        <ul class="list-unstyled list-spaces">
                            <li><a href="SEBList.aspx">SEB Report</a></li>
                            <li><a href="#">Receipt Uploading</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
