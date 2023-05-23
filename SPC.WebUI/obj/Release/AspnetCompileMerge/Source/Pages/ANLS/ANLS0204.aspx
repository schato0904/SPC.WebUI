<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ANLS0204.aspx.cs" Inherits="SPC.WebUI.Pages.ANLS.ANLS0204" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
<%-- 비교차이분석(anmr090m.aspx) --%>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;
        var chartHeight2 = 0;
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
            var height = Math.max(0, parseInt($(document.documentElement).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth() - 210));
            $("#divChart").height(height);
            //devGrid.SetWidth(parseInt($(".search").width() - 5, 10));
            devGrid1.SetHeight(190);
            devGrid2.SetHeight(190);

            chartWidth = parseInt(($(".search").width() - 38) / 2, 10);
            chartHeight1 = parseInt(height, 10);
            chartHeight2 = chartHeight1;

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartResized(devChart1, chartResized1, chartWidth, chartHeight1);
            fn_OnChartResized(devChart2, chartResized2, chartWidth, chartHeight1);
        });

        // 조회
        function fn_OnSearchClick() {
            if (false == fn_OnValidate()) return false;

            bSearch = false;

            devGrid1.PerformCallback();
            devGrid2.PerformCallback();
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
        function fn_OnPrintClick() {
            if (false == fn_OnValidate()) return;

            if (!bSearch) { alert('인쇄하시려면 먼저 조회를 해야합니다'); return; }

            var KEYS = '';
            // 업체
            KEYS += '<%#gsCOMPCD%>|';
            KEYS += encodeURIComponent('<%#gsCOMPNM%>') + '|';
            // 날짜
            KEYS += fn_GetCastText('hidUCFROMDT1') + '|';
            KEYS += fn_GetCastText('hidUCTODT1') + '|';
            // 품목
            KEYS += fn_GetCastText('txtUCITEMCD1') + '|';
            KEYS += encodeURIComponent(fn_GetCastText('txtUCITEMNM1')) + '|';
            // 공정
            KEYS += fn_GetCastText('txtUCWORKPOPCD1') + '|';
            KEYS += encodeURIComponent(fn_GetCastText('txtUCWORKNM1')) + '|';
            // 항목
            KEYS += fn_GetCastText('txtUCINSPITEMCD1') + '|';
            KEYS += encodeURIComponent(fn_GetCastText('txtUCINSPITEMNM1')) + '|';
            // 검사규격
            KEYS += fn_GetCastText('txtSTANDARD1') + '|';
            KEYS += fn_GetCastText('txtMAX1') + '|';
            KEYS += fn_GetCastText('txtMIN1') + '|';
            // 날짜
            KEYS += fn_GetCastText('hidUCFROMDT2') + '|';
            KEYS += fn_GetCastText('hidUCTODT2') + '|';
            // 품목
            KEYS += fn_GetCastText('txtUCITEMCD2') + '|';
            KEYS += encodeURIComponent(fn_GetCastText('txtUCITEMNM2')) + '|';
            // 공정
            KEYS += fn_GetCastText('txtUCWORKPOPCD2') + '|';
            KEYS += encodeURIComponent(fn_GetCastText('txtUCWORKNM2')) + '|';
            // 항목
            KEYS += fn_GetCastText('txtUCINSPITEMCD2') + '|';
            KEYS += encodeURIComponent(fn_GetCastText('txtUCINSPITEMNM2')) + '|';
            // 검사규격
            KEYS += fn_GetCastText('txtSTANDARD2') + '|';
            KEYS += fn_GetCastText('txtMAX2') + '|';
            KEYS += fn_GetCastText('txtMIN2');

            fn_OnPopupANLS0204Report(KEYS);
        }

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
                // 첫번째 그리드 완료시 첫번째차트 콜백 호출
                if (ASPxClientGridView.Cast('devGrid1') == s) {
                    fn_OnChartDoCallback(devChart1, chartWidth, chartHeight1);
                // 두번째 그리드 완료시 두번째차트 콜백 호출
                } else if (ASPxClientGridView.Cast('devGrid2') == s) {
                    fn_OnChartDoCallback(devChart2, chartWidth, chartHeight2);
                }

                bSearch = true;
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            if (fn_GetCastText('txtUCITEMCD1') == '') {
                alert('품목정보를 입력하세요!!');
                txtUCITEMCD1.Focus();
                return false;
            }

            if (fn_GetCastText('txtUCWORKPOPCD1') == '') {
                alert('공정정보를 입력하세요!!');
                txtUCWORKPOPCD1.Focus();
                return false;
            }

            if (fn_GetCastText('txtUCINSPITEMCD1') == '') {
                alert('항목정보를 입력하세요!!');
                txtUCINSPITEMCD1.Focus();
                return false;
            }

            if (fn_GetCastText('txtUCITEMCD2') == '') {
                alert('품목정보를 입력하세요!!');
                txtUCITEMCD2.Focus();
                return false;
            }

            if (fn_GetCastText('txtUCWORKPOPCD2') == '') {
                alert('공정정보를 입력하세요!!');
                txtUCWORKPOPCD2.Focus();
                return false;
            }

            if (fn_GetCastText('txtUCINSPITEMCD2') == '') {
                alert('항목정보를 입력하세요!!');
                txtUCINSPITEMCD2.Focus();
                return false;
            }
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

        // Set QCD34
        function fn_OnSetQCD34_1Values(resultValues) {
            // 검사항목
            fn_OnUCSettingInspectionItem1(resultValues);

            // 검사규격
            fn_SetTextValue('txtSTANDARD1', resultValues[4]);
            fn_SetTextValue('txtMAX1', resultValues[5]);
            fn_SetTextValue('txtMIN1', resultValues[6]);
            // 관리규격
            fn_SetTextValue('txtUCLR1', ((eval(resultValues[7]) + eval(resultValues[8])) / 2).toFixed(resultValues[12]));
            fn_SetTextValue('txtUCLX1', resultValues[7]);
            fn_SetTextValue('txtLCLX1', resultValues[8]);
            // 기타
            fn_SetTextValue('txtSERIALNO1', resultValues[10]);
            //txtSIRYO.SetText(resultValues[11]);
            //txtSIRYO.SetValue(resultValues[11]);
            //txtFREEPOINT.SetText(resultValues[12]);
            //txtFREEPOINT.SetValue(resultValues[12]);
        }

        // Set QCD34
        function fn_OnSetQCD34_2Values(resultValues) {
            // 검사항목
            fn_OnUCSettingInspectionItem2(resultValues);

            // 검사규격
            fn_SetTextValue('txtSTANDARD2', resultValues[4]);
            fn_SetTextValue('txtMAX2', resultValues[5]);
            fn_SetTextValue('txtMIN2', resultValues[6]);
            // 관리규격
            fn_SetTextValue('txtUCLR2', ((eval(resultValues[7]) + eval(resultValues[8])) / 2).toFixed(resultValues[12]));
            fn_SetTextValue('txtUCLX2', resultValues[7]);
            fn_SetTextValue('txtLCLX2', resultValues[8]);
            // 기타
            fn_SetTextValue('txtSERIALNO2', resultValues[10]);
            //txtSIRYO.SetText(resultValues[11]);
            //txtSIRYO.SetValue(resultValues[11]);
            //txtFREEPOINT.SetText(resultValues[12]);
            //txtFREEPOINT.SetValue(resultValues[12]);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="txtSERIALNO1" ClientInstanceName="txtSERIALNO1" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="txtSERIALNO2" ClientInstanceName="txtSERIALNO2" runat="server" ClientVisible="false" />
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <div class="col-sm-6">
                        <%--<div class="form-group">--%>
                            <div class="form-group" style="padding-left:13px;padding-right:8px;">
                                <div class="col-sm-12 bg-info" style="">집단1</div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">기간</label>
                                <div class="col-sm-10 control-label">
                                    <ucCTF:Date1 ID="ucDate1" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">품목</label>
                                <div class="col-sm-10 control-label">
                                    <ucCTF:Item1 ID="ucItem1" runat="server" usedModel="false" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">공정</label>
                                <div class="col-sm-10 control-label">
                                    <ucCTF:WorkPOP1 ID="ucWorkPOP1" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">항목</label>
                                <div class="col-sm-10 control-label">
                                    <ucCTF:InspectionItem1 ID="ucInspectionItem1" runat="server" validateFields="hidUCITEMCD1|품목;hidUCWORKPOPCD1|공정" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">검사규격</label>
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxTextBox ID="txtSTANDARD1" ClientInstanceName="txtSTANDARD1" runat="server" Width="100%" HorizontalAlign="Right">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                            <%--</div>
                            <div class="form-group">--%>
                                <label class="col-sm-2 control-label">상한규격</label>
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxTextBox ID="txtMAX1" ClientInstanceName="txtMAX1" runat="server" Width="100%" HorizontalAlign="Right">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                            <%--</div>
                            <div class="form-group">--%>
                                <label class="col-sm-2 control-label">하한규격</label>
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxTextBox ID="txtMIN1" ClientInstanceName="txtMIN1" runat="server" Width="100%" HorizontalAlign="Right">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                        <%--</div>--%>
                    </div>
                    <div class="col-sm-6">
                        <%--<div class="form-group">--%>
                            <div class="form-group" style="padding-left:8px;padding-right:13px;">
                                <div class="col-sm-12 bg-info" style="">집단2</div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">기간</label>
                                <div class="col-sm-10 control-label">
                                    <ucCTF:Date2 ID="ucDate2" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">품목</label>
                                <div class="col-sm-10 control-label">
                                    <ucCTF:Item2 ID="ucItem2" runat="server" usedModel="false" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">공정</label>
                                <div class="col-sm-10 control-label">
                                    <ucCTF:WorkPOP2 ID="ucWorkPOP2" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">항목</label>
                                <div class="col-sm-10 control-label">
                                    <ucCTF:InspectionItem2 ID="ucInspectionItem2" runat="server" validateFields="hidUCITEMCD2|품목;hidUCWORKPOPCD2|공정" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">검사규격</label>
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxTextBox ID="txtSTANDARD2" ClientInstanceName="txtSTANDARD2" runat="server" Width="100%" HorizontalAlign="Right">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                            <%--</div>
                            <div class="form-group">--%>
                                <label class="col-sm-2 control-label">상한규격</label>
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxTextBox ID="txtMAX2" ClientInstanceName="txtMAX2" runat="server" Width="100%" HorizontalAlign="Right">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                            <%--</div>
                            <div class="form-group">--%>
                                <label class="col-sm-2 control-label">하한규격</label>
                                <div class="col-sm-2 control-label">
                                    <dx:ASPxTextBox ID="txtMIN2" ClientInstanceName="txtMIN2" runat="server" Width="100%" HorizontalAlign="Right">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                            </div>
                        <%--</div>--%>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <%-- 차트 영역 --%>
            <div id="divChart" class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group" style="height:250px;">
                    <div class="col-sm-6">
                        <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                            ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="100px"
                            OnCustomCallback="devChart1_CustomCallback">
                            <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" BeginCallback="function(s, e) { chartResized1 = true; }" />
                        </dx:WebChartControl>
                    </div>
                    <div class="col-sm-6">
                        <dx:WebChartControl ID="devChart2" ClientInstanceName="devChart2" runat="server"
                            ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="100px"
                            OnCustomCallback="devChart2_CustomCallback">
                            <ClientSideEvents EndCallback="function(s, e) { chartResized2 = false; fn_OnChartEndCallback(s, e); }" BeginCallback="function(s, e) { chartResized2 = true; }" />
                        </dx:WebChartControl>
                    </div>
                </div>
            </div>
            <%-- 그리드 영역 --%>
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;height:200px;">
                <div class="form-group">
                    <div class="col-sm-6">                
                        <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                        <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_SEQ" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid1_CustomCallback">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                            <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_SEQ" Caption="No." Width="150px">
                                </dx:GridViewDataColumn>
                                <dx:GridViewBandColumn Caption="측정데이타">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="F_VALUE1" Caption="집단1" >
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="F_VALUE2" Caption="집단2" >
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="devGrid1Exporter" runat="server" GridViewID="devGrid1"></dx:ASPxGridViewExporter>
                    </div>
                    <div class="col-sm-6">
                        <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_SEQ" EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid2_CustomCallback">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Hidden" />
                            <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_SEQ" Caption="No."  />
                                <dx:GridViewDataColumn FieldName="F_GBNNM" Caption="규격" />
                                <dx:GridViewDataColumn FieldName="F_VALUE1" Caption="집단1" />
                                <dx:GridViewDataColumn FieldName="F_VALUE2" Caption="집단2" />
                            </Columns>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="devGrid2Exporter" runat="server" GridViewID="devGrid2"></dx:ASPxGridViewExporter>
                    </div>
                </div>
            </div>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>