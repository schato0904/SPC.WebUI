<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMachine.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucMachine" %>
<script type="text/javascript">
    var isITEMEndCallback = false;
    var timerITEM = null;

    function fn_OnucMachineEndCallback(s, e) {
        var MACHCD = s.cpITEMCD;
        var MACHNM = s.cpITEMNM;
        var MACHTYPECD = s.cpMODELCD;
        var MACHTYPENM = s.cpMODELNM;

        if (MACHCD != '' && MACHNM != '') {
            hiducMachineCD.SetValue(MACHCD);
            hiducMachineCD.SetText(MACHCD);
            txtucMachineNM.SetValue(MACHNM);
            txtucMachineNM.SetText(MACHNM);
            txtucMachineNM.GetMainElement().title = MACHNM;
            hiducMachTypeCD.SetValue(MACHTYPECD);
            hiducMachTypeCD.SetText(MACHTYPECD);
            txtucMachtypeNM.SetText(MACHTYPENM);
            txtucMachtypeNM.SetValue(MACHTYPENM);
       
            ItemPerFormCallbackTarget();
        } else {
            hiducMachineCD.SetValue('');
            hiducMachineCD.SetText('');
            txtucMachineNM.SetValue('');
            txtucMachineNM.SetText('');
            txtucMachineNM.GetMainElement().title = '';
            hiducMachTypeCD.SetValue('');
            hiducMachTypeCD.SetText('');
            txtucMachtypeNM.SetText('');
            txtucMachtypeNM.SetValue('');
                    }
        //isITEMEndCallback = parent.parent.isTreeITEMSetup;
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
    function fn_OnPopupucMachineSearch() {        
        var Option = '<%=searchOption%>'
        var pPage = '<%=Page.ResolveUrl("~/Pages/Common/Popup/MACHPOP.aspx")%>' +
            '?TITLE=설비별타입조회' +
            '&CRUD=R' +
            '&TYPE=UC' +
            '&OPTION=' + Option +
            '&MACHCD=' + txtucMachineCD.GetText() +
            '&MACHTYPECD=' + hiducMachTypeCD.GetText() +
            '&MACHGUBUN=<%=machGubun%>';
        fn_OnPopupOpen(pPage, 800, 500);
    }

    // 검색된 아이템 세팅
    function fn_OnUCSettingItem(MACHCD, MACHNM, MACHTYPECD, MACHTYPENM) {

        txtucMachineCD.SetText(MACHCD);
        txtucMachineNM.SetText(MACHNM);
        hiducMachineCD.SetValue(MACHCD);
        hiducMachineCD.SetText(MACHCD);
        hiducMachTypeCD.SetValue(MACHTYPECD);
        hiducMachTypeCD.SetText(MACHTYPECD);
        txtucMachtypeNM.SetText(MACHTYPENM);
    }

    // Tree용 Event
    function fn_OnSetupItemTree(oParam) {
       
    }

    function fn_ValidateITEM() {
        if (hiducMachineCD.GetValue() == "" || hiducMachineCD.GetValue() == null) {
            alert("설비코드를 입력하세요!!"); return false;
        } else {
            return true;
        }
    }
    $(document).ready(function () { if (!isPopup) parent.parent.isItemUserControl[parent.fn_GetIFrameID()] = true; });
</script>
<dx:ASPxTextBox ID="hidMACHCD" ClientInstanceName="hiducMachineCD" runat="server" ClientVisible="false" />
<dx:ASPxTextBox ID="hidMACHTYPECD" ClientInstanceName="hiducMachTypeCD" runat="server" ClientVisible="false" />
<div class="control-label" style="float: left; width: 28%;">
    <dx:ASPxTextBox ID="txtMACHCD" ClientInstanceName="txtucMachineCD" runat="server" Width="100%"
        OnInit="txtMACHCD_Init">
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 30%;">
    <dx:ASPxTextBox ID="txtMACHNM" ClientInstanceName="txtucMachineNM" runat="server" Width="100%">
        <ClientSideEvents Init="fn_OnControlDisableBox" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 30%;">
    <dx:ASPxTextBox ID="txtMACHTYPENM" ClientInstanceName="txtucMachtypeNM" runat="server" Width="100%">
        <ClientSideEvents Init="fn_OnControlDisableBox" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 9%;">
    <button class="btn btn-default btn-xs" data-toggle="button" title="설비조회" onclick="fn_OnPopupucMachineSearch(); return false;">
        <i class="i i-popup text"></i>
        <i class="i i-popup text-active text-danger"></i>
    </button>
</div>
<dx:ASPxCallback ID="ITEMCallback" ClientInstanceName="ITEMCallback" runat="server" OnCallback="ITEMCallback_Callback">
    <ClientSideEvents EndCallback="fn_OnucMachineEndCallback" />
</dx:ASPxCallback>
