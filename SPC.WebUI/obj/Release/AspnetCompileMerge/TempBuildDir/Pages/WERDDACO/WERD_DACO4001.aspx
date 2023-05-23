<%@ Page Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD_DACO4001.aspx.cs" Inherits="SPC.WebUI.Pages.WERDDACO.WERD_DACO4001" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;
        var chartResized1 = false;
        var chartResized2 = false;
        $(document).ready(function () {
        });

        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));

            $(".tblContents").height(height);
            chartWidth = parseInt(($(".search").width()), 10);
            $(".tdChart1").width(chartWidth);
            chartHeight1 = parseInt((height) / 2, 10);

            if (chartHeight1 < 200) {
                chartHeight1 = 200;
            }
            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);

            devGrid.SetHeight(height);
            devGrid.SetWidth(chartWidth / 2);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartResized(devChart1, chartResized1, chartWidth, chartHeight1);
            fn_OnChartResized(devChart2, chartResized2, chartWidth, chartHeight1);
        });

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

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');
            devGrid.PerformCallback();
            var oParams = chartWidth + '|' + chartHeight1
            devChart1.PerformCallback(oParams);
            devChart2.PerformCallback(oParams);
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

        function fn_OnChartEndCallback(s, e) {
            if (s.cpFunction != '' && s.cpFunction == 'resizeTo') {
                s.GetMainElement().style.width = s.cpChartWidth + 'px';
            }
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container" style="width: 100%;">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-2">
                        <ucCTF:Date runat="server" ID="ucDate" DateTimeOnly="false" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <table class="tblContents" style="width: 100%;" border="0">
                <tr>
                    <td style="width: 50%;">
                        <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_COMPANYNM" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid_CustomCallback">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                            <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_COMPANYNM" Caption="협력사명" CellStyle-HorizontalAlign="Left" Width="40%" />
                                <dx:GridViewDataColumn FieldName="F_NGCOUNT" Caption="불량수량" CellStyle-HorizontalAlign="Right" Width="30%" />
                                <dx:GridViewDataColumn FieldName="F_NGTIME" Caption="수리시간(초)" CellStyle-HorizontalAlign="Right" Width="30%" />
                            </Columns>
                        </dx:ASPxGridView>
                    </td>
                    <td style="width: 1%;"></td>
                    <td class="tdChart1" style="width: 49%; vertical-align: top;">
                        <div style="height: 50%;">
                            <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                                ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="False"
                                OnCustomCallback="devChart1_CustomCallback" CrosshairEnabled="True" Width="300px">
                                <clientsideevents endcallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized1 = true; }" />
                            </dx:WebChartControl>
                        </div>
                        <div style="height: 50%;">
                            <dx:WebChartControl ID="devChart2" ClientInstanceName="devChart2" runat="server"
                                ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="False"
                                OnCustomCallback="devChart2_CustomCallback" CrosshairEnabled="True" Width="300px">
                                <clientsideevents endcallback="function(s, e) { chartResized2 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized2 = true; }" />
                            </dx:WebChartControl>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
