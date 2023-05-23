<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WSTA0109.aspx.cs" Inherits="SPC.WebUI.Pages.WSTA.WSTA0109" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .header1 {
            display: none;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {

        });

        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;
        var chartResized1 = false;

        $(document).ready(function () {

        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            $(".tblContents").height(height);
            devGrid.SetWidth(parseInt($(".search").width() - 1, 10));
            devGrid.SetHeight(250);

            chartWidth = parseInt($(".search").width() - 1, 10);
            chartHeight1 = parseInt((height - 300), 10);

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
            fn_doSetGridEventAction('true');

            <%if (!gsVENDOR)
              {%>
            if (hidCOMPCD.GetValue() == "" || hidCOMPCD.GetValue() == null) {
                alert("업체를 선택하세요!!");
                return false;
            }
            if (hidFACTCD.GetValue() == "" || hidFACTCD.GetValue() == null) {
                alert("공장을 선택하세요!!");
                return false;
            }
           <%}%>

            if (hidUCITEMCD.GetValue() == "" || hidUCITEMCD.GetValue() == null) {
                alert("품목 코드를 입력하세요.");
                return false;
            }

            if (hidUCWORKPOPCD.GetValue() == "" || hidUCWORKPOPCD.GetValue() == null) {
                alert("공정코드를 입력하세요.");
                return false;
            }

            if (hidUCINSPITEMCD.GetValue() == "" || hidUCINSPITEMCD.GetValue() == null) {
                alert("검사항목을 입력하세요.");
                return false;
            }

            devGrid.PerformCallback();

        }

        // 입력
        function fn_OnNewClick() {
            devGrid.AddNewRow();
        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('수정할 데이타를 선택하세요!!');
                return false;
            }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.StartEditRowByKey(selectedKeys[i]);
            }
        }

        // 저장
        function fn_OnSaveClick() {
            if (!devGrid.batchEditApi.HasChanges())
                alert('변경된 사항이 없습니다');
            else
                devGrid.UpdateEdit();
        }

        // 취소
        function fn_OnCancelClick() {
            devGrid.UnselectAllRowsOnPage();
            devGrid.CancelEdit();
        }

        // 삭제
        function fn_OnDeleteClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('삭제할 데이타를 선택하세요!!');
                return false;
            }

            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제후 반드시 저장버튼을 눌러야 삭제가 완료됩니다.')) { return false; }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.DeleteRowByKey(selectedKeys[i]);
            }
        }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() {            
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
            } else {
                fn_OnChartDoCallback(devChart1, chartWidth, chartHeight1);
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
            fn_OnBatchValidate("F_BANCD", s, e);
            fn_OnBatchValidate("F_BANNM", s, e);

            //if (parseInt(e.visibleIndex, 10) >= 0) {
            //    fn_OnBatchValidate("F_SORTNO", s, e);
            //}
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                var editor = s.GetEditor('F_BANCD');
                fn_OnControlDisableBox(editor, null);
            } else {
                var editor = s.GetEditor('F_BANCD');
                fn_OnControlEnableBox(editor, null);
            }
        }

        function fn_OnSetQCD34Values(resultValues) {
            // 검사항목
            fn_OnUCSettingInspectionItem(resultValues);
            txtSERIALNO.SetValue(resultValues[10]);
            // 검사규격
            txtSTANDARD.SetValue(resultValues[4]);
            txtMAX.SetValue(resultValues[5]);
            txtMIN.SetValue(resultValues[6]);

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
        function fn_RowClick(s, e) {
            //alert(e.visibleIndex); chartWidth, chartHeight1
            var oParams = chartWidth + '|' + chartHeight1 + '|' + e.visibleIndex
            devChart1.PerformCallback(oParams);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group" style="display: <%if (!gsVENDOR)
                                                          {%>display<%}
                                                          else
                                                          { %>none<%} %>">
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
                    <label class="col-sm-1 control-label">분석기간</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" ID="ucDate"/>
                    </div>
                    <label class="col-sm-1 control-label">품목</label>
                    <div class="col-sm-3">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </div>
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-1 control-label">검사항목</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:InspectionItem ID="ucInspectionItem" runat="server" validateFields="hidUCITEMCD|품목;hidUCWORKPOPCD|공정" />
                        <dx:ASPxTextBox ID="txtSERIALNO" ClientInstanceName="txtSERIALNO" runat="server" ClientVisible="false" />
                    </div>
                    <div class="col-sm-1 control-label">규격</div>
                    <div class="col-sm-7 control-label">
                        <table>
                            <tr>
                                <td class="control-label">
                                    <dx:ASPxTextBox ID="txtSTANDARD" ClientInstanceName="txtSTANDARD" runat="server" Width="100%" HorizontalAlign="Right">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td>&nbsp;MAX &nbsp;</td>
                                <td class="control-label">
                                    <dx:ASPxTextBox ID="txtMAX" ClientInstanceName="txtMAX" runat="server" Width="100%" HorizontalAlign="Right">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td>&nbsp;MIN &nbsp;</td>
                                <td class="control-label">
                                    <dx:ASPxTextBox ID="txtMIN" ClientInstanceName="txtMIN" runat="server" Width="100%" HorizontalAlign="Right">
                                        <ClientSideEvents Init="fn_OnControlDisableBox" />
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <table class="tblContents" style="width: 100%;">
                <tr>
                    <td style="vertical-align: top; padding-top: 10px;">
                        <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                            ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="100px"
                            OnCustomCallback="devChart1_CustomCallback">
                            <clientsideevents endcallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized1 = true; }" />
                        </dx:WebChartControl>
                    </td>
                </tr>                
                <tr>
                    <td style="height: 290px; padding-top: 10px;">
                        <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                        <p>하단 그리드를 클릭하시면 해당하는 항목의 그래프를 볼 수 있습니다.</p>
                        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="true" Width="100%"
                            KeyFieldName="F_ITEMCD;F_ITEMNM" EnableViewState="false" EnableRowsCache="false"
                            OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                            OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnHtmlRowPrepared="devGrid_HtmlRowPrepared">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Auto" VerticalScrollableHeight="10" ShowStatusBar="Hidden" HorizontalScrollBarMode="Auto" />
                            <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" RowClick="fn_RowClick" />
                            <Templates>
                                <StatusBar>
                                    <ucCTF:Count ID="ucCount" runat="server" targetCtrl="devGrid" />
                                </StatusBar>
                            </Templates>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>                        
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
