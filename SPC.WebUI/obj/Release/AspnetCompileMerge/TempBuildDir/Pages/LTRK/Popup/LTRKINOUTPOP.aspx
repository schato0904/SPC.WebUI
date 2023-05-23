<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="LTRKINOUTPOP.aspx.cs" Inherits="SPC.WebUI.Pages.LTRK.Popup.LTRKINOUTPOP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_RendorTotalCount();
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
            devGrid.PerformCallback();
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
        function fn_OnExcelClick() {
            btnExport.DoClick();
        }

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

            fn_RendorTotalCount();
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <table class="contentTable">
                <colgroup>
                    <col style="width:80px;" />
                    <col style="width:200px;" />
                    <col style="width:80px;" />
                    <col style="width:100px;" />
                    <col style="width:80px;" />
                    <col style="width:100px;" />
                    <col style="width:80px;" />
                    <col style="width:120px;" />
                    <col style="width:80px;" />
                    <col />
                </colgroup>
                <tr>
                    <td class="tdLabel">수불일</td>
                    <td class="tdInput">
                        <ucCTF:Date runat="server" id="ucDate" />
                    </td>
                    <td class="tdLabel">재고수량</td>
                    <td class="tdInput">
                        <dx:ASPxSpinEdit ID="srcF_REMAINCNT" ClientInstanceName="srcF_REMAINCNT" runat="server" Width="100%"
                            DisplayFormatString="#,##0.#" SpinButtons-ClientVisible="false" HorizontalAlign="Right">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxSpinEdit>
                    </td>
                    <td class="tdLabel">단위</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_UNITNM" ClientInstanceName="srcF_UNITNM" runat="server" Width="100%" HorizontalAlign="Right">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">수불유형</td>
                    <td class="tdInput">
                        <ucCTF:CommonCodeDDL ID="ucCommonCode" runat="server" targetParams="AA|AAE7" />
                    </td>
                    <td class="tdLabel">품목</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_ITEMCD" ClientInstanceName="srcF_ITEMCD" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_ITEM" ClientInstanceName="srcF_ITEM" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
            <div class="divPadding"></div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_INVOICENO" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
				<Templates>
					<StatusBar>
						<ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
					</StatusBar>
				</Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_INPUTDATE" Caption="수불일" Width="120px" />
                    <dx:GridViewDataColumn FieldName="F_INOUTTP" Caption="수불유형" Width="120px" />
                    <dx:GridViewDataSpinEditColumn FieldName="F_BCNT" Caption="기초수량" Width="70px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_ICNT" Caption="입고수량" Width="70px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_OCNT" Caption="출고수량" Width="70px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_RCNT" Caption="재고수량" Width="70px">
                        <CellStyle HorizontalAlign="Right" Font-Bold="true"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataDateColumn FieldName="F_INSDT" Caption="등록일시" Width="150px" PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss" />
                    <dx:GridViewDataColumn FieldName="F_USER" Caption="작업자" Width="120px" />
                    <dx:GridViewDataColumn FieldName="F_VENDORNM" Caption="거래처" Width="120px" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="투입공정" Width="200px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick" />
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" />
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>