<%@ Page Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD_DACO6001.aspx.cs" Inherits="SPC.WebUI.Pages.WERDDACO.WERD_DACO6001" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;
        var chartHeight2 = 0;
        var chartResized1 = false;
        var chartResized2 = false;

        $(document).ready(function () {

        });


        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));

            $(".tblContents").height(height);

            chartWidth = parseInt(($(".search").width()), 10);
            chartWidth2 = parseInt(($(".search").width()) / 2, 10);

            $(".tdChart1").width(chartWidth);
            $(".tdChart2").width(chartWidth2);
            chartHeight1 = parseInt((height - 10) / 2, 10);

            if (chartHeight1 < 200) {
                chartHeight1 = 200;
            }
            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartResized(devChart1, chartResized1, chartWidth, chartHeight1);
        });

        // 차트 이벤트
        function fn_OnChartResized(chartObj, chartResized, _width, _height) {
            if (!chartResized) {
                fn_OnChartDoCallback(chartObj, _width, _height);
                fn_OnChartDoCallback(devChart2, _width, _height);
                fn_OnChartDoCallback(devChart3, _width, _height);
            }
        }

        function fn_OnChartDoCallback(chartObj, _width, _height) {
            var oParams = _width + '|' + _height;
            chartObj.PerformCallback(oParams);
        }

        // 조회
        function fn_OnSearchClick() {
            //fn_doSetGridEventAction('true');
            var oParams = chartWidth + '|' + chartHeight1
            devChart1.PerformCallback(oParams);
        }

        // 입력
        function fn_OnNewClick() {

        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

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
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {

        }

        function fn_OnLINEValueChanged(s, e) {
            var val = s.GetValue();
            hidCOMP.SetValue(val);
            LINEPerFormCallbackTarget();
        }
        function fn_OnLINEEndCallback(s, e) {
            isLINEEndCallback = parent.parent.isTreeLINESetup;
        }

        function fn_OnChartEndCallback(s, e) {
            if (s.cpFunction != '' && s.cpFunction == 'resizeTo') {
                s.GetMainElement().style.width = s.cpChartWidth + 'px';
            }
        }

        function onObjectSelected(s, e) {
            if (e.hitInfo.inSeriesPoint) {
                //alert(e.hitInfo.seriesPoint.argument + "(" + e.hitInfo.seriesPoint.values[0] + "M km²)");
                //alert(e.hitInfo.seriesPoint.argument);
                var arg = e.hitInfo.seriesPoint.argument;
                var oParams = chartWidth + '|' + chartHeight1 + '|' + arg;

                devChart2.PerformCallback(oParams);
                devChart3.PerformCallback(oParams);
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">검색일자</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" ID="ucDate" DateTimeOnly="false" />
                    </div>
                    <label class="col-sm-2">※ 협력사 클릭시 점유율이 조회됩니다.</label>                    
                </div>
            </div>
        </div>
        <div class="content">
            <table class="tblContents" style="width: 100%;" border="0">
                <tr>
                    <td class="tdChart1" colspan="3" style="vertical-align: top;">
                        <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                            ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="False" Height="100px"
                            OnCustomCallback="devChart1_CustomCallback" CrosshairEnabled="True" Width="300px">
                            <clientsideevents endcallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized1 = true; }" objectselected="onObjectSelected" />
                        </dx:WebChartControl>
                    </td>

                </tr>
                <tr>
                    <td class="tdChart2" colspan="1" style="vertical-align: top;">
                        <dx:WebChartControl ID="devChart2" ClientInstanceName="devChart2" runat="server"
                            ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="False" Height="100px"
                            OnCustomCallback="devChart2_CustomCallback" CrosshairEnabled="True" Width="300px">
                            <clientsideevents endcallback="function(s, e) { chartResized2 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized2 = true; }" />
                        </dx:WebChartControl>
                    </td>
                    <td colspan="1" style="width: 10px;"></td>
                    <td class="tdChart2" colspan="1" style="vertical-align: top;">
                        <dx:WebChartControl ID="devChart3" ClientInstanceName="devChart3" runat="server"
                            ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="False" Height="100px"
                            OnCustomCallback="devChart3_CustomCallback" CrosshairEnabled="True" Width="300px">
                            <clientsideevents endcallback="function(s, e) { chartResized2 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized2 = true; }" />
                        </dx:WebChartControl>                        
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
