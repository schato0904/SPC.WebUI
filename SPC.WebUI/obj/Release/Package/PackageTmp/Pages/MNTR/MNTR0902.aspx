<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MNTR0902.aspx.cs" Inherits="SPC.WebUI.Pages.MNTR.MNTR0902" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;
        var chartHeight2 = 0;
        var chartResized1 = false;
        var chartResized2 = false;
        var chartResized3 = false;

        $(document).ready(function () {
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            $(".tblContents").height(height-350);

            chartWidth = parseInt($(".search").width(), 10);
            chartHeight1 = parseInt((height - 350), 10);

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartResized(devChart1, chartResized1, chartWidth, chartHeight1);
        });

        // 차트 이벤트
        function fn_OnChartResized(chartObj, chartResized, _width, _height) {
            if (!chartResized) {
                pnlChart.PerformCallback(_width + "|" + _height + "|resize");
            }
        }

        // 조회
        function fn_OnSearchClick() {
            pnlChart.PerformCallback(chartWidth + "|" + chartHeight1 + "|");
            //pnlChart.PerformCallback();
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
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server" >
    <div class="container" >
        <div class="search" style="width:100%">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F; width:100%">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" ID="ucDate" SingleDate="true" />
                    </div>
                    <label class="col-sm-1 control-label">구분</label>
                    <div class="col-sm-2">
                        <dx:ASPxRadioButtonList ID="rdoGbn" runat="server" ClientInstanceName="rdoGbn" Width="100%" RepeatDirection="Horizontal" Border-BorderStyle="None" BackColor="White"
                            ValueField="F_GBN" CssClass="NoXButton">
                            <Paddings Padding="0" />
                            <Items>
                                <dx:ListEditItem Value="1" Text="자주" />
                                <dx:ListEditItem Value="2" Text="전수" />
                            </Items>
                        </dx:ASPxRadioButtonList>
                    </div>
                </div>
            </div>
        </div>
        <div class="content" style="width:100%;">
            <dx:ASPxCallbackPanel ID="pnlChart" runat="server" ClientInstanceName="pnlChart" Enabled="true" Visible="true" Width="100%" OnCallback="pnlChart_Callback" >
                <ClientSideEvents EndCallback="fn_OnEndCallback" />
                <Paddings PaddingBottom="10px"></Paddings>
                <PanelCollection>
                    <dx:PanelContent>
                        <table class="tblContents" style="width: 100%;">
                            <tr>
                                <td style="width:100%">
                                    <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server" 
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="350px" >
                                    </dx:WebChartControl>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top:5px">
                                    <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                                        KeyFieldName="F_COMPCD;F_FACTCD" EnableViewState="false" EnableRowsCache="false" >
                                        <Styles>
                                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                            <EditForm CssClass="bg-default"></EditForm>
                                        </Styles>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                                        <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <ClientSideEvents Init="function(s,e){s.SetHeight(350)}" CallbackError="fn_OnCallbackError" />                                                    
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
                                                    <dx:GridViewDataColumn FieldName="OK_CNT1" Caption="OK" Width="80px">
                                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="OC_CNT1" Caption="OC" Width="80px">
                                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="NG_CNT1" Caption="NG" Width="80px">
                                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewBandColumn Name="F_INSPECTION2" Caption="전수검사">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="ALL_CNT2" Caption="수신건수" Width="80px">
                                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="OK_CNT2" Caption="OK" Width="80px">
                                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="OC_CNT2" Caption="OC" Width="80px">
                                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="NG_CNT2" Caption="NG" Width="80px">
                                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                                    </dx:GridViewDataColumn>                                                    
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                                    <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
