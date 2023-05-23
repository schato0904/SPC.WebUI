<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBanPop.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucBanPop" %>
<script type="text/javascript">
    function fn_OnBANInit(s, e) {
        var parentParams = ''
        <%if (!Page.gsVENDOR) {%>
        var parentCompCD = parent.fn_OnGetCompCD();
        parentParams += (parentParams == '') ? parentCompCD : '|' + parentCompCD;
        var parentFactCD = parent.fn_OnGetFactCD();
        parentParams += (parentParams == '') ? parentFactCD : '|' + parentFactCD;
        <%}%>

        ddlBAN.PerformCallback(parentParams);

        fn_OnControlDisable(s, e);
    }

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
</script>
<dx:ASPxTextBox ID="hidBANCD" ClientInstanceName="hidBANCD" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlBAN" ClientInstanceName="ddlBAN" runat="server" Width="100%"
    IncrementalFilteringMode="None" CssClass="NoXButton"
    TextField="F_BANNM" ValueField="F_BANCD" OnDataBound="ddlBAN_DataBound" OnCallback="ddlBAN_Callback">
    <ClientSideEvents ValueChanged="fn_OnBANValueChanged" Init="fn_OnBANInit" />
</dx:ASPxComboBox>