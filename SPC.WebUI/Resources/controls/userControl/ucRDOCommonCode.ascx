<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucRDOCommonCode.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucRDOCommonCode" %>
<script type="text/javascript">
    function fn_OnCOMMONCODEValueChanged(s, e) {
        var val = s.GetValue();
        hidCOMMONCODECD.SetValue(val);
        CommonCodePerFormCallbackTarget();
    }

    // PerFormCallbak Event Target Control
    function CommonCodePerFormCallbackTarget() {
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
<dx:ASPxTextBox ID="hidCOMMONCODECD" ClientInstanceName="hidCOMMONCODECD" runat="server" ClientVisible="false" />
<dx:ASPxRadioButtonList ID="rdoCOMMONCODE" ClientInstanceName="rdoCOMMONCODE" runat="server" Width="100%"
        IncrementalFilteringMode="None" CssClass="NoXButton"
        OnDataBound="rdoCOMMONCODE_DataBound">
    <ClientSideEvents ValueChanged="fn_OnCOMMONCODEValueChanged" Init="fn_OnControlDisable" />
</dx:ASPxRadioButtonList>
