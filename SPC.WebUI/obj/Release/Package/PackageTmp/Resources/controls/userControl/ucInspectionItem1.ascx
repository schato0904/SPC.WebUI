<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucInspectionItem1.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucInspectionItem1" %>
<script type="text/javascript">
    var timerITEMCD1 = null;

    function fn_OnUCINSPITEMKeyUp1(s, e) {
        if (s.GetValue().length != 6) {
            fn_SetTextValue('txtUCINSPITEMNM', '');
        }
    }

    // PerFormCallbak Event Target Control
    function InspectionItemPerFormCallbackTarget1() {
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
    function fn_OnPopupUCInspectionItemSearch1() {
        if (true == fn_OnUCInspectionItemValidate1()) {
            if ('' != '<%=targetFields%>') {
                var vFields = '<%=targetFields%>'.split(';');

                var ITEMCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast(vFields[0])) ? '' : ASPxClientControl.Cast(vFields[0]).GetText();
                var WORKCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast(vFields[1])) ? '' : ASPxClientControl.Cast(vFields[1]).GetText();
            } else {
                var ITEMCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidUCITEMCD1')) ? '' : ASPxClientControl.Cast('hidUCITEMCD1').GetText();
                var WORKCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidUCWORKPOPCD1')) ? '' : ASPxClientControl.Cast('hidUCWORKPOPCD1').GetText();
            }
            var pPage = '<%=Page.ResolveUrl("~/Pages/Common/Popup/INSPITEMPOP.aspx")%>' +
                '?TITLE=검사항목조회' +
                '&CRUD=R' +
                '&TYPE=FORM1' +
                '&ITEMCD=' + ITEMCD + '' +
                '&WORKCD=' + WORKCD;
            fn_OnPopupOpen(pPage, 800, 500);
        }
    }

    // 검색된 검사항목 세팅
    function fn_OnUCSettingInspectionItem1(resultValues) {
        txtUCINSPITEMCD1.SetText(resultValues[0]);
        txtUCINSPITEMCD1.SetValue(resultValues[0]);
        txtUCINSPITEMNM1.SetText(resultValues[1]);
        txtUCINSPITEMNM1.SetValue(resultValues[1]);
        hidUCINSPITEMCD1.SetValue(resultValues[0]);
        hidUCINSPITEMCD1.SetText(resultValues[0]);
    }

    // Validate
    function fn_OnUCInspectionItemValidate1(s, e) {
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
    function fn_OnSetupInspectionTree1(oParam) {
        var oParams = oParam.split('|');

        fn_SetTextValue('hidUCINSPITEMCD1', oParams[10]);
        fn_SetTextValue('txtUCINSPITEMCD1', oParams[10]);
        fn_SetTextValue('txtUCINSPITEMNM1', oParams[11]);
        fn_SetTextValue('hidUCINSPECTIONCD1', oParams[12]);
        fn_SetTextValue('txtUCINSPECTIONNM1', oParams[13]);
        fn_SetTextValue('txtSTANDARD1', oParams[14]);
        fn_SetTextValue('txtMAX1', oParams[15]);
        fn_SetTextValue('txtMIN1', oParams[16]);
        fn_SetTextValue('txtUCLX1', oParams[17]);
        fn_SetTextValue('txtLCLX1', oParams[18]);
        fn_SetTextValue('txtUCLR1', oParams[19]);
        fn_SetTextValue('txtSERIALNO1', oParams[20]);
        fn_SetTextValue('txtSIRYO1', oParams[21]);
        fn_SetTextValue('txtFREEPOINT1', oParams[22]);

        timerITEMCD1 = setTimeout(fn_OnCheckItemCode1, 100);
    }

    // 부모값 입력 체크
    function fn_OnCheckItemCode1() {
        if (fn_OnValidateParentCtrl1() == false) {
            timerITEMCD1 = setTimeout(fn_OnCheckItemCode1, 100);
        } else {
            InspectionItemPerFormCallbackTarget1();
            clearTimeout(timerITEMCD1);
            timerITEMCD1 = null;
        }
    }

    function fn_OnValidateParentCtrl1() {
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

    $(document).ready(function () { if (!isPopup) parent.parent.isInspectionUserControl1[parent.fn_GetIFrameID()] = true; });
</script>
<dx:ASPxTextBox ID="hidINSPITEMCD1" ClientInstanceName="hidUCINSPITEMCD1" runat="server" ClientVisible="false" />
<div class="control-label" style="float: left; width: 28%;">
    <dx:ASPxTextBox ID="txtINSPITEMCD1" ClientInstanceName="txtUCINSPITEMCD1" runat="server" Width="100%"
        OnInit="txtINSPITEMCD1_Init">
        <ClientSideEvents KeyUp="fn_OnUCINSPITEMKeyUp1" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 61%;">
    <dx:ASPxTextBox ID="txtINSPITEMNM1" ClientInstanceName="txtUCINSPITEMNM1" runat="server" Width="100%">
        <ClientSideEvents Init="fn_OnControlDisableBox" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 9%;">
    <button class="btn btn-default btn-xs" data-toggle="button" title="검사항목조회" onclick="fn_OnPopupUCInspectionItemSearch1(); return false;">
        <i class="i i-popup text"></i>
        <i class="i i-popup text-active text-danger"></i>
    </button>
</div>