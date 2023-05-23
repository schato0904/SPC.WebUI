<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucComp.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucComp" %>
<script type="text/javascript">
    function fn_OnCOMPValueChanged(s, e) {
        factParam = '';
        var val = s.GetValue();
        hidCOMPCD.SetValue(val);
        COMPPerFormCallbackTarget();
    }

    // PerFormCallbak Event Target Control
    function COMPPerFormCallbackTarget() {
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

    // Get CompCD
    function fn_OnGetCompCD() {
        return hidCOMPCD.GetValue();
    }

    // Init
    function fn_COMPInit(s, e) {
        fn_OnControlDisable(s, e);
        
        var compParam = '<%=compParam%>';
        if (compParam != '') {
            // 업체
            fn_SetTextValue('hidCOMPCD', compParam);
            fn_SetSelectedItem('ddlCOMP', compParam);

            fn_OnCOMPValueChanged(s, e);
        }
    }
</script>
<dx:ASPxTextBox ID="hidCOMPCD" ClientInstanceName="hidCOMPCD" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlCOMP" ClientInstanceName="ddlCOMP" runat="server" Width="100%"
    IncrementalFilteringMode="None" CssClass="NoXButton"
    TextField="F_COMPNM" ValueField="F_COMPCD" OnDataBound="ddlCOMP_DataBound">
    <ClientSideEvents ValueChanged="fn_OnCOMPValueChanged" Init="fn_COMPInit" />
</dx:ASPxComboBox>
