<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tabFrame.aspx.cs" Inherits="SPC.WebUI.Resources.frames.tabFrame" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="~/Resources/jquery/jquery-ui-1.10.4.custom.min.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/icon.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/font.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/app.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/unresponsive.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/css/spc.css" />
    <!--[if lt IE 9]>
    <script src="<%#Page.ResolveUrl("~/Resources/scripts/ie/html5shiv.js")%>"></script>
    <script src="<%#Page.ResolveUrl("~/Resources/scripts/ie/respond.min.js")%>"></script>
    <script src="<%#Page.ResolveUrl("~/Resources/scripts/ie/excanvas.js")%>"></script>
    <![endif]-->
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/jquery/jquery-1.10.2.min.js")%>"></script>
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/jquery/jquery-ui-1.10.4.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/scripts/js/bootstrap.js")%>"></script>
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/scripts/js/app.js")%>"></script>
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/scripts/js/app.plugin.js")%>"></script>
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/scripts/js/SPCCommon.js")%>"></script>
    <style type="text/css">
        html, body {
            height: 100%;
            padding: 0px;
            margin: 0px;
        }
        .iframeContent {
            width: 100%;
            padding: 0px;
            margin: 0px;
            border-width: 0px;
            border-spacing: 0px;
            overflow-x: hidden;
        }
    </style>
    <script type="text/javascript">
        var navTabCount = 0;
        var navTabIndex = 0;
        var navTabMinIndex = 0;
        var navTabPGMID = [];

        // 동적으로 높이값을 계산한다
        function fn_TabAdjustSize() {
            var height = Math.max(0, $(document.body).height());
            var width = Math.max(0, $(document.body).width());
            var tabHeight = Math.max(0, $(".nav-tabs").height());
            $(".iframeContent").width(width);
            $(".iframeContent").height(height - tabHeight);
        }

        function fn_GetIFrameID() {
            var obj = null;
            $(".iframeContent").each(function (idx) {
                if (parseInt($(this).attr("idx"), 10) == navTabIndex) {
                    obj = $(this).attr("pgmid");
                }
            });

            return obj;
        }

        function fn_GetIFrameObj() {
            var obj = null;
            $(".iframeContent").each(function (idx) {
                if (parseInt($(this).attr("idx"), 10) == navTabIndex) {
                    obj = $(this)[0].contentWindow;
                }
            });
            
            return obj;
        }

        function fn_OnActivenavTabIndex(obj) {
            navTabIndex = parseInt($(obj).attr("idx"), 10);
        }

        function fn_OnActiveTab(id) {
            fn_OnClearActiveTab();

            $(".nav-tabs").children("#li-" + id).addClass("active");
            $(".tab-content").children("#" + id).addClass("active");

            navTabIndex = parseInt($(".nav-tabs").children("#li-" + id).attr("idx"), 10);
        }

        function fn_OnClearActiveTab() {
            $(".nav-tabs").children("li").each(function (idx) { $(this).removeClass("active"); });
            $(".tab-content").children("div").each(function (idx) { $(this).removeClass("active"); });
        }

        function fn_OnCreateTab(oParam, oSetParam) {
            if (navTabCount > 9) {
                alert('페이지는 최대 10개까지 열 수 있습니다.\r한 개 이상의 페이지를 닫으세요');
                return false;
            }

            // Cleare Active Tab
            fn_OnClearActiveTab();

            var oParams = oParam.split('|');
            var F_MODULE1 = oParams[0];
            var F_MODULE2 = oParams[1];
            var F_LINK = oParams[2];
            var F_PGMID = oParams[3];
            var F_PGMNM = oParams[4];

            // When Exists Tab Then ActiveTab
            var bExists = false;
            $(".nav-tabs").children("li").each(function () { if ($(this).attr("id") == "li-" + F_PGMID) { bExists = true; return false; } });

            if (bExists) {
                fn_OnActiveTab(F_PGMID);
                return;
            }

            // 탭 ID 배열
            fn_ArrayAdd(navTabPGMID, navTabCount, F_PGMID);

            // 배열초기화(Tree Node Click 에서 사용함)
            fn_ArrayAdd(parent.isBanUserControl, F_PGMID, false);
            fn_ArrayAdd(parent.isLineUserControl, F_PGMID, false);
            fn_ArrayAdd(parent.isItemUserControl, F_PGMID, false);
            fn_ArrayAdd(parent.isWorkUserControl, F_PGMID, false);
            fn_ArrayAdd(parent.isInspectionUserControl, F_PGMID, false);
            
            // 헤더 생성
            var imgSrc = '<%#Page.ResolveUrl("~/Resources/Images/closeBtn.jpg")%>'
            $(".nav-tabs").append(
                '<li idx="' + navTabCount + '" id="li-' + F_PGMID + '" class="active" style="background-color:#D1D5D8;">' +
                '<a idx="' + navTabCount + '" id="a-' + F_PGMID + '" href="#' + F_PGMID + '" data-toggle="tab" style="padding:5px;"><span idx="' + navTabCount + '" style="font-weight:bold;">' + F_PGMNM + '</span>&nbsp<i idx="' + navTabCount + '" class="fa fa-times-circle-o" style="cursor:pointer;" title="현재탭 닫기" /></a>' +
                '<span></span>' +
                '</li>');
            $("#a-" + F_PGMID).bind('click', function () { fn_OnActivenavTabIndex(this); });
            $("#a-" + F_PGMID).children("i").bind('click', function () { fn_OnDeleteTab(F_PGMID, this); });
            // 컨텐츠 생성
            $(".tab-content").append(
                '<div class="tab-pane active" id="' + F_PGMID + '">' +
                '<iframe idx="' + navTabCount + '" pgmid="' + F_PGMID + '" id="iframe-' + F_PGMID + '" class="iframeContent" src="../../Pages/' + F_LINK + '/' + F_PGMID + '.aspx?pParam=' + oParam + '&oSetParam=' + oSetParam + '" onload="fn_TabAdjustSize()" frameborder="0" />' +
                '</div>');
            navTabIndex = navTabCount;
            navTabCount++;
        }

        function fn_OnDeleteTab(id, obj) {
            if ($(".nav-tabs").children("#li-" + id).length > 0) {
                fn_OnTabActive(id, obj);
                // 헤더 제거
                $(".nav-tabs").children("#li-" + id).remove();
                // 컨텐츠 제거
                $(".tab-content").children("#" + id).children("#iframe-" + id).remove();
                $(".tab-content").children("#" + id).remove();
                // 배열제거
                fn_ArrayDelete(parent.isBanUserControl, id);
                fn_ArrayDelete(parent.isLineUserControl, id);
                fn_ArrayDelete(parent.isItemUserControl, id);
                fn_ArrayDelete(parent.isWorkUserControl, id);
                fn_ArrayDelete(parent.isInspectionUserControl, id);

                navTabCount--;
            }
        }

        function fn_OnTabActive(id, obj) {
            var nextID = '', prevID = '';
            var index = parseInt($(obj).attr("idx"), 10);

            if (navTabIndex == index && navTabCount > 1) {
                if (index == navTabMinIndex) {
                    nextID = $(".nav-tabs").children("#li-" + id).next().attr("id").replace("li-", "");
                    fn_OnActiveTab(nextID);
                } else {
                    prevID = $(".nav-tabs").children("#li-" + id).prev().attr("id").replace("li-", "");
                    if (prevID != 'menuClose' && prevID != 'allClose')
                        fn_OnActiveTab(prevID);
                }
            }
            
            if (index == navTabMinIndex && navTabCount > 1 && nextID != '') navTabMinIndex = parseInt($(".nav-tabs").children("#li-" + nextID).attr("idx"), 10);
        }

        function fn_OnGetTabObject(id) {
            return $(".nav-tabs").children("#li-" + id)
        }

        function fn_OnTabAllDelete() {
            if (navTabPGMID.length > 0) {
                $.each(navTabPGMID, function (key, value) {
                    fn_OnDeleteTab(value, fn_OnGetTabObject(value));
                });
            }
        }

        function doToggleLeftFrame() {
            parent.doToggleFrame('left');

            var bActiveMenu = parent.doGetLeftActive();

            if (!bActiveMenu) {
                $("#toggleLeft").attr("title", "메뉴열기");
                $("#toggleLeft").children("#closeLeft").removeClass("text");
                $("#toggleLeft").children("#closeLeft").addClass("text-active");
                $("#toggleLeft").children("#openLeft").removeClass("text-active");
                $("#toggleLeft").children("#openLeft").addClass("text");
            } else {
                $("#toggleLeft").attr("title", "메뉴닫기");
                $("#toggleLeft").children("#closeLeft").removeClass("text-active");
                $("#toggleLeft").children("#closeLeft").addClass("text");
                $("#toggleLeft").children("#openLeft").removeClass("text");
                $("#toggleLeft").children("#openLeft").addClass("text-active");
            }
        }

        function doToggleRightFrame() {
            parent.doToggleFrame('right');

            var bActiveMenu = parent.doGetRightActive();
            
            if (!bActiveMenu) {
                $("#toggleRight").attr("title", "탐색창열기");
                $("#toggleRight").children("#closeRight").removeClass("text-active");
                $("#toggleRight").children("#closeRight").addClass("text");
                $("#toggleRight").children("#openRight").removeClass("text");
                $("#toggleRight").children("#openRight").addClass("text-active");
            } else {
                $("#toggleRight").attr("title", "탐색창닫기");
                $("#toggleRight").children("#closeRight").removeClass("text");
                $("#toggleRight").children("#closeRight").addClass("text-active");
                $("#toggleRight").children("#openRight").removeClass("text-active");
                $("#toggleRight").children("#openRight").addClass("text");
            }
        }

        $(window).bind({
            load: function () {
                fn_TabAdjustSize();
            },
            resize: function () {
                fn_TabAdjustSize();
            }
        });

        $(document).ready(function () {
            fn_OnCreateTab('MM00|MM0000|COMM|COMM0101|공지사항|R|0');
            //fn_OnCreateTab('MM00|MM0000|COMM|COMM0102|공지사항|RCWSD|1');
        });
    </script>
</head>
<body style="border-left: solid 2px #eaeef1; overflow:hidden;">
    <form id="form1" runat="server">
        <section class="panel panel-default">
            <header class="panel-heading bg-light">
                <ul class="nav nav-tabs">
                    <li class="active">
                        <a id="toggleLeft" href="javascript:doToggleLeftFrame();" style="padding: 5px;" title="메뉴닫기">
                            <i id="closeLeft" class="i i-circleleft text"></i>
                            <i id="openLeft" class="i i-circleright text-active"></i>
                        </a>
                    </li>
                    <li class="active" style="background-color: #D1D5D8; font-weight: bold;">
                        <a href="javascript:fn_OnTabAllDelete();" style="padding: 5px;" title="전체닫기">전체닫기<i class="fa fa-times-circle-o text"></i>
                        </a>
                    </li>
                    <%if (!Convert.ToBoolean(gsMASTERCHK)) {%>
                    <li class="active" style="float:right;">
                        <a id="toggleRight" href="javascript:doToggleRightFrame();" style="padding: 5px;" title="탐색창열기">
                            <i id="closeRight" class="i i-circleleft text"></i>
                            <i id="openRight" class="i i-circleright text-active"></i>
                        </a>
                    </li>
                    <%}%>
                </ul>
            </header>
            <div class="panel-body no-padder">
                <div class="tab-content no-padder"></div>
            </div>
        </section>
    </form>
</body>
</html>
