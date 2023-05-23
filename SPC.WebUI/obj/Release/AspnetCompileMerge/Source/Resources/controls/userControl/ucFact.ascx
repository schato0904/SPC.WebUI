<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucFact.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucFact" %>
<script type="text/javascript">
    var isFactCallbackEnd = false;
    var timerFACT = null;

    function fn_OnFACTValueChanged(s, e) {
        if (s.GetItemCount() > 1) {
        var val = s.GetValue();
        hidFACTCD.SetValue(val);
        FACTPerFormCallbackTarget();
    }
    }

    // PerFormCallbak Event Target Control
    function FACTPerFormCallbackTarget() {
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

    // Get FactCD
    function fn_OnGetFactCD() {
        return hidFACTCD.GetValue();
    }

    // Init
    function fn_FACTInit(s, e) {
        fn_OnControlDisable(s, e);

        factParam = '<%=factParam%>';
    }

    // EndCallback
    function fn_FACTEndCallback(s, e) {
        if (factParam != '') {
            // 업체
            fn_SetTextValue('hidFACTCD', factParam);
            fn_SetSelectedItem('ddlFACT', factParam);

            isFactCallbackEnd = true;
        } else {
                ddlFACT.SetSelectedIndex(1);
                fn_SetTextValue('hidFACTCD', ddlFACT.GetValue());
            FACTPerFormCallbackTarget();
        }
    }
</script>
<dx:ASPxTextBox ID="hidFACTCD" ClientInstanceName="hidFACTCD" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlFACT" ClientInstanceName="ddlFACT" runat="server" Width="100%"
    IncrementalFilteringMode="None" CssClass="NoXButton"
    TextField="F_FACTNM" ValueField="F_FACTCD" OnDataBound="ddlFACT_DataBound" OnCallback="ddlFACT_Callback">
    <ClientSideEvents ValueChanged="fn_OnFACTValueChanged" Init="fn_FACTInit" EndCallback="fn_FACTEndCallback" />
</dx:ASPxComboBox>
