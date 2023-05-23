<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainFrame.aspx.cs" Inherits="SPC.WebUI.Resources.frames.mainFrame" %>
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
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/scripts/message/Message.js")%>"></script>
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/scripts/js/SPCCommon.js")%>"></script>
    <%--<script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/scripts/js/ASPxScriptIntelliSense.js")%>"></script>--%>
    <style type="text/css">
        html, body {
            height: 100%;
            padding: 0px;
            margin: 0px;
        }
        .ifrmContent {
            width: 100%;
            height: 100%;
            padding: 0px;
            margin: 0px;
            border-width: 0px;
            border-spacing: 0px;
        }
    </style>
    <script type="text/javascript">
        // 동적으로 높이값을 계산한다
        function fn_TabAdjustSize() {
            var height = Math.max(0, $(document.body).height());
            pageControl.SetHeight(height);
            var width = Math.max(0, $(document.body).width());
            $(".iframeContent").width(width);
            $(".iframeContent").height(height - 30);
        }

        function fn_OnTablInit(s, e) {
            fn_TabAdjustSize();
        }

        function fn_OnTabEndCallback(s, e) {
            fn_TabAdjustSize();
        }

        function fn_OnPanelEndCallback(s, e) {
            document.getElementById("iframe" + s.cpTabName).src = document.getElementById("iframe" + s.cpTabName).src;
            pageControl.SetActiveTab(pageControl.GetTabByName(s.cpTabName));
        }

        $(window).bind({
            load: function () {
                fn_TabAdjustSize();
            },
            resize: function () {
                fn_TabAdjustSize();
            }
        });

        function closeActiveTab(tabName) {
            devCallbackPanel.PerformCallback("del|" + tabName);
        }

        function createNewTab(oParam) {
            var oParams = oParam.split('|');
            if (pageControl.GetTabByName(oParams[3]) == null) {
                devCallbackPanel.PerformCallback("new|" + oParam);
            } else {
                pageControl.SetActiveTab(pageControl.GetTabByName(oParams[3]));
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxCallbackPanel ID="devCallbackPanel" runat="server" Width="100%"
            OnCallback="devCallbackPanel_Callback">
            <ClientSideEvents EndCallback="fn_OnPanelEndCallback" />
            <PanelCollection>
                <dx:PanelContent ID="pContent" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxPageControl ID="pageControl" ClientInstanceName="pageControl" runat="server" Width="100%" Height="100%"
                        EnableViewState="false" EnableHierarchyRecreation="True"
                        ActiveTabIndex="0" TabSpacing="0px" Paddings-Padding="0" EnableTabScrolling="false">
                        <ContentStyle Paddings-Padding="0"></ContentStyle>
                        <ClientSideEvents Init="fn_OnTablInit" EndCallback="fn_OnTabEndCallback" />
                    </dx:ASPxPageControl>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </form>
</body>
</html>
