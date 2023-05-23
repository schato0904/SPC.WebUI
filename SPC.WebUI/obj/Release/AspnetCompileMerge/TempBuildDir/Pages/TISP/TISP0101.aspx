<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="TISP0101.aspx.cs" Inherits="SPC.WebUI.Pages.TISP.TISP0101" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;
        var chartResized1 = false;        

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
            devGrid.SetWidth(parseInt($(".search").width() - 5, 10));
            devGrid.SetHeight(height-200);

            chartWidth = parseInt($(".search").width() - 5, 10);
            chartHeight1 = 200;

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnImgChartResized(devImage1, devCallbackPanel1, chartResized1, chartWidth, chartHeight1, -1);
        });

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');

            <%if (!gsVENDOR){%>
                if (hidCOMPCD.GetValue() == "" || hidCOMPCD.GetValue() == null) {
                    alert("업체를 선택하세요!!");
                    return false;
                }
                if (hidFACTCD.GetValue() == "" || hidFACTCD.GetValue() == null) {
                    alert("공장을 선택하세요!!");
                    return false;
                }
            <%}%>

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
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // 차트 이미지 이벤트
        function fn_OnImgChartResized(chartObj, callbackObj, chartResized, _width, _height, visibleIndex) {
            if (!chartResized) {
                
                if (parseInt(visibleIndex, 10) >= 0) {
                    fn_OnImgChartDoCallback(chartObj, callbackObj, _width, _height, visibleIndex);
                }
                chartObj.SetWidth(chartWidth);
                chartObj.SetHeight(chartHeight1);
            }
        }

        function fn_OnImgChartDoCallback(chartObj, callbackObj, _width, _height, visibleIndex) {
            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(visibleIndex));
            var oParams = _width + '|' + _height + '||' + rowKeys;
            callbackObj.PerformCallback(oParams);
            chartObj.SetWidth(chartWidth);
            chartObj.SetHeight(chartHeight1);
        }

        function fn_RowDbClick(s, e) {
            var rowKeys = fn_OnRowKeysNullValueToEmpty(devGrid.GetRowKey(e.visibleIndex));
            var KeyField = fn_GetCastText('hidCOMPCD') + '|' + fn_GetCastText('hidFACTCD') + '|' + hidUCFROMDT.GetText() + '|' + hidUCFROMDT.GetText() + '|' + rowKeys;

            fn_OnPopupEQMonitoring(KeyField);
        }

        function fn_OnCustomButtonClick(s, e) {
            devImage1.SetImageUrl('<%#Page.ResolveUrl("~/Resources/images/blank.png")%>');
            fn_OnImgChartResized(devImage1, devCallbackPanel1, chartResized1, chartWidth, chartHeight1, e.visibleIndex);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group" style="display:<%if (!gsVENDOR){%>display<%} else { %>none<%} %>">
                    <label class="col-sm-1 control-label">업체</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Comp ID="ucComp" runat="server" targetCtrls="ddlFACT" masterChk="0" nullText="선택" />
                    </div>
                    <label class="col-sm-1 control-label">공장</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Fact ID="ucFact" runat="server" nullText="선택" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate" DateTimeOnly="false"  SingleDate="true" />
                    </div>
                     <label class="col-sm-1 control-label">반</label>
                     <div class="col-sm-2 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" />
                     </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" machGubun="3" />
                    </div>                 
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" machGubun="3" />
                    </div>                    
                </div>
            </div>
        </div>
        <div class="content">
            <table>
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_ITEMCD;F_WORKCD;F_SERIALNO;F_SIRYO" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" >
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto"  />
                            <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"  AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                                RowDblClick="fn_RowDbClick" CustomButtonClick="fn_OnCustomButtonClick" />
                            <Templates>
                                <StatusBar>
                                    <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                                </StatusBar>
                            </Templates>
                            <Columns>                    
                                <dx:GridViewCommandColumn Caption="차트보기" Width="70px"  >
                                    <CustomButtons>                            
                                        <dx:GridViewCommandColumnCustomButton ID="조회" />
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="F_WORKTIME"  Caption="최종시간" Width="70px" />
                                <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="200px">
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_ITEMCD"  Caption="품목코드" Width="150px" />
                                <dx:GridViewDataColumn FieldName="F_ITEMNM"  Caption="품목명" Width="200px" >
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_INSPDETAIL"  Caption="검사항목" Width="120px" >
                                    <CellStyle HorizontalAlign="Left" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_STANDARD" Caption="검사기준" >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한" >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한" >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_UCLX" Caption="UCLX" >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_LCLX" Caption="LCLX" >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_UCLR" Caption="UCLR" >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_SIRYO" Caption="시료수" Visible="false">
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_CNT" Caption="생산수" >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_OVER" Caption="규격이탈" >
                                    <CellStyle HorizontalAlign="Right" />
                                </dx:GridViewDataColumn>
                                
                                <dx:GridViewDataColumn FieldName="F_WORKCD" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_SERIALNO" Visible="false" />
                            </Columns>                
                        </dx:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top:5px">
                        <dx:ASPxCallbackPanel ID="devCallbackPanel1" runat="server" ClientInstanceName="devCallbackPanel1"
                            OnCallback="devCallbackPanel1_Callback">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <dx:ASPxImage ID="devImage1" ClientInstanceName="devImage1" runat="server">
                                        <Border BorderWidth="0.5px" BorderColor="Gray" BorderStyle="Solid" />
                                    </dx:ASPxImage>
                                </dx:PanelContent>
                            </PanelCollection>
                            <ClientSideEvents CallbackError="fn_OnCallbackError" EndCallback="function(s, e) { chartResized1 = false; }" BeginCallback="function(s, e) { chartResized1 = true; }" />
                        </dx:ASPxCallbackPanel>
                    </td>
                </tr>
            </table>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>
