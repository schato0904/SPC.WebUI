<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucUser.ascx.cs" Inherits="SPC.WebUI.Resources.controls.userControl.ucUser" %>
<script type="text/javascript">
    $(document).ready(function () {
        var ClientInstanceID = '<%=ClientInstanceID%>';
        var TEXT1 = '<%= this.TEXT1.UniqueID %>';
        var TEXT2 = '<%= this.TEXT2.UniqueID %>';
        var TEXT3 = '<%= this.TEXT3.UniqueID %>';
        if (ClientInstanceID != '') { // && typeof(window[ClientInstanceID]) == 'undefined') {
            window[ClientInstanceID] = new (function (ClientInstanceID) {
                // 멤버변수 세팅
                this.ClientInstanceID = ClientInstanceID;
                this.TEXT1 = TEXT1;
                this.TEXT2 = TEXT2;
                this.TEXT3 = TEXT3;
                // 필드명 세팅
                this.TEXT1NM = 'F_USERID';
                this.TEXT2NM = 'F_USERNM';
                this.TEXT3NM = 'TEXT3';
                //function fn_OnPopupUserSearch(parentCallbackName, _USERID, TYPE) {
                this.PopupFunction = fn_OnPopupUserSearch;

                // 멤버함수 세팅
                this.GetTEXT1 = function () { return ASPxClientTextBox.Cast(this.TEXT1).GetText(); };
                this.GetTEXT2 = function () { return ASPxClientTextBox.Cast(this.TEXT2).GetText(); };
                this.GetTEXT3 = function () { return ASPxClientTextBox.Cast(this.TEXT3).GetText(); };
                this.SetTEXT1 = function (val) { ASPxClientTextBox.Cast(this.TEXT1).SetText(val); };
                this.SetTEXT2 = function (val) { ASPxClientTextBox.Cast(this.TEXT2).SetText(val); };
                this.SetTEXT3 = function (val) { ASPxClientTextBox.Cast(this.TEXT3).SetText(val); };
                this.GetText = function (nm) {
                    switch (nm) {
                        case this.TEXT1NM:
                            return this.GetTEXT1();
                        case this.TEXT2NM:
                            return this.GetTEXT2();
                        case this.TEXT3NM:
                            return this.GetTEXT3();
                        default:
                            return '';
                    }
                };
                this.OpenPopup = function () {
                    this.PopupFunction(this.ClientInstanceID, this.GetTEXT1(), 'UC');
                };
                this.GetJson = function (val1, val2, val3) {
                    var result = {};
                    result[this.TEXT1NM] = val1;
                    result[this.TEXT2NM] = val2;
                    result[this.TEXT3NM] = val3;
                    return result;
                };
                this.Clear = function () {
                    var clearVal = this.GetJson('', '', '');
                    this.fn_SetUCControl(clearVal);
                };
                this.fn_SetUCControl = function (returnValue) {
                    this.SetTEXT1(Trim(returnValue[this.TEXT1NM]));
                    this.SetTEXT2(Trim(returnValue[this.TEXT2NM]));
                    //this.SetTEXT3(Trim(returnValue.F_DEPTNMFULL));
                    ASPxClientTextBox.Cast(this.TEXT2).Validate();
                };
                // 유저컨트롤 컨테이너의 너비를 참조하여 컨트롤너비 세팅
                this.AdjustSize = function () {
                    var blockWidth = $('#divTEXT1_<%=ClientInstanceID %>').parent().width();
                    ASPxClientTextBox.Cast('<%=TEXT1.UniqueID%>').SetWidth(Math.floor(blockWidth * 0.3));
                    ASPxClientTextBox.Cast('<%=TEXT2.UniqueID%>').SetWidth(Math.floor(blockWidth * 0.7 - 29));
                };
                this.LostFocus = function (s, e) {
                    var thisObj = this;
                    if (s.GetText() == '') {
                        thisObj.Clear();
                        return;
                    }
                    //function fn_GetTable(CATEGORY, TABLE, PKEYNM, PKEYVALUE, successCallback)
                    fn_GetTable("Common", "SYUSR01", "F_USERID", this.GetTEXT1(), successCallback, failCallback);
                    function successCallback(returnValue) {
                        //var val = thisObj.GetJson(;
                        thisObj.fn_SetUCControl(returnValue);
                    }
                    function failCallback(errMsg) {
                        thisObj.Clear();
                    }
                };
                this.SetCtl = function (userid) {
                    var thisObj = this;
                    //function fn_GetTable(CATEGORY, TABLE, PKEYNM, PKEYVALUE, successCallback)
                    fn_GetTable("Common", "SYUSR01", "F_USERID", userid, successCallback, failCallback);
                    function successCallback(returnValue) {
                        //var val = thisObj.GetJson(;
                        thisObj.fn_SetUCControl(returnValue);
                    }
                    function failCallback(errMsg) {
                        thisObj.Clear();
                    }
                };
            })(ClientInstanceID);
        }

        //window[ClientInstanceID].AdjustSize();
    });
    // 1.텍스트박스 포커스 아웃시 조회
    // 2.텍스트박스 더블클릭시 팝업
    // 3.조회버튼 클릭시 팝업
</script>
<div id="divTEXT1_<%=ClientInstanceID %>" class="control-label" style="float:left;width:34%;padding-top:0;">
    <dx:ASPxTextBox ID="TEXT1" runat="server" Width="100%" class="form-control input-sm disabled" OnInit="TEXT1_Init" ClientSideEvents-GotFocus="function(s,e){ s.SelectAll(); }">
    </dx:ASPxTextBox>
    <dx:ASPxTextBox ID="TEXT3" runat="server" ClientVisible="false">
    </dx:ASPxTextBox>
</div>
<div class="control-label" style="float:left;width:54%;margin-left:2px;padding-top:0;">
    <dx:ASPxTextBox ID="TEXT2" runat="server" Width="100%" class="form-control input-sm" 
        ClientSideEvents-Init="fn_OnControlDisableBox" />
</div>
<div class="control-label" style="float:left;width:10%;text-align:left;padding-left:2px;vertical-align:middle;padding-top:0;">
    <button class="btn btn-default btn-xs" data-toggle="button" title="<%= strTitle %>" onclick="<%=ClientInstanceID%>.OpenPopup(); return false;">
        <i class="i i-popup text"></i>
        <i class="i i-popup text-active text-danger"></i>
    </button>
</div>