<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WSTA0101.aspx.cs" Inherits="SPC.WebUI.Pages.WSTA.WSTA0101" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .header1
        {
            display:none
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

        $(document).ready(function () {

        });

        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartWidth2 = 0;
        var chartHeight1 = 0;
        var chartResized1 = false;
        var chartResized2 = false;
        var _Itemcd = "";

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
            devGrid1.SetWidth(parseInt($(".search").width() - 1, 10));
            devGrid.SetHeight(280);

            var Width = parseInt((($(".search").width() - 1) / 100), 10);
            chartWidth = Width * 70
            chartWidth2 = Width * 30
            chartHeight1 = parseInt(height, 10) - (parseInt(devGrid.GetHeight(), 10) + parseInt(devGrid1.GetHeight(), 10) + 20);

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartResized(devChart1, chartResized1, chartWidth, chartHeight1);
            fn_OnChartResized2(devChart2, chartResized2, chartWidth2, chartHeight1);
        });

        // 조회
        function fn_OnSearchClick() {
            _Itemcd = "C";
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
            devGrid.AddNewRow();
        }

        // 수정
        function fn_OnModifyClick() {
        }

        // 저장
        function fn_OnSaveClick() {
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
                } else {
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                }
            } else {
                fn_AdjustSize();
                devGrid1.PerformCallback();
                fn_OnChartDoCallback(devChart1, chartWidth, chartHeight1);
                fn_OnChartDoCallback2(devChart2, chartWidth2, chartHeight1);
            }

            fn_grid(s, e);
        }

        function fn_grid(s, e) {
            var arrData = new Array();

            $(s.GetMainElement()).find('table[id$=DXMainTable]>tbody>tr[id$=DXDataRow0]>td:gt(0)').each(function (idx) {
                arrData[idx] = { "Itemcd": $(this).text() };
            });

            $(s.GetMainElement()).find('table[id$=DXMainTable]>tbody>tr[id$=DXDataRow1]>td:gt(0)').each(function (idx) {
                arrData[idx] = { "Itemnm": $(this).text(), "Itemcd": arrData[idx]["Itemcd"] };
            });

            $(s.GetMainElement()).find('table[id$=DXMainTable]>tbody>tr[id$=DXDataRow2]>td:gt(0)').each(function (idx) {
                arrData[idx] = { "Workcd": $(this).text(), "Itemnm": arrData[idx]["Itemnm"], "Itemcd": arrData[idx]["Itemcd"] };
            });

            $(s.GetMainElement()).find('table[id$=DXMainTable]>tbody>tr:gt(0)').each(function (idx) {
                $(this).find('td:gt(0)').each(function (idx) {
                    $(this).on("click", function () {
                        fn_Chart2Callback(arrData[idx]["Itemcd"], arrData[idx]["Itemnm"], arrData[idx]["Workcd"]);
                    });
                });
            });
        }

        function fn_Chart2Callback(itemcd, Itemnm, Workcd) {
            _Itemcd = itemcd + '|' + Itemnm + '|' + Workcd
            fn_OnChartDoCallback2(devChart2, chartWidth2, chartHeight1)
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
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

        function fn_OnChartResized2(chartObj, chartResized, _width, _height) {
            if (!chartResized2) {
                fn_OnChartDoCallback2(chartObj, _width, _height);
            }
        }

        function fn_OnChartDoCallback2(chartObj, _width, _height) {
            var oParams = _width + '|' + _height + '|' + _Itemcd;
            chartObj.PerformCallback(oParams);
        }

        function fn_OnChartEndCallback(s, e) {
            if (s.cpFunction != '' && s.cpFunction == 'resizeTo') {
                s.GetMainElement().style.width = s.cpChartWidth + 'px';
            }
        }

        function fn_alldata() {
            var keys = fn_GetCastText('hidUCFROMDT') + '|' +
            fn_GetCastText('hidUCTODT') + '|' +
            fn_GetCastText('hidBANCD') + '|' +
            fn_GetCastText('txtUCWORKPOPCD') + '|' +
            fn_GetCastText('txtUCWORKNM') + '|' +
            fn_GetCastText('hidMODELCD') + '|' +
            rdoGBN.GetValue() + '|' +
            rdoUNIT.GetValue() + '|';
            fn_OnPopup_WorstItem(keys);
        }

       

        function onCellClick(s, count, cellindex) {
            $(s.GetMainElement()).find('table[id$=DXMainTable]>tbody>tr>td').each(function (idx) {
                this.style.backgroundColor = '#FFFFFF';
                this.style.color = '#000000'
            });

            $(s.GetMainElement()).find('table[id$=DXMainTable]>tbody>tr').each(function (idx) {
                $(this).find('td').each(function (idx2) {
                    if (idx2 == cellindex) {
                        this.style.backgroundColor = '#A0A0A0';
                        this.style.color = '#FFFFFF'
                    }
                });
                
            });
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
                    <label class="col-sm-1 control-label">분석기간</label>
                    <div class="col-sm-3">
                        <ucCTF:Date runat="server" id="ucDate"   />
                    </div>
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-1 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" />
                    </div>
                    <label class="col-sm-1 control-label">공정</label>
                    <div class="col-sm-3 control-label">
                        <ucCTF:WorkPOP ID="ucWorkPOP" runat="server"  />
                    </div>
                </div>
                <div class="form-group">
                    <%--<div class="col-sm-1"></div>
                    <div class="col-sm-3"></div>--%>
                    <label class="col-sm-1 control-label">모델</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:ModelDDL ID="ucModelDDL" runat="server" />
                    </div>
                    <div class="col-sm-1"></div>
                    <label class="col-sm-1 control-label">구분</label>
                        <div class="col-sm-1">
                            <dx:ASPxRadioButtonList ID="rdoGBN" ClientInstanceName="rdoGBN" runat="server"
                                RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                                <Items>
                                    <dx:ListEditItem Value="1" Text="Worst" Selected="true" />
                                    <dx:ListEditItem Value="2" Text="Best"/>
                                </Items>
                            </dx:ASPxRadioButtonList>
                        </div>
                    <label class="col-sm-1 control-label">단위</label>
                        <div class="col-sm-3">
                            <dx:ASPxRadioButtonList ID="rdoUNIT" ClientInstanceName="rdoUNIT" runat="server"
                                RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None">
                                <Items>
                                    <dx:ListEditItem Value="1" Text="Percent(%)" Selected="true" />
                                    <dx:ListEditItem Value="2" Text="ppm"/>
                                </Items>
                            </dx:ASPxRadioButtonList>
                        </div>
                </div>
            </div>
        </div>
        <div class="content">
             <table class="tblContents" style="width: 100%;">
                 <tr>
                     <td style="height:20px; padding-top: 2px;" colspan="2" >
                         <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="true" Width="100%"
                            EnableViewState="false" EnableRowsCache="false" OnCustomCallback="devGrid1_CustomCallback"  
                            >
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center" CssClass="header1"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" />
                            <SettingsPager Mode="ShowAllRecords" />
                        </dx:ASPxGridView>
                     </td>

                 </tr>
                <tr>
                    <td style="vertical-align: top;padding-top: 10px;width:70%">
                        <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server"
                            ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="100px"
                            OnCustomCallback="devChart1_CustomCallback">
                            <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" BeginCallback="function(s, e) { chartResized1 = true; }" />
                        </dx:WebChartControl>
                    </td>
                    <td style="vertical-align: top;padding-top: 10px;width:30%">
                        <dx:WebChartControl ID="devChart2" ClientInstanceName="devChart2" runat="server"
                            ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="100px"
                            OnCustomCallback="devChart2_CustomCallback">
                            <ClientSideEvents EndCallback="function(s, e) { chartResized2 = false; fn_OnChartEndCallback(s, e); }" BeginCallback="function(s, e) { chartResized2 = true; }" />
                        </dx:WebChartControl>
                    </td>
                </tr>
                 <tr>
                     <td style="height: 290px; padding-top: 10px;" colspan="2">
                         <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="true" Width="100%"
                            KeyFieldName="F_ITEMCD;F_ITEMNM" EnableViewState="false" EnableRowsCache="false"
                            OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared"
                            OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" >  <%--OnHtmlRowPrepared="devGrid_HtmlRowPrepared"--%>
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" ></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Auto" VerticalScrollableHeight="10" ShowStatusBar="Hidden"  HorizontalScrollBarMode="Auto" />
                            <SettingsBehavior AllowSort="false" ColumnResizeMode="Control"   /> <%--AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" --%>
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                            <Templates>
                                <StatusBar>
                                    <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                                </StatusBar>
                            </Templates>
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
                        <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
                     </td>
                 </tr>
             </table>
        </div>
    </div>
</asp:Content>
