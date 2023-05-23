<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="topFrame.aspx.cs" Inherits="SPC.WebUI.Resources.frames.topFrame" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link type="text/css" rel="stylesheet" href="~/Resources/jquery/jquery-ui-1.10.4.custom.min.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/icon.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/font.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/app.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/unresponsive.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/spc.css" />
    <!--[if lt IE 9]>
    <script src="../script/ie/html5shiv.js"></script>
    <script src="../script/ie/respond.min.js"></script>
    <script src="../script/ie/excanvas.js"></script>
    <![endif]-->
    <script type="text/javascript" src="../jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jquery/jquery-ui-1.10.4.custom.min.js"></script>
    <script type="text/javascript" src="../scripts/message/Message.js"></script>
    <script type="text/javascript" src="../scripts/js/SPCCommon.js"></script>
    <script type="text/javascript" src="../scripts/js/app.js"></script>
    <script type="text/javascript" src="../scripts/js/app.plugin.js"></script>
    <script type="text/javascript" src="../scripts/js/bootstrap.js"></script>
    <style type="text/css">
        html, body {
            height: 100%;
            padding: 0px;
            margin: 0px;
        }
        body{
            width: auto !important;  /* Firefox will set width as auto */
            width:100%;              /* As IE ignores !important it will set width as 1000px; */
        }
        /*.header-tr > td > a:hover {
            background-color: #f2f4f8;
        }*/
    </style>
    <script type="text/javascript">
        function doSetMenu(code) {
            parent.doRendorMenu(code);
        }

        var arrWidth = [];
        var td_idx = 0;
        var td_curr = 0;
        var td_margin = 0;

        $(window).load(function () {
            doToggleArrow();

            $(".header-tr").find('td').each(function () {
                arrWidth[td_idx] = parseInt($(this).width(), 10) + 8;
                td_idx++;
            });
        });
        $(window).resize(function () {
            doToggleArrow();

            $("#menu-s").animate({ 'marginLeft': "0px" });
        });

        function doToggleArrow() {
            var menu_m = $("#menu-m");
            var menu_s = $("#menu-s");
            var menu_l = $("#menu-l");
            var menu_r = $("#menu-r");

            if (menu_s.width() > menu_m.width()) {
                menu_l.show();
                menu_r.show();
            } else {
                menu_l.hide();
                menu_r.hide();
            }
        }

        function fn_slideMenu(t) {
            var menu_m = $("#menu-m");
            var menu_s = $("#menu-s");
            var spacer = parseInt(menu_s.width() - menu_m.width(), 10);

            if (t == 'l' && td_curr > 0) {
                $("#menu-s").animate({ 'marginLeft': "+=" + arrWidth[td_curr - 1] + "px" });
                td_margin -= arrWidth[td_curr - 1];
                td_curr--;
            } else if (t == 'r' && menu_s.width() - td_margin > menu_m.width()) {
                $("#menu-s").animate({ 'marginLeft': "-=" + arrWidth[td_curr] + "px" });
                td_margin += arrWidth[td_curr];
                td_curr++;
            }
        }

        function fn_OnChangePassword() {
            parent.getMainFrameObj().fn_OnCreateTab('MM00|MM0000|COMM|COMM0201|비밀번호변경|S|1');
        }
    </script>
</head>
<body class="unresponsive">
    <form id="form1" runat="server">
        <table style="width:100%;height:50px;table-layout:fixed;">
            <tr class="bg-white">
                <td style="width:240px;min-width:243px;padding-left:5px;" class="text-nowrap bg-primary dk">
                    <!--logo-->
                    <a href="javascript:parent.doRendorContent();" style="font-size: 20px; display: inline-block; font-weight: 700; line-height: 36px;">
                        <img src="../images/logo_white.png" class="logo m-r-sm" alt="<%=gsCOMPNM%> SPC" style="width:30px; height:30px;margin-left:3px;margin-right:0px;" />
                        <span style="letter-spacing:-2px;color:#FFFFFF;"><%=gsCOMPNM%></span>
                    </a>
                </td>
                <td id="menu-l" style="width:30px;text-align:center;" class="bg-primary">
                    <button class="btn btn-lg btn-primary" style="padding-left:5px;padding-right:5px;height:50px;" onclick="fn_slideMenu('l'); return false;">
                        <i class="fa fa-arrow-circle-o-left"></i>
                    </button>
                </td>
                <td id="menu-m" style="overflow:hidden;padding-left:5px;" class="bg-primary">
                    <table id="menu-s" style="border:0px solid #ffffff">
                        <tr class="header-tr">
                            <asp:Repeater ID="rptMenu" runat="server" OnItemDataBound="rptMenu_ItemDataBound">
                                <ItemTemplate>
                                    <asp:Literal ID="content" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                    </table>
                </td>
                <td id="menu-r" style="width:30px;text-align:center;" class="bg-primary">
                    <button class="btn btn-lg btn-primary" style="padding-left:5px;padding-right:5px;height:50px;" onclick="fn_slideMenu('r'); return false;">
                        <i class="fa fa-arrow-circle-o-right"></i>
                    </button>
                </td>
                <%if (gsENCRTPTPW.Equals("1") && !gsDEV.Equals("1")) {%>
                <td style="width:110px;text-align:right;padding-right:5px;" class="bg-primary">
                    <button class="btn btn-sm btn-primary" onclick="fn_OnChangePassword(); return false;">
                        <i class="i i-settings"></i>
                        <span class="text">비밀번호변경</span>
                    </button>
                </td>
                <%}%>
                <td style="width:85px;text-align:right;padding-right:5px;" class="bg-primary">
                    <button class="btn btn-sm btn-primary" onclick="parent.doLogout(); return false;">
                        <i class="glyphicon glyphicon-log-out"></i>
                        <span class="text">Logout</span>
                    </button>
                </td>
            </tr>
        </table>
        
        <%--<section class="vbox">
            <header class="bg-white header header-md navbar navbar-fixed-top-xs box-shadow" style="overflow-x:hidden;">
                <div style="width: 240px; float: left; padding-top: 5px;">
                    
                </div>
                <!--menu-->  
                <div style="float: left;">
                    
                </div>
                <div class="topbutton" style="float: right; padding-top: 10px; padding-right: 5px;">
                    
                </div>
            </header>
        </section>--%>
    </form>
</body>
</html>
