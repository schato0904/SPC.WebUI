<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MNTR0904.aspx.cs" Inherits="SPC.WebUI.Pages.MNTR.MNTR0904" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .tblGrid {
            padding-top: 10px;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth1 = 0;
        var chartWidth2 = 0;
        var chartWidth3 = 0;
        var chartResized1 = false;
        var chartResized2 = false;
        var chartResized3 = false;

        $(document).ready(function () {
            fn_RendorTotalCount();

            $(".ucDate1").show();
            $(".ucDate2").hide();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            $("#tbl").height(height);

            chartWidth1 = parseInt($("#tdSpacer1").width(), 10);
            chartWidth2 = parseInt($("#tdSpacer2").width(), 10);
            chartWidth3 = parseInt($("#tdSpacer3").width(), 10);

            devGrid1.SetWidth(chartWidth1);
            devGrid2.SetWidth(chartWidth2);
            devGrid3.SetWidth(chartWidth3);
            devGrid1.SetHeight(height - 260);
            devGrid2.SetHeight(height - 260);
            devGrid3.SetHeight(height - 260);

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartResized(devChart1, chartResized1, chartWidth1, 250);
            fn_OnChartResized(devChart2, chartResized2, chartWidth2, 250);
            fn_OnChartResized(devChart3, chartResized3, chartWidth3, 250);
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

        function fn_OnChartEndCallback(s, e) {
            if (s.cpFunction != '' && s.cpFunction == 'resizeTo') {
                s.GetMainElement().style.width = s.cpChartWidth + 'px';
            }
        }

        // 조회
        function fn_OnSearchClick() {
            devGrid1.PerformCallback();
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

        // Grid1 End Callback
        function fn_OnGridEndCallback1(s, e) {
            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
            } else {
                fn_OnChartDoCallback(devChart1, chartWidth1, 250);

                devGrid2.PerformCallback('clear');
                devGrid3.PerformCallback('clear');
            }
            fn_RendorTotalCount();
        }

        // Grid2 End Callback
        function fn_OnGridEndCallback2(s, e) {
            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
            } else {
                fn_OnChartDoCallback(devChart2, chartWidth1, 250);

                devGrid3.PerformCallback('clear');
            }
            fn_RendorTotalCount();
        }

        // Grid3 End Callback
        function fn_OnGridEndCallback3(s, e) {
            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
            } else {
                fn_OnChartDoCallback(devChart3, chartWidth1, 250);
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

        // 공정구분 선택변경 시
        function fn_OnProcessSelectedIndexChanged(s, e) {
            if (s.GetValue() == '03') {
                $(".ucDate1").show();
                $(".ucDate2").hide();
            } else if (s.GetValue() == '08') {
                $(".ucDate1").hide();
                $(".ucDate2").show();
            }
        }

        // 업체별 클릭시
        function fn_OnDevGrid1RowDblClick(s, e) {
            if (e.visibleIndex < 0) return;

            var KeyField = devGrid1.GetRowKey(e.visibleIndex);
            devGrid2.PerformCallback(KeyField);

            var KeyFields = KeyField.split('|');

            fn_SetTextValue('hidCOMPNM', KeyFields[2]);
            fn_SetTextValue('hidITEMNM', '');
        }

        // 품목별 클릭시
        function fn_OnDevGrid2RowDblClick(s, e) {
            if (e.visibleIndex < 0) return;

            var KeyField = devGrid2.GetRowKey(e.visibleIndex);
            devGrid3.PerformCallback(KeyField);

            var KeyFields = KeyField.split('|');

            fn_SetTextValue('hidITEMNM', KeyFields[3]);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
    <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidCOMPNM" ClientInstanceName="hidCOMPNM" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="hidITEMNM" ClientInstanceName="hidITEMNM" runat="server" ClientVisible="false" />
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="ddlPROCESS" ClientInstanceName="ddlPROCESS" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <ClientSideEvents SelectedIndexChanged="fn_OnProcessSelectedIndexChanged" />
                            <Items>
                                <dx:ListEditItem Text="자주" Value="03" Selected="true" />
                                <dx:ListEditItem Text="전수" Value="08" />
                            </Items>
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3 control-label ucDate1">
                        <ucCTF:Date runat="server" ID="ucDate" />
                    </div>
                    <div class="col-sm-2 control-label ucDate2">
                        <ucCTF:Date1 runat="server" ID="ucDate1" SingleDate="true" />
                    </div>
                </div>
            </div>
            <table id="tblSpacer" style="width: 100%; height: 0px; visibility: hidden;">
                <tr>
                    <td id="tdSpacer1" style="width: 33%; padding-right: 10px;"></td>
                    <td id="tdSpacer2" style="width: 34%; padding-left: 10px; padding-right: 10px; border-left: solid 1px #ffffff; border-right: solid 1px #ffffff;"></td>
                    <td id="tdSpacer3" style="width: 33%; padding-left: 10px;"></td>
                </tr>
            </table>
        </div>
        <div class="content">
            <table id="tbl" style="width: 100%;">
                <tr>
                    <td style="width: 33%; padding-right: 10px;">
                        <table id="tbl1" style="width: 100%;">
                            <tr>
                                <td style="height: 250px;">
                                    <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server" Width="100px" Height="250px"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false"
                                        OnCustomCallback="devChart1_CustomCallback">
                                        <clientsideevents endcallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized1 = true; }" />
                                    </dx:WebChartControl>
                                </td>
                            </tr>
                            <tr>
                                <td class="tblGrid">
                                    <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                                        KeyFieldName="F_COMPCD;F_FACTCD;F_COMPNM" EnableViewState="false" EnableRowsCache="false"
                                        OnCustomCallback="devGrid1_CustomCallback">
                                        <Styles>
                                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                            <EditForm CssClass="bg-default"></EditForm>
                                        </Styles>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                                        <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback1" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnDevGrid1RowDblClick" />
                                        <Templates>
                                            <StatusBar>
                                                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid1" />
                                            </StatusBar>
                                        </Templates>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="업체명" Width="180px" />
                                            <dx:GridViewDataColumn FieldName="OK_CNT" Caption="OK" Width="50px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="OC_CNT" Caption="OC" Width="50px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="NG_CNT" Caption="NG" Width="50px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 34%; padding-left: 10px; padding-right: 10px; border-left: solid 1px #000000; border-right: solid 1px #000000;">
                        <table id="tbl2" style="width: 100%;">
                            <tr>
                                <td style="height: 250px;">
                                    <dx:WebChartControl ID="devChart2" ClientInstanceName="devChart2" runat="server" Width="100px" Height="250px"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false"
                                        OnCustomCallback="devChart2_CustomCallback">
                                        <clientsideevents endcallback="function(s, e) { chartResized2 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized2 = true; }" />
                                    </dx:WebChartControl>
                                </td>
                            </tr>
                            <tr>
                                <td class="tblGrid">
                                    <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                                        KeyFieldName="F_COMPCD;F_FACTCD;F_ITEMCD;F_ITEMNM" EnableViewState="false" EnableRowsCache="false"
                                        OnCustomCallback="devGrid2_CustomCallback">
                                        <Styles>
                                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                            <EditForm CssClass="bg-default"></EditForm>
                                        </Styles>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                                        <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback2" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnDevGrid2RowDblClick" />
                                        <Templates>
                                            <StatusBar>
                                                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid2" />
                                            </StatusBar>
                                        </Templates>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품번" Width="220px" />
                                            <dx:GridViewDataColumn FieldName="ALL_CNT" Caption="검사수" Width="50px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="NG_CNT" Caption="NG" Width="50px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 33%; padding-left: 10px;">
                        <table id="tbl3" style="width: 100%;">
                            <tr>
                                <td style="height: 250px;">
                                    <dx:WebChartControl ID="devChart3" ClientInstanceName="devChart3" runat="server" Width="100px" Height="250px"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false"
                                        OnCustomCallback="devChart3_CustomCallback">
                                        <clientsideevents endcallback="function(s, e) { chartResized3 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized3 = true; }" />
                                    </dx:WebChartControl>
                                </td>
                            </tr>
                            <tr>
                                <td class="tblGrid">
                                    <dx:ASPxGridView ID="devGrid3" ClientInstanceName="devGrid3" runat="server" AutoGenerateColumns="false" Width="100%"
                                        KeyFieldName="F_COMPCD;F_FACTCD" EnableViewState="false" EnableRowsCache="false"
                                        OnCustomCallback="devGrid3_CustomCallback">
                                        <Styles>
                                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                            <EditForm CssClass="bg-default"></EditForm>
                                        </Styles>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                                        <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback3" CallbackError="fn_OnCallbackError" />
                                        <Templates>
                                            <StatusBar>
                                                <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid3" />
                                            </StatusBar>
                                        </Templates>
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="F_INSPCD" Caption="항목코드" Visible="false" />
                                            <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="항목명" Width="220px" />
                                            <dx:GridViewDataColumn FieldName="ALL_CNT" Caption="검사수" Width="50px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="NG_CNT" Caption="NG" Width="50px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
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
