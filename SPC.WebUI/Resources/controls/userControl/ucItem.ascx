<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucItem.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucItem" %>
<script type="text/javascript">
    var isITEMEndCallback = false;
    var timerITEM = null;

    function fn_OnUCITEMLostFocus(s, e) {
        if (!s.GetText() || s.GetText() == '') {
            txtUCITEMNM.SetValue('');
            txtUCITEMNM.SetText('');
            txtUCITEMNM.GetMainElement().title = '';
            hidUCITEMCD.SetValue('');
            hidUCITEMCD.SetText('');
            <%if (true == usedModel) {%>
            var _modelCD = ASPxClientControl.Cast("hidUCMODELCD");
            var _modelNM = ASPxClientControl.Cast("txtUCMODELNM");
            _modelCD.SetValue('');
            _modelCD.SetText('');
            _modelNM.SetValue('');
            _modelNM.SetText('');
            _modelNM.GetMainElement().title = '';
            <%}%>
            // 공정 DropdownList가 있는 경우
            if (ASPxClientUtils.IsExists(ASPxClientControl.Cast('ddlWORK'))) {
                ItemPerFormCallbackTarget();
            }
            return;
        }  else
            ITEMCallback.PerformCallback();
    }

    function fn_OnUCITEMKeyUp(s, e) {
        fn_SetTextValue('hidUCITEMCD', s.GetValue());
    }

    function fn_OnUCITEMEndCallback(s, e) {
        var ITEMCD = s.cpITEMCD;
        var ITEMNM = s.cpITEMNM;
        var MODELCD = s.cpMODELCD;
        var MODELNM = s.cpMODELNM;

        if (ITEMCD != '' && ITEMNM != '') {
            hidUCITEMCD.SetValue(ITEMCD);
            hidUCITEMCD.SetText(ITEMCD);
            txtUCITEMNM.SetValue(ITEMNM);
            txtUCITEMNM.SetText(ITEMNM);
            txtUCITEMNM.GetMainElement().title = ITEMNM;
            <%if (true == usedModel) {%>
            var _modelCD = ASPxClientControl.Cast("hidUCMODELCD");
            var _modelNM = ASPxClientControl.Cast("txtUCMODELNM");
            if (MODELCD != '' && MODELNM != '') {
                _modelCD.SetValue(MODELCD);
                _modelCD.SetText(MODELCD);
                _modelNM.SetValue(MODELNM);
                _modelNM.SetText(MODELNM);
                _modelNM.GetMainElement().title = MODELNM;
            } else {
                _modelCD.SetValue('');
                _modelCD.SetText('');
                _modelNM.SetValue('');
                _modelNM.SetText('');
                _modelNM.GetMainElement().title = '';
            }
            <%}%>

            ItemPerFormCallbackTarget();
        } else {
            hidUCITEMCD.SetValue('');
            hidUCITEMCD.SetText('');
            txtUCITEMNM.SetValue('');
            txtUCITEMNM.SetText('');
            txtUCITEMNM.GetMainElement().title = '';
            <%if (true == usedModel) {%>
            var _modelCD = ASPxClientControl.Cast("hidUCMODELCD");
            var _modelNM = ASPxClientControl.Cast("txtUCMODELNM");
            _modelCD.SetValue('');
            _modelCD.SetText('');
            _modelNM.SetValue('');
            _modelNM.SetText('');
            _modelNM.GetMainElement().title = '';
            <%}%>
        }

        isITEMEndCallback = parent.parent.isTreeITEMSetup;
    }

    // PerFormCallbak Event Target Control
    function ItemPerFormCallbackTarget() {
        if ('' != '<%=targetCtrls%>') {
            var tCtrls = '<%=targetCtrls%>'.split(';');
            
            if (tCtrls.length > 0) {
                $.each(tCtrls, function (key, tCtrl) {
                    var Ctrl = ASPxClientControl.Cast(tCtrl);
                    Ctrl.PerformCallback();
                });
            }
        }
    }

    // 아이템검색창 오픈
    function fn_OnPopupUCItemSearch() {        
        var Option = '<%=searchOption%>'
        var BANCD = IsNull(ASPxClientControl.Cast('hidBANCD'), '') == '' ? '' : ASPxClientControl.Cast('hidBANCD').GetText();
        var FROMDT = IsNull(ASPxClientControl.Cast('hidUCFROMDT'), '') == '' ? '' : ASPxClientControl.Cast('hidUCFROMDT').GetText();
        var TODT = IsNull(ASPxClientControl.Cast('hidUCTODT'), '') == '' ? '' : ASPxClientControl.Cast('hidUCTODT').GetText();
        var pPage = '<%=Page.ResolveUrl("~/Pages/Common/Popup/ITEMPOP.aspx")%>' +
            '?TITLE=품목조회' +
            '&CRUD=R' +
            '&TYPE=UC' +
            '&OPTION=' + Option +
            '&BANCD=' + BANCD + '' +
            '&ITEMCD=' + txtUCITEMCD.GetText() +
            '&MACHGUBUN=<%=machGubun%>' +
            '&FROMDT=' + FROMDT +
            '&TODT=' + TODT;
        fn_OnPopupOpen(pPage, 800, 500);
    }

    // 검색된 아이템 세팅
    function fn_OnUCSettingItem(ITEMCD, ITEMNM, MODELCD, MODELNM) {
        txtUCITEMCD.SetText(ITEMCD);
        txtUCITEMNM.SetText(ITEMNM);
        hidUCITEMCD.SetValue(ITEMCD);
        hidUCITEMCD.SetText(ITEMCD);
        <%if (true == usedModel) {%>
        var _modelCD = ASPxClientControl.Cast("hidUCMODELCD");
        var _modelNM = ASPxClientControl.Cast("txtUCMODELNM");
        _modelCD.SetValue(MODELCD);
        _modelCD.SetText(MODELCD);
        _modelNM.SetValue(MODELNM);
        _modelNM.SetText(MODELNM);
        _modelNM.GetMainElement().title = MODELNM;
        <%}%>
    }

    // 검색된 아이템 세팅
    function fn_ucItemSetValue(ITEMCD) {
        txtUCITEMCD.SetText(ITEMCD);
        hidUCITEMCD.SetValue(ITEMCD);
        
        ITEMCallback.PerformCallback();
    }

    // Tree용 Event
    function fn_OnSetupItemTree(oParam) {
        if (!isITEMEndCallback) {
            timerITEM = setTimeout(function () { fn_OnSetupItemTree(oParam); }, 1000);
            isITEMEndCallback = parent.parent.isTreeITEMSetup;
        } else {
            clearTimeout(timerITEM);
            timerITEM = null;
            isITEMEndCallback = false;
            parent.parent.isTreeITEMSetup = false;
            var oParams = oParam.split('|');

            fn_SetTextValue('hidUCITEMCD', oParams[4]);
            fn_SetTextValue('txtUCITEMCD', oParams[4]);
            fn_SetTextValue('txtUCITEMNM', oParams[5]);

            fn_SetTextValue('hidUCMODELCD', oParams[6]);
            fn_SetTextValue('txtUCMODELNM', oParams[7]);

            ItemPerFormCallbackTarget();
        }
    }

    function fn_ValidateITEM() {
        if (hidUCITEMCD.GetValue() == "" || hidUCITEMCD.GetValue() == null) {
            alert("품목을 입력하세요!!"); return false;
        } else {
            return true;
        }
    }
    $(document).ready(function () { if (!isPopup) parent.parent.isItemUserControl[parent.fn_GetIFrameID()] = true; });
</script>
<dx:ASPxTextBox ID="hidITEMCD" ClientInstanceName="hidUCITEMCD" runat="server" ClientVisible="false" />
<div class="control-label" style="float: left; width: 42%;">
    <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtUCITEMCD" runat="server" Width="100%"
        OnInit="txtITEMCD_Init">
        <ClientSideEvents LostFocus="fn_OnUCITEMLostFocus" KeyUp="fn_OnUCITEMKeyUp" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 47%;">
    <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtUCITEMNM" runat="server" Width="100%">
        <ClientSideEvents Init="fn_OnControlDisableBox" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 9%;">
    <button class="btn btn-default btn-xs" data-toggle="button" title="품목조회" onclick="fn_OnPopupUCItemSearch(); return false;">
        <i class="i i-popup text"></i>
        <i class="i i-popup text-active text-danger"></i>
    </button>
</div>
<dx:ASPxCallback ID="ITEMCallback" ClientInstanceName="ITEMCallback" runat="server" OnCallback="ITEMCallback_Callback">
    <ClientSideEvents EndCallback="fn_OnUCITEMEndCallback" />
</dx:ASPxCallback>
