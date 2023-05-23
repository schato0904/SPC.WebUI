<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucInspdetail.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucInspdetail" %>
<script type="text/javascript">
    var timerITEMCD = null;

    function fn_OnUCINSPITEMKeyUp(s, e) {
        if (fn_GetCastValue('txtUCINSPITEMCD').length != 6) {
            fn_SetTextValue('txtUCINSPITEMNM', '');
        }
    }

    // 검사항목검색창 오픈
    function fn_OnPopupUCInspectionItemSearch() {
        var pPage = '<%=Page.ResolveUrl("~/Pages/Common/Popup/MEAINSPPOP.aspx")%>' +
                '?TITLE=검사항목조회' +
                '&CRUD=R';
        fn_OnPopupOpen(pPage, 800, 500);
    }

    // 검색된 검사항목 세팅
    function fn_OnSettingMeainsp(CODE, NAME) {
        hidUCINSPITEMCD.SetText(CODE);
        hidUCINSPITEMCD.SetValue(CODE);
        txtUCINSPITEMCD.SetValue(CODE);
        txtUCINSPITEMCD.SetText(CODE);
        txtUCINSPITEMNM.SetValue(NAME);
        txtUCINSPITEMNM.SetText(NAME);
    }

    // Tree용 Event
    function fn_OnSetupInspectionTree(oParam) {
        var oParams = oParam.split('|');
        
        fn_SetTextValue('hidUCINSPITEMCD', oParams[10]);
        fn_SetTextValue('txtUCINSPITEMCD', oParams[10]);
        fn_SetTextValue('txtUCINSPITEMNM', oParams[11]);
       
    }

    function fn_ValidateINSPITEM() {
        if (hidUCINSPITEMCD.GetValue() == "" || hidUCINSPITEMCD.GetValue() == null) {
            alert("검사항목을 입력하세요!!"); return false;
        } else {
            return true;
        }
    }

    $(document).ready(function () { if (!isPopup) parent.parent.isInspectionUserControl[parent.fn_GetIFrameID()] = true; });
</script>
<dx:ASPxTextBox ID="hidINSPITEMCD" ClientInstanceName="hidUCINSPITEMCD" runat="server" ClientVisible="false" />
<div class="control-label" style="float: left; width: 28%;">
    <dx:ASPxTextBox ID="txtINSPITEMCD" ClientInstanceName="txtUCINSPITEMCD" runat="server" Width="100%"
        OnInit="txtINSPITEMCD_Init">
        <ClientSideEvents KeyUp="fn_OnUCINSPITEMKeyUp" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 61%;">
    <dx:ASPxTextBox ID="txtINSPITEMNM" ClientInstanceName="txtUCINSPITEMNM" runat="server" Width="100%">
        <ClientSideEvents Init="fn_OnControlDisableBox" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 9%;">
    <button class="btn btn-default btn-xs" data-toggle="button" title="검사항목조회" onclick="fn_OnPopupUCInspectionItemSearch(); return false;">
        <i class="i i-popup text"></i>
        <i class="i i-popup text-active text-danger"></i>
    </button>
</div>