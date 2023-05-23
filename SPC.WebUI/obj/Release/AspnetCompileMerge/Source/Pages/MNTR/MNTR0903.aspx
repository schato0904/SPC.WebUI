<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="MNTR0903.aspx.cs" Inherits="SPC.WebUI.Pages.MNTR.MNTR0903" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartHeight = 0;
        var chartResized1 = false;
        var chartResized2 = false;

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

            $('#divGrid').width($('.content').width() - 300 - 10); 
            $('#divChart').width(300).height(height);

            chartHeight = parseInt(height / 2, 10) - 5;
            
            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartInPanelResized(devCallbackPanel, chartResized1, 300, chartHeight);
        });

        // 차트 이벤트
        function fn_OnChartInPanelResized(callbackPanelObj, chartResized, _width, _height) {
            if (!chartResized) {
                fn_OnChartInPanelDoCallback(callbackPanelObj, _width, _height);
            }
        }

        function fn_OnChartInPanelDoCallback(callbackPanelObj, _width, _height) {
            var oParams = _width + '|' + _height;
            callbackPanelObj.PerformCallback(oParams);
        }

        function fn_OnChartInPanelEndCallback(s, e) {
            if (s.cpFunction != '' && s.cpFunction == 'resizeTo') {
                s.GetMainElement().style.height = s.cpChartHeight + 'px';
            }
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
            //else {
            //    fn_OnChartDoCallback(devChart1, 300, chartHeight);
            //    fn_OnChartDoCallback(devChart2, 300, chartHeight);
            //}
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        //  CallbackPanel Callback
        function fn_OnCallbackPanelEndCallback(s, e) {
            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);

                s.cpResultCode = "";
                s.cpResultMsg = "";
            }
        }

        // Grid Callback Error
        function fn_OnCallbackPanelCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        function ucWeek_Changed(s, e) {
            alert(s.cpCURR_WEEK);
        }

        function ucDate_Changed(s, e) {
            alert(s.GetValue());
        }

        function srcCondition_Changed(s, e) {
            var now = new Date();
            var wdate = new Date(now.getFullYear(), now.getMonth(), now.getDate());
            var mdate = new Date(now.getFullYear(), now.getMonth(), 1);
            // 전수이고, 선택주 또는 월이 금주 또는 당월일 경우에만, 일자선택 활성화
            if (isJUNSU()) {
                if (isCURRWEEK()) {
                    $('#divDate').show();
                    wdate.setDate(wdate.getDate() - wdate.getDay());
                    txtFROMDT1.SetMinDate(wdate);
                    txtFROMDT1.SetDate(now);
                }
                if (isCURRMONTH()) {
                    $('#divDate').show();
                    txtFROMDT1.SetMinDate(mdate);
                    txtFROMDT1.SetDate(now);
                }
            } else {
                $('#divDate').hide();
                fn_SetTextValue('hidUCFROMDT1', '');
            }

            function isJUNSU() {
                return ASPxClientRadioButton.Cast('srcGBN2').GetChecked();
            }
            function isCURRWEEK() {
                return (ASPxClientRadioButton.Cast('srcDATEGBNWEEK').GetChecked() == true)
                    && (ASPxClientComboBox.Cast('ddlWeek1').GetValue() == ASPxClientComboBox.Cast('ddlWeek1').cpCURR_WEEK);
            }
            function isCURRMONTH() {
                var d = new Date();
                var curr_yearmonth = d.getFullYear() + "-" + String(d.getMonth() + 101).substring(1);
                return (ASPxClientRadioButton.Cast('srcDATEGBNMONTH').GetChecked() == true)
                    && (ASPxClientDateEdit.Cast('txtFROMDT').GetText() == curr_yearmonth);
            }
        }

        function fn_OnGridDblClick(s, e) {
            var grid = ASPxClientGridView.Cast(s);
            var vals = grid.GetRowKey(e.visibleIndex);
            var params = '300|' + chartHeight + '|' + vals;
            devCallbackPanel.PerformCallback(params);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">구분</label>
                    <div class="col-sm-2">
                        <dx:ASPxRadioButton ID="srcGBN1" runat="server" ClientInstanceName="srcGBN1" Text="자주" Checked="true" GroupName="srcGBN" CssClass="visible-sm-inline-block" ClientSideEvents-CheckedChanged="srcCondition_Changed"></dx:ASPxRadioButton>
                        <dx:ASPxRadioButton ID="srcGBN2" runat="server" ClientInstanceName="srcGBN2" Text="전수" GroupName="srcGBN" CssClass="visible-sm-inline-block" ClientSideEvents-CheckedChanged="srcCondition_Changed"></dx:ASPxRadioButton>
                    </div>
                    <label class="col-sm-1 control-label">
                        <dx:ASPxRadioButton ID="srcDATEGBNWEEK" runat="server" ClientInstanceName="srcDATEGBNWEEK" Text="주선택" Checked="true" GroupName="srcDATEGBN" ClientSideEvents-CheckedChanged="srcCondition_Changed"></dx:ASPxRadioButton>
                    </label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Week ID="ucWeek" runat="server" SingleWeek="True" Changed="srcCondition_Changed" />
                    </div>
                    <label class="col-sm-1 control-label">
                        <dx:ASPxRadioButton ID="srcDATEGBNMONTH" runat="server" ClientInstanceName="srcDATEGBNMONTH" Text="월선택" GroupName="srcDATEGBN" ClientSideEvents-CheckedChanged="srcCondition_Changed"></dx:ASPxRadioButton>
                    </label>
                    <div class="col-sm-2">
                        <ucCTF:Date runat="server" ID="ucDate" SingleDate="true" MonthOnly="true" Changed="srcCondition_Changed" />
                    </div>
                    <div id="divDate" class="col-sm-3" style="display:none;">
                        <label class="col-sm-4 control-label">일자선택</label>
                        <div class="col-sm-8">
                            <ucCTF:Date1 runat="server" ID="ucDate1" SingleDate="true" CurrentWeekOnly="true" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <div id="divGrid" class="visible-sm-inline-block" style="width:75%;padding-left:0px;padding-right:5px;vertical-align:top;">
                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_COMPNM;F_CP_A;F_CP_B;F_CP_C;F_CP_D;F_CPK_A;F_CPK_B;F_CPK_C;F_CPK_D;F_COMPCD" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid_CustomCallback">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                    <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" AllowDragDrop="false" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnGridDblClick" />
                    <Templates>
                        <StatusBar>
                            <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="F_COMPNM" Caption="업체명" Width="100%">
                        </dx:GridViewDataColumn>
                        <dx:GridViewBandColumn Name="CP_GROUP" Caption="Cp">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="F_CP_A" Caption="우수" Width="80px" HeaderStyle-BackColor="#1aae88">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_CP_B" Caption="양호" Width="80px" HeaderStyle-BackColor="#1ccacc">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_CP_C" Caption="보통" Width="80px" HeaderStyle-BackColor="#fcc633">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_CP_D" Caption="미흡" Width="80px" HeaderStyle-BackColor="#e33244">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Name="CPK_GROUP" Caption="Cpk">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="F_CPK_A" Caption="우수" Width="80px" HeaderStyle-BackColor="#1aae88">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_CPK_B" Caption="양호" Width="80px" HeaderStyle-BackColor="#1ccacc">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_CPK_C" Caption="보통" Width="80px" HeaderStyle-BackColor="#fcc633">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="F_CPK_D" Caption="미흡" Width="80px" HeaderStyle-BackColor="#e33244">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                        <dx:GridViewDataColumn FieldName="F_COMPCD" Caption="업체코드" Visible="false"></dx:GridViewDataColumn>
                    </Columns>
                </dx:ASPxGridView>            
                <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
            </div><div id="divChart" class="visible-sm-inline-block" style="width:25%;padding-left:5px;padding-right:0px;">
                <dx:ASPxCallbackPanel ID="devCallbackPanel" ClientInstanceName="devCallbackPanel" runat="server"
                    OnCallback="devCallbackPanel_Callback" Height="100%">
                    <ClientSideEvents EndCallback="fn_OnCallbackPanelEndCallback" CallbackError="fn_OnCallbackPanelCallbackError" />
                    <PanelCollection>
                        <dx:PanelContent>
                            <div id="divChartTop" style="height:50%;vertical-align:top;padding-bottom:5px;" class="visible-sm-inline-block">
                                <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                                    ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="100px" CssClass="visible-sm-inline-block">
                                    <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" BeginCallback="function(s, e) { chartResized1 = true; }" />
                                </dx:WebChartControl>
                            </div>
                            <div id="divChartBottom" style="height:50%;vertical-align:bottom;padding-top:5px;" class="visible-sm-inline-block">
                                <dx:WebChartControl ID="devChart2" ClientInstanceName="devChart2" runat="server"
                                    ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="100px" CssClass="visible-sm-inline-block">
                                    <ClientSideEvents EndCallback="function(s, e) { chartResized2 = false; fn_OnChartEndCallback(s, e); }" BeginCallback="function(s, e) { chartResized2 = true; }" />
                                </dx:WebChartControl>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </div>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
