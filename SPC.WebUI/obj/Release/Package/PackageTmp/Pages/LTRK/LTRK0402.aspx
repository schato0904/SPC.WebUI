<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="LTRK0402.aspx.cs" Inherits="SPC.WebUI.Pages.LTRK.LTRK0402" %>
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
                    <col style="width:180px;" />
                    <col style="width:80px;" />
                    <col style="width:180px;" />
                    <col style="width:80px;" />
                    <col style="width:180px;" />
                    <col />
                    <col style="width:180px;" />
                </colgroup>
                <tr>
                    <td class="tdLabel">출고일</td>
                    <td class="tdInput">
                        <ucCTF:Date runat="server" id="ucDate" />
                    </td>
                    <td class="tdLabel">거래처</td>
                    <td class="tdInput">
                        <dx:ASPxComboBox ID="ddlF_VENDOR" ClientInstanceName="ddlF_VENDOR" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton" />
                    </td>
                    <td class="tdLabel">출고LOT</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_OUTLOTNO" ClientInstanceName="srcF_OUTLOTNO" runat="server" Width="100%" />
                    </td>
                    <td class="tdLabel">품목분류</td>
                    <td class="tdInput tdContentR">
                        <dx:ASPxComboBox ID="ddlF_GUBN" ClientInstanceName="ddlF_GUBN" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton" />
                    </td>
                    <td class="tdInput tdContentLR"></td>
                    <td class="tdInput tdContentL" style="color:red;">
                        <dx:ASPxCheckBox ID="chkISENABLED" ClientInstanceName="chkISENABLED" runat="server" Checked="true" CssClass="font-bold" Text="삭제한 출고현황제외" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">품목</td>
                    <td class="tdInput tdContentR" colspan="5">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </td>
                    <td class="tdInput tdContentL" colspan="4"></td>
                </tr>
            </table>
            <div class="divPadding"></div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_OUTNO" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
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
                    <dx:GridViewDataColumn FieldName="F_OUTDATE" Caption="출고일" Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_INDATE" Caption="입고일" Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_MAKEDATE" Caption="생산일" Width="80px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품번" Width="130px" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품명">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_PRODUCTNO" Caption="생산번호" Width="120px" />
                    <dx:GridViewDataColumn FieldName="F_OUTLOTNO" Caption="출고로트" Width="150px" />
                    <dx:GridViewDataColumn FieldName="F_VENDORCD" Caption="거래처코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_VENDORNM" Caption="거래처" Width="150px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_GUBNNM" Caption="품목분류" Width="120px" />
                    <dx:GridViewDataSpinEditColumn FieldName="F_OUTCNT" Caption="출고수량" Width="70px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataColumn FieldName="F_UNITNM" Caption="단위" Width="70px" />
                    <dx:GridViewDataColumn FieldName="F_STATUS" Caption="상태" Width="40px" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick" />
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" />
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>