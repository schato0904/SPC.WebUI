<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ANLS0101_WORK.aspx.cs" Inherits="SPC.WebUI.Pages.ANLS.ANLS0101_WORK" %>
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
        var chartHeight2 = 0;
        var chartResized1 = false;
        var chartResized2 = false;
        var chartResized3 = false;
        var chartResized4 = false;
        var bSearch = false;

        $(document).ready(function () {            
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));

            $(".content").height(height);

            if (height < 550) {
                $(".content").css("overflow-y", "auto").css("overflow-x", "hidden");
            }

            var width = height < 550 ? 400 + scrollbarWidthAny() : 400;
            height = height < 550 ? 550 : height;

            $(".tblContents").height(height);

            chartWidth = parseInt(($(".search").width() - 5)/2, 10);
            chartHeight1 = parseInt((height-20) / 2, 10);

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartResized(devChart1, chartResized1, chartWidth, chartHeight1);
            fn_OnChartResized(devChart2, chartResized2, chartWidth, chartHeight1);
            fn_OnChartResized(devChart3, chartResized3, chartWidth, chartHeight1);
            fn_OnChartResized(devChart4, chartResized4, chartWidth, chartHeight1);
        });

        // 조회
        function fn_OnSearchClick() {
           

            if (!fn_ValidateITEM()) { return; };
            if (!fn_ValidateWORK()) { return; };
            

            bSearch = false;

            devCallback.PerformCallback();

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
        function fn_OndevCallback(s, e) {

            if (s.cpResultCode != '') {
                if (s.cpResultCode == 'pager') {
                    // 페이저 Callback
                    fn_pagerPerformCallback(s.cpResultMsg);

                    bSearch = true;

                    fn_OnChartDoCallback(devChart1, chartWidth, chartHeight1);
                    fn_OnChartDoCallback(devChart2, chartWidth, chartHeight1);
                    fn_OnChartDoCallback(devChart3, chartWidth, chartHeight1);
                    fn_OnChartDoCallback(devChart4, chartWidth, chartHeight1);

                } else {
                    alert(s.cpResultMsg);
                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            } else {
                
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

        // Set QCD34
        function fn_OnSetQCD34Values(resultValues) {
            // 검사항목
            fn_OnUCSettingInspectionItem(resultValues);
            // 검사유형
            fn_OnSetInspectionItem(resultValues);

            // 기타
            txtSERIALNO.SetText(resultValues[10]);
            txtSERIALNO.SetValue(resultValues[10]);
            txtSIRYO.SetText(resultValues[11]);
            txtSIRYO.SetValue(resultValues[11]);
            txtFREEPOINT.SetText(resultValues[12]);
            txtFREEPOINT.SetValue(resultValues[12]);
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
                        
                    </div>
                    <div class="col-sm-4 control-label">                        
                        <div class="form-group">
                            <div class="col-sm-8 control-label">&nbsp;</div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 control-label">공정</label>
                            <div class="col-sm-8 control-label">
                                <ucCTF:WorkPOP ID="ucWorkPOP" runat="server" />
                            </div>
                        </div>          
                    </div>                    
                    <div class="col-sm-4 control-label">
                        <div class="form-group">
                            <div class="col-sm-8 control-label">&nbsp;</div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">기종</label>
                            <div class="col-sm-3 control-label">
                                <ucCTF:Model ID="ucModel" runat="server" />
                            </div>
                            <div class="col-sm-6 control-label">
                                <dx:ASPxCheckBox ID="chk_reject" ClientInstanceName="chk_reject" runat="server" Text="규격이탈제외" Checked="true" />
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
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="False" Height="10px"
                                        OnCustomCallback="devChart1_CustomCallback" CrosshairEnabled="True" Width="300px">
                                        <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" BeginCallback="function(s, e) { chartResized1 = true; }" />
                                    </dx:WebChartControl>
                                </td>
                                <td style="vertical-align: top;padding-left:10px">
                                    <dx:WebChartControl ID="devChart2" ClientInstanceName="devChart2" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="10px"
                                        OnCustomCallback="devChart2_CustomCallback">
                                        <ClientSideEvents EndCallback="function(s, e) { chartResized2 = false; fn_OnChartEndCallback(s, e); }" BeginCallback="function(s, e) { chartResized2 = true; }" />
                                    </dx:WebChartControl>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top;">
                                    <dx:WebChartControl ID="devChart3" ClientInstanceName="devChart3" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="False" Height="10px"
                                        OnCustomCallback="devChart3_CustomCallback" CrosshairEnabled="True" Width="300px">
                                        <ClientSideEvents EndCallback="function(s, e) { chartResized3 = false; fn_OnChartEndCallback(s, e); }" BeginCallback="function(s, e) { chartResized3 = true; }" />
                                    </dx:WebChartControl>
                                </td>
                                <td style="vertical-align: top;padding-left:10px">
                                    <dx:WebChartControl ID="devChart4" ClientInstanceName="devChart4" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="10px"
                                        OnCustomCallback="devchart44_CustomCallback">
                                        <ClientSideEvents EndCallback="function(s, e) { chartResized4 = false; fn_OnChartEndCallback(s, e); }" BeginCallback="function(s, e) { chartResized4 = true; }" />
                                    </dx:WebChartControl>
                                </td>
                            </tr>                            
                        </table>
                    </td>                    
                </tr>
            </table>
        </div>
        <div class="paging">
            <ucCTF:PagerChart ID="ucPager" runat="server" PageSize="50" targetCtrls="devCallback" />
        </div>
    </div>
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server" OnCallback="devCallback_Callback">
        <ClientSideEvents EndCallback="fn_OndevCallback" />
    </dx:ASPxCallback>
</asp:Content>
