<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="DIOF0402.aspx.cs" Inherits="SPC.WebUI.Pages.DIOF.DIOF0402" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var sINSPKINDCD = '';
        var inspVisibleIndex = -1;
        var changedVisibleIndex = [];
        var ngClasses = {};
        var ngArray = [];
        var sOldMeasure = null;
        var sOldJudge = null;
        var chartWidth = 0;
        var chartHeight = 0;
        var chartResized = false;
        var viewVisibleIndex = -1;

        $(document).ready(function () {
            // 입력상자 Enter Key Event
            $('#inputTable input[type="text"]').keypress(function (e) {
                if (e.keyCode == 13) {
                    fn_DoSaveNgReason();
                    return false;
                }
            });

            // 점검일에서 엔터키 입력 시 점검항목 목록 조회
            $('#inspSearchTable input[type="text"]').keypress(function (e) {
                if (e.keyCode == 13) {
                    fn_OnDateChanged();
                    return false;
                }
            });

            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);

            top = $(".container_r").offset().top;
            searchHeight = $(".search_r").height() > 0 ? $(".search_r").height() + 6 : 0;
            pagingHeight = $(".paging_r").height() > 0 ? $(".paging_r").height() - 20 : 0;
            height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGridInsp.SetHeight(height);

            devGridData.SetHeight(110);

            $('#subPopup').dialog('option', 'width', $(document).width() - 100);
            $('#subPopup').dialog('option', 'height', 440);
            
            chartWidth = parseInt($('#subPopup').dialog('option', 'width'), 10) - 346;
            chartHeight = 200;

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartResized(devChart, chartResized, chartWidth, chartHeight);
        });

        // 조회
        function fn_OnSearchClick() {
            fn_doSetGridEventAction('true');
            fn_OnInputSelectClear();
            devGrid.PerformCallback();
            devGridInsp.PerformCallback();
        }

        // 점검항목조회
        function fn_OnSubSearchClick() {
            fn_doSetGridEventAction('true');
            devGridInsp.PerformCallback();
        }

        // 입력
        function fn_OnNewClick() { }

        // 수정
        function fn_OnModifyClick() { }

        // 저장
        function fn_OnSaveClick() { }

        // 취소
        function fn_OnCancelClick() { }

        // 삭제
        function fn_OnDeleteClick() { }

        // 인쇄
        function fn_OnPrintClick() { }

        // 엑셀
        function fn_OnExcelClick() { }

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

        // 점검 Grid End Callback
        function fn_OnGridInspEndCallback(s, e) {
            if (s.cpResultCode != '') {
                if (s.cpResultCode == '9') {
                    ngArray = [];
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

        // Validate
        function fn_OnValidate(s, e) { }

        // Grid Row DblClick
        function fn_OnRowDblClick(s, e) {
            fn_doSetGridEventAction('false');
            fn_OnInputSelectData(devGrid.GetRowKey(e.visibleIndex).split('|'));
            devGridInsp.PerformCallback();
            fn_OnChartDoCallback(devChart, chartWidth, chartHeight);
        }

        // 입력폼 초기화
        function fn_OnInputSelectClear() {
            fn_SetTextValue('srcF_MACHIDX', '');
            fn_SetTextValue('srcF_MACHCD', '');
            fn_SetTextValue('srcF_MACHNM', '');
            fn_SetTextValue('srcF_BANCD', '');
            fn_SetTextValue('srcF_BANNM', '');
            fn_SetTextValue('srcF_LINECD', '');
            fn_SetTextValue('srcF_LINENM', '');
            fn_SetTextValue('srcF_IMAGENO', '');
        }

        // 입력폼 조회값입력
        function fn_OnInputSelectData(rowKey) {
            fn_SetTextValue('srcF_MACHIDX', rowKey[0]);
            fn_SetTextValue('srcF_MACHCD', rowKey[1]);
            fn_SetTextValue('srcF_MACHNM', rowKey[2]);
            fn_SetTextValue('srcF_BANCD', rowKey[3]);
            fn_SetTextValue('srcF_BANNM', rowKey[4]);
            fn_SetTextValue('srcF_LINECD', rowKey[5]);
            fn_SetTextValue('srcF_LINENM', rowKey[6]);
            fn_SetTextValue('srcF_IMAGENO', rowKey[7]);
        }

        // 설비선택여부
        function isSelectMach() {
            return fn_GetCastValue('srcF_MACHIDX') != '' && fn_GetCastValue('srcF_MACHCD') != '' && fn_GetCastValue('srcF_MACHNM') != '';
        }

        // 항목 이미지 및 차트 오픈
        function fn_OnCustomButtonClick(s, e) {
            viewVisibleIndex = e.visibleIndex;
            devGridInsp.GetRowValues(viewVisibleIndex, "F_IMAGESEQ", fn_SetInspImage);
            devGridData.PerformCallback(devGridInsp.GetRowKey(viewVisibleIndex).split('|')[0]);

            $("#subPopup").dialog("open");
        }

        function fn_SetInspImage(val) {
            var sIMAGENO = val;
            var objImage = $('#srcImage');
            var objDiv = $('#divImage');
            $(objDiv).width(300);
            $(objDiv).height(200);
            var imageUrl = sIMAGENO == '' ? '' : rootURL + 'API/Common/Download.ashx'
                + '?attfileno=' + sIMAGENO
                + '&attfileseq=1'
                + '&data_gbn=E'
                + '&compcd=<%=gsCOMPCD%>';

            $(objImage).attr('src', imageUrl);
            testImage(imageUrl, objImage);
        }

        // 차트 이벤트
        function fn_OnChartResized(chartObj, chartResized, _width, _height) {
            if (!chartResized) {
                fn_OnChartDoCallback(chartObj, _width, _height);
            }
        }

        function fn_OnChartDoCallback(chartObj, _width, _height) {
            if (viewVisibleIndex >= 0) {
                var vals = devGridInsp.GetRowKey(viewVisibleIndex).split('|');
                var oParams = chartWidth + '|' + chartHeight + '|' + vals[0] + '|' + encodeURIComponent(vals[1]);
            } else {
                var oParams = _width + '|' + _height + '||';
            }
            chartObj.PerformCallback(oParams);
        }

        function fn_OnChartEndCallback(s, e) {
            if (s.cpFunction != '' && s.cpFunction == 'resizeTo') {
                s.GetMainElement().style.width = s.cpChartWidth + 'px';
            }
        }

        function fn_OnGridDataEndCallback(s, e) {
            if (s.cpResultCode != '') {
                alert(s.cpResultMsg);
                s.cpResultCode = "";
                s.cpResultMsg = "";
            } else {
                fn_OnChartDoCallback(devChart, chartWidth, chartHeight);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <table class="layerTable">
        <colgroup>
            <col style="width:480px;" />
            <col />
        </colgroup>
        <tr>
            <td>
                <div class="container">
                    <div class="search">
                        <table class="contentTable">
                            <colgroup>
                                <col style="width:60px;" />
                                <col style="width:180px;" />
                                <col style="width:60px;" />
                                <col />
                            </colgroup>
                            <tr>
                                <td class="tdLabel">반</td>
                                <td class="tdInput">
                                    <ucCTF:BanMulti runat="server" id="schF_BANCD" ClientInstanceName="schF_BANCD" targetCtrls="schF_LINECD" />
                                </td>
                                <td class="tdLabel">라인</td>
                                <td class="tdInput">
                                    <ucCTF:LineMulti runat="server" ID="schF_LINECD" ClientInstanceName="schF_LINECD" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel">설비명</td>
                                <td class="tdInput" colspan="3">
                                    <dx:ASPxTextBox ID="schF_MACHNM" ClientInstanceName="schF_MACHNM" runat="server" Width="100%" MaxLength="50" />
                                </td>
                            </tr>
                        </table>
                        <div style="width:100%;text-align:left;font-weight:bold;color:red;">점검항목별 트렌드를 보려면 아래에서 해당 설비를 더블클릭하세요</div>
                        <div class="divPadding"></div>
                    </div>
                    <div class="content">
                        <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
                        <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
                        <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_MACHIDX;F_MACHCD;F_MACHNM;F_BANCD;F_BANNM;F_LINECD;F_LINENM;F_IMAGENO" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGrid_CustomCallback">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                            <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                                RowDblClick="fn_OnRowDblClick" />
                            <Templates>
                                <StatusBar>
                                    <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                                </StatusBar>
                            </Templates>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반" Width="100px" />
                                <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인" Width="100px" />
                                <dx:GridViewDataColumn FieldName="F_MACHCD" Caption="설비코드" Width="80px" />
                                <dx:GridViewDataColumn FieldName="F_MACHNM" Caption="설비명" Width="100%">
                                    <CellStyle HorizontalAlign="Left" Wrap="True"></CellStyle>
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn FieldName="F_MACHIDX" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_BANCD" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_LINECD" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_IMAGENO" Visible="false" />
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                    <div class="paging"></div>
                </div>
            </td>
            <td>
                <div class="container_r">
                    <div class="search_r">
                        <dx:ASPxTextBox ID="srcF_MACHIDX" ClientInstanceName="srcF_MACHIDX" runat="server" ClientVisible="false" />
                        <dx:ASPxTextBox ID="srcF_IMAGENO" ClientInstanceName="srcF_IMAGENO" runat="server" ClientVisible="false" />
                        <table class="contentTable">
                            <colgroup>
                                <col style="width:70px;" />
                                <col style="width:150px;" />
                                <col style="width:70px;" />
                                <col style="width:150px;" />
                                <col style="width:70px;" />
                                <col />
                            </colgroup>
                            <tr>
                                <td class="tdLabel">반</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_BANCD" ClientInstanceName="srcF_BANCD" runat="server" ClientVisible="false" />
                                    <dx:ASPxTextBox ID="srcF_BANNM" ClientInstanceName="srcF_BANNM" runat="server" Width="100%">
                                        <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td class="tdLabel">라인</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_LINECD" ClientInstanceName="srcF_LINECD" runat="server" ClientVisible="false" />
                                    <dx:ASPxTextBox ID="srcF_LINENM" ClientInstanceName="srcF_LINENM" runat="server" Width="100%">
                                        <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td class="tdLabel">기간</td>
                                <td class="tdInput">
                                    <ucCTF:Date runat="server" id="ucDate" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel">설비코드</td>
                                <td class="tdInput">
                                    <dx:ASPxTextBox ID="srcF_MACHCD" ClientInstanceName="srcF_MACHCD" runat="server" Width="100%">
                                        <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td class="tdLabel">설비명</td>
                                <td class="tdInput" colspan="3">
                                    <dx:ASPxTextBox ID="srcF_MACHNM" ClientInstanceName="srcF_MACHNM" runat="server" Width="100%">
                                        <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="divPadding"></div>
                    </div>
                    <div class="content_r">
                        <dx:ASPxGridView ID="devGridInsp" ClientInstanceName="devGridInsp" runat="server" AutoGenerateColumns="false" Width="100%"
                            KeyFieldName="F_INSPIDX;F_INSPNM" EnableViewState="false" EnableRowsCache="false"
                            OnCustomCallback="devGridInsp_CustomCallback">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                                <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                                <EditForm CssClass="bg-default"></EditForm>
                            </Styles>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                            <SettingsBehavior AllowSort="false" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridInspEndCallback" CallbackError="fn_OnCallbackError"
                                CustomButtonClick="fn_OnCustomButtonClick" />
                            <Templates>
                                <StatusBar>
                                    <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGridInsp" />
                                </StatusBar>
                            </Templates>
                            <Columns>
                                <dx:GridViewCommandColumn Width="60px">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton Text="보기" />
                                    </CustomButtons>
                                    <HeaderTemplate>
                                        <i class="fa fa-bar-chart-o" title="트랜드"></i>
                                        <i class="fa fa-picture-o" title="항목사진"></i>
                                        <i class="i i-grid3" title="측정정보"></i>
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="F_INSPORDER" Caption="순서" Width="50px" />
                                <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="항목명" Width="150px">
                                    <CellStyle HorizontalAlign="Left" Wrap="True"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_INSPREMARK" Caption="점검내용" Width="180px">
                                    <CellStyle HorizontalAlign="Left" Wrap="True"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_INSPWAY" Caption="점검방법" Width="240px">
                                    <CellStyle HorizontalAlign="Left" Wrap="True"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_INSPKINDNM" Caption="판정방식" Width="70px" />
                                <dx:GridViewDataColumn FieldName="F_STAND" Caption="기준" Width="90px">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MAX" Caption="상한" Width="90px">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_MIN" Caption="하한" Width="90px">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="F_CYCLENM" Caption="점검주기" Width="100px" />
                                <dx:GridViewDataColumn FieldName="F_UNITNM" Caption="단위" Width="50px" />

                                <dx:GridViewDataColumn FieldName="F_INSPIDX" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_INSPKINDCD" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_CYCLECD" Visible="false" />
                                <dx:GridViewDataColumn FieldName="F_IMAGESEQ" Visible="false" />
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                    <div class="paging_r">
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <div id="subPopup">
        <table id="subTable" border="0" style="width:100%;">
            <colgroup>
                <col style="width:300px;" />
                <col style="width:10px;" />
                <col style="height:100px;" />
            </colgroup>
            <tr>
                <td style="border:1px solid #808080;"><div id="divImage" style="position:relative;"><img id="srcImage" src="" class="centerImage resizeImageRatio" /></div></td>
                <td></td>
                <td style="height:200px;">
                    <dx:WebChartControl ID="devChart" ClientInstanceName="devChart" runat="server"
                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="False" Height="200px"
                        OnCustomCallback="devChart_CustomCallback" CrosshairEnabled="True">
                        <ClientSideEvents EndCallback="function(s, e) { chartResized = false; fn_OnChartEndCallback(s, e); }" BeginCallback="function(s, e) { chartResized = true; }" />
                    </dx:WebChartControl>
                </td>
            </tr>
            <tr><td style="height:10px;"></td></tr>
            <tr>
                <td colspan="3" style="height:120px;">
                    <dx:ASPxGridView ID="devGridData" ClientInstanceName="devGridData" runat="server" AutoGenerateColumns="true" Width="100%"
                        KeyFieldName="날짜" EnableViewState="false" EnableRowsCache="false"
                        OnCustomCallback="devGridData_CustomCallback">
                        <Styles>
                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                            <EditForm CssClass="bg-default"></EditForm>
                        </Styles>
                        <Settings VerticalScrollBarMode="Hidden" VerticalScrollableHeight="0" HorizontalScrollBarMode="Visible" ShowStatusBar="Hidden" />
                        <SettingsBehavior AllowSort="false" AllowSelectSingleRowOnly="true" AllowSelectByRowClick="true" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridDataEndCallback" CallbackError="fn_OnCallbackError" />
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        // 이상발생 레이아웃 Dialog
        $("#subPopup").dialog({
            autoOpen: false,
            modal: true,
            closeOnEscape: false,
            draggable: false,
            title: "설비사진",
            classes: {
                "ui-dialog": "highlight",
                "ui-dialog-titlebar": "highlight"
            },
            open: function (event, ui) {
                $('#divImage').css('overflow', 'hidden'); //this line does the actual hiding
            }
        });
    </script>
</asp:Content>