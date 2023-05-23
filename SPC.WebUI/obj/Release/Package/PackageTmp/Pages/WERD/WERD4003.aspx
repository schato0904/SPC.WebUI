<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD4003.aspx.cs" Inherits="SPC.WebUI.Pages.WERD.WERD4003" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;

        var chartResized1 = false;
        var chartResized2 = false;

        $(document).ready(function () {
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            $(".tblContents").height(height - 210);
            //devGrid.SetWidth(parseInt($(".tblContents").width() - 1, 10));
            devGrid.SetHeight(191);
            devGrid2.SetHeight(191);
            devGrid3.SetHeight(191);


            chartWidth = parseInt($(".tblContents").width() - 1, 10) * 0.5;
            chartHeight1 = parseInt((height - 240), 10);
            //alert(parseInt($(".tblContents").width() - 1, 10))
            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
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
            devGrid.PerformCallback();
            devGrid2.PerformCallback();
            devGrid3.PerformCallback();
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

            fn_OnChartDoCallback(devChart1, chartWidth, chartHeight1);
            fn_OnChartDoCallback(devChart2, chartWidth, chartHeight1);
        }


        function devGrid_RowDblClick(s, e) {
        }
        function fn_CallbackComplete(s, e) {
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {

        }

        function fn_OnChartEndCallback(s, e) {
            if (s.cpFunction != '' && s.cpFunction == 'resizeTo') {
                s.GetMainElement().style.width = s.cpChartWidth + 'px';
            }

        }

    </script>
</asp:Content>












<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">


    <div class="container">
        <div class="search">
            <table class="InputTable">
                <colgroup>
                    <col style="width: 10%" />
                    <col style="width: 23%" />
                    <col style="width: 10%" />
                    <col style="width: 23%" />
                    <col style="width: 10%" />
                    <col style="width: 24%" />
                </colgroup>
                <tr>
                    <td class="tdTitle">
                        <label>작업일자</label>
                    </td>
                    <td class="tdContent">
                        <ucCTF:Date runat="server" ID="ucDate" DateTimeOnly="false" MonthOnly="true" SingleDate="true" MaxDate="0" />
                    </td>
                    <td class="tdTitle">품목</td>
                    <td class="tdContent">
                        <ucCTF:Item ID="ucItem" runat="server" />
                        <%--<ucCTF:Item1 ID="ucItem1" runat="server" usedModel="true" useWERD="true" />--%>
                    </td>
                    <td class="tdTitle">공정명</td>
                    <td class="tdContent">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" />
                        <%--<ucCTF:WorkPOP1 ID="ucWorkPOP1" runat="server" useWERD="true" validateFields="hidUCITEMCD1|품목" />--%>
                    </td>
                </tr>
            </table>
        </div>






        <div class="content">
            <table class="tblContents" style="width: 100%;">


                <tr>
                    <td style="vertical-align: top; padding-top: 10px;">
                        <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                            ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="False"
                            OnCustomCallback="devChart1_CustomCallback" CrosshairEnabled="True" Width="300px" Height="200px">
                            <clientsideevents endcallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized1 = true; }" />
                        </dx:WebChartControl>
                    </td>

                    <td style="vertical-align: top; padding-top: 10px;">
                        <dx:WebChartControl ID="devChart2" ClientInstanceName="devChart2" runat="server"
                            ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="False"
                            OnCustomCallback="devChart2_CustomCallback" CrosshairEnabled="True" Width="300px" Height="200px">
                            <clientsideevents endcallback="function(s, e) { chartResized2 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized2 = true; }" />
                        </dx:WebChartControl>
                    </td>


                </tr>

            </table>
        </div>
        <div>
            <table>

                  <colgroup>
                    <col style="width: 34%" />
                    <col style="width: 33%" />
                    <col style="width: 33%" />
                   
                </colgroup>

                <tr>
                    <td>

                        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                            OnCustomCallback="devGrid_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" EnableViewState="false" EnableRowsCache="false"
                            Settings-ShowFooter="False" ShowGroupFooter="VisibleIfExpanded">
                            <SettingsDataSecurity AllowDelete="false" AllowEdit="false" AllowInsert="false" />
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" ShowFooter="true" />
                            <SettingsBehavior AllowSort="false" AllowDragDrop="false" />
                            <SettingsPager Mode="ShowAllRecords" />

                            <ClientSideEvents Init="fn_OnGridInit" CallbackError="fn_OnCallbackError" EndCallback="fn_OnEndCallback" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_GUBUN" Caption="구분" Width="40%">
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn FieldName="F_CNT" Caption="수량" Width="60%">
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>


                            </Columns>

                            <TotalSummary>

                                <dx:ASPxSummaryItem Visible="false" DisplayFormat="{0}" />

                            </TotalSummary>
                        </dx:ASPxGridView>

                        

                    </td>
                    <td>
                        <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                            OnCustomCallback="devGrid2_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" EnableViewState="false" EnableRowsCache="false"
                            Settings-ShowFooter="False" ShowGroupFooter="VisibleIfExpanded">
                            <SettingsDataSecurity AllowDelete="false" AllowEdit="false" AllowInsert="false" />
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" ShowFooter="true" />
                            <SettingsBehavior AllowSort="false" AllowDragDrop="false" />
                            <SettingsPager Mode="ShowAllRecords" />

                            <ClientSideEvents Init="fn_OnGridInit" CallbackError="fn_OnCallbackError" EndCallback="fn_OnEndCallback" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_CODENMKR" Caption="부적합유형" Width="40%">
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn FieldName="F_CNT" Caption="수량" Width="60%">
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>


                            </Columns>

                            <TotalSummary>

                                <dx:ASPxSummaryItem Visible="false" DisplayFormat="{0}" />

                            </TotalSummary>
                        </dx:ASPxGridView>

                    </td>
                    <td>
                        <dx:ASPxGridView ID="devGrid3" ClientInstanceName="devGrid3" runat="server" AutoGenerateColumns="false" Width="100%"
                            OnCustomCallback="devGrid3_CustomCallback" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" EnableViewState="false" EnableRowsCache="false"
                            Settings-ShowFooter="False" ShowGroupFooter="VisibleIfExpanded">
                            <SettingsDataSecurity AllowDelete="false" AllowEdit="false" AllowInsert="false" />
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" ShowFooter="true" />
                            <SettingsBehavior AllowSort="false" AllowDragDrop="false" />
                            <SettingsPager Mode="ShowAllRecords" />

                            <ClientSideEvents Init="fn_OnGridInit" CallbackError="fn_OnCallbackError" EndCallback="fn_OnEndCallback" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_CODENMKR" Caption="부적합원인" Width="40%">
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn FieldName="F_CNT" Caption="수량" Width="60%">
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>


                            </Columns>

                            <TotalSummary>

                                <dx:ASPxSummaryItem Visible="false" DisplayFormat="{0}" />

                            </TotalSummary>
                        </dx:ASPxGridView>
                    </td>



                </tr>
            </table>
        </div>

    <dx:ASPxButton ID="ASPxButton1" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" />
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick">
    </dx:ASPxGridViewExporter>



    <div class="paging">
    </div>
    </div>
</asp:Content>
