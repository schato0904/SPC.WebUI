<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucItemPOP.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucItem2" %>
<script type="text/javascript">
    var isITEMEndCallback2 = false;
    var timerITEM2 = null;

    function fn_OnUCITEM2LostFocus(s, e) {
        if (!s.GetText() || s.GetText() == '') {
            txtUCITEMNM2.SetValue('');
            txtUCITEMNM2.SetText('');
            txtUCITEMNM2.GetMainElement().title = '';
            hidUCITEMCD2.SetValue('');
            hidUCITEMCD2.SetText('');
            <%if (true == usedModel) {%>
            var _modelCD = ASPxClientControl.Cast("hidUCMODELCD2");
            var _modelNM = ASPxClientControl.Cast("txtUCMODELNM2");
            _modelCD.SetValue('');
            _modelCD.SetText('');
            _modelNM.SetValue('');
            _modelNM.SetText('');
            _modelNM.GetMainElement().title = '';
            <%}%>
            return;
        }  else
            ITEMCallback2.PerformCallback();
    }

    function fn_OnUCITEM2EndCallback(s, e) {
        var ITEMCD = s.cpITEMCD2;
        var ITEMNM = s.cpITEMNM2;
        var MODELCD = s.cpMODELCD2;
        var MODELNM = s.cpMODELNM2;

        if (ITEMCD != '' && ITEMNM != '') {
            hidUCITEMCD2.SetValue(ITEMCD);
            hidUCITEMCD2.SetText(ITEMCD);
            txtUCITEMNM2.SetValue(ITEMNM);
            txtUCITEMNM2.SetText(ITEMNM);
            txtUCITEMNM2.GetMainElement().title = ITEMNM;
            <%if (true == usedModel) {%>
            var _modelCD = ASPxClientControl.Cast("hidUCMODELCD2");
            var _modelNM = ASPxClientControl.Cast("txtUCMODELNM2");
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

            ItemPerFormCallbackTarget2();
        } else {
            hidUCITEMCD2.SetValue('');
            hidUCITEMCD2.SetText('');
            txtUCITEMNM2.SetValue('');
            txtUCITEMNM2.SetText('');
            txtUCITEMNM2.GetMainElement().title = '';
            <%if (true == usedModel) {%>
            var _modelCD = ASPxClientControl.Cast("hidUCMODELCD2");
            var _modelNM = ASPxClientControl.Cast("txtUCMODELNM2");
            _modelCD.SetValue('');
            _modelCD.SetText('');
            _modelNM.SetValue('');
            _modelNM.SetText('');
            _modelNM.GetMainElement().title = '';
            <%}%>
        }

        isITEMEndCallback2 = parent.parent.isTreeITEMSetup2;
    }

    // PerFormCallbak Event Target Control
    function ItemPerFormCallbackTarget2() {
        if ('' != '<%=targetCtrls%>') {
            var tCtrls = '<%=targetCtrls%>'.split(';');
            
            if (tCtrls.length > 0) {
                tCtrls.forEach(function (tCtrl) {
                    var Ctrl = ASPxClientControl.Cast(tCtrl);
                    Ctrl.PerformCallback();
                });
            }
        }
    }

    // 아이템검색창 오픈
    function fn_OnPopupUCItemSearch2() {
        var BANCD = IsNull(ASPxClientControl.Cast('hidBANCD'), '') == '' ? '' : ASPxClientControl.Cast('hidBANCD').GetText();
        var pPage = '<%=Page.ResolveUrl("~/Pages/Common/Popup/ITEMPOP.aspx")%>' +
            '?TITLE=품목조회' +
            '&CRUD=R' +
            '&TYPE=UC2' +
            '&BANCD=' + BANCD + '' +
            '&ITEMCD=' + txtUCITEMCD2.GetText();
        fn_OnPopupOpen(pPage, 800, 500);
    }

    // 검색된 아이템 세팅
    function fn_OnUCSettingItem2(ITEMCD, ITEMNM, MODELCD, MODELNM) {
        txtUCITEMCD2.SetText(ITEMCD);
        txtUCITEMNM2.SetText(ITEMNM);
        hidUCITEMCD2.SetValue(ITEMCD);
        hidUCITEMCD2.SetText(ITEMCD);
        <%if (true == usedModel) {%>
        var _modelCD = ASPxClientControl.Cast("hidUCMODELCD2");
        var _modelNM = ASPxClientControl.Cast("txtUCMODELNM2");
        _modelCD.SetValue(MODELCD);
        _modelCD.SetText(MODELCD);
        _modelNM.SetValue(MODELNM);
        _modelNM.SetText(MODELNM);
        _modelNM.GetMainElement().title = MODELNM;
        <%}%>
    }

    // Tree용 Event
    function fn_OnSetupItemTree2(oParam) {
        if (!isITEMEndCallback2) {
            timerITEM2 = setTimeout(function () { fn_OnSetupItemTree2(oParam); }, 1000);
            isITEMEndCallback2 = parent.parent.isTreeITEMSetup2;
        } else {
            clearTimeout(timerITEM2);
            timerITEM2 = null;
            isITEMEndCallback2 = false;
            parent.parent.isTreeITEMSetup2 = false;
            var oParams = oParam.split('|');

            fn_SetTextValue('hidUCITEMCD2', oParams[4]);
            fn_SetTextValue('txtUCITEMCD2', oParams[4]);
            fn_SetTextValue('txtUCITEMNM2', oParams[5]);

            fn_SetTextValue('hidUCMODELCD2', oParams[6]);
            fn_SetTextValue('txtUCMODELNM2', oParams[7]);

            ItemPerFormCallbackTarget2();
        }
    }

    $(document).ready(function () { if (!isPopup) parent.parent.isItemUserControl2[parent.fn_GetIFrameID()] = true; });
</script>
<dx:ASPxTextBox ID="hidITEMCD2" ClientInstanceName="hidUCITEMCD2" runat="server" ClientVisible="false" />
<div class="control-label" style="float: left; width: 42%;">
    <dx:ASPxTextBox ID="txtITEMCD2" ClientInstanceName="txtUCITEMCD2" runat="server" Width="100%"
        OnInit="txtITEMCD2_Init">
        <ClientSideEvents LostFocus="fn_OnUCITEM2LostFocus" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 47%;">
    <dx:ASPxTextBox ID="txtITEMNM2" ClientInstanceName="txtUCITEMNM2" runat="server" Width="100%">
        <ClientSideEvents Init="fn_OnControlDisableBox" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 9%;">
    <button class="btn btn-default btn-xs" data-toggle="button" title="품목조회" onclick="fn_OnPopupUCItemSearch2(); return false;">
        <i class="i i-popup text"></i>
        <i class="i i-popup text-active text-danger"></i>
    </button>
</div>
<dx:ASPxCallback ID="ITEMCallback2" ClientInstanceName="ITEMCallback2" runat="server" OnCallback="ITEM2Callback_Callback">
    <ClientSideEvents EndCallback="fn_OnUCITEM2EndCallback" />
</dx:ASPxCallback>
