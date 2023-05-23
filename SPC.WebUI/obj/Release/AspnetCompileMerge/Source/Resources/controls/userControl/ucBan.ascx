<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBan.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucBan" %>
<script type="text/javascript">
    function fn_OnBANValueChanged(s, e) {
        var val = s.GetValue();

        hidBANCD.SetValue(val);
        BanPerFormCallbackTarget();
    }

    // PerFormCallbak Event Target Control
    function BanPerFormCallbackTarget() {
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

    // Tree용 Event
    function fn_OnSetupBanTree(oParam) {
        var oParams = oParam.split('|');
        
        fn_SetTextValue('hidBANCD', oParams[0]);
        fn_SetSelectedItem('ddlBAN', oParams[0]);

        BanPerFormCallbackTarget();
    }

    function fn_Validateban() {
        if (hidBANCD.GetValue() == "" || hidBANCD.GetValue() == null) {
            alert("반을 선택하세요!!"); return false;
        } else {
            return true;
        }
    }

    function fn_OnSetUCBan(oParam) {
        fn_SetTextValue('hidBANCD', oParam);
        fn_SetSelectedItem('ddlBAN', oParam);
        var ban = ASPxClientComboBox.Cast('ddlBAN');
        ban.SetValue(oParam)
        ban.SetText(ban.FindItemByValue(oParam).text);

        BanPerFormCallbackTarget();
    }

    $(document).ready(function () { <%if (false==bAutoFillByTree) {%>if(!isPopup) parent.parent.isBanUserControl[parent.fn_GetIFrameID()] = true;<%}%> });
</script>
<dx:ASPxTextBox ID="hidBANCD" ClientInstanceName="hidBANCD" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlBAN" ClientInstanceName="ddlBAN" runat="server" Width="100%"
    IncrementalFilteringMode="None" CssClass="NoXButton"
    TextField="F_BANNM" ValueField="F_BANCD" OnDataBound="ddlBAN_DataBound" OnCallback="ddlBAN_Callback">
    <ClientSideEvents ValueChanged="fn_OnBANValueChanged" Init="fn_OnControlDisable" />
</dx:ASPxComboBox>