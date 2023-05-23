<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="SYST0105.aspx.cs" Inherits="SPC.WebUI.Pages.SYST.SYST0105" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .left {
            height: 100%;
            padding-right: 10px;
            vertical-align: top;
        }
        .right {
            width: 630px;
            height: 100%;
            vertical-align: top;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 50;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
            devGridDetail.SetHeight(height);
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

        // 상세보기
        function fn_OnRowDblClick(s, e) {
            devGridDetail.PerformCallback(devGrid.GetRowKey(e.visibleIndex));
        }

        function fn_OnAuthSelectedIndexChanged(s, e) {
            hidAuthority.SetValue(s.GetValue());
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
                    <label class="col-sm-1 control-label">권한</label>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxTextBox ID="hidAuthority" ClientInstanceName="hidAuthority" runat="server" ClientVisible="false" />
                        <dx:ASPxComboBox ID="ddlAuthority" ClientInstanceName="ddlAuthority" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton"
                            OnInit="ddlAuthority_Init" OnDataBound="ddlAuthority_DataBound">
                            <ClientSideEvents SelectedIndexChanged="fn_OnAuthSelectedIndexChanged" />
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">사업장</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Fact ID="ucFact" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <table style="width:100%;">
                <tr>
                    <td class="left">
                        <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_USERID" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" HorizontalScrollBarMode="Visible" ShowStatusBar="Visible" />
                            <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                            <Templates>
                                <StatusBar>
                                    <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                                </StatusBar>
                            </Templates>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_USERNM" Caption="성명" Width="100px" />
                                <dx:GridViewDataColumn FieldName="F_USERID" Caption="사용자ID" Width="130px" />
                                <dx:GridViewDataColumn FieldName="F_TOTALCNT" Caption="계" Width="60px" />
                            </Columns>
                        </dx:ASPxGridView>

                        <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_USERID" EnableViewState="false" EnableRowsCache="false" Visible="false"
                            OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" HorizontalScrollBarMode="Visible" ShowStatusBar="Visible" />
                            <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
                            <Templates>
                                <StatusBar>
                                    <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                                </StatusBar>
                            </Templates>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_USERNM" Caption="성명" Width="100px" />
                                <dx:GridViewDataColumn FieldName="F_USERID" Caption="사용자ID" Width="130px" />
                                <dx:GridViewDataColumn FieldName="F_TOTALCNT" Caption="계" Width="60px" />
                            </Columns>
                        </dx:ASPxGridView>

                        <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid2" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                        <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
                    </td>
                    <td class="right">
                        <dx:ASPxGridView ID="devGridDetail" ClientInstanceName="devGridDetail" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_WORKDAY;F_PGMID" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGridDetail_CustomCallback" OnCustomColumnDisplayText="devGridDetail_CustomColumnDisplayText">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                            <SettingsBehavior AllowSort="false" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                            <Templates>
                                <StatusBar>
                                    <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGridDetail" />
                                </StatusBar>
                            </Templates>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_WORKDAY" Caption="접속일자" Width="100px" />
                                <dx:GridViewDataColumn FieldName="F_MODULE1" Caption="대메뉴" Width="150px">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MODULE2" Caption="중메뉴" Width="150px">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_PGMNM" Caption="프로그램명" Width="150px">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_WORKCNT" Caption="접속횟수" Width="80px" />
                                <dx:GridViewDataColumn FieldName="F_PGMID" Caption="프로그ID" Visible="false" />
                            </Columns>
                        </dx:ASPxGridView>
                    </td>
                </tr>
            </table>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
