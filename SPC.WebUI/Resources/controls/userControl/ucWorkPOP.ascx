<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucWorkPOP.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucWorkPOP" %>
<script type="text/javascript">
    var isWORKEndCallback = false;
    var timerWORK = null;

    function fn_OnUCWORKLostFocus(s, e) {
        if (!s.GetText() || s.GetText() == '') {
            txtUCWORKNM.SetValue('');
            txtUCWORKNM.SetText('');
            hidUCWORKPOPCD.SetValue('');
            hidUCWORKPOPCD.SetText('');            
            txtUCWORKNM.GetMainElement().title = '';
            return;
        }  else
            WORKCallback.PerformCallback();
    }

    function fn_OnUCWORKKeyUp(s, e) {
        fn_SetTextValue('hidUCWORKPOPCD', s.GetValue());
    }

    function fn_OnUCWORDEndCallback(s, e) {
        var code = s.cpWORKCD;
        var text = s.cpWORKNM;

        if (code != '' && text != '') {
            if (code == hidUCWORKPOPCD.GetValue()) {
                hidUCWORKPOPCD.SetValue(code);
                hidUCWORKPOPCD.SetText(code);
                txtUCWORKNM.SetValue(text);
                txtUCWORKNM.SetText(text);
                txtUCWORKNM.GetMainElement().title = text;

                <%=CallBackInsp%>;
            } else {
                txtUCWORKNM.SetValue('');
                txtUCWORKNM.SetText('');
                txtUCWORKNM.GetMainElement().title = '';
            }
        } else {
            hidUCWORKPOPCD.SetValue('');
            hidUCWORKPOPCD.SetText('');
            txtUCWORKNM.SetValue('');
            txtUCWORKNM.SetText('');
            txtUCWORKNM.GetMainElement().title = '';
        }

        isWORKEndCallback = parent.parent.isTreeWORKSetup;
    }

    function fn_OnPopupWorkSearch() {        
        var _ITEMCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidUCITEMCD')) ? '' : ASPxClientControl.Cast('hidUCITEMCD').GetText();
        var _WORKCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidUCWORKPOPCD')) ? '' : ASPxClientControl.Cast('hidUCWORKPOPCD').GetText();
        
        if (txtUCWORKPOPCD.GetValue() == "" || txtUCWORKPOPCD.GetValue() == null) {
            _WORKCD = "";
        }
        pPage = rootURL + 'Pages/Common/Popup/WORKPOP.aspx' +
            '?TITLE=공정조회' +
            '&CRUD=R' +
            '&TYPE=FORM' +
            '&ITEMCD=' + _ITEMCD +
            '&WORKCD=' + _WORKCD +
            '&MACHGUBUN=<%=machGubun%>';
        fn_OnPopupOpen(pPage, '800', '500');
    }

    // 검색된 아이템 세팅
    function fn_OnSettingWork(CODE, TEXT, BANCD, LINECD) {
        txtUCWORKPOPCD.SetText(CODE);
        txtUCWORKNM.SetText(TEXT);
        hidUCWORKPOPCD.SetValue(CODE);
        hidUCWORKPOPCD.SetText(CODE);

        <%=CallBackInsp%>;
    }

    // Tree용 Event
    function fn_OnSetupWorkTree(oParam) {
        if (!isWORKEndCallback) {
            timerWORK = setTimeout(function () { fn_OnSetupWorkTree(oParam); }, 1000);
            isWORKEndCallback = parent.parent.isTreeWORKSetup;
        } else {
            clearTimeout(timerWORK);
            isWORKEndCallback = false;
            parent.parent.isTreeWORKSetup = false;
            var oParams = oParam.split('|');

            fn_SetTextValue('hidUCWORKPOPCD', oParams[8]);
            fn_SetTextValue('txtUCWORKPOPCD', oParams[8]);
            fn_SetTextValue('txtUCWORKNM', oParams[9]);

            <%=CallBackInsp%>;
        }
    }

    function fn_ValidateWORK() {
        if (hidUCWORKPOPCD.GetValue() == "" || hidUCWORKPOPCD.GetValue() == null) {
            alert("공정을 입력하세요!!"); return false;
        } else {
            return true;
        }
    }

    $(document).ready(function () { if (!isPopup) parent.parent.isWorkUserControl[parent.fn_GetIFrameID()] = true; });
</script>
<dx:ASPxTextBox ID="hidWORKPOPCD" ClientInstanceName="hidUCWORKPOPCD" runat="server" ClientVisible="false" />
<div class="control-label" style="float: left; width: 28%;">
    <dx:ASPxTextBox ID="txtWORKPOPCD" ClientInstanceName="txtUCWORKPOPCD" runat="server" Width="100%"
        OnInit="txtWORKPOPCD_Init">
        <ClientSideEvents LostFocus="fn_OnUCWORKLostFocus" KeyUp="fn_OnUCWORKKeyUp" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 61%;">    
    <dx:ASPxTextBox ID="txtWORKNM" ClientInstanceName="txtUCWORKNM" runat="server" Width="100%">
        <ClientSideEvents Init="fn_OnControlDisableBox" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 9%;">
    <button class="btn btn-default btn-xs" data-toggle="button" title="공정조회" onclick="fn_OnPopupWorkSearch(); return false;">
        <i class="i i-popup text"></i>
        <i class="i i-popup text-active text-danger"></i>
    </button>
</div>
<dx:ASPxCallback ID="WORKCallback" ClientInstanceName="WORKCallback" runat="server" OnCallback="WORKCallback_Callback">
    <ClientSideEvents EndCallback="fn_OnUCWORDEndCallback" />
</dx:ASPxCallback>
