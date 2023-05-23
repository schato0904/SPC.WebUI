<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rightFrame.aspx.cs" Inherits="SPC.WebUI.Resources.frames.rightFrame" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
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
    <link rel="stylesheet" type="text/css" href="~/Resources/css/spc.css" />
    <link rel="stylesheet" type="text/css" href="~/Resources/jquery/jquery.treeview/jquery.treeview.css" />
    <script type="text/javascript">var isCsLogin = '<%#Request.Cookies["spckey"]["CSLOGIN"]%>'</script>
    <!--[if lt IE 9]>
    <script src="../script/ie/html5shiv.js"></script>
    <script src="../script/ie/respond.min.js"></script>
    <script src="../script/ie/excanvas.js"></script>
    <![endif]-->
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/jquery/jquery-1.10.2.min.js")%>"></script>
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/jquery/jquery-ui-1.10.4.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/jquery/jquery.treeview/jquery.treeview.js")%>"></script>
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/scripts/js/SPCCommon.js")%>"></script>
    <script type="text/javascript" src="<%#Page.ResolveUrl("~/Resources/scripts/js/DevExpress.js")%>"></script>
    <style type="text/css">
        html, body {
            width: 240px;
            height: 100%;
            padding: 0px;
            margin: 0px;
            background-color: white;
        }
        body {
            border-left:1px solid #e0e0e0;
        }

        #header {
            width: 240px;
            padding: 10px 5px 0px 5px;
        }

        #content {
            width: 230px;    
            position: absolute;
            top: 65px;
            left: 6px;
            bottom: 0;
            padding: 0px 5px 0px 5px;
            overflow: scroll;
        }
    </style>
    <script type="text/javascript">
        var isExists = false;

        $(function () {
            $("#tree").treeview({
                persist: "location",
                animated: "fast",
                collapsed: true,
                prerendor: true
            });
        });

        $(document).ready(function () {
            
        });

        function fn_AdjustSize() {
        }

        function fn_OnCallbackPanelCallback() {
            if (parent.isTreeCallback == false && parent.bRightActive == false) {
                //devCallbackPanel.PerformCallback();
                parent.isTreeCallback = true;
            }
        }

        //  CallbackPanel Callback
        function fn_OnCallbackPanelEndCallback(s, e) {
            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
            }

            $("#tree").treeview({
                persist: "location",
                animated: "fast",
                collapsed: true,
                prerendor: true
            });
        }

        // Grid Callback Error
        function fn_OnCallbackPanelCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Get IFrameObj
        function fn_GetIFrameObj() {
            return parent.getMainFrameObj().fn_GetIFrameObj();
        }

        // Get IFramePGMID
        function fn_GetIFrameID() {
            return parent.getMainFrameObj().fn_GetIFrameID();
        }

        // 반선택
        function fn_OnSetupContentBanTree(oParam) {
            fn_doClearSearchBoxForTree();

            var frameID = fn_GetIFrameID();

            isExists = parent.isBanUserControl[frameID];
            
            if (isExists == true) {
                fn_GetIFrameObj().fn_OnSetupBanTree(oParam);
            }
        }

        // 라인선택
        function fn_OnSetupContentLineTree(oParam) {
            fn_doClearSearchBoxForTree();

            var frameID = fn_GetIFrameID();

            isExists = parent.isBanUserControl[frameID];

            if (isExists == true) {
                fn_GetIFrameObj().fn_OnSetupBanTree(oParam);
            }

            isExists = parent.isLineUserControl[frameID];

            if (isExists == true) {
                parent.isTreeLINESetup = true;
                fn_GetIFrameObj().fn_OnSetupLineTree(oParam);
            }
        }

        // 품목선택
        function fn_OnSetupContentItemTree(oParam) {
            fn_doClearSearchBoxForTree();

            var frameID = fn_GetIFrameID();

            isExists = parent.isBanUserControl[frameID];

            if (isExists == true) {
                fn_GetIFrameObj().fn_OnSetupBanTree(oParam);
            }

            isExists = parent.isLineUserControl[frameID];

            if (isExists == true) {
                parent.isTreeLINESetup = true;
                fn_GetIFrameObj().fn_OnSetupLineTree(oParam);
            }

            isExists = parent.isItemUserControl[frameID];

            if (isExists == true) {
                parent.isTreeITEMSetup = true;
                fn_GetIFrameObj().fn_OnSetupItemTree(oParam);
            }
        }

        // 공정선택
        function fn_OnSetupContentWorkTree(oParam) {
            fn_doClearSearchBoxForTree();

            var frameID = fn_GetIFrameID();

            isExists = parent.isBanUserControl[frameID];

            if (isExists == true) {
                fn_GetIFrameObj().fn_OnSetupBanTree(oParam);
            }

            isExists = parent.isLineUserControl[frameID];

            if (isExists == true) {
                parent.isTreeLINESetup = true;
                fn_GetIFrameObj().fn_OnSetupLineTree(oParam);
            }

            isExists = parent.isItemUserControl[frameID];

            if (isExists == true) {
                parent.isTreeITEMSetup = true;
                fn_GetIFrameObj().fn_OnSetupItemTree(oParam);
            }

            isExists = parent.isWorkUserControl[frameID];

            if (isExists == true) {
                parent.isTreeWORKSetup = true;
                fn_GetIFrameObj().fn_OnSetupWorkTree(oParam);
            }
        }

        // 검사항목선택
        function fn_OnSetupContentInspectionTree(oParam) {
            fn_doClearSearchBoxForTree();

            var frameID = fn_GetIFrameID();

            isExists = parent.isBanUserControl[frameID];

            if (isExists == true) {
                fn_GetIFrameObj().fn_OnSetupBanTree(oParam);
            }

            isExists = parent.isLineUserControl[frameID];

            if (isExists == true) {
                parent.isTreeLINESetup = true;
                fn_GetIFrameObj().fn_OnSetupLineTree(oParam);
            }

            isExists = parent.isItemUserControl[frameID];

            if (isExists == true) {
                parent.isTreeITEMSetup = true;
                fn_GetIFrameObj().fn_OnSetupItemTree(oParam);
            }

            isExists = parent.isWorkUserControl[frameID];

            if (isExists == true) {
                parent.isTreeWORKSetup = true;
                fn_GetIFrameObj().fn_OnSetupWorkTree(oParam);
            }

            isExists = parent.isInspectionUserControl[frameID];
            
            if (isExists == true) {
                fn_GetIFrameObj().fn_OnSetupInspectionTree(oParam);
            }
        }

        function fn_doClearSearchBoxForTree() {
            fn_GetIFrameObj().fn_doClearSearchBoxForTree();
        }

        function fn_OnMACHGUBUNValueChanged(s, e) {
            var val = s.GetValue();
            devCallbackPanel.PerformCallback();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="header">
            <ucCTF:Ban ID="ucBan" runat="server" targetCtrls="devCallbackPanel" bAutoFillByTree="true" nullText="반을 선택하세요" />
            <div style="padding-top:5px;"></div>
            <dx:ASPxComboBox ID="ddlMACHGUBUN" runat="server" ClientInstanceName="ddlMACHGUBUN" Width="100%"
                IncrementalFilteringMode="None" CssClass="NoXButton">
                <ClientSideEvents ValueChanged="fn_OnMACHGUBUNValueChanged" Init="fn_OnControlDisable" />
                <Items>
                    <dx:ListEditItem Text="공정구분 전체" Value="" Selected="true" />
                    <dx:ListEditItem Text="자주검사" Value="0" />
                    <dx:ListEditItem Text="전수검사" Value="3" />
                </Items>
            </dx:ASPxComboBox>
        </div>
        <div id="content">
            <ul id="tree" class="treeview">
                <dx:ASPxCallbackPanel ID="devCallbackPanel" ClientInstanceName="devCallbackPanel" runat="server"
                    OnCallback="devCallbackPanel_Callback">
                    <ClientSideEvents EndCallback="fn_OnCallbackPanelEndCallback" CallbackError="fn_OnCallbackPanelCallbackError" />
                    <PanelCollection>
                        <dx:PanelContent>
                            <asp:Literal ID="ltlContents" runat="server" />
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </ul>
        </div>
    </form>
</body>
</html>
