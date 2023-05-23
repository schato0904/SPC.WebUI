<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLine.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucLine" %>
<script type="text/javascript">
    var isLINEEndCallback = false;
    var timerLINE = null;

    function fn_OnLINEValueChanged(s, e) {
        var val = s.GetValue();
        hidLINECD.SetValue(val);
        LINEPerFormCallbackTarget();
    }

    // PerFormCallbak Event Target Control
    function LINEPerFormCallbackTarget() {
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

    function fn_OnLINEEndCallback(s, e) {
        isLINEEndCallback = parent.parent.isTreeLINESetup;
    }

    // Tree용 Event
    function fn_OnSetupLineTree(oParam) {
        if (!isLINEEndCallback) {
            timerLINE = setTimeout(function () { fn_OnSetupLineTree(oParam); }, 1000);
            isLINEEndCallback = parent.parent.isTreeLINESetup;
        } else {
            clearTimeout(timerLINE);
            timerLINE = null;
            isLINEEndCallback = false;
            parent.parent.isTreeLINESetup = false;
            var oParams = oParam.split('|');
            
            fn_SetTextValue('hidLINECD', oParams[2]);
            fn_SetSelectedItem('ddlLINE', oParams[2]);

            LINEPerFormCallbackTarget();
        }
    }

    function fn_Validateline() {
        if (hidLINECD.GetValue() == "" || hidLINECD.GetValue() == null) {
            alert("라인을 선택하세요!!"); return false;
        } else {
            return true;
        }
    }

    function fn_OnSetUCLine(oParam) {
        //var Ctrl = ASPxClientControl.Cast('ddlLINE')
        //if (Ctrl != null) {
        //    Ctrl.PerformCallback();
        //}

        fn_SetTextValue('hidLINECD', oParam);
        fn_SetSelectedItem('ddlLINE', oParam);
        var line = ASPxClientComboBox.Cast('ddlLINE');
        line.SetValue(oParam)
        line.SetText(line.FindItemByValue(oParam).text);
        //LINEPerFormCallbackTarget();

    }
    $(document).ready(function () { if (!isPopup) parent.parent.isLineUserControl[parent.fn_GetIFrameID()] = true; });
</script>
<dx:ASPxTextBox ID="hidLINECD" ClientInstanceName="hidLINECD" runat="server" ClientVisible="false" />
<dx:ASPxComboBox ID="ddlLINE" ClientInstanceName="ddlLINE" runat="server" Width="100%" 
    IncrementalFilteringMode="None" CssClass="NoXButton"
    TextField="F_LINENM" ValueField="F_LINECD" OnDataBound="ddlLINE_DataBound" OnCallback="ddlLINE_Callback">
    <ClientSideEvents ValueChanged="fn_OnLINEValueChanged" Init="fn_OnControlDisable" EndCallback="fn_OnLINEEndCallback" />
</dx:ASPxComboBox>