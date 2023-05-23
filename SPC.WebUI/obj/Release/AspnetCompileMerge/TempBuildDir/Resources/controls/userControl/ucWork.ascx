<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucWork.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucWork" %>
<script type="text/javascript">
    var isWORKEndCallback = false;
    var timerWORK = null;

    function fn_OnWORKValueChanged(s, e) {
        var val = s.GetValue();
        hidWORKCD.SetValue(val);
    }

    function fn_OnWORKEndCallback(s, e) {
        isWORKEndCallback = parent.parent.isTreeWORKSetup;
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

            fn_SetTextValue('hidWORKCD', oParams[8]);
            fn_SetSelectedItem('ddlWORK', oParams[8]);
        }
    }

    function fn_OnSetUCWork(oParam) {
        if (typeof (oParam) == 'undefined' || oParam == null || oParam == '') {
            fn_SetTextValue('hidWORKCD', oParam);
            fn_SetSelectedItem('ddlWORK', oParam);
            var work = ASPxClientComboBox.Cast('ddlWORK');
            work.SetValue(oParam)
            work.SetText("전체");
        } else {
            fn_SetTextValue('hidWORKCD', oParam);
            fn_SetSelectedItem('ddlWORK', oParam);
            var work = ASPxClientComboBox.Cast('ddlWORK');
            work.SetValue(oParam)
            work.SetText(work.FindItemByValue(oParam).text);
        }
    }


    $(document).ready(function () { if (!isPopup) parent.parent.isWorkUserControl[parent.fn_GetIFrameID()] = true; });
</script>
<dx:ASPxTextBox ID="hidWORKCD" ClientInstanceName="hidWORKCD" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlWORK" ClientInstanceName="ddlWORK" runat="server" Width="100%"
    IncrementalFilteringMode="None" CssClass="NoXButton"
    TextField="F_WORKNM" ValueField="F_WORKCD"
    OnDataBound="ddlWORK_DataBound" OnCallback="ddlWORK_Callback">
    <ClientSideEvents ValueChanged="fn_OnWORKValueChanged" Init="fn_OnControlDisable" EndCallback="fn_OnWORKEndCallback" />
</dx:ASPxComboBox>