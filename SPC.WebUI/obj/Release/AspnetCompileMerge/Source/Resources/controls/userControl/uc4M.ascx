<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc4M.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.uc4M" %>
<script type="text/javascript">
    var is4MEndCallback = false;
    var timer4M = null;

    function fn_On4MValueChanged(s, e) {
        var val = s.GetValue();
        hid4MCD.SetValue(val);
    }

    function fn_On4MEndCallback(s, e) {
        is4MEndCallback = parent.parent.isTree4MSetup;
    }

    // Tree용 Event
    function fn_OnSetup4MTree(oParam) {
        if (!is4MEndCallback) {
            timer4M = setTimeout(function () { fn_OnSetup4MTree(oParam); }, 1000);
            is4MEndCallback = parent.parent.isTree4MSetup;
        } else {
            clearTimeout(timer4M);
            is4MEndCallback = false;
            parent.parent.isTree4MSetup = false;
            var oParams = oParam.split('|');

            fn_SetTextValue('hid4MCD', oParams[8]);
            fn_SetSelectedItem('ddl4M', oParams[8]);
        }
    }

    function fn_OnSetUC4M(oParam) {
        fn_SetTextValue('hid4MCD', oParam);
        fn_SetSelectedItem('ddl4M', oParam);
        var VAL = ASPxClientComboBox.Cast('ddl4M');
        VAL.SetValue(oParam)
        VAL.SetText(VAL.FindItemByValue(oParam).text);
        //ctrl.FindItemByValue(val).index
    }


</script>
<dx:ASPxTextBox ID="hid4MCD" ClientInstanceName="hid4MCD" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddl4M" ClientInstanceName="ddl4M" runat="server" Width="100%"
    IncrementalFilteringMode="None" CssClass="NoXButton"
    TextField="F_FOURNM" ValueField="F_FOURCD"
    OnDataBound="ddl4M_DataBound" OnCallback="ddl4M_Callback">
    <ClientSideEvents ValueChanged="fn_On4MValueChanged" Init="fn_OnControlDisable" EndCallback="fn_On4MEndCallback" />
</dx:ASPxComboBox>