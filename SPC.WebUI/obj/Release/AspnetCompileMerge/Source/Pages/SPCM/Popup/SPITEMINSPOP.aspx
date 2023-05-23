<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="SPITEMINSPOP.aspx.cs" Inherits="SPC.WebUI.Pages.SPCM.Popup.SPITEMINSPOP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
        }

        // 조회
        function fn_OnSearchClick() {
        }

        // 입력
        function fn_OnNewClick() {
            
            if (fn_GetCastValue('txtCNT') == "") {
                alert("수량을 입력하세요.");
                txtCNT.Focus();
                return;
            }
            if (fn_GetCastValue('txtPRICE') == "") {
                alert("단가을 입력하세요.");
                txtPRICE.Focus();
                return;
            }

            var rowkeys = fn_GetCastValue('txtITCD') + '|' + fn_GetCastValue('txtITNM') + '|' + fn_GetCastValue('txtCNT') + '|' + fn_GetCastValue('txtPRICE') + '|' +
                          fn_GetCastSelectedItemValue('ddlCscd');
            
            parent.fn_gridInsItem(rowkeys);
            parent.fn_devPopupClose();
        }

        // 수정
        function fn_OnModifyClick() {

        }

        // 저장
        function fn_OnSaveClick() {

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
            fn_doSetGridEventAction('false');

            if (s.cpResultCode != '') {
                if (s.cpResultCode == 'pager') {
                    // 페이저 Callback
                    fn_pagerPerformCallback(s.cpResultMsg);
                } else {
                    alert(s.cpResultMsg);

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

        // RowDblClick
        function fn_OnRowDblClick(s, e) {
            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));
            var rowKey = rowKeys.split('|');

            

            parent.fn_OnSettingItem(rowKey[0], rowKey[1]);
            parent.fn_devPopupClose();
        }
        // KeyUpEvent
        function fn_OnMeasureKeyUp(s, e) {
            fn_RemoveHangul(s, e);
        }

        // KeyPressEvent
        function fn_OnMeasureKeyPress(s, e) {
            fn_ValidateOnlyFloat(s, e);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
 
<div class="container">
    <div class="search">
        <table class="contentTable">
            <colgroup>
                <col style="width:80px;" />
                <col style="width:180px;" />
                <col style="width:80px;" />
                <col style="width:180px;" />
                <col style="width:80px;" />
                <col style="width:180px;" />
                <col />
            </colgroup>
            <tr>
                <td class="tdLabel">품목코드</td>
                <td class="tdInput">
                    <dx:ASPxTextBox ID="txtITCD" ClientInstanceName="txtITCD" runat="server" Width="100%"></dx:ASPxTextBox>
                </td>
                <td class="tdLabel">품목명</td>
                <td class="tdInput" colspan="3">
                    <dx:ASPxTextBox ID="txtITNM" ClientInstanceName="txtITNM" runat="server" Width="100%"></dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdLabel">수량</td>
                <td class="tdInput">
                    <dx:ASPxTextBox ID="txtCNT" ClientInstanceName="txtCNT" runat="server" Width="100%" >
                        <ClientSideEvents KeyUp="fn_OnMeasureKeyUp" KeyPress="fn_OnMeasureKeyPress" />
                    </dx:ASPxTextBox>
                </td>
                <td class="tdLabel">단가</td>
                <td class="tdInput">
                    <dx:ASPxTextBox ID="txtPRICE" ClientInstanceName="txtPRICE" runat="server" Width="100%">
                        <ClientSideEvents KeyUp="fn_OnMeasureKeyUp" KeyPress="fn_OnMeasureKeyPress" />
                    </dx:ASPxTextBox>
                </td>
                <td class="tdLabel">거래처</td>
                <td class="tdInput">
                    <dx:ASPxComboBox ID="ddlCscd" ClientInstanceName="ddlCscd" runat="server" Width="100%"
                        IncrementalFilteringMode="None" CssClass="NoXButton">
                    </dx:ASPxComboBox>
                </td>
            </tr>
        </table>
        <div class="divPadding"></div>
    </div>
    <div class="content">
    </div>
    <div class="paging"></div>
</div>
</asp:Content>
