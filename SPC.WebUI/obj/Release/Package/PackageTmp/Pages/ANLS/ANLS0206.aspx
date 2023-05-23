<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ANLS0206.aspx.cs" Inherits="SPC.WebUI.Pages.ANLS.ANLS0206" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth1 = 0;
        var chartWidth2 = 500;
        var chartHeight = 0;
        var chartResized1 = false;
        var chartResized2 = false;
        var bSearch = false;

        $(document).ready(function () {
            
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            $(".tblContents").height(height);
            devGrid.SetWidth(parseInt($(".search").width() - 5, 10));
            devGrid.SetHeight(190);

            chartWidth1 = parseInt($(".search").width() - 5, 10) - chartWidth2 - 5;
            chartHeight = parseInt(height - 210, 10);

            $(".tdChart1").width(chartWidth1 + 20);
            $(".tdChart2").width(chartWidth2);

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartResized(devChart1, chartResized1, chartWidth1, chartHeight);
            fn_OnChartResized(devChart2, chartResized2, chartWidth2, chartHeight);
        });

        // 조회
        function fn_OnSearchClick() {
            bSearch = false;

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
            } else {
                bSearch = true;
                
                fn_OnChartDoCallback(devChart1, chartWidth1, chartHeight);
                fn_OnChartDoCallback(devChart2, chartWidth2, chartHeight);
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {

        }

        // Set QCD34
        function fn_OnSetQCD34Values(resultValues) {
            // 검사항목
            fn_OnUCSettingInspectionItem(resultValues);
        }

        // 차트 이벤트
        function fn_OnChartResized(chartObj, chartResized, _width, _height) {
            if (!chartResized) {
                fn_OnChartDoCallback(chartObj, _width, _height);
            }
        }

        function fn_OnChartDoCallback(chartObj, _width, _height) {
            var oParams = _width + '|' + _height;
            chartObj.PerformCallback(oParams);
        }

        function fn_OnChartEndCallback(s, e) {
            if (s.cpFunction != '' && s.cpFunction == 'resizeTo') {
                s.GetMainElement().style.width = s.cpChartWidth + 'px';
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="txtSERIALNO" ClientInstanceName="txtSERIALNO" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="txtFREEPOINT" ClientInstanceName="txtFREEPOINT" runat="server" ClientVisible="false" Text="0" />
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">기간</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Date runat="server" id="ucDate" MonthOnly="true" TodayFromDiff="365" />
                    </div>
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" targetCtrls="ddlLINE" />
                    </div>
                    <label class="col-sm-1 control-label">라인</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Line ID="ucLine" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" />
                    </div>
                    <label class="col-sm-1 control-label">항목</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:InspectionItem ID="ucInspectionItem" runat="server" validateFields="hidUCITEMCD|품목;hidUCWORKPOPCD|공정" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <table class="tblContents" style="width: 100%;">
                <tr>
                    <td id="tdLeft" style="padding-right: 5px;">
                        <table class="tblContents" style="width: 100%;">
                            <tr>
                                <td class="tdChart1" style="vertical-align: top;padding-right:5px;">
                                    <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="100px"
                                        OnCustomCallback="devChart1_CustomCallback">
                                    </dx:WebChartControl>
                                </td>
                                <td class="tdChart2" style="vertical-align: top;">
                                    <dx:WebChartControl ID="devChart2" ClientInstanceName="devChart2" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="100px"
                                        OnCustomCallback="devChart2_CustomCallback" OnCustomDrawSeriesPoint="devChart2_CustomDrawSeriesPoint" >
                                    </dx:WebChartControl>
                                </td>
                            </tr>                            
                            <tr>
                                <td colspan="2" style="height: 200px; padding-top: 10px;">   
                                    <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                                    <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />                                 
                                    <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False" Width="100%"
                                        KeyFieldName="구분" EnableViewState="false" EnableRowsCache="false"
                                        OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared">
                                        <Styles>
                                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                            <EditForm CssClass="bg-default"></EditForm>
                                        </Styles>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" HorizontalScrollBarMode="Visible" ShowStatusBar="Hidden" />
                                        <SettingsBehavior AllowSort="false" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
