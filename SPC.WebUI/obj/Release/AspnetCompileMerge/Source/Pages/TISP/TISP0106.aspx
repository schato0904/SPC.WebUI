<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="TISP0106.aspx.cs" Inherits="SPC.WebUI.Pages.TISP.TISP0106" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;
        var chartResized1 = false;
        var isCallback = 0;

        $(document).ready(function () {
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            
            chartWidth = parseInt($(".search").width() - 5, 10);
            chartHeight1 = parseInt(height + 20);

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

            isCallback = 1;

            fn_SetTextValue('txtTCNT', '');
            fn_SetTextValue('txtNCNT', '');
            fn_SetTextValue('txtNGRATE', '');

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
            //fn_OnSetInspectionItem(resultValues);

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
        }

        // 차트 이미지 이벤트
        function fn_OnChartResized(chartObj, chartResized, _width, _height) {
            if (!chartResized) {
                fn_OnChartDoCallback(chartObj, _width, _height);
            }
        }

        function fn_OnChartDoCallback(chartObj, _width, _height) {
            var oParams = _width + '|' + _height + '|' + isCallback;
            chartObj.PerformCallback(oParams);
        }

        function fn_OnChartEndCallback(s, e) {
            if (s.cpFunction != '' && s.cpFunction == 'resizeTo') {
                s.GetMainElement().style.width = s.cpChartWidth + 'px';
            } else if (s.cpFunction != '' && s.cpFunction == 'setResult') {
                var values = s.cpChartWidth.split('|');
                if (values.length == 3) {
                    fn_SetTextValue('txtTCNT', values[0]);
                    fn_SetTextValue('txtNCNT', values[1]);
                    fn_SetTextValue('txtNGRATE', values[2]);
                }
            } else {
                alert(s.cpChartWidth);
            }

            isCallback = 0;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <dx:ASPxTextBox ID="txtSERIALNO" ClientInstanceName="txtSERIALNO" runat="server" ClientVisible="false" />
    <div class="container">
        <div class="search">
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
                            <label class="col-sm-4 control-label">구간</label>
                            <div class="col-sm-4 control-label">
                                <dx:ASPxComboBox ID="ddlTYPE" ClientInstanceName="ddlTYPE" runat="server" Width="100%">                            
                                    <Items>
                                        <dx:ListEditItem Text="1시간" Value="1" Selected="true" />
                                        <dx:ListEditItem Text="2시간" Value="2" />
                                        <dx:ListEditItem Text="3시간" Value="3" />
                                        <dx:ListEditItem Text="4시간" Value="4" />
                                        <dx:ListEditItem Text="6시간" Value="6" />
                                        <dx:ListEditItem Text="8시간" Value="8" />
                                        <dx:ListEditItem Text="주/야" Value="12" />
                                        <dx:ListEditItem Text="1일" Value="24" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">시작시간</label>
                            <div class="col-sm-7 control-label">
                                <dx:ASPxRadioButtonList ID="rdoSTHOUR" ClientInstanceName="rdoSTHOUR" runat="server"
                                    RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                                    <Items>
                                        <dx:ListEditItem Value="8" Text="오전8시(~익일07:59:59)" Selected="true" />
                                        <dx:ListEditItem Value="0" Text="자정" />
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </div>
                            <label class="col-sm-1 control-label"><input type="button" style="visibility:hidden;" /></label>
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
                                <dx:ASPxCheckBox ID="chkOnlyNG" ClientInstanceName="chkOnlyNG" runat="server" Text="불량 있는 경우만 보기" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 control-label">
                        <div class="form-group" style="padding-top:8px;">
                            <label class="col-sm-3 control-label"></label>
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
                        <div class="form-group" style="padding-top:8px;">
                            <label class="col-sm-3 control-label"></label>
                            <label class="col-sm-3 control-label">총검사수</label>
                            <label class="col-sm-3 control-label">총불량수</label>
                            <label class="col-sm-3 control-label">총불량율</label>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">조회결과</label>
                            <div class="col-sm-3 control-label">
                                <dx:ASPxTextBox ID="txtTCNT" ClientInstanceName="txtTCNT" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                            <div class="col-sm-3 control-label">
                                <dx:ASPxTextBox ID="txtNCNT" ClientInstanceName="txtNCNT" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                            <div class="col-sm-3 control-label">
                                <dx:ASPxTextBox ID="txtNGRATE" ClientInstanceName="txtNGRATE" runat="server" Width="100%" HorizontalAlign="Right">
                                    <ClientSideEvents Init="fn_OnControlDisableBox" />
                                </dx:ASPxTextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="False" Height="10px"
                OnCustomCallback="devChart1_CustomCallback" CrosshairEnabled="True" Width="300px">
                <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" BeginCallback="function(s, e) { chartResized1 = true; }" />
            </dx:WebChartControl>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>