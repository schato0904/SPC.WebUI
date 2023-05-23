<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="TISP0104.aspx.cs" Inherits="SPC.WebUI.Pages.TISP.TISP0104" %>
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

            chartWidth = parseInt($(".search").width() - 5, 10);
            chartHeight1 = parseInt((height / 2) - 10, 10);

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnImgChartResized(devImage1, devCallbackPanel1, chartResized1, chartWidth, chartHeight1);
            fn_OnImgChartResized(devImage2, devCallbackPanel2, chartResized1, chartWidth, chartHeight1);
        });

        // 조회
        function fn_OnSearchClick() {
            if (!fn_ValidateITEM()) { return;};
            if (!fn_ValidateWORK()) { return; };
            if (!fn_ValidateINSPITEM()) { return; };

            bSearch = false;

            var oParams = chartWidth + '|' + chartHeight1;
            callbackControl.PerformCallback(oParams);
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

            fn_OnPopupTISP0104Report(KEYS);
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
                bSearch = true;

                fn_OnImgChartDoCallback(devImage1, devCallbackPanel1, chartWidth, chartHeight1);
                fn_OnImgChartDoCallback(devImage2, devCallbackPanel2, chartWidth, chartHeight1);
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
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
            var oParams = _width + '|' + _height;
            callbackObj.PerformCallback(oParams);
            chartObj.SetWidth(chartWidth);
            chartObj.SetHeight(chartHeight1);
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
                            <div class="col-sm-8 control-label">
                                <ucCTF:Date ID="ucDate" runat="server"  SingleDate="true" />
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
                            <div class="col-sm-4 control-label">
                                <dx:ASPxTextBox ID="txtSIRYO" ClientInstanceName="txtSIRYO" runat="server"  Width="100%" >
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                                
                            </div>
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
                            &nbsp;
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
                                <td style="vertical-align: top; padding-bottom:5px">
                                    <dx:ASPxCallbackPanel ID="devCallbackPanel1" runat="server" ClientInstanceName="devCallbackPanel1"
                                        OnCallback="devCallbackPanel1_Callback">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent1" runat="server">
                                                <dx:ASPxImage ID="devImage1" ClientInstanceName="devImage1" runat="server">
                                                    <Border BorderWidth="0.5px" BorderColor="Gray" BorderStyle="Solid" />
                                                </dx:ASPxImage>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top;">
                                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel2" runat="server" ClientInstanceName="devCallbackPanel2"
                                        OnCallback="devCallbackPanel2_Callback">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent2" runat="server">
                                                <dx:ASPxImage ID="devImage2" ClientInstanceName="devImage2" runat="server">
                                                    <Border BorderWidth="0.5px" BorderColor="Gray" BorderStyle="Solid" />
                                                </dx:ASPxImage>
                                            </dx:PanelContent>
                                        </PanelCollection>
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
    <!-- CallBack 처리를 위한 객체 -->
    <dx:ASPxCallback ID="callbackControl" ClientInstanceName="callbackControl" runat="server"
        OnCallback="callbackControl_Callback">
        <ClientSideEvents CallbackError="fn_OnCallbackError" EndCallback="function(s, e) {chartResized1 = false; devLoadingPanel.Hide(); fn_OnEndCallback(s, e);}" BeginCallback="function(s, e) { chartResized1 = true; devLoadingPanel.Show(); }" />
    </dx:ASPxCallback>
    <dx:ASPxLoadingPanel ID="devLoadingPanel" ClientInstanceName="devLoadingPanel" runat="server"></dx:ASPxLoadingPanel>
</asp:Content>
