<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="MEAS3001POP.aspx.cs" Inherits="SPC.WebUI.Pages.MEAS.Popup.MEAS3001POP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var parentCallback = null;
        var F_DKINDCD = 'AAF101';

        $(document).ready(function () {
            var parentCallbackNm = '<%=parentCallback%>';
            if (parentCallbackNm != '' && typeof (parent) != "undefined" && typeof (parent[parentCallbackNm]) == "function") {
                parentCallback = parent[parentCallbackNm];
            }
            

            fn_OnSearchClick();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
        }

        // 조회
        function fn_OnSearchClick() {
            var parentParams = ''
            
            devGrid.PerformCallback(parentParams);
        }

        // 입력
        function fn_OnNewClick() {

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
            alert(e.message);
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        // RowDblClick
        function fn_OnRowDblClick(s, e) {
            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));
            var rowKey = rowKeys.split('|');
            
            //F_MS01MID;F_EQUIPNO;F_EQUIPNM;F_MAKERNM;F_MODEL;F_INDT;F_LASTFIXDT;F_PROCNM
            // JSON 값으로 반환
            var i = 0;
            var returnValue = {
                "F_MS01MID": rowKey[i++]
               , "F_EQUIPNO": rowKey[i++]
               , "F_EQUIPNM": rowKey[i++]
               , "F_MAKERNM": rowKey[i++]
               , "F_MODEL": rowKey[i++]
               , "F_INDT": rowKey[i++]
               , "F_LASTFIXDT": rowKey[i++]
            };

            if (parentCallback != null) {
                parentCallback(returnValue);
            }

            parent.fn_devPopupClose();
        }
        
        // 거래처 팝업
        function fn_PopupCustSearch(ctrlId) {
            fn_OnPopupCustSearch('SetCust_' + ctrlId, ASPxClientControl.Cast(ctrlId).GetText());
        }

        // 거래처 팝업 선택 처리(조회조건)
        function SetCust_schF_CUSTID(returnValue) {
            if (Trim(returnValue.F_IN01MID) != "") {
                schF_IN01MID.SetText(returnValue.F_IN01MID);
            }
            if (Trim(returnValue.F_CUSTNM) != "") {
                schF_CUSTNM.SetText(returnValue.F_CUSTNM);
            }
        }

        function srcF_EQUIPNO_OnKeyPress(s, e) {
            //alert(e);
            var event = e.htmlEvent;
            if (event.keyCode == 13) {
                fn_OnSearchClick();
                event.returnValue = false;
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search" style="margin-bottom:5px;">
            <table class="InputTable">
                <colgroup>
                    <col style="width:15%" />
                    <col style="width:35%" />
                    <col style="width:15%" />
                    <col style="width:35%" />
                </colgroup>
                <tr>
                    <td class="tdTitle">
                        <label>관리번호</label>
                    </td>
                    <td class="tdContent">
                            <dx:ASPxTextBox ID="srcF_EQUIPNO" ClientInstanceName="srcF_EQUIPNO" runat="server" Width="100%" >
                                <ClientSideEvents KeyPress="srcF_EQUIPNO_OnKeyPress" />
                            </dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_MS01MID;F_EQUIPNO;F_EQUIPNM;F_MAKERNM;F_MODEL;F_INDT;F_LASTFIXDT" 
                EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                <SettingsBehavior AllowSort="false" AllowDragDrop="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPNO" Caption="관리번호" Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_EQUIPNM" Caption="측정기명" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataTextColumn>                    

                    <dx:GridViewDataTextColumn FieldName="F_MS01MID" Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MAKERNM" Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_MODEL" Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_INDT" Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_LASTFIXDT" Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_PROCNM" Visible="false"></dx:GridViewDataTextColumn>

                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>