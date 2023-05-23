<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD_DACO5001.aspx.cs" Inherits="SPC.WebUI.Pages.WERD.WERD_DACO5001" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;
        var chartHeight2 = 0;
        var chartResized1 = false;
        var chartResized2 = false;

        $(document).ready(function () {
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt(fn_GetDocumentHeight() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));

            $(".tblContents").height(height);

            chartWidth = parseInt(($(".search").width()), 10);

            $(".tdChart1").width(chartWidth);
            chartHeight1 = parseInt((height - 250), 10);
            if (chartHeight1 < 200) {
                chartHeight1 = 200;
            }
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
        }

        function fn_CallbackComplete(s, e) {
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            //alert(e.message);
        }

        function fn_OnChartEndCallback(s, e) {
            if (s.cpFunction != '' && s.cpFunction == 'resizeTo') {
                s.GetMainElement().style.width = s.cpChartWidth + 'px';
            }

        }

        function fn_rdoGBN_Change(s, e) {
            if (rdoGBN.GetValue() == "35") {

                document.getElementById("div_type").style.display = "";
                document.getElementById("div_cause").style.display = "none";
            }
            else {

                document.getElementById("div_type").style.display = "none";
                document.getElementById("div_cause").style.display = "";
            }
        }

        function fn_OnLINEValueChanged(s, e) {
            var val = s.GetValue();
            hidCOMP.SetValue(val);
        }

        function fn_OnLINEEndCallback(s, e) {
            isLINEEndCallback = parent.parent.isTreeLINESetup;
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <dx:ASPxComboBox ID="yearCOMBO" ClientInstanceName="yearCOMBO" runat="server" OnCallback="yearCOMBO_Callback" IncrementalFilteringMode="None">
                            <ClientSideEvents Init="fn_OnControlDisable" />
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-1 control-label">협력사명</label>
                    <div class="col-sm-1">
                        <dx:ASPxTextBox ID="hidCOMP" ClientInstanceName="hidCOMP" runat="server" ClientVisible="false" />
                        <dx:ASPxComboBox ID="ddlCOMP" ClientInstanceName="ddlCOMP" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton"
                            TextField="F_LINENM" ValueField="F_LINECD" OnDataBound="ddlLINE_DataBound" OnCallback="ddlLINE_Callback">
                            <ClientSideEvents ValueChanged="fn_OnLINEValueChanged" Init="fn_OnControlDisable" EndCallback="fn_OnLINEEndCallback" />
                        </dx:ASPxComboBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group"></div>
        <div class="content">
            <table style="width: 100%;" border="0">
                <tr>
                    <td id="tdLeft">
                        <table class="tblContents" style="width: 100%;" border="0">
                            <tr>
                                <td class="tdChart1" style="vertical-align: top;">
                                    <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="False" Height="100px"
                                        OnCustomCallback="devChart1_CustomCallback" CrosshairEnabled="True" Width="300px">
                                        <clientsideevents endcallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized1 = true; }" />
                                    </dx:WebChartControl>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 250px; padding-top: 5px;" colspan="2">
                                    <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="true" Width="100%"
                                        OnDataBinding="devGrid_DataBinding"
                                        OnCustomCallback="devGrid_CustomCallback"
                                        KeyFieldName="F_GUBUN;" EnableViewState="false" EnableRowsCache="false" OnHtmlRowPrepared="devGrid_HtmlRowPrepared">
                                        <Styles>
                                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                            <EditForm CssClass="bg-default"></EditForm>
                                        </Styles>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" HorizontalScrollBarMode="Visible" ShowStatusBar="Hidden" />
                                        <SettingsBehavior AllowSort="false" AllowDragDrop="false" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <ClientSideEvents Init="function(s,e){s.SetHeight(240)}" CallbackError="fn_OnCallbackError" EndCallback="fn_OnEndCallback" />
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="F_GUBUN" Caption="구분" Width="10%" />
                                            <dx:GridViewDataColumn FieldName="F_N1" Caption="1월" Width="10%" CellStyle-HorizontalAlign="Right"/>
                                            <dx:GridViewDataColumn FieldName="F_N2" Caption="2월" Width="10%" CellStyle-HorizontalAlign="Right" />
                                            <dx:GridViewDataColumn FieldName="F_N3" Caption="3월" Width="10%" CellStyle-HorizontalAlign="Right"/>
                                            <dx:GridViewDataColumn FieldName="F_N4" Caption="4월" Width="10%" CellStyle-HorizontalAlign="Right"/>
                                            <dx:GridViewDataColumn FieldName="F_N5" Caption="5월" Width="10%" CellStyle-HorizontalAlign="Right"/>
                                            <dx:GridViewDataColumn FieldName="F_N6" Caption="6월" Width="10%" CellStyle-HorizontalAlign="Right"/>
                                            <dx:GridViewDataColumn FieldName="F_N7" Caption="7월" Width="10%" CellStyle-HorizontalAlign="Right"/>
                                            <dx:GridViewDataColumn FieldName="F_N8" Caption="8월" Width="10%" CellStyle-HorizontalAlign="Right"/>
                                            <dx:GridViewDataColumn FieldName="F_N9" Caption="9월" Width="10%" CellStyle-HorizontalAlign="Right"/>
                                            <dx:GridViewDataColumn FieldName="F_N10" Caption="10월" Width="10%" CellStyle-HorizontalAlign="Right"/>
                                            <dx:GridViewDataColumn FieldName="F_N11" Caption="11월" Width="10%" CellStyle-HorizontalAlign="Right"/>
                                            <dx:GridViewDataColumn FieldName="F_N12" Caption="12월" Width="10%" CellStyle-HorizontalAlign="Right"/>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>

