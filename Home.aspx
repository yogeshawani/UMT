<%@ Page Title="UMT : Home" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="home" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="container-fluid" style="padding-left: 0px; padding-right: 0px;">
        <!-- Indicators -->
        <script src="js/jssor.slider-21.1.5.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            jssor_1_slider_init = function () {

                var jssor_1_SlideoTransitions = [
              [{ b: -1, d: 1, o: -1 }, { b: 0, d: 1000, o: 1}],
              [{ b: 1900, d: 2000, x: -379, e: { x: 7}}],
              [{ b: 1900, d: 2000, x: -379, e: { x: 7}}],
              [{ b: -1, d: 1, o: -1, r: 288, sX: 9, sY: 9 }, { b: 1000, d: 900, x: -1400, y: -660, o: 1, r: -288, sX: -9, sY: -9, e: { r: 6} }, { b: 1900, d: 1600, x: -200, o: -1, e: { x: 16}}]
            ];

                var jssor_1_options = {
                    $AutoPlay: true,
                    $SlideDuration: 800,
                    $SlideEasing: $Jease$.$OutQuint,
                    $CaptionSliderOptions: {
                        $Class: $JssorCaptionSlideo$,
                        $Transitions: jssor_1_SlideoTransitions
                    },
                    $ArrowNavigatorOptions: {
                        $Class: $JssorArrowNavigator$
                    },
                    $BulletNavigatorOptions: {
                        $Class: $JssorBulletNavigator$
                    }
                };

                var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);

                //responsive code begin
                //you can remove responsive code if you don't want the slider scales while window resizing
                function ScaleSlider() {
                    var refSize = jssor_1_slider.$Elmt.parentNode.clientWidth;
                    if (refSize) {
                        refSize = Math.min(refSize, 1920);
                        jssor_1_slider.$ScaleWidth(refSize);
                    }
                    else {
                        window.setTimeout(ScaleSlider, 30);
                    }
                }
                ScaleSlider();
                $Jssor$.$AddEvent(window, "load", ScaleSlider);
                $Jssor$.$AddEvent(window, "resize", ScaleSlider);
                $Jssor$.$AddEvent(window, "orientationchange", ScaleSlider);
                //responsive code end
            };
        
        </script>
        <style>
            /* jssor slider bullet navigator skin 05 css */
            /*
        .jssorb05 div           (normal)
        .jssorb05 div:hover     (normal mouseover)
        .jssorb05 .av           (active)
        .jssorb05 .av:hover     (active mouseover)
        .jssorb05 .dn           (mousedown)
        */
            .jssorb05
            {
                position: absolute;
            }
            .jssorb05 div, .jssorb05 div:hover, .jssorb05 .av
            {
                position: absolute; /* size of bullet elment */
                width: 16px;
                height: 16px;
                background: url('img/b05.png') no-repeat;
                overflow: hidden;
                cursor: pointer;
            }
            .jssorb05 div
            {
                background-position: -7px -7px;
            }
            .jssorb05 div:hover, .jssorb05 .av:hover
            {
                background-position: -37px -7px;
            }
            .jssorb05 .av
            {
                background-position: -67px -7px;
            }
            .jssorb05 .dn, .jssorb05 .dn:hover
            {
                background-position: -97px -7px;
            }
            
            /* jssor slider arrow navigator skin 22 css */
            /*
        .jssora22l                  (normal)
        .jssora22r                  (normal)
        .jssora22l:hover            (normal mouseover)
        .jssora22r:hover            (normal mouseover)
        .jssora22l.jssora22ldn      (mousedown)
        .jssora22r.jssora22rdn      (mousedown)
        */
        .div {
 background:url(img/bg-pattern.png), linear-gradient(180deg, #663333,#a75454,#c38787);
  /*background-image:url();
  background:url(img/bg-pattern.png),linear-gradient(to left,#7b4397,#dc2430);*/
  height:480px;
}
            .jssora22l, .jssora22r
            {
                display: block;
                position: absolute; /* size of arrow element */
                width: 40px;
                height: 58px;
                cursor: pointer;
                background: url('img/a22.png') center center no-repeat;
                overflow: hidden;
            }
            .jssora22l
            {
                background-position: -10px -31px;
            }
            .jssora22r
            {
                background-position: -70px -31px;
            }
            .jssora22l:hover
            {
                background-position: -130px -31px;
            }
            .jssora22r:hover
            {
                background-position: -190px -31px;
            }
            .jssora22l.jssora22ldn
            {
                background-position: -250px -31px;
            }
            .jssora22r.jssora22rdn
            {
                background-position: -310px -31px;
            }
        </style>
      <%--  <div class="row">
            <div class="col-sm-12">--%>
                <div id="jssor_1" style="position: relative; margin: 0 auto; top: 0px; left: 0px;
                    width: 1300px; height: 500px; overflow: hidden; visibility: hidden;">
                    <!-- Loading Screen -->
                    <div data-u="loading" style="position: absolute; top: 0px; left: 0px;">
                        <div style="filter: alpha(opacity=70); opacity: 0.7; position: absolute; display: block;
                            top: 0px; left: 0px; width: 100%; height: 100%;">
                        </div>
                        <div style="position: absolute; display: block; background: url('img/loading.gif') no-repeat center center;
                            top: 0px; left: 0px; width: 100%; height: 100%;">
                        </div>
                    </div>
                    <div data-u="slides" style="cursor: default; position: relative; top: 0px; left: 0px;
                        width: 1300px; height: 500px; overflow: hidden;">
                        <div data-p="225.00" style="display: none;">
                            <img data-u="image" src="img/red.jpg" />
                            <div style="position: absolute; top: 30px; left: 30px; width: 480px; height: auto;
                                font-size: 50px; color: #ffffff; text-align: center">
                                <b style="/* margin-right: 5px; */padding: 5px; background-color: #3ab386; box-shadow: 1px 2px 4px rgba(0, 0, 0, .5);">
                                    UTILITY</b> <b style="/* margin-right: 5px; */padding: 5px; background-color: #b10746;
                                        box-shadow: 2px 2px rgba(0, 0, 0, .5);">MANAGEMENT</b> <b style="/* margin-right: 5px;
                                            */padding: 5px; background-color: #e7a61a; box-shadow: 1px 2px 4px rgba(0, 0, 0, .5);">
                                            TOOL</b>
                            </div>
                            <div style="position: absolute; top: 300px; left: 30px; width: 480px; height: 120px;
                                font-size: 30px; color: #ffffff; line-height: 38px;">
                                <i class="glyphicon glyphicon-th"></i> Responsive Design , <br /> Auto Geo <i class="glyphicon glyphicon-map-marker"></i>  Location Tracker 
                            </div>
                            <div data-u="caption" data-t="0" style="position: absolute; top: 120px; left: 650px;
                                width: 470px; height: 220px;">
                                <img style="position: absolute; top: 0px; left: 0px; width: 470px; height: 220px;"
                                    src="img/c-phone-horizontal.png" />
                                <div style="position: absolute; top: 4px; left: 45px; width: 379px; height: 213px;
                                    overflow: hidden;">
                                    <img data-u="caption" data-t="1" style="position: absolute; top: 0px; left: 0px;
                                        width: 379px; height: 213px;" src="img/screenshot.PNG" />
                                    <img data-u="caption" data-t="2" style="position: absolute; top: 0px; left: 379px;
                                        width: 379px; height: 213px;" src="img/ss2.jpg" />
                                </div>
                                <img style="position: absolute; top: 4px; left: 45px; width: 379px; height: 213px;"
                                    src="img/c-navigator-horizontal.png" />
                                <img data-u="caption" data-t="3" style="position: absolute; top: 740px; left: 1600px;
                                    width: 257px; height: 300px;" src="img/c-finger-pointing.png" />
                            </div>
                        </div>
                        <div data-p="225.00" style="display: none;">
                            <img data-u="image" src="img/purple.jpg" />
                            <div style="position: absolute; top: 50%; left: 30px; width: 100%; height: auto;
                                font-size: 50px; color: #ffffff; text-align: center">
                                <b style="/* margin-right: 5px; */padding: 5px; background-color: #3ab386; box-shadow: 1px 2px 4px rgba(0, 0, 0, .5);">
                                    UTILITY</b> <b style="/* margin-right: 5px; */padding: 5px; background-color: #b10746;
                                        box-shadow: 2px 2px rgba(0, 0, 0, .5);">MANAGEMENT</b> <b style="/* margin-right: 5px;
                                            */padding: 5px; background-color: #e7a61a; box-shadow: 1px 2px 4px rgba(0, 0, 0, .5);">
                                            TOOL</b>
                            </div>
                        </div>                       
                        <div data-p="225.00" data-po="80% 55%" style="display: none;">
                            <img data-u="image" src="img/blue.jpg" />
                        </div>
                    </div>
                    <!-- Bullet Navigator -->
                    <div data-u="navigator" class="jssorb05" style="bottom: 16px; right: 16px;" data-autocenter="1">
                        <!-- bullet navigator item prototype -->
                        <div data-u="prototype" style="width: 16px; height: 16px;">
                        </div>
                    </div>
                    <!-- Arrow Navigator -->
                    <span data-u="arrowleft" class="jssora22l" style="top: 0px; left: 12px; width: 40px;
                        height: 58px;" data-autocenter="2"></span><span data-u="arrowright" class="jssora22r"
                            style="top: 0px; right: 12px; width: 40px; height: 58px;" data-autocenter="2">
                    </span>
                </div>
            <%--</div>
        </div>--%>
        <div class="div">
        &nbsp;
        </div>
        <script type="text/javascript">            jssor_1_slider_init();</script>
    </div>
</asp:Content>
