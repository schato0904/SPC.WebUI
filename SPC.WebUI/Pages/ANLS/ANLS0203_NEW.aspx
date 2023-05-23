<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ANLS0203_NEW.aspx.cs" Inherits="SPC.WebUI.Pages.ANLS.ANLS0203_NEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;
        var chartResized1 = false;
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

            $(".content").height(height);

            chartWidth = parseInt($(".search").width() - 400 - 5, 10);
            chartHeight1 = parseInt((height), 10);
            $(".tblchartContents").height(parseInt((height), 10));
            if ((parseInt((height))) < 330) {
                chartHeight1 = 330;
                $(".tblchartContents").height(330);
            }

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartResized(devChart1, chartResized1, chartWidth, chartHeight1);

        });

        // 조회
        function fn_OnSearchClick() {


            if (!fn_ValidateITEM()) { return; };
            if (!fn_ValidateWORK()) { return; };
            if (!fn_ValidateINSPITEM()) { return; };

            bSearch = false;

            fn_OnChartDoCallback(devChart1, chartWidth, chartHeight1);
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
            // 시료수
            KEYS += fn_GetCastText('txtSIRYO') + '|';
            // 검사규격
            KEYS += fn_GetCastText('txtSTANDARD') + '|';
            KEYS += fn_GetCastText('txtMAX') + '|';
            KEYS += fn_GetCastText('txtMIN') + '|';
            // 항목순번
            KEYS += fn_GetCastText('txtSERIALNO');

            fn_OnPopupANLS0203Report(KEYS);
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
                fn_OnSetSearchResult(s.cpResult1, s.cpResult2);
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
            // 검사항목
            fn_OnUCSettingInspectionItem(resultValues);
            // 검사유형
            fn_OnSetInspectionItem(resultValues);

            // 검사규격
            txtSTANDARD.SetText(resultValues[4]);
            txtMAX.SetText(resultValues[5]);
            txtMIN.SetText(resultValues[6]);
            txtSTANDARD.SetValue(resultValues[4]);
            txtMAX.SetValue(resultValues[5]);
            txtMIN.SetValue(resultValues[6]);

            // 기타
            txtSERIALNO.SetText(resultValues[10]);
            txtSERIALNO.SetValue(resultValues[10]);
            txtSIRYO.SetText(resultValues[11]);
            txtSIRYO.SetValue(resultValues[11]);
            txtFREEPOINT.SetText(resultValues[12]);
            txtFREEPOINT.SetValue(resultValues[12]);
        }

        // 차트 이벤트
        function fn_OnChartResized(chartObj, chartResized, _width, _height) {
            if (!chartResized) {
                fn_OnChartDoCallback(chartObj, _width, _height, 'resize');
            }
        }

        function fn_OnChartDoCallback(chartObj, _width, _height, resize) {
            var oParams = _width + '|' + _height + '|' + resize;
            chartObj.PerformCallback(oParams);
        }

        function fn_OnChartEndCallback(s, e) {
            if (s.cpResultCode == '0') {
                alert(s.cpResultMsg);
                s.cpResultCode = "";
                s.cpResultMsg = "";
            } else {
                if (s.cpFunction != '' && s.cpFunction == 'resizeTo') {
                    s.GetMainElement().style.width = s.cpChartWidth + 'px';
                }

                if (s.cpResult1 != "") {
                    bSearch = true;

                    fn_OnSetSearchResult(s.cpResult1, s.cpResult2);
                }
            }

        }

        // 검사규격과 분석결과를 세팅한다
        function fn_OnSetSearchResult(result1, result2) {
            var freePoint = parseInt(txtFREEPOINT.GetValue(), 10);
            var resultValues1 = result1.split('|');
            var resultValues2 = result2.split('|');

            // 분석결과
            $('#tdResult01').text(fn_SetToFixed(resultValues2[0], freePoint));
            $('#tdResult02').text(fn_SetToFixed(resultValues2[1], freePoint));
            $('#tdResult03').text(fn_SetToFixed(resultValues2[2], freePoint + 1));
            $('#tdResult04').text(fn_SetToFixed(resultValues2[3], freePoint));
            $('#tdResult05').text(fn_SetToFixed(resultValues2[4], 2));
            $('#tdResult06').text(fn_SetToFixed(resultValues2[5], 2));
            $('#tdResult07').text(fn_SetToFixed(resultValues2[6], freePoint));
            $('#tdResult08').text(fn_SetToFixed(resultValues2[7], freePoint));
            $('#tdResult09').text(fn_SetToFixed(resultValues2[8], freePoint + 1));
            $('#tdResult10').text(fn_SetToFixed(resultValues2[10], freePoint));
            $('#tdResult11').text(fn_SetToFixed(resultValues2[9], freePoint));
            $('#tdResult12').text(fn_SetToFixed(resultValues2[11], 0));
            $('#tdResult13').text(fn_SetToFixed(resultValues2[12] == '' ? '0' : resultValues2[12], 2));
            $('#tdResult14').text(fn_SetToFixed(resultValues2[13] == '' ? '0' : resultValues2[13], 2));
            $('#tdResult15').text(resultValues2[15]);
            $('#tdResult16').text(resultValues2[16]);
            $('#tdResult17').text(fn_SetToFixed(resultValues1[11], freePoint + 1));
            $('#tdResult18').text(fn_SetToFixed(resultValues1[17], freePoint + 1));
            $('#tdResult19').text(fn_SetToFixed(resultValues1[12], freePoint + 1));
            $('#tdResult20').text(fn_SetToFixed(resultValues1[18], freePoint + 1));
            $('#tdResult21').text(fn_SetToFixed(resultValues1[10], freePoint + 1));
            $('#tdResult22').text(fn_SetToFixed(resultValues1[16], freePoint + 1));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="txtSERIALNO" ClientInstanceName="txtSERIALNO" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="txtSIRYO" ClientInstanceName="txtSIRYO" runat="server" ClientVisible="false" />
    <dx:ASPxTextBox ID="txtFREEPOINT" ClientInstanceName="txtFREEPOINT" runat="server" ClientVisible="false" Text="0" />
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <div class="col-sm-5 control-label">
                        <div class="form-group">
                            <label class="col-sm-2 control-label">기간</label>
                            <div class="col-sm-10 control-label">
                                <ucCTF:Date ID="ucDate" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">품목</label>
                            <div class="col-sm-10 control-label">
                                <ucCTF:Item ID="ucItem" runat="server" usedModel="true" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">공정</label>
                            <div class="col-sm-10 control-label">
                                <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">항목</label>
                            <div class="col-sm-10 control-label">
                                <ucCTF:InspectionItem ID="ucInspectionItem" runat="server" validateFields="hidUCITEMCD|품목;hidUCWORKPOPCD|공정" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-5 control-label">
                        <div class="form-group">
                            <div class="col-sm-12 control-label" style="padding-right: 0px;">
                                <div style="float: right;">
                                    &nbsp;
                                </div>
                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">기종</label>
                            <div class="col-sm-10 control-label">
                                <ucCTF:Model ID="ucModel" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">구분</label>
                            <div class="col-sm-10 control-label">
                                <ucCTF:InspectionBox ID="ucInspectionBox" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label"></label>
                            <div class="col-sm-10 control-label">
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2 control-label">
                        <div class="form-group">
                            <label class="col-sm-6 control-label">검사규격</label>
                            <div class="col-sm-6 control-label">
                                <dx:ASPxTextBox ID="txtSTANDARD" ClientInstanceName="txtSTANDARD" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-6 control-label">상한규격</label>
                            <div class="col-sm-6 control-label">
                                <dx:ASPxTextBox ID="txtMAX" ClientInstanceName="txtMAX" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-6 control-label">하한규격</label>
                            <div class="col-sm-6 control-label">
                                <dx:ASPxTextBox ID="txtMIN" ClientInstanceName="txtMIN" runat="server" Width="100%" HorizontalAlign="Right">
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
                    <td id="tdLeft" style="padding-right: 5px; vertical-align: top">
                        <table class="tblchartContents" style="width: 100%;">
                            <tr>
                                <td style="vertical-align: top;">
                                    <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="100px"
                                        OnCustomCallback="devChart1_CustomCallback">
                                        <clientsideevents endcallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized1 = false; }" />
                                    </dx:WebChartControl>
                                </td>

                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 400px; padding-left: 5px; vertical-align: top">
                        <table class="tblchartContents" style="width: 100%; vertical-align: top">
                            <tr>
                                <td style="height: 100%; padding-top: 1px; vertical-align: top">
                                    <section class="panel panel-default" style="margin-bottom: 0px; height: 100%">
                                        <header class="panel-heading text-center font-bold" style="border: solid 1px #9F9F9F; background-color: #F9F9F9;">분석결과</header>
                                        <div class="table-responsive">
                                            <table class="table table-striped b-t b-light" style="border: solid 1px #9F9F9F;">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">최대치</td>
                                                        <td id="tdResult01" style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">최소치</td>
                                                        <td id="tdResult02" style="width: 25%; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">X평균</td>
                                                        <td id="tdResult03" style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">범위</td>
                                                        <td id="tdResult04" style="width: 25%; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">Cp</td>
                                                        <td id="tdResult05" style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">Cpk</td>
                                                        <td id="tdResult06" style="width: 25%; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">6시그마(장)</td>
                                                        <td id="tdResult07" style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">6시그마(단)</td>
                                                        <td id="tdResult08" style="width: 25%; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">표준편차</td>
                                                        <td id="tdResult09" style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">예상부적합률</td>
                                                        <td id="tdResult10" style="width: 25%; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">예상수율(%)</td>
                                                        <td id="tdResult11" style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">예상PPM</td>
                                                        <td id="tdResult12" style="width: 25%; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">상한추정불량</td>
                                                        <td id="tdResult13" style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">하한추정불량</td>
                                                        <td id="tdResult14" style="width: 25%; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">검사횟수</td>
                                                        <td id="tdResult15" style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">규격이탈수</td>
                                                        <td id="tdResult16" style="width: 25%; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">UCLX(목표)</td>
                                                        <td id="tdResult17" style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">UCLX</td>
                                                        <td id="tdResult18" style="width: 25%; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">LCLX(목표)</td>
                                                        <td id="tdResult19" style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">LCLX</td>
                                                        <td id="tdResult20" style="width: 25%; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">UCLR(목표)</td>
                                                        <td id="tdResult21" style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                        <td style="width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;" class="text-center">UCLR</td>
                                                        <td id="tdResult22" style="width: 25%; padding-left: 3px; padding-right: 3px;" class="text-right m-r-xs"></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </section>
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
