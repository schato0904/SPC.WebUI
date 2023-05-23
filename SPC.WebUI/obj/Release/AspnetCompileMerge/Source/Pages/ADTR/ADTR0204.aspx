<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ADTR0204.aspx.cs" Inherits="SPC.WebUI.Pages.ADTR.ADTR0204" %>
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
            devGrid.SetHeight(height - 280);
            devGridDetail.SetHeight(250);
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
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        // Detail 조회
        function fn_OnRowDblClick(s, e) {
            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));

            hidGridParentKey.SetValue(rowKeys);
            devGridDetail.PerformCallback();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">기간</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Date ID="ucDate" runat="server" />
                    </div>
                    <%--<label class="col-sm-1 control-label">생산팀</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Fact ID="ucFact" runat="server" />
                    </div>--%>
                    <label class="col-sm-2 control-label">공정이상 관리대상</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:CommonCodeDDL ID="ucCommonCode" runat="server" targetParams="AA|AAE2" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <table style="width:100%;">
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_FACTCD;F_BANCD;F_LINECD" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid_CustomCallback">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                            <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                            <Columns>                                
                                <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="200px">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="200px">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_TOTALCNT" Caption="발생건수" Width="100px">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_CHKCNT" Caption="조치수" Width="100px">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_CHACNT" Caption="미조치수" Width="100px">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_RATE" Caption="조치율" Width="100px">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn FieldName="F_FACTCD" Caption="공장코드" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_BANCD" Caption="반코드" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_LINECD" Caption="라인코드" Visible="false" />
                            </Columns>
                        </dx:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 250px; padding-top: 10px;">
                        <dx:ASPxTextBox ID="hidGridParentKey" ClientInstanceName="hidGridParentKey" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxGridView ID="devGridDetail" ClientInstanceName="devGridDetail" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_WORKDATE;F_MEASNO;F_TSERIALNO" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGridDetail_CustomCallback">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                            <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_WORKDATE" Caption="작업일" Width="100px" />
                                <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품목코드" Width="150px" />
                                <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품목명" Width="200px" />
                                <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="200px" />
                                <dx:GridViewDataColumn FieldName="F_QTYCD" Caption="구분" Width="80px" />
                                <dx:GridViewDataColumn FieldName="F_INSPDETAIL" Caption="검사항목" Width="150px" />
                                <dx:GridViewBandColumn Name="F_INSPECTION" Caption="검사규격">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="규격" Width="100px">
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한" Width="100px">
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한" Width="100px">
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Name="F_MEASURE" Caption="측정값">
                                </dx:GridViewBandColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                        <dx:ASPxGridViewExporter ID="devGridExporter2" runat="server" GridViewID="devGridDetail" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                        <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="paging">
            <ucCTF:Pager ID="ucPager" runat="server" PageSize="200" targetCtrls="devGridDetail" />
        </div>
    </div>
</asp:Content>
