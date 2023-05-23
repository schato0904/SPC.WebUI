<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="SPC.WebUI.MainForm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 frameset //EN" "http://www.w3.org/TR/html4/frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%=m_sSiteTitle%></title>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Resources/jquery/jquery-1.10.2.min.js")%>"></script>
    <script type="text/javascript">
        var isBanUserControl = {};
        var isLineUserControl = {};
        var isItemUserControl = {};
        var isItemUserControl1 = {};
        var isItemUserControl2 = {};
        var isWorkUserControl = {};
        var isWorkUserControl1 = {};
        var isWorkUserControl2 = {};
        var isEquipUserControl = {};
        var isInspectionUserControl = {};
        var isInspectionUserControl1 = {};
        var isInspectionUserControl2 = {};
        var isPopupVisible = false;
        var isTreeLINESetup = false;
        var isTreeITEMSetup = false;
        var isTreeITEMSetup1 = false;
        var isTreeITEMSetup2 = false;
        var isTreeWORKSetup = false;
        var isTreeWORKSetup1 = false;
        var isTreeWORKSetup2 = false;

        var bLeftActive = true;
        var isTreeCallback = false;
        function doGetLeftActive() {
            bLeftActive = !bLeftActive;
            return bLeftActive;
        }
        var bRightActive = false;
        function doGetRightActive() {
            bRightActive = !bRightActive;
            $("#rightFrame")[0].contentWindow.fn_OnDevTreeCallback();
            return bRightActive;
        }

        function doToggleFrame(t) {
            var _cols = sframe.cols.split(',');
            var leftSize = _cols[0];
            var rightSize = _cols[2];
            
            if (t == 'left') {
                _cols[0] = leftSize == '0' ? '240' : '0';
            } else if (t == 'right') {
                _cols[2] = rightSize == '0' ? '240' : '0';
            }
            
            sframe.cols = _cols.join(',');
        }

        function doLogout() {
            document.getElementById('mainFrame').src = 'LogoutForm.aspx';
        }

        function doRendorMenu(topMenuCD) {
            var _topFrame = document.getElementById('topFrame');
            var innerDoc = _topFrame.contentDocument || _topFrame.contentWindow.document;
            var TopMenuCD = innerDoc.getElementById('hidTopMenuCD');

            document.getElementById('leftFrame').src = 'Resources/frames/leftFrame.aspx?topMenuCD=' + topMenuCD;

            if (!bLeftActive) {
                $("#mainFrame")[0].contentWindow.doToggleLeftFrame();
            }
        }

        function doRendorContent() {
            $("#mainFrame")[0].contentWindow.fn_OnCreateTab('MM00|MM0000|COMM|COMM0101|공지사항|R|0');
            //$("#mainFrame")[0].contentWindow.fn_OnCreateTab('MM00|MM0000|COMM|COMM0102|공지사항|R|0');

            if (false == bLeftActive) {
                $("#mainFrame")[0].contentWindow.doToggleLeftFrame();
            }
            if (true == bRightActive) {
                $("#mainFrame")[0].contentWindow.doToggleRightFrame();
            }
        }

        function doCreateTab(oParams, oSetParams) {
            oSetParams = oSetParams == null ? '' : oSetParams;
            $("#mainFrame")[0].contentWindow.fn_OnCreateTab(oParams, oSetParams);
        }

        function getMainFrameObj() {
            return $("#mainFrame")[0].contentWindow;
        }
    </script>
</head>
    <frameset border="0" framespacing="0" rows="50,*" frameborder="0">
        <frame id="topFrame" name="topFrame" marginwidth="0" marginheight="0" src="Resources/frames/topFrame.aspx" frameborder="0" noresize="noresize" scrolling="no" />
        <frameset id="sframe" border="0" frameborder="0" cols="240,*,0">
		    <frame id="leftFrame" name="leftFrame" marginwidth="0" marginheight="0" src="Resources/frames/leftFrame.aspx" frameborder="0" scrolling="no" />
            <frame id="mainFrame" name="mainFrame" marginwidth="0" marginheight="0" src="Resources/frames/tabFrame.aspx" frameborder="0" scrolling="no" />
            <frame id="rightFrame" name="rightFrame" marginwidth="0" marginheight="0" src="Resources/frames/rightDevFrame.aspx" frameborder="0" scrolling="no" />
	    </frameset>
    </frameset>
</html>
