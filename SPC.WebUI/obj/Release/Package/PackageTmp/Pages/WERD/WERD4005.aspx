<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD4005.aspx.cs" Inherits="SPC.WebUI.Pages.WERD.WERD4005" %>

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

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".InputTable").height() > 0 ? $(".InputTable").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt(fn_GetDocumentHeight() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));

            $(".tblContents").height(height);



            chartWidth = parseInt(($(".InputTable").width() - 5), 10);

            $(".tdChart1").width(chartWidth);
            chartHeight1 = parseInt((height - 250), 10);
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
            }
        }

        function fn_OnChartDoCallback(chartObj, _width, _height) {
            var oParams = _width + '|' + _height;
            chartObj.PerformCallback(oParams);
        }

        // 조회
        function fn_OnSearchClick() {
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

            fn_OnChartDoCallback(devChart1, chartWidth, chartHeight1);

        }



        function fn_CallbackComplete(s, e) {
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            //alert(e.message);
        }

        function fn_OnChartEndCallback(s, e) {
            if (s.cpFunction != '' && s.cpFunction == 'resizeTo') {
                s.GetMainElement().style.width = s.cpChartWidth + 'px';
            }

        }


        function fn_rdoGBN_Change(s, e) {
            if (rdoGBN.GetValue() == "35") {

                document.getElementById("div_type").style.display = "";
                document.getElementById("div_cause").style.display = "none";
            }
            else {

                document.getElementById("div_type").style.display = "none";
                document.getElementById("div_cause").style.display = "";
            }
        }

    </script>
</asp:Content>












<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <table class="InputTable">
            <colgroup>
                <col style="width:10%" />
                <col style="width:23%" />
                <col style="width:10%" />
                <col style="width:23%" />
                <col style="width:10%" />
                <col style="width:24%" />
            </colgroup>
            <tr>
                <td class="tdTitle">
                    <label>작업일자</label>
                </td>
                <td class="tdContent" >
                   <ucCTF:Date runat="server" id="ucDate" DateTimeOnly="false" MaxDate="334"  MonthOnly="true" MaxMonth="12"  />                    
                </td>                
                 <td class="tdTitle">품목</td>
                <td class="tdContent">
                    <ucCTF:Item ID="ucItem" runat="server" />
                    <%--<ucCTF:Item1 ID="ucItem1" runat="server" usedModel="true" useWERD="true" />--%>
                </td>
                <td class="tdTitle">공정명</td>
                <td class="tdContent">
                    <ucCTF:WorkPOP ID="ucWorkPOP" runat="server"  />
                    <%--<ucCTF:WorkPOP1 ID="ucWorkPOP1" runat="server" useWERD="true" validateFields="hidUCITEMCD1|품목" />--%>
                </td>  
            </tr>
            <tr>
                <td class="tdTitle">
                    <label>조회조건</label>
                </td>
                <td class="tdContent">
                   <dx:ASPxRadioButtonList ID="rdoGBN" ClientInstanceName="rdoGBN" runat="server"
                        RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                    <ClientSideEvents ValueChanged="fn_rdoGBN_Change" />
                        <Items>
                            <dx:ListEditItem Value="35" Text="부적합유형" />
                            <dx:ListEditItem Value="36" Text="부적합원인" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
                <td class="tdTitle">
                    <label id="lblSearch" >부적합원인/유형</label>
                </td>
                <td class="tdContent" >
                    <div id="div_type">
                        <dx:ASPxComboBox ID="srcF_NGTYPECD" ClientInstanceName="srcF_NGTYPECD" 
                            runat="server" ValueType="System.String" Width="100%">
                        </dx:ASPxComboBox>
                    </div>
                    <div id="div_cause" style="display:none">
                        <dx:ASPxComboBox ID="srcF_NGCAUSECD" ClientInstanceName="srcF_NGCAUSECD" 
                            runat="server" ValueType="System.String" Width="100%">
                        </dx:ASPxComboBox>
                    </div>
                </td>
                <td class="tdTitle">구분</td>
                <td class="tdContent">
                    <dx:ASPxRadioButtonList ID="rdoBAN" ClientInstanceName="rdoBAN" runat="server"
                        RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                        <Items>
                            <dx:ListEditItem Value="RATE" Text="부적합률(%)" Selected="true" />
                            <dx:ListEditItem Value="PPM" Text="PPM" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
            </tr>
         </table>
        <div class="form-group"></div>


        <div class="content">
            <table style="width: 100%;" border="0">
                <tr>
                    <td id="tdLeft">
                        <table class="tblContents" style="width: 100%;" border="0">
                            <tr>
                                <td class="tdChart1" style="vertical-align: top;">
                                    <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="False" Height="100px"
                                        OnCustomCallback="devChart1_CustomCallback" CrosshairEnabled="True" Width="300px" >
                                        <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" BeginCallback="function(s, e) { chartResized1 = true; }" />                                        
                                    </dx:WebChartControl>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 250px; padding-top: 5px;" colspan="2" >
                                    <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="true" Width="100%" 
                                        OnDataBinding="devGrid_DataBinding"  
                                        OnCustomCallback="devGrid_CustomCallback"
                                        KeyFieldName="검사일자" EnableViewState="false" EnableRowsCache="false"  OnHtmlRowPrepared="devGrid_HtmlRowPrepared" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared">
                                        <Styles>
                                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                            <EditForm CssClass="bg-default"></EditForm>
                                        </Styles>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" HorizontalScrollBarMode="Visible" ShowStatusBar="Hidden" />
                                        <SettingsBehavior AllowSort="false"  AllowDragDrop="false" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <ClientSideEvents Init="function(s,e){s.SetHeight(240)}" CallbackError="fn_OnCallbackError" EndCallback="fn_OnEndCallback" />
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="paging">
        </div>
    </div>
</asp:Content>

