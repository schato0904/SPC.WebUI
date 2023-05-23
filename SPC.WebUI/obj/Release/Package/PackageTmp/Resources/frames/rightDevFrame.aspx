<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rightDevFrame.aspx.cs" Inherits="SPC.WebUI.Resources.frames.rightDevFrame" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
            top: 35px;
            left: 3px;
            bottom: 0;
        }
    </style>
    <script type="text/javascript">
        var isExists = false;

        $(document).ready(function () {

        });

        function fn_AdjustSize() {
            var height = Math.max(0, $(document).height());
            devTree.SetHeight(height - _hMargin - 20);
        }

        function fn_OnDevTreeCallback() {
            if (parent.isTreeCallback == false && parent.bRightActive == false) {
                //devTree.PerformCallback();
                parent.isTreeCallback = true;
            }
        }

        function fn_OnNodeDblClick(s, e) {
            var fields = 'F_LEVEL;F_BANCD;F_BANNM;F_LINECD;F_LINENM;F_ITEMCD;F_ITEMNM;F_MODELCD;F_MODELNM;F_WORKCD;F_WORKNM;F_MEAINSPCD;F_INSPDETAIL;F_INSPCD;F_INSPNM;' +
                'F_STANDARD;F_MAX;F_MIN;F_UCLX;F_LCLX;F_UCLR;F_SERIALNO;F_SIRYO;F_FREEPOINT';
            devTree.GetNodeValues(e.nodeKey, fields, OnGetNodeValues);
        }

        function OnGetNodeValues(values) {
            var vals = values.splice(1, values.length).join('|');
            if (values[0] == '1')
                fn_OnSetupContentBanTree(vals);
            else if (values[0] == '2')
                fn_OnSetupContentLineTree(vals);
            else if (values[0] == '3')
                fn_OnSetupContentItemTree(vals);
            else if (values[0] == '4')
                fn_OnSetupContentWorkTree(vals);
            else if (values[0] == '5')
                fn_OnSetupContentInspectionTree(vals);
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

            isExists = parent.isItemUserControl1[frameID];

            if (isExists == true) {
                parent.isTreeITEMSetup1 = true;
                fn_GetIFrameObj().fn_OnSetupItemTree1(oParam);
            }

            isExists = parent.isItemUserControl2[frameID];

            if (isExists == true) {
                parent.isTreeITEMSetup2 = true;
                fn_GetIFrameObj().fn_OnSetupItemTree2(oParam);
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

            isExists = parent.isItemUserControl1[frameID];

            if (isExists == true) {
                parent.isTreeITEMSetup1 = true;
                fn_GetIFrameObj().fn_OnSetupItemTree1(oParam);
            }

            isExists = parent.isItemUserControl2[frameID];

            if (isExists == true) {
                parent.isTreeITEMSetup2 = true;
                fn_GetIFrameObj().fn_OnSetupItemTree2(oParam);
            }

            isExists = parent.isWorkUserControl[frameID];

            if (isExists == true) {
                parent.isTreeWORKSetup = true;
                fn_GetIFrameObj().fn_OnSetupWorkTree(oParam);
            }

            isExists = parent.isWorkUserControl1[frameID];

            if (isExists == true) {
                parent.isTreeWORKSetup1 = true;
                fn_GetIFrameObj().fn_OnSetupWorkTree1(oParam);
            }

            isExists = parent.isWorkUserControl2[frameID];

            if (isExists == true) {
                parent.isTreeWORKSetup2 = true;
                fn_GetIFrameObj().fn_OnSetupWorkTree2(oParam);
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

            isExists = parent.isItemUserControl1[frameID];

            if (isExists == true) {
                parent.isTreeITEMSetup1 = true;
                fn_GetIFrameObj().fn_OnSetupItemTree1(oParam);
            }

            isExists = parent.isItemUserControl2[frameID];

            if (isExists == true) {
                parent.isTreeITEMSetup2 = true;
                fn_GetIFrameObj().fn_OnSetupItemTree2(oParam);
            }

            isExists = parent.isWorkUserControl[frameID];

            if (isExists == true) {
                parent.isTreeWORKSetup = true;
                fn_GetIFrameObj().fn_OnSetupWorkTree(oParam);
            }

            isExists = parent.isWorkUserControl1[frameID];

            if (isExists == true) {
                parent.isTreeWORKSetup1 = true;
                fn_GetIFrameObj().fn_OnSetupWorkTree1(oParam);
            }

            isExists = parent.isWorkUserControl2[frameID];

            if (isExists == true) {
                parent.isTreeWORKSetup2 = true;
                fn_GetIFrameObj().fn_OnSetupWorkTree2(oParam);
            }

            isExists = parent.isInspectionUserControl[frameID];

            if (isExists == true) {
                fn_GetIFrameObj().fn_OnSetupInspectionTree(oParam);
            }

            isExists = parent.isInspectionUserControl1[frameID];

            if (isExists == true) {
                fn_GetIFrameObj().fn_OnSetupInspectionTree1(oParam);
            }

            isExists = parent.isInspectionUserControl2[frameID];

            if (isExists == true) {
                fn_GetIFrameObj().fn_OnSetupInspectionTree2(oParam);
            }
        }

        function fn_doClearSearchBoxForTree() {
            fn_GetIFrameObj().fn_doClearSearchBoxForTree();
        }

        function fn_OnMACHGUBUNValueChanged(s, e) {
            var val = s.GetValue();
            devTree.PerformCallback();
        }

        // Tree End Callback
        function fn_OnEndCallback(s, e) {
            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="header">
            <ucCTF:Ban ID="ucBan" runat="server" targetCtrls="devTree" bAutoFillByTree="true" nullText="반 전체" />
            <div style="padding-top: 5px;"></div>
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
            <dx:ASPxTreeList ID="devTree" ClientInstanceName="devTree" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_CODE" ParentFieldName="F_GROUPCD"
                OnCustomCallback="devTree_CustomCallback" OnDataBound="devTree_DataBound">
                <Columns>
                    <dx:TreeListDataColumn FieldName="F_CODENM" Caption="코드명" VisibleIndex="0" Width="220px" />
                    <dx:TreeListDataColumn FieldName="F_LEVEL" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_BANCD" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_BANCD" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_BANNM" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_LINECD" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_LINENM" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_ITEMCD" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_ITEMNM" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_WORKCD" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_WORKNM" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_MEAINSPCD" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_INSPDETAIL" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_INSPCD" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_INSPNM" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_STANDARD" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_MAX" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_MIN" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_UCLX" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_LCLX" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_UCLR" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_SERIALNO" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_SIRYO" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_FREEPOINT" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_MODELCD" Visible="false" />
                    <dx:TreeListDataColumn FieldName="F_MODELNM" Visible="false" />
                </Columns>
                <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" ScrollableHeight="0" ShowColumnHeaders="false" />
                <SettingsBehavior ExpandCollapseAction="NodeDblClick" AllowSort="false" AllowFocusedNode="True" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" NodeDblClick="fn_OnNodeDblClick" />
            </dx:ASPxTreeList>
        </div>
    </form>
</body>
</html>
