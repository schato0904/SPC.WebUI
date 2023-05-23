<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MNTR0101.aspx.cs" Inherits="SPC.WebUI.Pages.MNTR.MNTR0101" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {
            fn_RendorTotalCount();
            // 조회
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

        // DblClick
        function fn_OnRowDblClick(s, e) {
            var visibleIndex = e.visibleIndex
            if (visibleIndex < 0) return;
            
            var rowKeys = fn_OnRowKeysNullValueToEmptyWithEncode(devGrid.GetRowKey(e.visibleIndex));

            var oParams = rowKeys + '|';
            oParams += fn_GetCastText('hidUCFROMDT');
            
            parent.fn_OnDeleteTab('ADTR0102', parent.fn_OnGetTabObject('ADTR0102'));
            parent.parent.doCreateTab('MM02|MM0201|ADTR|ADTR0102|라인모니터링|RE|1', oParams);

            parent.fn_OnDeleteTab('TISP0103', parent.fn_OnGetTabObject('TISP0103'));
            parent.parent.doCreateTab('MM07|MM0701|TISP|TISP0103|라인모니터링|R|1', oParams);
        }

        // CustomButtonClick
        function fn_OnCustomButtonClick(visibleIndex, buttonID) {
            if (visibleIndex < 0) return;

            var rowKeys = fn_OnRowKeysNullValueToEmptyWithEncode(devGrid.GetRowKey(visibleIndex));

            var oParams = rowKeys + '|';
            oParams += fn_GetCastText('hidUCFROMDT');

            if (buttonID == 'btn03A') {
                parent.fn_OnDeleteTab('ADTR0102', parent.fn_OnGetTabObject('ADTR0102'));
                parent.parent.doCreateTab('MM02|MM0201|ADTR|ADTR0102|라인모니터링|RE|1', oParams);
            } else if (buttonID == 'btn08A') {
                parent.fn_OnDeleteTab('TISP0103', parent.fn_OnGetTabObject('TISP0103'));
                parent.parent.doCreateTab('MM07|MM0701|TISP|TISP0103|라인모니터링|R|1', oParams);
            }

            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">업체</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Comp ID="ucComp" runat="server" targetCtrls="ddlFACT" masterChk="0" nullText="선택" />
                    </div>
                    <label class="col-sm-1 control-label">공장</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Fact ID="ucFact" runat="server" nullText="선택" />
                    </div>
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-2">
                        <ucCTF:Date runat="server" ID="ucDate" SingleDate="true" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_COMPCD;F_FACTCD" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" CustomButtonClick="fn_OnCustomButtonClick" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="업체명" Width="180px" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewBandColumn Name="F_INSPECTION1" Caption="자주검사">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="ALL_CNT1" Caption="수신건수" Width="80px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="OK_CNT1" Caption="OK" Width="70px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="OC_CNT1" Caption="OC" Width="70px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="NG_CNT1" Caption="NG" Width="70px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="LAST_TIME1" Caption="최근수신시간" Width="120px">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn Caption="모니터링" UnboundType="String" Width="70px">
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="btn03A" runat="server" Text="보기" OnInit="btn03A_Init" />
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Name="F_INSPECTION2" Caption="전수검사">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="ALL_CNT2" Caption="수신건수" Width="80px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="OK_CNT2" Caption="OK" Width="70px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="OC_CNT2" Caption="OC" Width="70px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="NG_CNT2" Caption="NG" Width="70px">
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="LAST_TIME2" Caption="최근수신시간" Width="120px">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn Caption="모니터링" UnboundType="String" Width="70px">
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="btn08A" runat="server" Text="보기" OnInit="btn08A_Init" />
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
