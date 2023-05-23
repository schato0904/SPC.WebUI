<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucWorkPOP2.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucWorkPOP2" %>
<script type="text/javascript">
    var isWORKEndCallback2 = false;
    var timerWORK2 = null;

    function fn_OnUCWORKLostFocus2(s, e) {
        if (!s.GetText() || s.GetText() == '') {
            txtUCWORKNM2.SetValue('');
            txtUCWORKNM2.SetText('');
            hidUCWORKPOPCD2.SetValue('');
            hidUCWORKPOPCD2.SetText('');            
            txtUCWORKNM2.GetMainElement().title = '';
            return;
        }  else
            WORKCallback2.PerformCallback();
    }

    function fn_OnUCWORKKeyUp2(s, e) {
        fn_SetTextValue('hidUCWORKPOPCD', s.GetValue());
    }

    function fn_OnUCWORDEndCallback2(s, e) {
        var code = s.cpWORKCD;
        var text = s.cpWORKNM;

        if (code != '' && text != '') {
            hidUCWORKPOPCD2.SetValue(code);
            hidUCWORKPOPCD2.SetText(code);
            txtUCWORKNM2.SetValue(text);
            txtUCWORKNM2.SetText(text);
            txtUCWORKNM2.GetMainElement().title = text;
        } else {
            hidUCWORKPOPCD2.SetValue('');
            hidUCWORKPOPCD2.SetText('');
            txtUCWORKNM2.SetValue('');
            txtUCWORKNM2.SetText('');
            txtUCWORKNM2.GetMainElement().title = '';
        }
    }

    function fn_OnPopupWorkSearch2() {        
        var _ITEMCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidUCITEMCD2')) ? '' : ASPxClientControl.Cast('hidUCITEMCD2').GetText();
        var _WORKCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidUCWORKPOPCD2')) ? '' : ASPxClientControl.Cast('hidUCWORKPOPCD2').GetText();        

        pPage = rootURL + 'Pages/Common/Popup/WORKPOP.aspx' +
            '?TITLE=공정조회' +
            '&CRUD=R' +
            '&TYPE=FORM2' +
            '&ITEMCD=' + _ITEMCD +
            '&WORKCD=' + _WORKCD;
        fn_OnPopupOpen(pPage, '800', '500');
    }

    // 검색된 아이템 세팅
    function fn_OnSettingWork2(CODE, TEXT, BANCD, LINECD) {
        txtUCWORKPOPCD2.SetText(CODE);
        txtUCWORKNM2.SetText(TEXT);
        hidUCWORKPOPCD2.SetValue(CODE);
        hidUCWORKPOPCD2.SetText(CODE);

        <%=CallBackInsp%>;
    }

    // Tree용 Event
    function fn_OnSetupWorkTree2(oParam) {
        if (!isWORKEndCallback2) {
            timerWORK2 = setTimeout(function () { fn_OnSetupWorkTree2(oParam); }, 1000);
            isWORKEndCallback2 = parent.parent.isTreeWORKSetup2;
        } else {
            clearTimeout(timerWORK2);
            isWORKEndCallback2 = false;
            parent.parent.isTreeWORKSetup2 = false;
            var oParams = oParam.split('|');

            fn_SetTextValue('hidUCWORKPOPCD2', oParams[8]);
            fn_SetTextValue('txtUCWORKPOPCD2', oParams[8]);
            fn_SetTextValue('txtUCWORKNM2', oParams[9]);

            <%=CallBackInsp%>;
        }
    }

    $(document).ready(function () { if (!isPopup) parent.parent.isWorkUserControl2[parent.fn_GetIFrameID()] = true; });
</script>
<dx:ASPxTextBox ID="hidWORKPOPCD2" ClientInstanceName="hidUCWORKPOPCD2" runat="server" ClientVisible="false" />
<div class="control-label" style="float: left; width: 28%;">
    <dx:ASPxTextBox ID="txtWORKPOPCD2" ClientInstanceName="txtUCWORKPOPCD2" runat="server" Width="100%"
        OnInit="txtWORKPOPCD2_Init">
        <ClientSideEvents LostFocus="fn_OnUCWORKLostFocus2" KeyUp="fn_OnUCWORKKeyUp2" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 61%;">    
    <dx:ASPxTextBox ID="txtWORKNM2" ClientInstanceName="txtUCWORKNM2" runat="server" Width="100%">
        <ClientSideEvents Init="fn_OnControlDisableBox" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 9%;">
    <button class="btn btn-default btn-xs" data-toggle="button" title="공정조회" onclick="fn_OnPopupWorkSearch2(); return false;">
        <i class="i i-popup text"></i>
        <i class="i i-popup text-active text-danger"></i>
    </button>
</div>
<dx:ASPxCallback ID="WORKCallback2" ClientInstanceName="WORKCallback2" runat="server" OnCallback="WORKCallback2_Callback">
    <ClientSideEvents EndCallback="fn_OnUCWORDEndCallback2" />
</dx:ASPxCallback>
