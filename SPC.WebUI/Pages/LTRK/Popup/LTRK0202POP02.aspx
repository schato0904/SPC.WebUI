<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="LTRK0202POP02.aspx.cs" Inherits="SPC.WebUI.Pages.LTRK.Popup.LTRK0202POP02" %>
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
    <dx:ASPxTextBox ID="srcF_ORDERGROUP" ClientInstanceName="srcF_ORDERGROUP" runat="server" ClientVisible="false" />
    <div class="container">
        <div class="search">
            <table class="contentTable">
                <colgroup>
                    <col style="width:70px;" />
                    <col />
                    <col style="width:60px;" />
                    <col style="width:80px;" />
                    <col style="width:70px;" />
                    <col style="width:90px;" />
                    <col style="width:70px;" />
                    <col style="width:90px;" />
                    <col style="width:40px;" />
                    <col style="width:70px;" />
                </colgroup>
                <tr>
                    <td class="tdLabel">작지번호</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_ORDERNO" ClientInstanceName="srcF_ORDERNO" runat="server" CssClass="no-border" Width="100%" HorizontalAlign="Center">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">지시일</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_ORDERDATE" ClientInstanceName="srcF_ORDERDATE" runat="server" CssClass="no-border" Width="100%" HorizontalAlign="Center">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">지시수량</td>
                    <td class="tdInput">
                        <dx:ASPxSpinEdit ID="srcF_ORDERCNT" ClientInstanceName="srcF_ORDERCNT" runat="server" Width="100%"
                            DisplayFormatString="#,##0.#" SpinButtons-ClientVisible="false" HorizontalAlign="Right">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxSpinEdit>
                    </td>
                    <td class="tdLabel">생산수량</td>
                    <td class="tdInput">
                        <dx:ASPxSpinEdit ID="srcF_USEDCNT" ClientInstanceName="srcF_USEDCNT" runat="server" Width="100%"
                            DisplayFormatString="#,##0.#" SpinButtons-ClientVisible="false" HorizontalAlign="Right">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxSpinEdit>
                    </td>
                    <td class="tdLabel">단위</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_UNITNM" ClientInstanceName="srcF_UNITNM" runat="server" CssClass="no-border" Width="100%" HorizontalAlign="Center">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">품목</td>
                    <td class="tdInput" colspan="9">
                        <dx:ASPxTextBox ID="srcF_ITEMCD" ClientInstanceName="srcF_ITEMCD" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_ITEM" ClientInstanceName="srcF_ITEM" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">공정</td>
                    <td class="tdInput" colspan="9">
                        <dx:ASPxTextBox ID="srcF_WORKCD" ClientInstanceName="srcF_WORKCD" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_WORK" ClientInstanceName="srcF_WORK" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">설비</td>
                    <td class="tdInput" colspan="9">
                        <dx:ASPxTextBox ID="srcF_EQUIPCD" ClientInstanceName="srcF_EQUIPCD" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_EQUIP" ClientInstanceName="srcF_EQUIP" runat="server" Width="100%">
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
                KeyFieldName="F_DAYPRODUCTNO" EnableViewState="false" EnableRowsCache="false">
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
                    <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="작업일자" Width="100px" />
                    <dx:GridViewDataColumn FieldName="F_JOBSTDT" Caption="시작시간" Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_JOBENDT" Caption="종료시간" Width="80px" />
                    <dx:GridViewDataSpinEditColumn FieldName="F_PRODUCTQTY" Caption="생산수" Width="100px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_INSPQTY" Caption="검사수" Width="100px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_NGQTY" Caption="부적합수" Width="100px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_NGRATE" Caption="부적합률" Width="100px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#0" />
                    </dx:GridViewDataSpinEditColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>