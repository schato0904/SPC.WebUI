<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ANLS0102_borgwarner.aspx.cs" Inherits="SPC.WebUI.Pages.ANLS.ANLS0102_borgwarner" %>
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
            
            chartWidth = parseInt($(".search").width() - 5, 10);
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
            if (!fn_ValidateINSPITEM()) { return; };

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
        function fn_OnPrintClick() {
        }

        // 엑셀
        function fn_OnExcelClick() { }

        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            fn_doSetGridEventAction('false');
            if (s.cpResultCode == '0') {
                fn_OnChartDoCallback(devChart1, chartWidth, chartHeight1);
                fn_OnChartDoCallback(devChart2, chartWidth, chartHeight1);
            }

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

                fn_OnSetSearchResult(s.cpResult1, s.cpResult2);

                fn_OnChartDoCallback(devChart1, chartWidth, chartHeight1);
                fn_OnChartDoCallback(devChart2, chartWidth, chartHeight1);
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
            // 검사유형
            fn_OnSetInspectionItem(resultValues);
            // 검사규격 F_MEAINSPCD;F_INSPDETAIL;F_INSPCD;F_INSPNM;F_STANDARD;F_MAX;F_MIN;F_UCLX;F_LCLX;F_UCLR;F_SIRYO;F_FREEPOINT

            txtSTANDARD.SetText(resultValues[4]);
            txtMAX.SetText(resultValues[5]);
            txtMIN.SetText(resultValues[6]);
            txtSTANDARD.SetValue(resultValues[4]);
            txtMAX.SetValue(resultValues[5]);
            txtMIN.SetValue(resultValues[6]);
            txtUCLX.SetText(resultValues[7]);
            txtLCLX.SetText(resultValues[8]);
            txtUCLR.SetText(resultValues[9]);

            txtMEAINSPCD.SetText(resultValues[0]);
            // 기타
            txtSERIALNO.SetText(resultValues[10]);
            txtSERIALNO.SetValue(resultValues[10]);
            txtSIRYO.SetText(resultValues[10]);
            txtSIRYO.SetValue(resultValues[10]);
            txtFREEPOINT.SetText(resultValues[11]);
            txtFREEPOINT.SetValue(resultValues[11]);
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
        function fn_OnSetSearchResult(result1, result2) {
            var freePoint = parseInt(txtFREEPOINT.GetValue(), 10);
            var resultValues1 = result1.split('|');

            // 관리규격
            fn_SetTextValue('txtUCLR', fn_SetToFixed(resultValues1[11], freePoint + 1));
            fn_SetTextValue('txtUCLX', fn_SetToFixed(resultValues1[12], freePoint + 1));
            fn_SetTextValue('txtLCLX', fn_SetToFixed(resultValues1[13], freePoint + 1));
            // 계산식
            fn_SetTextValue('txtCUCLR', fn_SetToFixed(resultValues1[17], freePoint + 1));
            fn_SetTextValue('txtCUCLX', fn_SetToFixed(resultValues1[18], freePoint + 1));
            fn_SetTextValue('txtCLCLX', fn_SetToFixed(resultValues1[19], freePoint + 1));
        }
    </script>
    <script>
        var timerITEMCD = null;

        function fn_OnUCINSPITEMKeyUp(s, e) {
            if (fn_GetCastValue('txtUCINSPITEMCD').length != 6) {
                fn_SetTextValue('txtUCINSPITEMNM', '');
            }
        }

        // 검사항목검색창 오픈
        function fn_OnPopupUCInspectionItemSearch() {
            var ITEMCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidUCITEMCD')) ? '' : ASPxClientControl.Cast('hidUCITEMCD').GetText();
            var WORKCD = !ASPxClientUtils.IsExists(ASPxClientControl.Cast('hidUCWORKPOPCD')) ? '' : ASPxClientControl.Cast('hidUCWORKPOPCD').GetText();


            var pPage = '<%=Page.ResolveUrl("~/Pages/ANLS/Popup/ANLS0102_borgPOP.aspx")%>' +
                    '?TITLE=검사항목조회' +
                    '&CRUD=R' +
                    '&ITEMCD=' + ITEMCD +
                    '&WORKCD=' + WORKCD;
            fn_OnPopupOpen(pPage, 800, 500);
        }

        // 검색된 검사항목 세팅
        function fn_OnSettingMeainsp(CODE, NAME) {
            hidUCINSPITEMCD.SetText(CODE);
            hidUCINSPITEMCD.SetValue(CODE);
            txtUCINSPITEMCD.SetValue(CODE);
            txtUCINSPITEMCD.SetText(CODE);
            txtUCINSPITEMNM.SetValue(NAME);
            txtUCINSPITEMNM.SetText(NAME);
        }

        // Tree용 Event
        function fn_OnSetupInspectionTree(oParam) {
            var oParams = oParam.split('|');

            fn_SetTextValue('hidUCINSPITEMCD', oParams[10]);
            fn_SetTextValue('txtUCINSPITEMCD', oParams[10]);
            fn_SetTextValue('txtUCINSPITEMNM', oParams[11]);

        }

        function fn_ValidateINSPITEM() {
            if (hidUCINSPITEMCD.GetValue() == "" || hidUCINSPITEMCD.GetValue() == null) {
                alert("검사항목을 입력하세요!!"); return false;
            } else {
                return true;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="txtSERIALNO" ClientInstanceName="txtSERIALNO" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="txtMEAINSPCD" ClientInstanceName="txtMEAINSPCD" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="txtFREEPOINT" ClientInstanceName="txtFREEPOINT" runat="server" ClientVisible="false" Text="0" />
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group" style="display: <%if (!gsVENDOR){%>display<%} else{ %>none<%} %>">
                    <div class="col-sm-4 control-label">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">업체</label>
                            <div class="col-sm-8 control-label">
                                <ucCTF:Comp ID="ucComp" runat="server" targetCtrls="ddlFACT" masterChk="0" nullText="선택" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 control-label">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">공장</label>
                            <div class="col-sm-8 control-label">
                                <ucCTF:Fact ID="ucFact" runat="server" nullText="선택" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-4 control-label">
                        <div class="form-group">
                            <label class="col-sm-4 control-label">기간</label>
                            <div class="col-sm-8 control-label">
                                <ucCTF:Date ID="ucDate" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">품목</label>
                            <div class="col-sm-8 control-label">
                                <ucCTF:Item ID="ucItem" runat="server" usedModel="true" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">공정</label>
                            <div class="col-sm-8 control-label">
                                <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">항목</label>
                            <div class="col-sm-8 control-label">
                                <dx:ASPxTextBox ID="hidINSPITEMCD" ClientInstanceName="hidUCINSPITEMCD" runat="server" ClientVisible="false" />
                                <div class="control-label" style="float: left; width: 28%;">
                                    <dx:ASPxTextBox ID="txtINSPITEMCD" ClientInstanceName="txtUCINSPITEMCD" runat="server" Width="100%"
                                        OnInit="txtINSPITEMCD_Init">
                                        <ClientSideEvents KeyUp="fn_OnUCINSPITEMKeyUp" />
                                    </dx:ASPxTextBox>
                                </div>
                                <div style="float: left; width: 1%;">&nbsp;</div>
                                <div class="control-label" style="float: left; width: 61%;">
                                    <dx:ASPxTextBox ID="txtINSPITEMNM" ClientInstanceName="txtUCINSPITEMNM" runat="server" Width="100%">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </div>
                                <div style="float: left; width: 1%;">&nbsp;</div>
                                <div class="control-label" style="float: left; width: 9%;">
                                    <button class="btn btn-default btn-xs" data-toggle="button" title="검사항목조회" onclick="fn_OnPopupUCInspectionItemSearch(); return false;">
                                        <i class="i i-popup text"></i>
                                        <i class="i i-popup text-active text-danger"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 control-label">
                        <div class="form-group">
                            <div class="col-sm-8 control-label" style="padding-right: 0px;">
                                <div style="float: right;">
                                    <dx:ASPxRadioButtonList ID="chk_calc" ClientInstanceName="chk_calc" runat="server"
                                        RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                                        <Items>
                                            <dx:ListEditItem Value="0" Text="관리용" />
                                            <dx:ListEditItem Value="1" Text="계산식" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </div>
                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">기종</label>
                            <div class="col-sm-8 control-label">
                                <ucCTF:Model ID="ucModel" runat="server" />
                            </div>
                        </div>
                    <div class="form-group">
                        <label class="col-sm-4 control-label">구분</label>
                        <div class="col-sm-8 control-label">
                            <ucCTF:InspectionBox ID="ucInspectionBox" runat="server" />
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
            </div>
        </div>
    </div>
    <div class="content">
        <table class="tblContents" style="width: 100%;">
            <tr>
                <td id="tdLeft" style="padding-right: 5px;">
                    <table class="tblContents" style="width: 100%;">
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
                                    KeyFieldName="검사일자" EnableViewState="false" EnableRowsCache="false"
                                    OnCustomCallback="devGrid_CustomCallback">
                                    <Styles>
                                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                        <EditForm CssClass="bg-default"></EditForm>
                                    </Styles>
                                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" HorizontalScrollBarMode="Visible" ShowStatusBar="Hidden" />
                                    <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowDblClick="fn_OnRowDblClick" />
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
