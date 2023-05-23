<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="INSP0101POP_CHUNIL.aspx.cs" Inherits="SPC.WebUI.Pages.INSP.Popup.INSP0101POP_CHUNIL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            hidKEY.SetText(parent.fn_OnGetSelectedKeys());
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            //devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {

        }

        // 입력
        function fn_OnNewClick() {

        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {
            if (!confirm('입력하신 내용으로 검사성적서를 생성하시겠습니까?\r계속진행하려면 [확인]을 누르세요!!')) return;

            fn_SetTextValue('hidCOMPNM', fn_GetCastText('txtCOMPNM'));
            fn_SetTextValue('hidORDER', fn_GetCastText('txtORDER'));
            fn_SetTextValue('hidREVNO', fn_GetCastText('txtREVNO'));
            fn_SetTextValue('hidREVDT', fn_GetCastText('txtREVDT'));
            fn_SetTextValue('hidREVDESC', fn_GetCastText('txtREVDESC'));
            fn_SetTextValue('hidREVMANAGER', fn_GetCastText('txtREVMANAGER'));
            fn_SetTextValue('hidREVAPPROVER', fn_GetCastText('txtREVAPPROVER'));
            fn_SetTextValue('hidEONO', fn_GetCastText('txtEONO'));
            fn_SetTextValue('hidLOTNO1', fn_GetCastText('txtLOTNO1'));
            fn_SetTextValue('hidLOTNO2', fn_GetCastText('txtLOTNO2'));
            fn_SetTextValue('hidPONO', fn_GetCastText('txtPONO'));
            fn_SetTextValue('hidQUANTITY', fn_GetCastText('txtQUANTITY'));
            fn_SetTextValue('hidPLACE', fn_GetCastText('txtPLACE'));
            fn_SetTextValue('hidDATE', fn_GetCastText('txtDATE'));
            fn_SetTextValue('hidFORMAT', fn_GetCastText('txtFORMAT'));

            // 용도(선택방식 & 입력방식)
            if (ASPxClientUtils.IsExists(ASPxClientControl.Cast('txtUSAGE')) && ASPxClientUtils.IsExists(ASPxClientControl.Cast('chkUSAGE'))) {
                var selectItems = chkUSAGE.GetSelectedItems();
                var oItems = '';
                for (var i = 0; i < selectItems.length; i++) {
                    oItems += i == 0 ? "" : "|";
                    oItems += selectItems[i].value;
                }

                if (fn_GetCastText('txtUSAGE') != '') {
                    oItems += ",";
                    oItems += fn_GetCastText('txtUSAGE');
                }

                fn_SetTextValue('hidUSAGE', oItems);
            }
                // 용도(입력방식)
            else if (ASPxClientUtils.IsExists(ASPxClientControl.Cast('txtUSAGE'))) {
                fn_SetTextValue('hidUSAGE', fn_GetCastText('txtUSAGE'));
            }
                // 용도(선택방식)
            else if (ASPxClientUtils.IsExists(ASPxClientControl.Cast('chkUSAGE'))) {
                var selectItems = chkUSAGE.GetSelectedItems();
                var oItems = '';
                for (var i = 0; i < selectItems.length; i++) {
                    oItems += i == 0 ? "" : "|";
                    oItems += selectItems[i].value;
                }
                fn_SetTextValue('hidUSAGE', oItems);
            }

            // 검사방식
            if (ASPxClientUtils.IsExists(ASPxClientControl.Cast('chkINSPECTION'))) {
                var selectItems = chkINSPECTION.GetSelectedItems();
                var oItems = '';
                for (var i = 0; i < selectItems.length; i++) {
                    oItems += i == 0 ? "" : "|";
                    oItems += selectItems[i].value;
                }
                fn_SetTextValue('hidINSPECTION', oItems);
            }

            // 납품방식
            if (ASPxClientUtils.IsExists(ASPxClientControl.Cast('chkTYPE'))) {
                var selectItems = chkTYPE.GetSelectedItems();
                var oItems = '';
                for (var i = 0; i < selectItems.length; i++) {
                    oItems += i == 0 ? "" : "|";
                    oItems += selectItems[i].value;
                }
                fn_SetTextValue('hidTYPE', oItems);
            }

            devCallback.PerformCallback();
        }

        // 취소
        function fn_OnCancelClick() {

        }

        // 삭제
        function fn_OnDeleteClick() {

        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() { }

        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                if (s.cpResultCode == '1') {
                    parent.fn_devPopupClose();
                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                } else {
                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        // 고객사 선택 변경 시
        function fn_OnSelectedIndexChanged(s, e) {
            callbackPanel.PerformCallback(s.GetValue());
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="hidKEY" ClientInstanceName="hidKEY" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidCOMPNM" ClientInstanceName="hidCOMPNM" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidORDER" ClientInstanceName="hidORDER" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidREVNO" ClientInstanceName="hidREVNO" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidREVDT" ClientInstanceName="hidREVDT" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidREVDESC" ClientInstanceName="hidREVDESC" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidREVMANAGER" ClientInstanceName="hidREVMANAGER" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidREVAPPROVER" ClientInstanceName="hidREVAPPROVER" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidEONO" ClientInstanceName="hidEONO" runat="server" ClientVisible="false"/>
    <dx:ASPxTextBox ID="hidLOTNO1" ClientInstanceName="hidLOTNO1" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidLOTNO2" ClientInstanceName="hidLOTNO2" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidPONO" ClientInstanceName="hidPONO" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidUSAGE" ClientInstanceName="hidUSAGE" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidINSPECTION" ClientInstanceName="hidINSPECTION" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidQUANTITY" ClientInstanceName="hidQUANTITY" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidTYPE" ClientInstanceName="hidTYPE" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidPLACE" ClientInstanceName="hidPLACE" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidDATE" ClientInstanceName="hidDATE" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidFORMAT" ClientInstanceName="hidFORMAT" runat="server" ClientVisible="false" />
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                
                <div>
                    <div>
                        
                        <dx:ASPxMemo ID="memoETC" ClientInstanceName="memoETC" runat="server" Text="" Width="100%" Height="200px" MaxLength="200"/>
                    </div>

                </div>
                <div class="form-group">
                    <%--<label class="col-sm-8 control-label">메모를 입력 후 저장 버튼을 클릭하세요.</label>--%>
                    <label style="text-align:left; margin-left:20px;">기타 사항을 입력 후 저장 버튼을 클릭하세요.</label>
                    <div style="visibility:hidden">
                        <dx:ASPxComboBox ID="ddlCustomer" ClientInstanceName="ddlCustomer" runat="server" Width="200px"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <ClientSideEvents SelectedIndexChanged="fn_OnSelectedIndexChanged" />
                        </dx:ASPxComboBox>
                        
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxCallbackPanel ID="callbackPanel" ClientInstanceName="callbackPanel" runat="server" Enabled="true" Visible="true" Width="100%" OnCallback="callbackPanel_Callback">
                <PanelCollection>
                    <dx:PanelContent>
                        <dx:ASPxTextBox ID="hidCUSTOMCD" ClientInstanceName="hidCUSTOMCD" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="hidREPORTCD" ClientInstanceName="hidREPORTCD" runat="server" ClientVisible="false" />
                        <asp:PlaceHolder ID="pHolder" runat="server" />
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </div>
        <div class="paging"></div>
    </div>
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
        <ClientSideEvents CallbackError="fn_OnCallbackError" EndCallback="fn_OnEndCallback" />
    </dx:ASPxCallback>
</asp:Content>