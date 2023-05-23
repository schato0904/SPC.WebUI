<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucItemMulti.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucItemMulti" %>
<%-- 
    사용 예)    
    1) web.config에 등록 필요
            <add tagPrefix="ucCTF" tagName="ItemMulti" src="~/Resources/controls/userControl/ucItemMulti.ascx" />
    2) .aspx
            <ucCTF:ItemMulti id="srcF_ITEMCD" runat="server" ClientInstanceName="srcF_ITEMCD" OnChanged="function(s,e){ CallbackFunction(s,e); }"  />
            : targetCtrls, searchOption, machGubun 속성은 기존과 동일하게 사용
            : Dev컨트롤과 유사하게 ClientInstanceName으로 접근하여 사용
            : OnChanged 이벤트 제공(javascript)
    3) javascript
            품목코드 : srcF_ITEMCD.GetValue()
            품목명 : srcF_ITEMCD.GetText()
            초기화 : srcF_ITEMCD.Clear()
            품목이 설정되거나 변경되었을때 OnChanged 이벤트 사용하여 처리
    4) .cs
            품목코드 : srcF_ITEMCD.ITEMCD 또는 srcF_ITEMCD.GetValue()
            품목명 : srcF_ITEMCD.ITEMNM 또는 srcF_ITEMCD.GetText()
            품목 설정시 : srcF_ITEMCD.ITEMCD = '......'; srcF_ITEMCD.ITEMNM = '......'; 형태로 설정
     --%>
<script type="text/javascript">
    var _ID = '<%=this.ClientInstanceName%>';
   

    $(document).ready(function () {        
        var ID;

        if (_ID != '') { // && typeof(window[ID]) == 'undefined') {
            window[_ID] = new (function () {
                var _hidITEMCD = '<%=this.hidITEMCD.ClientInstanceName%>';
                var _txtITEMCD = '<%=this.txtITEMCD.ClientInstanceName%>';
                var _txtITEMNM = '<%=this.txtITEMNM.ClientInstanceName%>';
                var _ITEMCallback = '<%=this.ITEMCallback.ClientInstanceName%>';
                var _targetCtrls = '<%=this.targetCtrls%>';
                var _url = '<%=Page.ResolveUrl("~/Pages/Common/Popup/ITEMPOPMulti.aspx")%>';
                var _searchOption = '<%=searchOption%>';
                var _machGubun = '<%=machGubun%>';
                var _btnSearch = '<%=this.btnSearch.ClientID%>';
                var _OnChanged = (function () { return <%=this.OnChanged%>; })();
                var _hidUCMODELCD = '<%=hidUCMODELCD%>';
                var _txtUCMODELNM = '<%=txtUCMODELNM%>';

                this.isITEMEndCallback = false;
                this.timerITEM = null;
                // 멤버변수 세팅
                this.ID = _ID;
                var hidITEMCD = ASPxClientTextBox.Cast(_hidITEMCD);
                var txtITEMCD = ASPxClientTextBox.Cast(_txtITEMCD);
                var txtITEMNM = ASPxClientTextBox.Cast(_txtITEMNM);
                var ITEMCallback = ASPxClientCallback.Cast(_ITEMCallback);
                var targetCtrls = _targetCtrls;
                var url = _url;
                var searchOption = _searchOption;
                var machGubun = _machGubun;
                var btnSearch = _btnSearch;
                var OnChanged = _OnChanged;
                var hidUCMODELCD = ASPxClientTextBox.Cast(_hidUCMODELCD) instanceof ASPxClientTextBox ? ASPxClientTextBox.Cast(_hidUCMODELCD) : null;
                var txtUCMODELNM = ASPxClientTextBox.Cast(_txtUCMODELNM) instanceof ASPxClientTextBox ? ASPxClientTextBox.Cast(_txtUCMODELNM) : null;

                // 멤버 메소드
                this.fn_OnUCITEMLostFocus = function (s, e) {
                    if (!s.GetText() || s.GetText() == '') {
                        this.fn_OnUCSettingItem('', '', '', '');

                        // To Do: 공정 콤보박스 처리
                        //// 공정 DropdownList가 있는 경우
                        //if (ASPxClientUtils.IsExists(ASPxClientControl.Cast('ddlWORK'))) {
                        //    ItemPerFormCallbackTarget();
                        //}
                        return;
                    } else
                        ITEMCallback.PerformCallback();
                };

                this.fn_OnUCITEMKeyUp = function (s, e) {
                    fn_SetTextValue(hidUCITEMCD, s.GetValue());
                };

                this.fn_OnUCITEMEndCallback = function (s, e) {
                    var ITEMCD = s.cpITEMCD;
                    var ITEMNM = s.cpITEMNM;
                    var MODELCD = s.cpMODELCD;
                    var MODELNM = s.cpMODELNM;

                    if (ITEMCD != '' && ITEMNM != '') {
                        this.fn_OnUCSettingItem(ITEMCD, ITEMNM, MODELCD, MODELNM);

                        this.ItemPerFormCallbackTarget();
                    } else {
                        this.fn_OnUCSettingItem('', '', '', '');
                    }

                    // To Do: 트리 처리
                    //isITEMEndCallback = parent.parent.isTreeITEMSetup;
                };

                // PerFormCallbak Event Target Control
                this.ItemPerFormCallbackTarget = function () {
                    if ('' != targetCtrls) {
                        var tCtrls = targetCtrls.split(';');

                        if (tCtrls.length > 0) {
                            $.each(tCtrls, function (key, tCtrl) {
                                var Ctrl = ASPxClientControl.Cast(tCtrl);
                                Ctrl.PerformCallback();
                            });
                        }
                    }
                };

                // 아이템검색창 오픈
                // To Do: 반코드 어떻게 할 것인지? 1. 기존 반코드 uc 사용시 2. ucBanMulti 사용시
                this.fn_OnPopupUCItemSearch = function () {
                    var Option = searchOption;
                    var BANCD = IsNull(ASPxClientControl.Cast('hidBANCD'), '') == '' ? '' : ASPxClientControl.Cast('hidBANCD').GetText();
                    var pPage = url +
                        '?TITLE=품목조회' +
                        '&CRUD=R' +
                        '&TYPE=UC' +
                        '&OPTION=' + Option +
                        '&BANCD=' + BANCD + '' +
                        '&ITEMCD=' + txtITEMCD.GetText() +
                        '&MACHGUBUN=' + machGubun +
                        '&UCID=' + this.ID; // 호출한 uc에 접근할수 있도록 ID를 파라미터로 전송
                    fn_OnPopupOpen(pPage, 800, 500);
                };

                // 검색된 아이템 세팅
                this.fn_OnUCSettingItem = function (ITEMCD, ITEMNM, MODELCD, MODELNM) {
                    txtITEMCD.SetText(ITEMCD);
                    txtITEMNM.SetText(ITEMNM);
                    hidITEMCD.SetValue(ITEMCD);
                    hidITEMCD.SetText(ITEMCD);
                    txtITEMNM.GetMainElement().title = ITEMNM;

                    if (hidUCMODELCD && txtUCMODELNM) {
                        hidUCMODELCD.SetValue(MODELCD);
                        hidUCMODELCD.SetText(MODELCD);
                        txtUCMODELNM.SetValue(MODELNM);
                        txtUCMODELNM.SetText(MODELNM);
                        txtUCMODELNM.GetMainElement().title = MODELNM;
                    }

                    if (typeof OnChanged == 'function') {
                        OnChanged.call(window, this, null);
                    }
                };

                // To Do: 트리 처리
                // Tree용 Event
                this.fn_OnSetupItemTree = function (oParam) {
                    if (!this.isITEMEndCallback) {
                        this.timerITEM = setTimeout(function () { this.fn_OnSetupItemTree(oParam); }, 1000);
                        this.isITEMEndCallback = parent.parent.isTreeITEMSetup;
                    } else {
                        clearTimeout(this.timerITEM);
                        this.timerITEM = null;
                        this.isITEMEndCallback = false;
                        parent.parent.isTreeITEMSetup = false;
                        var oParams = oParam.split('|');

                        fn_SetTextValue(hidUCITEMCD, oParams[4]);
                        fn_SetTextValue(txtUCITEMCD, oParams[4]);
                        fn_SetTextValue(txtUCITEMNM, oParams[5]);

                        fn_SetTextValue(hidUCMODELCD, oParams[6]);
                        fn_SetTextValue(txtUCMODELNM, oParams[7]);

                        this.ItemPerFormCallbackTarget();
                    }
                };

                this.fn_ValidateITEM = function () {
                    if (hidUCITEMCD.GetValue() == "" || hidUCITEMCD.GetValue() == null) {
                        alert("품목을 입력하세요!!"); return false;
                    } else {
                        return true;
                    }
                };

                this.GetValue = function () {
                    return hidITEMCD.GetText();
                };

                this.GetText = function () {
                    return txtITEMNM.GetText();
                };

                this.Clear = function () {
                    this.fn_OnUCSettingItem('', '', '', '');
                };

                // To Do: 처리 필요
                //$(document).ready(function () { if (!isPopup) parent.parent.isItemUserControl[parent.fn_GetIFrameID()] = true; });

                // 이벤트 핸들러 연결
                //txtITEMCD.LostFocus.AddHandler(fn_OnUCITEMLostFocus);
                //txtITEMCD.KeyUp.AddHandler(fn_OnUCITEMKeyUp);
                //$(txtITEMCD.GetMainElement()).on('dblclick', fn_OnPopupUCItemSearch);

                //ITEMCallback.EndCallback.AddHandler(fn_OnUCITEMEndCallback);
                //$('#' + btnSearch).on('click', function () { fn_OnPopupUCItemSearch(); return false; });

            })(ID);
        }
    });
</script>
<dx:ASPxTextBox ID="hidITEMCD" ClientInstanceName="hidITEMCD" runat="server" ClientVisible="false" />
<div class="control-label" style="float: left; width: 42%;">
    <dx:ASPxTextBox ID="txtITEMCD" ClientInstanceName="txtITEMCD" runat="server" Width="100%" OnInit="txtITEMCD_Init">
        <ClientSideEvents LostFocus="<%= this.ClientInstanceID %>.fn_OnUCITEMLostFocus" KeyUp="<%= this.ClientInstanceID %>.fn_OnUCITEMKeyUp"  />

    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 47%;">
    <dx:ASPxTextBox ID="txtITEMNM" ClientInstanceName="txtITEMNM" runat="server" Width="100%">
        <ClientSideEvents Init="fn_OnControlDisableBox" />
    </dx:ASPxTextBox>
</div>
<div style="float: left; width: 1%;">&nbsp;</div>
<div class="control-label" style="float: left; width: 9%;">
    <button id="btnSearch" runat="server" class="btn btn-default btn-xs" data-toggle="button" title="품목조회" onclick="<%= this.ClientInstanceID %>.fn_OnPopupUCItemSearch()" >
        <i class="i i-popup text"></i>
        <i class="i i-popup text-active text-danger"></i>
    </button>
</div>
<dx:ASPxCallback ID="ITEMCallback" ClientInstanceName="ITEMCallback" runat="server" OnCallback="ITEMCallback_Callback">
    <ClientSideEvents EndCallback="<%= this.ClientInstanceID %>.fn_OnUCITEMEndCallback" />
</dx:ASPxCallback>
