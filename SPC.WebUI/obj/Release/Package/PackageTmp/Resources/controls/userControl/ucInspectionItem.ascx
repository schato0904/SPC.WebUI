<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucInspectionItem.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucInspectionItem" %>
<script type="text/javascript">
    var timerITEMCD = null;

    function fn_OnUCINSPITEMKeyUp(s, e) {
        if (fn_GetCastValue('txtUCINSPITEMCD').length != 6) {
            fn_SetTextValue('txtUCINSPITEMNM', '');
        }
    }

    // PerFormCallbak Event Target Control
    function InspectionItemPerFormCallbackTarget() {
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

    // 검사항목검색창 오픈
    function fn_OnPopupUCInspectionItemSearch() {
        if (true == fn_OnUCInspectionItemValidate()) {
            if ('' != '<%=targetFields%>') {
                var vFields = '<%=targetFields%>'.split(';');

                var ITEMCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast(vFields[0])) ? '' : ASPxClientControl.Cast(vFields[0]).GetText();
                var WORKCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast(vFields[1])) ? '' : ASPxClientControl.Cast(vFields[1]).GetText();
            } else {
                var ITEMCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidUCITEMCD')) ? '' : ASPxClientControl.Cast('hidUCITEMCD').GetText();
                var WORKCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidUCWORKPOPCD')) ? '' : ASPxClientControl.Cast('hidUCWORKPOPCD').GetText();
            }

        
            var pPage = '<%=Page.ResolveUrl(string.Format("~/Pages/Common/Popup/{0}",Pagenm))%>' +
                '?TITLE=검사항목조회' +
                '&CRUD=R' +
                '&ITEMCD=' + ITEMCD + '' +
                '&WORKCD=' + WORKCD;
            fn_OnPopupOpen(pPage, 800, 500);
        }
    }

    // 검색된 검사항목 세팅
    function fn_OnUCSettingInspectionItem(resultValues) {
        txtUCINSPITEMCD.SetText(resultValues[0]);
        txtUCINSPITEMCD.SetValue(resultValues[0]);
        txtUCINSPITEMNM.SetText(resultValues[1]);
        txtUCINSPITEMNM.SetValue(resultValues[1]);
        hidUCINSPITEMCD.SetValue(resultValues[0]);
        hidUCINSPITEMCD.SetText(resultValues[0]);

        InspectionItemPerFormCallbackTarget();
    }

    // Validate
    function fn_OnUCInspectionItemValidate() {
        var bValidate = true;
        if ('' != '<%=validateFields%>') {
            var vFields = '<%=validateFields%>'.split(';');

            if (vFields.length > 0) {
                $.each(vFields, function (key, value) {
                    var Fields = value.split('|');
                    var Field = ASPxClientControl.Cast(Fields[0]);
                    if (Field.GetText() == '') {
                        alert(Fields[1] + '을 입력하세요!!');
                        bValidate = false;
                        return false;
                    }
                });
            }
        }

        return bValidate;
    }

    // Tree용 Event
    function fn_OnSetupInspectionTree(oParam) {
        var oParams = oParam.split('|');
        
        fn_SetTextValue('hidUCINSPITEMCD', oParams[10]);
        fn_SetTextValue('txtUCINSPITEMCD', oParams[10]);
        fn_SetTextValue('txtUCINSPITEMNM', oParams[11]);
        fn_SetTextValue('hidUCINSPECTIONCD', oParams[12]);
        fn_SetTextValue('txtUCINSPECTIONNM', oParams[13]);
        fn_SetTextValue('txtSTANDARD', oParams[14]);
        fn_SetTextValue('txtMAX', oParams[15]);
        fn_SetTextValue('txtMIN', oParams[16]);
        fn_SetTextValue('txtUCLX', oParams[17]);
        fn_SetTextValue('txtLCLX', oParams[18]);
        fn_SetTextValue('txtUCLR', oParams[19]);
        fn_SetTextValue('txtSERIALNO', oParams[20]);
        fn_SetTextValue('txtSIRYO', oParams[21]);
        fn_SetTextValue('txtFREEPOINT', oParams[22]);

        timerITEMCD = setTimeout(fn_OnCheckItemCode, 100);
    }

    // 부모값 입력 체크
    function fn_OnCheckItemCode() {
        if (fn_OnValidateParentCtrl() == false) {
            timerITEMCD = setTimeout(fn_OnCheckItemCode, 100);
        } else {
            InspectionItemPerFormCallbackTarget();
            clearTimeout(timerITEMCD);
            timerITEMCD = null;
        }
    }

    function fn_OnValidateParentCtrl() {
        var bValidate = true;
        if ('' != '<%=validateFields%>') {
            var vFields = '<%=validateFields%>'.split(';');

            if (vFields.length > 0) {
                $.each(vFields, function (key, value) {
                    var Fields = value.split('|');
                    var Field = ASPxClientControl.Cast(Fields[0]);
                    if (Field.GetText() == '') {
                        bValidate = false;
                        return false;
                    }
                });
            }
        }

        return bValidate;
    }

    function fn_ValidateINSPITEM() {
        if (hidUCINSPITEMCD.GetValue() == "" || hidUCINSPITEMCD.GetValue() == null) {
            alert("검사항목을 입력하세요!!"); return false;
        } else {
            return true;
        }
    }

    $(document).ready(function () { if (!isPopup) parent.parent.isInspectionUserControl[parent.fn_GetIFrameID()] = true; });
</script>
<dx:ASPxTextBox ID="hidINSPITEMCD" ClientInstanceName="hidUCINSPITEMCD" runat="server" ClientVisible="false" />
<div class="control-label" style="float: left; width: 28%;">
    <dx:ASPxTextBox ID="txtINSPITEMCD" ClientInstanceName="txtUCINSPITEMCD" runat="server" Width="100%"
        OnInit="txtINSPITEMCD_Init">
        <ClientSideEvents KeyUp="fn_OnUCINSPITEMKeyUp" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 61%;">
    <dx:ASPxTextBox ID="txtINSPITEMNM" ClientInstanceName="txtUCINSPITEMNM" runat="server" Width="100%">
        <ClientSideEvents Init="fn_OnControlDisableBox" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 9%;">
    <button class="btn btn-default btn-xs" data-toggle="button" title="검사항목조회" onclick="fn_OnPopupUCInspectionItemSearch(); return false;">
        <i class="i i-popup text"></i>
        <i class="i i-popup text-active text-danger"></i>
    </button>
</div>