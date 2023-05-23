<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucWorkPOP1.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucWorkPOP1" %>
<script type="text/javascript">
    var isWORKEndCallback1 = false;
    var timerWORK1 = null;

    function fn_OnUCWORKLostFocus1(s, e) {
        if (!s.GetText() || s.GetText() == '') {
            txtUCWORKNM1.SetValue('');
            txtUCWORKNM1.SetText('');
            hidUCWORKPOPCD1.SetValue('');
            hidUCWORKPOPCD1.SetText('');            
            txtUCWORKNM1.GetMainElement().title = '';
            return;
        }  else
            WORKCallback1.PerformCallback();
    }

    function fn_OnUCWORKKeyUp1(s, e) {
        fn_SetTextValue('hidUCWORKPOPCD', s.GetValue());
    }

    function fn_OnUCWORDEndCallback1(s, e) {
        var code = s.cpWORKCD;
        var text = s.cpWORKNM;

        if (code != '' && text != '') {
            hidUCWORKPOPCD1.SetValue(code);
            hidUCWORKPOPCD1.SetText(code);
            txtUCWORKNM1.SetValue(text);
            txtUCWORKNM1.SetText(text);
            txtUCWORKNM1.GetMainElement().title = text;
        } else {
            hidUCWORKPOPCD1.SetValue('');
            hidUCWORKPOPCD1.SetText('');
            txtUCWORKNM1.SetValue('');
            txtUCWORKNM1.SetText('');
            txtUCWORKNM1.GetMainElement().title = '';
        }
    }

    function fn_OnPopupWorkSearch1() {

        if (true == fn_OnUCWorkValidate1()) {
            var _ITEMCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidUCITEMCD1')) ? '' : ASPxClientControl.Cast('hidUCITEMCD1').GetText();
            var _WORKCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidUCWORKPOPCD1')) ? '' : ASPxClientControl.Cast('hidUCWORKPOPCD1').GetText();
            var Popup = '<%=Page.ResolveUrl("~/Pages/Common/Popup/WORKPOP.aspx")%>'
        <%if(useWERD){%>
            Popup = '<%=Page.ResolveUrl("~/Pages/Common/Popup/WORKPOP_WERD.aspx")%>'
        <%}%>

            pPage = Popup +
                '?TITLE=공정조회' +
                '&CRUD=R' +
                '&TYPE=FORM1' +
                '&ITEMCD=' + _ITEMCD +
                '&WORKCD=' + _WORKCD;
            fn_OnPopupOpen(pPage, '800', '500');

        }
        
    }

    // Validate
    function fn_OnUCWorkValidate1(s, e) {
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

    // 검색된 아이템 세팅
    function fn_OnSettingWork1(CODE, TEXT, BANCD, LINECD) {
        txtUCWORKPOPCD1.SetText(CODE);
        txtUCWORKNM1.SetText(TEXT);
        hidUCWORKPOPCD1.SetValue(CODE);
        hidUCWORKPOPCD1.SetText(CODE);

        <%=CallBackInsp%>;
    }

    // 검색된 아이템 세팅
    function fn_OnSettingWork1_WERD(CODE, TEXT, BANCD, LINECD) {
        txtUCWORKPOPCD1.SetText(CODE);
        txtUCWORKNM1.SetText(TEXT);
        hidUCWORKPOPCD1.SetValue(CODE);
        hidUCWORKPOPCD1.SetText(CODE);
    }

    // Tree용 Event
    function fn_OnSetupWorkTree1(oParam) {
        if (!isWORKEndCallback1) {
            timerWORK1 = setTimeout(function () { fn_OnSetupWorkTree1(oParam); }, 1000);
            isWORKEndCallback1 = parent.parent.isTreeWORKSetup1;
        } else {
            clearTimeout(timerWORK1);
            isWORKEndCallback1 = false;
            parent.parent.isTreeWORKSetup1 = false;
            var oParams = oParam.split('|');

            fn_SetTextValue('hidUCWORKPOPCD1', oParams[8]);
            fn_SetTextValue('txtUCWORKPOPCD1', oParams[8]);
            fn_SetTextValue('txtUCWORKNM1', oParams[9]);

            <%=CallBackInsp%>;
        }
    }

    function fn_WorkcdPop1DisableBox(value) {
        var s = ASPxClientControl.Cast("txtUCWORKPOPCD1")
        var inputElement = s.GetInputElement();
        if (!value) {
            inputElement.disabled = true;
            inputElement.readonly = true;
            inputElement.style.backgroundColor = '#cccccc';
            var mainElement = s.GetMainElement();
            mainElement.style.backgroundColor = '#cccccc';
        } else {
            inputElement.disabled = false;
            inputElement.readonly = false;
            inputElement.style.backgroundColor = '#ffffff';
            var mainElement = s.GetMainElement();
            mainElement.style.backgroundColor = '#ffffff';
        }
    }

    function fn_WorkPopUPCheck() {
        var inputElement = ASPxClientControl.Cast("txtUCWORKPOPCD1").GetInputElement();

        if (!inputElement.readonly) {
            fn_OnPopupWorkSearch1();
        }
    }

    function fn_GetWorkCD() {
        return hidUCWORKPOPCD1.GetValue();
    }

    function fn_ValidateWORK1() {
        if (hidUCWORKPOPCD1.GetValue() == "" || hidUCWORKPOPCD1.GetValue() == null) {
            alert("공정을 입력하세요!!"); return false;
        } else {
            return true;
        }
    }

    $(document).ready(function () { if (!isPopup) parent.parent.isWorkUserControl1[parent.fn_GetIFrameID()] = true; });
</script>
<dx:ASPxTextBox ID="hidWORKPOPCD1" ClientInstanceName="hidUCWORKPOPCD1" runat="server" ClientVisible="false" />
<div class="control-label" style="float: left; width: 28%;">
    <dx:ASPxTextBox ID="txtWORKPOPCD1" ClientInstanceName="txtUCWORKPOPCD1" runat="server" Width="100%"
        OnInit="txtWORKPOPCD1_Init">
        <ClientSideEvents LostFocus="fn_OnUCWORKLostFocus1" KeyUp="fn_OnUCWORKKeyUp1" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 61%;">    
    <dx:ASPxTextBox ID="txtWORKNM1" ClientInstanceName="txtUCWORKNM1" runat="server" Width="100%">
        <ClientSideEvents Init="fn_OnControlDisableBox" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 9%;">
    <button class="btn btn-default btn-xs" data-toggle="button" title="공정조회" onclick="fn_WorkPopUPCheck(); return false;">
        <i class="i i-popup text"></i>
        <i class="i i-popup text-active text-danger"></i>
    </button>
</div>
<dx:ASPxCallback ID="WORKCallback1" ClientInstanceName="WORKCallback1" runat="server" OnCallback="WORKCallback1_Callback">
    <ClientSideEvents EndCallback="fn_OnUCWORDEndCallback1" />
</dx:ASPxCallback>
