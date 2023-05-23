<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucEquip.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucEquip" %>
<script type="text/javascript">
    function fn_OnEQUIPValueChanged(s, e) {
        var val = s.GetValue();

        hidEQUIPCD.SetValue(val);
        EquipPerFormCallbackTarget();
    }

    // PerFormCallbak Event Target Control
    function EquipPerFormCallbackTarget() {
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
    function fn_OnSetupEquipTree(oParam) {
        var oParams = oParam.split('|');

        fn_SetTextValue('hidEQUIPCD', oParams[0]);
        fn_SetSelectedItem('ddlEQUIP', oParams[0]);

        EquipPerFormCallbackTarget();
    }

    function fn_Validateban() {
        if (hidEQUIPCD.GetValue() == "" || hidEQUIPCD.GetValue() == null) {
            alert("반을 선택하세요!!"); return false;
        } else {
            return true;
        }
    }

    function fn_OnSetUCEquip(oParam) {
        fn_SetTextValue('hidEQUIPCD', oParam);
        fn_SetSelectedItem('ddlEQUIP', oParam);
        var ban = ASPxClientComboBox.Cast('ddlEQUIP');
        ban.SetValue(oParam)
        ban.SetText(ban.FindItemByValue(oParam).text);

        EquipPerFormCallbackTarget();
    }

    $(document).ready(function () { if (!isPopup) parent.parent.isEquipUserControl[parent.fn_GetIFrameID()] = true; });
</script>
<dx:ASPxTextBox ID="hidEQUIPCD" ClientInstanceName="hidEQUIPCD" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlEQUIP" ClientInstanceName="ddlEQUIP" runat="server" Width="100%"
    IncrementalFilteringMode="None" CssClass="NoXButton"
    TextField="F_EQUIPNM" ValueField="F_EQUIPCD" OnDataBound="ddlEQUIP_DataBound" OnCallback="ddlEQUIP_Callback">
    <ClientSideEvents ValueChanged="fn_OnEQUIPValueChanged" Init="fn_OnControlDisable" />
</dx:ASPxComboBox>