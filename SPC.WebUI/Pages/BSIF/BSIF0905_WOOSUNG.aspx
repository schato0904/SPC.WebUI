<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0905_WOOSUNG.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0905_WOOSUNG" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;
        var chartHeight2 = 0;
        var chartResized1 = false;
        var chartResized2 = false;
        var chartResized3 = false;

        $(document).ready(function () {
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt(fn_GetDocumentHeight() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));

            $(".tblContents").height(height);



            chartWidth = parseInt($(".search").width(), 10);

            $(".tdChart1").width(chartWidth);
            chartHeight1 = parseInt((height - 210), 10);

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

                fn_OnChartDoCallback(chartObj, _width, _height, 'resize');
            }
        }

        function fn_OnChartDoCallback(chartObj, _width, _height, resize) {
            var oParams = _width + '|' + _height + '|' + resize;
            chartObj.PerformCallback(oParams);
        }

        function fn_OnChartEndCallback(s, e) {
            if (s.cpFunction != '' && s.cpFunction == 'resizeTo') {
                s.GetMainElement().style.width = s.cpChartWidth + 'px';
            }
        }

        // 조회
        function fn_OnSearchClick() {


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

            //fn_OnChartDoCallback(devChart1, chartWidth, chartHeight1);

        }



        function fn_CallbackComplete(s, e) {
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            //alert(e.message);
        }




        function WorkerTarget() {
            workerCOMBO.PerformCallback();
        }

        function fn_YearChanged(s, e) {
            WorkerTarget();
        }

    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">

                <div class="form-group">

                    <label class="col-sm-1 control-label">연도</label>


                    <div class="col-sm-1 control-label">
                        <dx:ASPxComboBox ID="yearCOMBO" ClientInstanceName="yearCOMBO" runat="server" OnCallback="yearCOMBO_Callback" IncrementalFilteringMode="None">
                            <ClientSideEvents ValueChanged="fn_YearChanged" Init="fn_OnControlDisable" />
                        </dx:ASPxComboBox>
                    </div>

                    <label class="col-sm-1 control-label"></label>
                    <div class="col-sm-1 control-label">
                    </div>
               

                    <label class="col-sm-1 control-label">작업자</label>


                    <div class="col-sm-1 control-label">

                        <dx:ASPxComboBox ID="workerCOMBO" ClientInstanceName="workerCOMBO" runat="server" OnCallback="workerCOMBO_Callback"
                            IncrementalFilteringMode="None" TextField="F_WORKMAN" ValueField="F_WORKMANCD">
                            <ClientSideEvents Init="fn_OnControlDisable" />
                        </dx:ASPxComboBox>
                    </div>



                </div>

            </div>
        </div>
        <div class="form-group"></div>




        <div class="content">
            <table style="width: 100%;" border="0">
                <tr>
                    <td id="tdLeft">
                        <table class="tblContents">
                            <tr>
                                <td class="tdChart1" style="vertical-align: top;">
                                    <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server" ToolTipEnabled="False"
                                        ViewStateMode="Disabled" EnableViewState="false" OnCustomCallback="devChart1_CustomCallback">
                                        <clientsideevents endcallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized1 = true; }" />
                                    </dx:WebChartControl>



                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>




    </div>






</asp:Content>
