<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="leftFrame.aspx.cs" Inherits="SPC.WebUI.Resources.frames.leftFrame" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" class="app">
<head runat="server">
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
    <script type="text/javascript" src="../scripts/js/SPCPopup.js"></script>
    <script type="text/javascript" src="../scripts/js/app.js"></script>
    <script type="text/javascript" src="../scripts/js/app.plugin.js"></script>
    <script type="text/javascript" src="../scripts/js/bootstrap.js"></script>
    <style type="text/css">
        html, body {
            height: 100%;
            padding: 0px;
            margin: 0px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var menuAnchor = $("nav").children("ul").children("li").children("a");
            $(menuAnchor).each(function (i) {
                if ($(this).attr("href") == "#") {
                    $(this).bind("click", function () { fnToggleIcon($(this), i); });
                }
            });
        });

        $(window).bind({
            load: function () {
                $('#scrollSection').height($('.vbox').height() - $('.footer').height());
            },
            resize: function () {
                $('#scrollSection').height($('.vbox').height() - $('.footer').height());
            }
        });

        function fnToggleIcon(obj, idx) {
            var menuAnchor = $("nav").children("ul").children("li").children("a");

            $(menuAnchor).each(function (i) {
                if ($(this).attr("href") == "#" && i != idx) {
                    if ($(this).children("i").attr("class") == "i i-folder-minus icon") {
                        $(this).children("i").attr("class", "i i-folder-plus icon");
                    }
                }
            });

            var menuIcon = $(obj).children("i");
            if (menuIcon.attr("class").indexOf("plus") >= 0) {
                menuIcon.removeAttr("class");
                menuIcon.attr("class", "i i-folder-minus icon");
            } else if (menuIcon.attr("class").indexOf("minus") >= 0) {
                menuIcon.removeAttr("class");
                menuIcon.attr("class", "i i-folder-plus icon");
            }
        }

        function doSetMidMenu(code, text) {
            //var _topFrame = parent.document.getElementById('topFrame');
            //var innerDoc = _topFrame.contentDocument || _topFrame.contentWindow.document;

            //var LeftMenuCD = innerDoc.getElementById('hidLeftMenuCD');
            //var LeftMenuNM = innerDoc.getElementById('hidLeftMenuNM');

            //LeftMenuCD.value = code;
            //LeftMenuNM.value = text;
        }

        function doCreateTab(oParams) {
            parent.doCreateTab(oParams);
        }

        function fn_OnDownloadMessenger(t) {
            var file = (t == 'admin') ? '관리자' : '작업자';
            if (!confirm('DM' + file + '프로그램을 다운로드 하시겠습니까?\r사용권한이 있는 사용자만 다운로드 하세요!!\r\n계속 진행하려면 확인을 누르세요'))
                return;
            else
                window.open('../../CTF_FILES/MESSENGER/DM' + file + '프로그램.exe');
        }

        function fn_OnDownloadSilverLight(ver) {
            if (!confirm('실버라이트 ' + ver + '비트용 프로그램을 다운로드 하시겠습니까?\r\n계속 진행하려면 확인을 누르세요\r\n다운로드 후 압축을 풀고 설치하세요!!'))
                return;
            else
                window.open('../../API/Common/FileDownload.ashx?attfolder=SILVERLIGHT&attfilename=Silverlight_x' + ver + '.zip');
        }
    </script>
</head>
<body class="bg-light unresponsive">
    <section class="vbox" style="height: 100%;">
        <section class="hbox stretch" style="height: 100%;">
            <!-- .aside -->
            <aside class="bg-light aside-md hidden-print" id="nav" style="height: 100%;">
                <section class="vbox" style="height: 100%;">
                    <section id="scrollSection" class="w-f scrollable" style="height: 100%;">
                        <div data-height="auto" data-disable-fade-out="true" data-distance="0" data-size="10px" data-railopacity="0.2">
                            <div class="clearfix wrapper dk" style="height:82px;">
                                <div class="dropdown">
                                    <span class="thumb avatar pull-left m-r">
                                        <img src="../images/a0.gif" class="dker" alt="..." />
                                        <i class="on md b-light"></i>
                                    </span>
                                    <span class="hidden-nav-xs clear">
                                        <span class="block m-t-xs">
                                            <strong class="font-bold text-lt text-lg"><%=gsUSERNM%></strong>
                                        </span>
                                        <span class="text-muted text-xs block"><%=gsDEPARTNM%></span>
                                    </span>
                                </div>
                            </div>
                            <!-- nav -->
                            <form id="form1" runat="server">
                                <nav class="nav-primary">
                                    <%if (!String.IsNullOrEmpty(m_sTopMenuCD)) {%>
                                    <div class="hidden-nav-xs padder m-t-sm m-b-sm font-bold text-black"><%=m_sTopMenuNM%></div>
                                    <ul class="nav nav-main" data-ride="collapse">
                                        <asp:Repeater ID="rpt_Menu1" runat="server" OnItemDataBound="rpt_Menu1_ItemDataBound">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Literal ID="literal1" runat="server"></asp:Literal>
                                                    <ul class="nav dk">
                                                        <asp:Repeater ID="rpt_Menu2" runat="server">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="literal2" runat="server"></asp:Literal>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                    <div class="line dk hidden-nav-xs"></div>
                                    <%}%>
                                    <div class="hidden-nav-xs padder m-t-sm m-b-sm font-bold text-black">Quick Menu</div>
                                    <ul class="nav">
                                        <li>
                                            <a href="http://sr2.cyberwin.co.kr/ctfSR/login.asp?CGB=2&CID=<%=gsCSRCOMPCD%>&CPW=&CNM=<%=gsUSERNM%>" target="_blank" title="사이트 오류 등 기능 문의">
                                                <i class="i i-sm i-question text-info-dk"></i>
                                                <span>CSR</span>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="http://ezh.kr/ctf/" target="_blank" title="원격지원 요청">
                                                <i class="i i-sm i-domain3 text-danger-dk"></i>
                                                <span>Help Desk</span>
                                            </a>
                                        </li>
                                        <%if (Convert.ToBoolean(gsUSEBOARD) == true) {%>
                                        <li>
                                            <a href="javascript:doCreateTab('MM00|MM0000|BORD|BORD0101|게시판|RC|1');" title="게시판">
                                                <i class="fa i-sm fa-comment text-success-dk"></i>
                                                <span>게시판</span>
                                            </a>
                                        </li>
                                        <%}%>
                                    </ul>
                                    <div class="line dk hidden-nav-xs"></div>
                                    <div class="hidden-nav-xs padder m-t-sm m-b-sm font-bold text-black">실버라이트</div>
                                    <ul class="nav">
                                        <li>
                                            <a href="javascript:fn_OnDownloadSilverLight('32');">
                                                <i class="i i-sm i-user3 text-info-dk"></i>
                                                <span>32비트용 실버라이트</span>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="javascript:fn_OnDownloadSilverLight('64');">
                                                <i class="i i-sm i-users3 text-danger-dk"></i>
                                                <span>64비트용 실버라이트</span>
                                            </a>
                                        </li>
                                    </ul>
                                    <%--<div class="line dk hidden-nav-xs"></div>
                                    <div class="hidden-nav-xs padder m-t-sm m-b-sm font-bold text-black">DM 프로그램</div>
                                    <ul class="nav">
                                        <li>
                                            <a href="javascript:fn_OnDownloadMessenger('admin');">
                                                <i class="i i-sm i-user3 text-info-dk"></i>
                                                <span>DM관리자프로그램</span>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="javascript:fn_OnDownloadMessenger('worker');">
                                                <i class="i i-sm i-users3 text-danger-dk"></i>
                                                <span>DM작업자프로그램 </span>
                                            </a>
                                        </li>
                                    </ul>--%>
                                    <asp:PlaceHolder ID="pHolderFavorites" runat="server" />
                                </nav>
                            </form>
                            <!-- / nav -->
                        </div>
                    </section>
                    <%if (!String.IsNullOrEmpty(m_sLastJoinInfo)) {%>
                    <footer class="footer text-muted text-xs dk" style="padding-left:15px;">
                        <p><%=m_sLastJoinInfo%></p>
                    </footer>
                    <%}%>
                </section>
            </aside>
            <!-- /.aside -->
        </section>
    </section>
</body>
</html>
