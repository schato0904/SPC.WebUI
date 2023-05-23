<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucInspectionItem2.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucInspectionItem2" %>
<script type="text/javascript">
    var timerITEMCD2 = null;

    function fn_OnUCINSPITEMKeyUp2(s, e) {
        if (s.GetValue().length != 6) {
            fn_SetTextValue('txtUCINSPITEMNM', '');
        }
    }

    // PerFormCallbak Event Target Control
    function InspectionItemPerFormCallbackTarget2() {
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
    function fn_OnPopupUCInspectionItemSearch2() {
        if (true == fn_OnUCInspectionItemValidate2()) {
            if ('' != '<%=targetFields%>') {
                var vFields = '<%=targetFields%>'.split(';');

                var ITEMCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast(vFields[0])) ? '' : ASPxClientControl.Cast(vFields[0]).GetText();
                var WORKCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast(vFields[1])) ? '' : ASPxClientControl.Cast(vFields[1]).GetText();
            } else {
                var ITEMCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidUCITEMCD2')) ? '' : ASPxClientControl.Cast('hidUCITEMCD2').GetText();
                var WORKCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidUCWORKPOPCD2')) ? '' : ASPxClientControl.Cast('hidUCWORKPOPCD2').GetText();
            }
            var pPage = '<%=Page.ResolveUrl("~/Pages/Common/Popup/INSPITEMPOP.aspx")%>' +
                '?TITLE=검사항목조회' +
                '&CRUD=R' +
                '&TYPE=FORM2' +
                '&ITEMCD=' + ITEMCD + '' +
                '&WORKCD=' + WORKCD;
            fn_OnPopupOpen(pPage, 800, 500);
        }
    }

    // 검색된 검사항목 세팅
    function fn_OnUCSettingInspectionItem2(resultValues) {
        txtUCINSPITEMCD2.SetText(resultValues[0]);
        txtUCINSPITEMCD2.SetValue(resultValues[0]);
        txtUCINSPITEMNM2.SetText(resultValues[1]);
        txtUCINSPITEMNM2.SetValue(resultValues[1]);
        hidUCINSPITEMCD2.SetValue(resultValues[0]);
        hidUCINSPITEMCD2.SetText(resultValues[0]);
    }

    // Validate
    function fn_OnUCInspectionItemValidate2(s, e) {
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
    function fn_OnSetupInspectionTree2(oParam) {
        var oParams = oParam.split('|');

        fn_SetTextValue('hidUCINSPITEMCD2', oParams[10]);
        fn_SetTextValue('txtUCINSPITEMCD2', oParams[10]);
        fn_SetTextValue('txtUCINSPITEMNM2', oParams[11]);
        fn_SetTextValue('hidUCINSPECTIONCD2', oParams[12]);
        fn_SetTextValue('txtUCINSPECTIONNM2', oParams[13]);
        fn_SetTextValue('txtSTANDARD2', oParams[14]);
        fn_SetTextValue('txtMAX2', oParams[15]);
        fn_SetTextValue('txtMIN2', oParams[16]);
        fn_SetTextValue('txtUCLX2', oParams[17]);
        fn_SetTextValue('txtLCLX2', oParams[18]);
        fn_SetTextValue('txtUCLR2', oParams[19]);
        fn_SetTextValue('txtSERIALNO2', oParams[20]);
        fn_SetTextValue('txtSIRYO2', oParams[21]);
        fn_SetTextValue('txtFREEPOINT2', oParams[22]);

        timerITEMCD2 = setTimeout(fn_OnCheckItemCode2, 100);
    }

    // 부모값 입력 체크
    function fn_OnCheckItemCode2() {
        if (fn_OnValidateParentCtrl2() == false) {
            timerITEMCD2 = setTimeout(fn_OnCheckItemCode2, 100);
        } else {
            InspectionItemPerFormCallbackTarget2();
            clearTimeout(timerITEMCD2);
            timerITEMCD2 = null;
        }
    }

    function fn_OnValidateParentCtrl2() {
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

    $(document).ready(function () { if (!isPopup) parent.parent.isInspectionUserControl2[parent.fn_GetIFrameID()] = true; });

</script>
<dx:ASPxTextBox ID="hidINSPITEMCD2" ClientInstanceName="hidUCINSPITEMCD2" runat="server" ClientVisible="false" />
<div class="control-label" style="float: left; width: 28%;">
    <dx:ASPxTextBox ID="txtINSPITEMCD2" ClientInstanceName="txtUCINSPITEMCD2" runat="server" Width="100%"
        OnInit="txtINSPITEMCD2_Init">
        <ClientSideEvents KeyUp="fn_OnUCINSPITEMKeyUp2" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 61%;">
    <dx:ASPxTextBox ID="txtINSPITEMNM2" ClientInstanceName="txtUCINSPITEMNM2" runat="server" Width="100%">
        <ClientSideEvents Init="fn_OnControlDisableBox" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 9%;">
    <button class="btn btn-default btn-xs" data-toggle="button" title="검사항목조회" onclick="fn_OnPopupUCInspectionItemSearch2(); return false;">
        <i class="i i-popup text"></i>
        <i class="i i-popup text-active text-danger"></i>
    </button>
</div>