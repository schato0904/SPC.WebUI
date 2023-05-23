<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="TISP01POP_NEW.aspx.cs" Inherits="SPC.WebUI.Pages.TISP.TISP01POP_NEW" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .td-first {
            width: 25%; border-right: 1px solid #dcdcdc; padding-left: 3px; padding-right: 3px;
        }
        .td-last {
            width: 25%; padding-left: 3px; padding-right: 3px;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;
        var chartResized1 = false;
        var bSearch = false;
        var isCallback = 0;

        $(document).ready(function () {
            // 모니터링에서 넘어온 경우 조회시작
            var oSetParam = "<%=oSetParam%>";
            if (oSetParam != '') {
                var oSetParams = oSetParam.split('|');//F_ITEMCD|F_ITEMNM|F_WORKCD|F_WORKNM|F_MEAINSPCD|F_INSPDETAIL|F_SERIALNO|F_SIRYO|F_FREEPOINT|F_STANDARD|F_MAX|F_MIN
                // 검색일
                fn_SetTextValue('hidUCFROMDT', oSetParams[0]);
                fn_SetDate('txtFROMDT', convertDateString(oSetParams[0]));
                fn_SetTextValue('hidUCTODT', oSetParams[1]);
                fn_SetDate('txtTODT', convertDateString(oSetParams[1]));
                //품목
                fn_SetTextValue('hidUCITEMCD', oSetParams[2]);
                fn_SetTextValue('txtUCITEMCD', oSetParams[2]);
                fn_SetTextValue('txtUCITEMNM', oSetParams[3]);
                //공정
                fn_SetTextValue('hidUCWORKPOPCD', oSetParams[4]);
                fn_SetTextValue('txtUCWORKPOPCD', oSetParams[4]);
                fn_SetTextValue('txtUCWORKNM', oSetParams[5]);
                //검사항목
                fn_SetTextValue('hidUCINSPITEMCD', oSetParams[6]);
                fn_SetTextValue('txtUCINSPITEMCD', oSetParams[6]);
                fn_SetTextValue('txtUCINSPITEMNM', oSetParams[7]);
                fn_SetTextValue('txtSERIALNO', oSetParams[8]);
                fn_SetTextValue('txtSIRYO', oSetParams[9]);
                fn_SetTextValue('txtFREEPOINT', oSetParams[10]);
                //규격
                fn_SetTextValue('txtSTANDARD', fn_SetToFixed(oSetParams[11], oSetParams[10]));
                fn_SetTextValue('txtMAX', fn_SetToFixed(oSetParams[12], oSetParams[10]));
                fn_SetTextValue('txtMIN', fn_SetToFixed(oSetParams[13], oSetParams[10]));
                //구분
                fn_SetTextValue('hidUCINSPECTIONCD', 'AAC501');
                fn_SetTextValue('txtUCINSPECTIONNM', '<%=GetCommonCodeText("AAC501")%>');
            }
        });

        // 공장 콜백 체크
        function fn_OnFactCallbackEnd(val) {
            if (!isFactCallbackEnd) {
                timerFACT = setTimeout(function () { fn_OnFactCallbackEnd(val); }, 1000);
            } else {
                clearTimeout(timerFACT);
                timerFACT = null;
                isFactCallbackEnd = false;

                devGrid.PerformCallback();
            }
        }

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            $(".tblContents").height(height);

            chartWidth = parseInt($(".search").width() - 5, 10);
            chartHeight1 = parseInt(height - 40);

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnImgChartResized(devImage1, devCallbackPanel1, chartResized1, chartWidth, chartHeight1);
        });

        // 조회
        function fn_OnSearchClick() {
            if (!fn_ValidateITEM()) { return; };
            if (!fn_ValidateWORK()) { return; };
            if (!fn_ValidateINSPITEM()) { return; };

            bSearch = false;
            isCallback = 1;

            fn_OnImgChartDoCallback(devImage1, devCallbackPanel1, chartWidth, chartHeight1);
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

            fn_OnImgChartDoCallback(devImage1, devCallbackPanel1, chartWidth, chartHeight1);
        }

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
            } else {
                chartResized1 = false;
                bSearch = true;
                isCallback = 0;
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
            fn_OnPopupTotalMeasurement(keys);
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
            // 관리규격
            txtUCLX.SetText(resultValues[7]);
            txtLCLX.SetText(resultValues[8]);
            txtUCLR.SetText(resultValues[9]);
            // 기타
            txtSERIALNO.SetText(resultValues[10]);
            txtSERIALNO.SetValue(resultValues[10]);
            txtSIRYO.SetText(resultValues[11]);
            txtSIRYO.SetValue(resultValues[11]);
            txtFREEPOINT.SetText(resultValues[12]);
            txtFREEPOINT.SetValue(resultValues[12]);
        }

        // 차트 이미지 이벤트
        function fn_OnImgChartResized(chartObj, callbackObj, chartResized, _width, _height) {
            if (!chartResized) {
                fn_OnImgChartDoCallback(chartObj, callbackObj, _width, _height);
                chartObj.SetWidth(chartWidth);
                chartObj.SetHeight(chartHeight1);
            }
        }

        function fn_OnImgChartDoCallback(chartObj, callbackObj, _width, _height) {
            var oParams = _width + '|' + _height + '|' + isCallback;
            callbackObj.PerformCallback(oParams);
            chartObj.SetWidth(chartWidth);
            chartObj.SetHeight(chartHeight1);
        }

        // 품질이상제기 등록
        function fn_OnPopupQulityResponse() {
            fn_OnPopupIPCM0101(
                fn_GetCastText('hidCOMPCD'),
                fn_GetCastText('hidFACTCD'),
                fn_GetCastText('txtUCITEMCD'),
                fn_GetCastText('txtUCITEMNM'),
                fn_GetCastText('txtUCWORKPOPCD'),
                fn_GetCastText('txtUCWORKNM'),
                fn_GetCastText('txtUCINSPITEMCD'),
                fn_GetCastText('txtUCINSPITEMNM'),
                fn_GetCastText('txtFROMDT'),
                fn_GetCastText('txtTODT')
                );
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
                <div class="form-group" style="display: <%if (!gsVENDOR){%>display<%} else { %>none<%} %>">
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
                                <ucCTF:Date runat="server" id="ucDate" DateTimeOnly="false" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">품목</label>
                            <div class="col-sm-8 control-label">
                                <ucCTF:Item ID="ucItem" runat="server" usedModel="true" machGubun="3" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">공정</label>
                            <div class="col-sm-8 control-label">
                                <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" machGubun="3" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">항목</label>
                            <div class="col-sm-8 control-label">
                                <ucCTF:InspectionItem ID="ucInspectionItem" runat="server" validateFields="hidUCITEMCD|품목;hidUCWORKPOPCD|공정" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 control-label">
                        <div class="form-group">
                            <div class="col-sm-8 control-label" style="padding-right: 0px;">
                                <div style="float:right;">
                                    <dx:ASPxRadioButtonList ID="chk_calc" ClientInstanceName="chk_calc" runat="server"
                                        RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                                        <Items>
                                            <dx:ListEditItem Value="0" Text="관리용" />
                                            <dx:ListEditItem Value="1" Text="계산식" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </div>
                            </div>
                            <label class="col-sm-4 control-label">
                                <button class="btn btn-sm btn-danger" onclick="return false;">
                                    <i class="fa fa-copy"></i>
                                    <span class="text">관리한계이력</span>
                                </button>
                            </label>
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
                            <label class="col-sm-4 control-label"></label>
                            <div class="col-sm-4 control-label">
                                <dx:ASPxCheckBox ID="chk_reject" ClientInstanceName="chk_reject" runat="server" Text="규격이탈제외" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 control-label">
                        <div class="form-group">
                            <label class="col-sm-3 control-label"><input type="button" style="visibility:hidden;" /></label>
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
                                <td id="tdDesc" runat="server" style="height: 20px; padding-bottom:10px; text-align: right;">
                                    <button class="btn btn-sm btn-danger" onclick="fn_OnRowDblClick(); return false;">
                                        <i class="i i-layer"></i>
                                        <span class="text">전체측정값보기</span>
                                    </button>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top;">
                                    <dx:ASPxCallbackPanel ID="devCallbackPanel1" runat="server" ClientInstanceName="devCallbackPanel1"
                                        OnCallback="devCallbackPanel1_Callback">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent1" runat="server">
                                                <dx:ASPxImage ID="devImage1" ClientInstanceName="devImage1" runat="server" ShowLoadingImage="true">
                                                    <Border BorderWidth="0.5px" BorderColor="Gray" BorderStyle="Solid" />
                                                </dx:ASPxImage>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                        <ClientSideEvents CallbackError="fn_OnCallbackError" EndCallback="fn_OnEndCallback" BeginCallback="function(s, e) { chartResized1 = true; }" />
                                    </dx:ASPxCallbackPanel>
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