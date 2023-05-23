<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucItem1.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucItem1" %>
<script type="text/javascript">
    var isITEMEndCallback1 = false;
    var timerITEM1 = null;

    function fn_OnUCITEM1LostFocus(s, e) {
        if (!s.GetText() || s.GetText() == '') {
            txtUCITEMNM1.SetValue('');
            txtUCITEMNM1.SetText('');
            txtUCITEMNM1.GetMainElement().title = '';
            hidUCITEMCD1.SetValue('');
            hidUCITEMCD1.SetText('');
            <%if (true == usedModel) {%>
            var _modelCD = ASPxClientControl.Cast("hidUCMODELCD1");
            var _modelNM = ASPxClientControl.Cast("txtUCMODELNM1");
            _modelCD.SetValue('');
            _modelCD.SetText('');
            _modelNM.SetValue('');
            _modelNM.SetText('');
            _modelNM.GetMainElement().title = '';
            <%}%>
            return;
        }  else
            ITEMCallback1.PerformCallback();
    }

    function fn_OnUCITEM1EndCallback(s, e) {
        var ITEMCD = s.cpITEMCD1;
        var ITEMNM = s.cpITEMNM1;
        var MODELCD = s.cpMODELCD1;
        var MODELNM = s.cpMODELNM1;

        if (ITEMCD != '' && ITEMNM != '') {
            hidUCITEMCD1.SetValue(ITEMCD);
            hidUCITEMCD1.SetText(ITEMCD);
            txtUCITEMNM1.SetValue(ITEMNM);
            txtUCITEMNM1.SetText(ITEMNM);
            txtUCITEMNM1.GetMainElement().title = ITEMNM;
            <%if (true == usedModel) {%>
            var _modelCD = ASPxClientControl.Cast("hidUCMODELCD1");
            var _modelNM = ASPxClientControl.Cast("txtUCMODELNM1");
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

            ItemPerFormCallbackTarget1();
        } else {
            hidUCITEMCD1.SetValue('');
            hidUCITEMCD1.SetText('');
            txtUCITEMNM1.SetValue('');
            txtUCITEMNM1.SetText('');
            txtUCITEMNM1.GetMainElement().title = '';
            <%if (true == usedModel) {%>
            var _modelCD = ASPxClientControl.Cast("hidUCMODELCD1");
            var _modelNM = ASPxClientControl.Cast("txtUCMODELNM1");
            _modelCD.SetValue('');
            _modelCD.SetText('');
            _modelNM.SetValue('');
            _modelNM.SetText('');
            _modelNM.GetMainElement().title = '';
            <%}%>
        }

        isITEMEndCallback1 = parent.parent.isTreeITEMSetup1;
    }

    // PerFormCallbak Event Target Control
    function ItemPerFormCallbackTarget1() {
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
    function fn_OnPopupUCItemSearch1() {
        var Popup = '<%=Page.ResolveUrl("~/Pages/Common/Popup/ITEMPOP.aspx")%>'
        <%if(useWERD){%>
        Popup = '<%=Page.ResolveUrl("~/Pages/Common/Popup/ITEMPOP_WERD.aspx")%>'
        <%}%>
        var BANCD = IsNull(ASPxClientControl.Cast('hidBANCD'), '') == '' ? '' : ASPxClientControl.Cast('hidBANCD').GetText();
        var pPage = Popup + 
            '?TITLE=품목조회' +
            '&CRUD=R' +
            '&TYPE=UC1' +
            '&BANCD=' + BANCD + '' +
            '&ITEMCD=' + txtUCITEMCD1.GetText();
        fn_OnPopupOpen(pPage, 800, 500);
    }

    // 검색된 아이템 세팅
    function fn_OnUCSettingItem1(ITEMCD, ITEMNM, MODELCD, MODELNM, UNITCD, UNITNM) {
        txtUCITEMCD1.SetText(ITEMCD);
        txtUCITEMNM1.SetText(ITEMNM);
        hidUCITEMCD1.SetValue(ITEMCD);
        hidUCITEMCD1.SetText(ITEMCD);
        <%if (true == usedModel) {%>
        var _modelCD = ASPxClientControl.Cast("hidUCMODELCD1");
        var _modelNM = ASPxClientControl.Cast("txtUCMODELNM1");
        _modelCD.SetValue(MODELCD);
        _modelCD.SetText(MODELCD);
        _modelNM.SetValue(MODELNM);
        _modelNM.SetText(MODELNM);
        _modelNM.GetMainElement().title = MODELNM;
        <%}%>

        <%if (true == usedUnit) {%>
        var _unitCD = ASPxClientControl.Cast("F_UNITCD");
        var _unitNM = ASPxClientControl.Cast("F_UNITNM");
        _unitCD.SetValue(UNITCD);
        _unitCD.SetText(UNITCD);
        _unitNM.SetValue(UNITNM);
        _unitNM.SetText(UNITNM);
        <%}%>


    }

    // Tree용 Event
    function fn_OnSetupItemTree1(oParam) {
        if (!isITEMEndCallback1) {
            timerITEM1 = setTimeout(function () { fn_OnSetupItemTree1(oParam); }, 1000);
            isITEMEndCallback1 = parent.parent.isTreeITEMSetup1;
        } else {
            clearTimeout(timerITEM1);
            timerITEM1 = null;
            isITEMEndCallback1 = false;
            parent.parent.isTreeITEMSetup1 = false;
            var oParams = oParam.split('|');

            fn_SetTextValue('hidUCITEMCD1', oParams[4]);
            fn_SetTextValue('txtUCITEMCD1', oParams[4]);
            fn_SetTextValue('txtUCITEMNM1', oParams[5]);

            fn_SetTextValue('hidUCMODELCD1', oParams[6]);
            fn_SetTextValue('txtUCMODELNM1', oParams[7]);

            ItemPerFormCallbackTarget1();
        }
    }

    function fn_Itemcd1DisableBox(value) {
        var s = ASPxClientControl.Cast("txtUCITEMCD1")
        var inputElement = s.GetInputElement();
        if (!value) {            
            inputElement.disabled = true;
            inputElement.readonly = true;
            inputElement.style.backgroundColor = '#cccccc';
            var mainElement = s.GetMainElement();
            mainElement.style.backgroundColor = '#cccccc';
        } else {
            inputElement.disabled = false;
            inputElement.readonly = false;
            inputElement.style.backgroundColor = '#ffffff';
            var mainElement = s.GetMainElement();
            mainElement.style.backgroundColor = '#ffffff';
        }
    }

    function fn_PopUPCheck() {
        var inputElement = ASPxClientControl.Cast("txtUCITEMCD1").GetInputElement();

        if (!inputElement.readonly) {
            fn_OnPopupUCItemSearch1();
        }
    }

    function fn_GetItemCD() {
        return hidUCITEMCD1.GetValue();
    }

    function fn_ValidateITEM1() {
        if (hidUCITEMCD1.GetValue() == "" || hidUCITEMCD1.GetValue() == null) {
            alert("품목을 입력하세요!!"); return false;
        } else {
            return true;
        }
    }
    $(document).ready(function () { if (!isPopup) parent.parent.isItemUserControl1[parent.fn_GetIFrameID()] = true; });
</script>
<dx:ASPxTextBox ID="hidITEMCD1" ClientInstanceName="hidUCITEMCD1" runat="server" ClientVisible="false" />
<div class="control-label" style="float: left; width: 42%;">
    <dx:ASPxTextBox ID="txtITEMCD1" ClientInstanceName="txtUCITEMCD1" runat="server" Width="100%"
        OnInit="txtITEMCD1_Init">
        <ClientSideEvents LostFocus="fn_OnUCITEM1LostFocus" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 47%;">
    <dx:ASPxTextBox ID="txtITEMNM1" ClientInstanceName="txtUCITEMNM1" runat="server" Width="100%">
        <ClientSideEvents Init="fn_OnControlDisableBox" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 9%;">
    <button class="btn btn-default btn-xs" data-toggle="button" title="품목조회" onclick="fn_PopUPCheck(); return false;">
        <i class="i i-popup text"></i>
        <i class="i i-popup text-active text-danger"></i>
    </button>
</div>
<dx:ASPxCallback ID="ITEMCallback1" ClientInstanceName="ITEMCallback1" runat="server" OnCallback="ITEM1Callback_Callback">
    <ClientSideEvents EndCallback="fn_OnUCITEM1EndCallback" />
</dx:ASPxCallback>
