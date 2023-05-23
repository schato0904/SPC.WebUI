<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucModelDDL.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucModelDDL" %>
<script type="text/javascript">
    function fn_OnMODELValueChanged(s, e) {
        var val = s.GetValue();
        //hidBANCD.SetValue(val);
        hidMODELCD.SetValue(val);
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
</script>
<dx:ASPxTextBox ID="hidMODELCD" ClientInstanceName="hidMODELCD" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlMODEL" ClientInstanceName="ddlMODEL" runat="server" Width="100%"
    IncrementalFilteringMode="None" CssClass="NoXButton"
    TextField="F_MODELNM" ValueField="F_MODELCD" OnDataBound="ddlMODEL_DataBound" OnCallback="ddlMODEL_Callback">
    <ClientSideEvents ValueChanged="fn_OnMODELValueChanged" Init="fn_OnControlDisable" />
</dx:ASPxComboBox>