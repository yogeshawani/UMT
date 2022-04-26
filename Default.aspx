<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Site Survey</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="Utility Management Tool" content="UMT">
    <meta name="UMT" content="">
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/Login.css" rel="stylesheet" type="text/css" />    
    <script src="js/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">
    // When the DOM is ready, run this function
$(document).ready(function() {
  //Set the carousel options
  $('#quote-carousel').carousel({
    pause: true,
    interval: 4000,
  });
});
    </script>
    <style type="text/css">
    .footer2
    {
        color:#fff;        
    }
    .footer2 a
    {
        color:#fff;
    }
    #Background
    {
        position:fixed;
        top:0px;
        bottom:0px;
        left:0px;
        right:0px;
    background-color:Gray;   
    z-index:999; 
    /*filter:alpha(opacity=40);*/
    opacity:0.9;
    }

#Progress 
{
    position:fixed;
    top:40%;
    left:40%;
    width:170px;
    text-align:center;    
    padding:15px; 
    z-index:1000;   
    font-weight:600;
 }
    </style>
</head>
<body style="background-color:#ddd;">

    <div class="container">
        <div class="row page-header" style="margin-bottom:5%;">
            <div class="col-md-4">
           <%-- <img src="img/UMTlogo3.png" alt="UMT Logo" />--%>
                <h1 style="font-size: 40px !important; font-weight: 600; color: #fff; text-shadow: 2px 0px 3px #000;
                    margin-top: 10%;">
                    <span style="color: #3ab386;">SITE</span><br /> <span style="color: #b10746;">SURVEY</span> <br /> <span style="color: #e7a61a;">TOOL</span></h1>
                    
            </div>
            <div class="col-md-4 col-md-offset-0">
             <form id="form1" runat="server" class="form-horizontal" role="form">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
              <div id="Background" class=""></div>
                    <div id="Progress">                    
                    <img alt="" src="img/squares.gif" />
               </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>                
                    <div class="panel panel-default">
                    <div class="panel-heading">
                        <strong>Login</strong>
                    </div>
                    <div class="panel-body">                       
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">
                                Email</label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txt_Username" runat="server" Placeholder="User Name" CssClass="form-control"></asp:TextBox>                                
                            </div>
                            <div class="col-sm-2">
                            <asp:RequiredFieldValidator ID="rqfv_username" CssClass="text-nowrap" runat="server" style="margin-left:-50%;" Text="*" ForeColor="Red" ErrorMessage="Please provide username" ValidationGroup="Login" ControlToValidate="txt_Username"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group"">
                            <label for="inputPassword3" class="col-sm-3 control-label">
                                Password</label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txt_Password" runat="server" Placeholder="Password" TextMode="Password"
                                    CssClass="form-control"></asp:TextBox>                            
                            </div>
                            <div class="col-sm-2">
                                    <asp:RequiredFieldValidator ID="rqfv_password" runat="server" style="margin-left:-50%;" Text="*" ForeColor="Red" ErrorMessage="Please provide password" ValidationGroup="Login" ControlToValidate="txt_Password"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group last">
                            <div class="col-sm-offset-3 col-sm-9">
                                <asp:Button ID="btn_Login" runat="server" Text="Login" 
                                    CssClass="btn btn-success" onclick="btn_Login_Click" ValidationGroup="Login" />
                                <asp:Button ID="btn_Reset" runat="server" Text="Reset" 
                                    CssClass="btn btn-danger" onclick="btn_Reset_Click" />
                                     
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Login" />
                            </div>
                        </div>
                        
                    </div>
                </div>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Login" />
                </Triggers>
                </asp:UpdatePanel>
                
                </form>
            </div>
            <div class="col-md-4" style="margin-top:10%">
           &nbsp; <%--<a href="#"  class="btn btn-sm btn-primary">Request for Demo</a>--%>
            </div>
        </div>
        <div class='row'>
    <div class='col-md-offset-2 col-md-8'>
      <div class="carousel slide" data-ride="carousel" id="quote-carousel">
        <!-- Bottom Carousel Indicators -->
        <ol class="carousel-indicators">
          <li data-target="#quote-carousel" data-slide-to="0" class="active"></li>
          <li data-target="#quote-carousel" data-slide-to="1"></li>
          <li data-target="#quote-carousel" data-slide-to="2"></li>
          <li data-target="#quote-carousel" data-slide-to="3"></li>
          <li data-target="#quote-carousel" data-slide-to="4"></li>
        </ol>
        
        <!-- Carousel Slides / Quotes -->
        <div class="carousel-inner">
        
          <!-- Quote 1 -->
          <div class="item active">
            
              <div class="row">
                <div class="col-sm-3 text-center">
                  <img class="img-circle" src="http://radiofonteviva.com/images/62642d2e606abb2cb03bc37726137d4e.png" style="width: 150px;height:150px;">                  
                </div>
                <div class="col-sm-9 text-center">
                  <p> <h1 style="font-size: 40px !important; font-weight: 600; color:#fff; margin-top:5%; text-shadow: 2px 2px #000;">
                    <i class="glyphicon glyphicon-th"></i> Mobile friendly Responsive Design </h1></p>                  
                </div>
              </div>
            
          </div>
          <!-- Quote 2 -->
          <div class="item">
            
              <div class="row">
                <div class="col-sm-3 text-center">
                  <img class="img-circle" src="http://12traffic.com/images/12traff/target-world.png" style="width: 150px;height:150px;">
                </div>
                <div class="col-sm-9 text-center">
                  <p> <h1 style="font-size: 40px !important; font-weight: 600; color: #fff; text-shadow: 2px 2px #000;">
                    Auto Geo <i class="glyphicon glyphicon-map-marker"></i> Location Tracker</h1></p>                  
                </div>
              </div>
            
          </div>
          <!-- Quote 3 -->
          <div class="item">
            
              <div class="row">
                <div class="col-sm-3 text-center">
                  <img class="img-circle" src="img/gear-wheel.png" style="width: 150px;height:150px;">
                </div>
                <div class="col-sm-9 text-center">
                  <p>
                  <h1 style="font-size: 40px !important; font-weight: 600; color:#fff; text-shadow: 2px 2px #000;">
                    Hassles free <i class="glyphicon glyphicon-cog"></i> Asset Registration</h1>
                  </p>                  
                </div>
              </div>            
          </div>
          <div class="item">
            
              <div class="row">
                <div class="col-sm-3 text-center">
                  <img class="img-responsive" src="img/gas-station.png" style="width: 150px;height:150px;">
                </div>
                <div class="col-sm-9 text-center">
                  <p>
                  <h1 style="font-size: 40px !important; font-weight: 600; color:#fff; text-shadow: 2px 2px #000;">
                    Fuel Management</h1>
                  </p>                  
                </div>
              </div>            
          </div>
          <div class="item">
            
              <div class="row">
                <div class="col-sm-3 text-center">
                  <img class="img-responsive" src="img/Preventative_Maintenance_Icon.png" style="width: 150px;height:150px;">
                </div>
                <div class="col-sm-9 text-center">
                  <p>
                  <h1 style="font-size: 40px !important; font-weight: 600; color:#fff; text-shadow: 2px 2px #000;">
                    Preventive Maintenance</h1>
                  </p>                  
                </div>
              </div>            
          </div>
        </div>
        
        <!-- Carousel Buttons Next/Prev -->
        <a data-slide="prev" href="#quote-carousel" class="left carousel-control"><i class="fa fa-chevron-left"></i></a>
        <a data-slide="next" href="#quote-carousel" class="right carousel-control"><i class="fa fa-chevron-right"></i></a>
      </div>                          
    </div>
  </div>
    </div>
    <%--<div style='position:absolute;z-index:5;top:45%;left:45%;'>
                 <img id="imgAjax" alt="loading..." title="loading..." src="img/loading.gif" style="width: 100px; height:100px" /><br /> <br />
   </div>--%>
   <div class="container">   
   <div class="row">
   <hr />
   </div>
   </div>
     <footer id="footer" style="color:#fff !important;">
            <div class="footer2">
                <div class="container">
                    <div class="row">
                        <div class="col-md-6 widget">
                            <div class="widget-body">
                                <p class="simplenav">
                                    <a href="#">About Us</a> |
								<a href="#">Terms & Conditions</a> |
								<a href="#">Policies</a> |
								<a href="#">Contact us</a>
                                </p>
                            </div>
                        </div>

                        <div class="col-md-6 widget">
                            <div class="widget-body">
                                <p class="">
                                    Copyright &copy; 2016-17 & Powered by, <a href="#" rel="designer">M B Infotech</a>. Make In India
                                </p>
                            </div>
                        </div>

                    </div>
                    <!-- /row of widgets -->
                </div>
            </div>

        </footer>
</body>
</html>
