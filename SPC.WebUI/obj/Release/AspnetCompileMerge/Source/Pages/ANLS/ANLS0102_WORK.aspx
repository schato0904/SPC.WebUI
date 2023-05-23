<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ANLS0102_WORK.aspx.cs" Inherits="SPC.WebUI.Pages.ANLS.ANLS0102_WORK" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;
        var chartHeight2 = 0;
        var chartResized1 = false;
        var chartResized2 = false;
        var chartResized3 = false;
        var bSearch = false;

        $(document).ready(function () {
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            $(".tblContents").height(height);
            devGridWork.SetHeight(150);
            devGrid.SetWidth(parseInt($(".search").width() - 5, 10)-400);
            devGrid.SetHeight(190);
            devGrid2.SetHeight(height);

            chartWidth = parseInt($(".search").width() - 5, 10)-400;
            chartHeight1 = parseInt((height - 210) / 2, 10);
            chartHeight2 = height - 335;

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
            
            if (!fn_ValidateITEM()) { return; };
            //if (!fn_ValidateWORK()) { return; };
            if (!fn_ValidateINSPITEM()) { return; };

            if (false == fn_WorkCheck())
                return false;

            bSearch = false;
            //devGrid2.PerformCallback();
            devGrid.PerformCallback();
        }

        function fn_WorkCheck() {
            selectedKeys = devGridWork.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('공정을 선택하세요!!');
                return false;
            }

            if (selectedKeys.length > 50) {
                alert('공정은 최대 50개까지 선택 가능합니다!!');
                return false;
            }

            var WorkCD = selectedKeys.join('');

            txtCNT.SetValue(selectedKeys.length);
            txtWORKCD.SetValue(WorkCD)
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
            if (!fn_ValidateITEM()) { return; };
            if (!fn_ValidateWORK()) { return; };
            if (!fn_ValidateINSPITEM()) { return; };

            if (!bSearch) { alert('인쇄하시려면 먼저 조회를 해야합니다'); return; }

            var KEYS = '';
            // 업체
            KEYS += '<%#gsCOMPCD%>|';
            KEYS += encodeURIComponent('<%#gsCOMPNM%>') + '|';
            // 날짜
            KEYS += fn_GetCastText('hidUCFROMDT') + '|';
            KEYS += fn_GetCastText('hidUCTODT') + '|';
            // 품목
            KEYS += fn_GetCastText('txtUCITEMCD') + '|';
            KEYS += encodeURIComponent(fn_GetCastText('txtUCITEMNM')) + '|';
            KEYS += encodeURIComponent(fn_GetCastText('txtUCMODELNM')) + '|';
            // 공정
            KEYS += fn_GetCastText('txtUCWORKPOPCD') + '|';
            KEYS += encodeURIComponent(fn_GetCastText('txtUCWORKNM')) + '|';
            // 항목
            KEYS += fn_GetCastText('txtUCINSPITEMCD') + '|';
            KEYS += encodeURIComponent(fn_GetCastText('txtUCINSPITEMNM')) + '|';
            // 관리식 or 계산식
            KEYS += encodeURIComponent(fn_GetCastSelectedItemText('chk_calc')) + '|';
            // 시료수
            KEYS += fn_GetCastText('txtSIRYO') + '|';
            // 검사규격
            KEYS += fn_GetCastText('txtSTANDARD') + '|';
            KEYS += fn_GetCastText('txtMAX') + '|';
            KEYS += fn_GetCastText('txtMIN') + '|';
            // 관리규격
            KEYS += fn_GetCastText('txtUCLR') + '|';
            KEYS += fn_GetCastText('txtUCLX') + '|';
            KEYS += fn_GetCastText('txtLCLX') + '|';
            // 항목순번
            KEYS += fn_GetCastText('txtSERIALNO') + '|';
            // 규격이탈제외
            KEYS += !chk_reject.GetChecked() ? "0" : "1";

            fn_OnPopupANLS0102Report(KEYS);
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
                bSearch = true;

                devGrid2.PerformCallback();

                fn_OnSetSearchResult(s.cpResult1);

                fn_OnChartDoCallback(devChart1, chartWidth, chartHeight1);
                fn_OnChartDoCallback(devChart2, chartWidth, chartHeight1);

                
            }
        }

        function fn_OnEndCallback2(s, e) {
            
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

        // Validate
        function fn_OnValidate(s, e) {
        }

        // RowDblClick
        function fn_OnRowDblClick(s, e) {
            var keys = fn_GetCastText('hidUCFROMDT') + '|' +
                        fn_GetCastText('hidUCTODT') + '|' +
                        fn_GetCastText('txtUCITEMCD') + '|' +
                        fn_GetCastText('txtUCITEMNM') + '|' +
                        fn_GetCastText('txtUCWORKPOPCD') + '|' +
                        fn_GetCastText('txtUCWORKNM') + '|' +
                        fn_GetCastText('txtUCINSPITEMCD') + '|' +
                        fn_GetCastText('txtUCINSPITEMNM') + '|' +
                        fn_GetCastText('txtSTANDARD') + '|' +
                        fn_GetCastText('txtMAX') + '|' +
                        fn_GetCastText('txtMIN') + '|' +
                        fn_GetCastText('txtSERIALNO');
            fn_OnPopupMeasurement(keys);
        }

        // Set QCD34
        function fn_OnSetQCD34Values(resultValues) {
            //alert(resultValues)
            // 검사항목
            fn_OnUCSettingInspectionItem(resultValues);
            // 검사유형
            //fn_OnSetInspectionItem(resultValues);
            //F_MEAINSPCD;F_INSPDETAIL;F_STANDARD;F_MAX;F_MIN;F_UCLX;F_LCLX;F_UCLR;F_SIRYO;F_ITEMCD;F_FREEPOINT
            // 검사규격
            txtSTANDARD.SetText(resultValues[2]);
            txtMAX.SetText(resultValues[3]);
            txtMIN.SetText(resultValues[4]);
            txtSTANDARD.SetValue(resultValues[2]);
            txtMAX.SetValue(resultValues[3]);
            txtMIN.SetValue(resultValues[4]);
            txtUCLX.SetText(resultValues[5]);
            txtLCLX.SetText(resultValues[6]);
            txtUCLR.SetText(resultValues[7]);

            // 기타
            //txtSERIALNO.SetText(resultValues[10]);
            //txtSERIALNO.SetValue(resultValues[10]);
            txtSIRYO.SetText(resultValues[8]);
            txtSIRYO.SetValue(resultValues[8]);
            txtFREEPOINT.SetText(resultValues[10]);
            txtFREEPOINT.SetValue(resultValues[10]);
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

        // 검사규격과 분석결과를 세팅한다
        function fn_OnSetSearchResult(result1) {
            var freePoint = parseInt(txtFREEPOINT.GetValue(), 10);
            var resultValues1 = result1.split('|');

            // 관리규격
            fn_SetTextValue('txtUCLR', fn_SetToFixed(resultValues1[10], freePoint + 1));
            fn_SetTextValue('txtUCLX', fn_SetToFixed(resultValues1[11], freePoint + 1));
            fn_SetTextValue('txtLCLX', fn_SetToFixed(resultValues1[12], freePoint + 1));
            // 계산식
            fn_SetTextValue('txtCUCLR', fn_SetToFixed(resultValues1[16], freePoint + 1));
            fn_SetTextValue('txtCUCLX', fn_SetToFixed(resultValues1[17], freePoint + 1));
            fn_SetTextValue('txtCLCLX', fn_SetToFixed(resultValues1[18], freePoint + 1));

            // 분석결과
            //$('#tdResult01').text(fn_SetToFixed(resultValues2[0], freePoint));
            //$('#tdResult02').text(fn_SetToFixed(resultValues2[1], freePoint));
            //$('#tdResult03').text(fn_SetToFixed(resultValues2[2], freePoint + 1));
            //$('#tdResult04').text(fn_SetToFixed(resultValues2[3], freePoint));
            //$('#tdResult05').text(fn_SetToFixed(resultValues2[4], freePoint));
            //$('#tdResult06').text(fn_SetToFixed(resultValues2[5], freePoint));
            //$('#tdResult07').text(fn_SetToFixed(resultValues2[6], freePoint));
            //$('#tdResult08').text(fn_SetToFixed(resultValues2[7], freePoint));
            //$('#tdResult09').text(fn_SetToFixed(resultValues2[8], freePoint + 1));
            //$('#tdResult10').text(fn_SetToFixed(resultValues2[10], freePoint));
            //$('#tdResult11').text(fn_SetToFixed(resultValues2[9], freePoint));
            //$('#tdResult12').text(fn_SetToFixed(resultValues2[11], 0));
            //$('#tdResult13').text(fn_SetToFixed(resultValues2[12] == '' ? '0' : resultValues2[12], 2));
            //$('#tdResult14').text(fn_SetToFixed(resultValues2[13] == '' ? '0' : resultValues2[13], 2));
            //$('#tdResult15').text(resultValues2[15]);
            //$('#tdResult16').text(resultValues2[16]);
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
                    <div class="col-sm-4 control-label">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">검색일자</label>
                            <div class="col-sm-8">
                                <ucCTF:Date runat="server" id="ucDate"  />
                            </div>        
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">품목</label>
                            <div class="col-sm-8">
                                <ucCTF:Item ID="ucItem" runat="server" />
                            </div>   
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">검사항목</label>
                            <div class="col-sm-8 control-label">
                                <ucCTF:InspectionItem ID="ucInspectionItem" runat="server" validateFields="hidUCITEMCD|품목" targetCtrls="devGridWork" inspGroup="true" />
                                <dx:ASPxTextBox ID="txtMEAINSPCD" ClientInstanceName="txtMEAINSPCD" runat="server" ClientVisible="false" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">시료</label>
                            <div class="col-sm-2 control-label">
                                <dx:ASPxTextBox ID="txtSIRYO" ClientInstanceName="txtSIRYO" runat="server" Width="100%">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                            <div class="col-sm-6 control-label">
                                <dx:ASPxCheckBox ID="chk_reject" ClientInstanceName="chk_reject" runat="server" Text="규격이탈제외" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 control-label">
                        <div class="form-group">
                            <label class="col-sm-3 control-label">
                                <input type="button" style="visibility: hidden;" /></label>
                            <label class="col-sm-3 control-label">규격</label>
                            <label class="col-sm-3 control-label">상한규격</label>
                            <label class="col-sm-3 control-label">하한규격</label>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">검사규격</label>
                            <div class="col-sm-3 control-label">
                                <dx:ASPxTextBox ID="txtSTANDARD" ClientInstanceName="txtSTANDARD" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                            <div class="col-sm-3 control-label">
                                <dx:ASPxTextBox ID="txtMAX" ClientInstanceName="txtMAX" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                            <div class="col-sm-3 control-label">
                                <dx:ASPxTextBox ID="txtMIN" ClientInstanceName="txtMIN" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">관리용</label>
                            <div class="col-sm-3 control-label">
                                <dx:ASPxTextBox ID="txtUCLR" ClientInstanceName="txtUCLR" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                            <div class="col-sm-3 control-label">
                                <dx:ASPxTextBox ID="txtUCLX" ClientInstanceName="txtUCLX" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                            <div class="col-sm-3 control-label">
                                <dx:ASPxTextBox ID="txtLCLX" ClientInstanceName="txtLCLX" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">계산식</label>
                            <div class="col-sm-3 control-label">
                                <dx:ASPxTextBox ID="txtCUCLR" ClientInstanceName="txtCUCLR" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                            <div class="col-sm-3 control-label">
                                <dx:ASPxTextBox ID="txtCUCLX" ClientInstanceName="txtCUCLX" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                            <div class="col-sm-3 control-label">
                                <dx:ASPxTextBox ID="txtCLCLX" ClientInstanceName="txtCLCLX" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 control-label">
                        <div class="form-group">
                            <dx:ASPxTextBox ID="txtWORKCD" ClientInstanceName="txtWORKCD" runat="server" ClientVisible="false" />
                            <dx:ASPxTextBox ID="txtCNT" ClientInstanceName="txtCNT" runat="server" ClientVisible="false" />
                            <dx:ASPxGridView ID="devGridWork" ClientInstanceName="devGridWork" runat="server" AutoGenerateColumns="true" 
                            KeyFieldName="F_WORKCD" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGridWork_CustomCallback"  >
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                            <SettingsBehavior AllowSort="false" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnEndCallback2" CallbackError="fn_OnCallbackError" />
                            <Templates>
                                <StatusBar>
                                    <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGridWork" />
                                </StatusBar>
                            </Templates>
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                                    <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                            ClientSideEvents-CheckedChanged="function(s, e) { devGridWork.SelectAllRowsOnPage(s.GetChecked()); }" />
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="F_WORKCD" Caption="공정코드" Width="100px" />
                                <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="180px">
                                    <CellStyle HorizontalAlign="Left"></CellStyle>
                                </dx:GridViewDataColumn>                        
                            </Columns>
                        </dx:ASPxGridView>
                        </div>
                    </div>           
                </div>
            </div>
        </div>
    <div class="content">
        <table class="tblContents" style="width: 100%;">
            <tr>
                <td id="tdLeft" style="padding-right:10px" >
                    <table class="tblContents" style="width: 100%;" >
                        <tr>
                            <td style="vertical-align: top;">
                                <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                                    ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="100px"
                                    OnCustomCallback="devChart1_CustomCallback">
                                    <clientsideevents endcallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized1 = true; }" />
                                </dx:WebChartControl>
                            </td>                            
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <dx:WebChartControl ID="devChart2" ClientInstanceName="devChart2" runat="server"
                                    ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="100px"
                                    OnCustomCallback="devChart2_CustomCallback">
                                    <clientsideevents endcallback="function(s, e) { chartResized2 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized2 = true; }" />
                                </dx:WebChartControl>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 200px; padding-top: 10px;">
                                <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                                <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                                <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="true" Width="100%"
                                    KeyFieldName="공정" EnableViewState="false" EnableRowsCache="false"
                                    OnCustomCallback="devGrid_CustomCallback">
                                    <Styles>
                                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                        <EditForm CssClass="bg-default"></EditForm>
                                    </Styles>
                                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" HorizontalScrollBarMode="Visible" ShowStatusBar="Hidden" />
                                    <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnEndCallback" CallbackError="fn_OnCallbackError"  />
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <dx:ASPxGridView ID="devGrid2" ClientInstanceName="devGrid2" runat="server" AutoGenerateColumns="true" Width="100%"
                        KeyFieldName="F_WORKNM;F_WORKCD" EnableViewState="false" EnableRowsCache="false"
                        OnCustomCallback="devGrid2_CustomCallback" OnHtmlRowPrepared="devGrid_HtmlRowPrepared"
                        >
                        <Styles>
                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                            <EditForm CssClass="bg-default"></EditForm>
                        </Styles>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" />
                        <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnEndCallback2" CallbackError="fn_OnCallbackError"/>
                        <Columns>
                            <dx:GridViewDataColumn FieldName="F_WORKNM"  Caption="공정명" Width="160px" >
                                <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_CP" Caption="Cp"  Width="100px"  />                    
                            <dx:GridViewDataColumn FieldName="F_CPK" Caption="Cpk"  Width="100px"  />                    
                        </Columns>                
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>
    <div class="paging"></div>
    </div>
</asp:Content>
