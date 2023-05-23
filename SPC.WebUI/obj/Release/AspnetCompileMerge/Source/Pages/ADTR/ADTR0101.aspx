<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="ADTR0101.aspx.cs" Inherits="SPC.WebUI.Pages.ADTR.ADTR0101" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .oto-style
        {
            width: 100%;
            border-left: 1px solid #C0C0C0;
            border-right: 1px solid #C0C0C0;
            border-top: 1px solid #C0C0C0;
            border-bottom: 1px solid #C0C0C0;
            border-collapse: collapse;
            padding: 4px 4px 4px 4px;
            background-color: #000000;
            font-weight: bold;
            font-size: 16px;
            font-family: Arial, HYHeadLine, HYMyeongJo-Extra;
        }

        .oto-style1
        {
            width: 100%;
            border-left: 1px solid #C0C0C0;
            border-right: 1px solid #C0C0C0;
            border-top: 1px solid #C0C0C0;
            border-bottom: 1px solid #C0C0C0;
            border-collapse: collapse;
            padding: 4px 4px 4px 4px;
            background-color: #000000;
            font-weight: bold;
            font-size: 16px;
            font-family: Arial, HYHeadLine, HYMyeongJo-Extra;
        }

            .oto-style1 td {
                border-left: 1px solid #C0C0C0;
                border-right: 1px solid #C0C0C0;
                border-top: 1px solid #C0C0C0;
                border-bottom: 1px solid #C0C0C0;
                border-collapse: collapse;
                padding-right:2px;
            }

            .oto-style1 thead td {
                text-align:center;                
            }

         .auto-style2
        {
            width: 100%;
            background-color: #000000;
            font-size: 16px;
            color: #ffd800;
            font-family: Arial, HYHeadLine, HYMyeongJo-Extra;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var intervalId;
        var intervalId_1;
        var height;

        intervalId_1 = setInterval("fn_refreshTime()", 1000);

        $(document).ready(function () {
            fn_OnSearchClick();
            //fn_ResetTimer();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            height = Math.max(0, $(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth());

            $(".contentTbl").height(parseInt(height / 2) - 10, 10);
        }

        // 조회
        function fn_OnSearchClick() {
            <%=ucStopwatch.ClientInstanceName%>Init();
            pnlChart.PerformCallback();
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
            fn_AdjustSize()
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        function fn_refreshTime() {            
        }

        function fn_ResetTimer() {


            var localIntervalTerm = intervalTerm * 60000;

            if (intervalId)
                clearInterval(intervalId);

            intervalId = setInterval("fn_Timer()", localIntervalTerm);
        }

        function fn_Timer() {
            fn_OnSearchClick();
        }
        function fn_MonitoringClick(PCNAME) {
            fn_OnPopupMonitoring(PCNAME);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server" >
    <div class="container" >
        <div class="search" style="width:100%">            
            <table class="oto-style" border="0" style="height:50px;">
                <tr style="color: #ffd800;">
                    <td>
                        <div style="font-size: 36px; font-weight: bolder; color: white; ">실시간 라인 모니터링</div>
                    </td>
                    <td style="width: 300px;">
                        현재시간: <ucCTF:Clock ID="ucCLOCK" runat="server"></ucCTF:Clock>
                    </td>
                    <td style="width: 220px;">
                        <ucCTF:Stopwatch ID="ucStopwatch" runat="server" ClientInstanceName="STATUSDIFF" ItemList="M|10;M|20;M|30;M|40;M|50;M|60" ShowRemaintime="true" CallbackEvent="fn_OnSearchClick" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="content" style="width:100%;">
            <dx:ASPxCallbackPanel ID="pnlChart" runat="server" ClientInstanceName="pnlChart" Enabled="true" Visible="true" Width="100%" OnCallback="pnlChart_Callback" >
                <ClientSideEvents EndCallback="fn_OnEndCallback" />
                <Paddings PaddingBottom="10px"></Paddings>
                <PanelCollection>
                    <dx:PanelContent>
                        <div style="background-color:black">
                             <table id="contentTable" style="width: 100%;">
                                <tr>
                                    <td style="width: 100%;padding-right:10px; padding-left:10px;padding-top:10px">
                                        <table class="oto-style1 contentTbl" border="1" style="width:100%; ">
                                            <asp:Literal runat="server" ID="txtTABLE1"></asp:Literal>
                                        </table>
                                    </td>
                                </tr>
                                 <tr>
                                     <td style="height:15px;">&nbsp;</td>
                                 </tr>
                                 <tr>
                                     <td style="width: 100%;padding-right:10px; padding-left:10px; padding-bottom:10px;">
                                        <table class="oto-style1 contentTbl" border="1" style="width:100%; height:100%" >
                                            <asp:Literal runat="server" ID="txtTABLE2"></asp:Literal>
                                        </table>
                                    </td>
                                 </tr>
                            </table>
                        </div>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
