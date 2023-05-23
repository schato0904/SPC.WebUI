<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="PLCM2007.aspx.cs" Inherits="SPC.WebUI.Pages.PLCM.PLCM2007" %>

<%-- 작업지시서 조회 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var chartWidth = 0;
        var chartHeight1 = 0;
        var rightHeight = 0;
        var chartResized1 = false;
        var totngcnt = 0;
        var _fieldList = "";
        var key;

        $(document).ready(function () {
            SetClear();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            chartWidth = parseInt($(".container1").width(), 10);
            fnASPxGridView_ReHeight(devGrid);
            rightHeight = parseInt(devGrid.GetHeight(), 10);

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);

            devGrid1.SetHeight(rightHeight - 540);
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartResized(devChart1, chartResized1, chartWidth, 500);
        });

        // 조회
        function fn_OnSearchClick() {

            devGrid1.PerformCallback('clear');
            fn_Check();
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
            SetClear();
            devGrid1.PerformCallback('clear');
        }

        // 삭제
        function fn_OnDeleteClick() {
        }

        // 인쇄
        function fn_OnPrintClick() {
        }

        // 엑셀
        function fn_OnExcelClick() {
            if (devGrid1.GetVisibleRowsOnPage() == 0) {
                ucNavi.SetNoticeOnce('조회된 내용이 없습니다.', 'red');
                return false;
            }
            btnExport.DoClick();
        }
        // 초기화
        function SetInit(isFirst) {
            // 초기화
            SetClear(isFirst);
            SetDefault();
        }
        // 컨트롤값 클리어
        function SetClear() {
        }
        // 기본값 설정
        function SetDefault() {

        }

        // 페이지 활성/비활성화
        function SetPageEnable(enable) {
        }

        // 페이지모드 조회
        function GetPageMode() {
            return hidPageMode.GetText();
        }


        // 이벤트 핸들러
        // Grid End Callback
        function fn_OnEndCallback(s, e) {
            // 리턴값 처리
            var result = '';
            var data = '';
            if (Trim(s.cpResult) != '') {
                try {
                    result = JSON.parse(s.cpResult);
                    if (result.error != '') {
                        alert(result.error);
                    } else {
                        if (result.msg != '') {
                            ucNavi.SetNoticeOnce(result.msg);
                        }
                        if (result.data != '') {
                            data = result.data;
                        }
                    }
                } catch (err) {
                    alert(err);
                }
            }

            // 그리드별 처리
            switch (s) {
                //case devGrid:
                //    break;
                case devGrid1:
                    //devChart1.PerformCallback();
                    //fn_OnChartResized(devChart1, chartResized1, chartWidth, 500);
                    var oParams = chartWidth + '|' + 500;
                    devChart1.PerformCallback(oParams);
                    SetChartSize(chartWidth, 500);
                    fn_OnSetSearchResult(s.cpResult1);
                    break;
            }
        }

        // 검사규격과 분석결과를 세팅한다
        function fn_OnSetSearchResult(result1) {
            var freePoint = parseInt(3, 10);
            var resultValues1 = result1.split('|');
            if (resultValues1[0] == null || resultValues1[0] == '') {
                $('#tdResult01').text("");
                $('#tdResult02').text("");
                $('#tdResult03').text("");
                $('#tdResult04').text("");
                $('#tdResult05').text("");
                $('#tdResult06').text("");
                $('#tdResult07').text("");
            }
            else {
                $('#tdResult01').text(fn_SetToFixed(resultValues1[0], freePoint));
                $('#tdResult02').text(fn_SetToFixed(resultValues1[1], freePoint));
                $('#tdResult03').text(fn_SetToFixed(resultValues1[2], freePoint + 1));
                $('#tdResult04').text(fn_SetToFixed(resultValues1[3], freePoint));
                $('#tdResult05').text(fn_SetToFixed(resultValues1[4], 2));
                $('#tdResult06').text(fn_SetToFixed(resultValues1[5], 2));
                $('#tdResult07').text(fn_SetToFixed(resultValues1[6], freePoint));
            }
            // 분석결과
        }

        // ASPxCallback 사용 : 우측항목 조회, 저장, 삭제
        function devCallback_CallbackComplete(s, e) {
            try {
                if (e.result != '') {
                    var result = JSON.parse(e.result);
                    if (typeof result.msg != 'undefined' && Trim(result.msg) != '') ucNavi.SetNoticeOnce(result.msg);
                    if (typeof result.error != 'undefined' && Trim(result.error) != '') {
                        alert(result.error);
                        return false;
                    }
                    switch (result.action) {
                        case "VIEW":
                            if (typeof result.data == 'string') {
                                result.data = JSON.parse(decodeURIComponent(result.data || '{}'));
                            }
                            fn_OnValidate();
                            break;
                        default:
                            break;
                    }
                }
            } catch (err) {
                alert(err);
            }
        }

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            alert(e.message);
        }

        // Validate
        function fn_OnValidate(s, e) {
            var result = true;
            result = ASPxClientEdit.ValidateEditorsInContainerById('divRight');
            return result;
        }

        function devGrid_RowDblClick(s, e) {
            //var key = s.GetRowKey(e.visibleIndex);
            //ViewList(key);
        }

        function fn_schF_FROMYMD_ValueChanged(s, e) {
            schF_MASTERID.PerformCallback();
        }

        function fn_schF_INSPCD_ValueChanged(s, e) {
            //devGrid1.PerformCallback('clear');
        }
        function fn_schF_MASTERID_ValueChanged(s, e) {
            //devGrid1.PerformCallback('clear');
            devGrid.PerformCallback();
        }
        function fn_F_USEYN_changed(s, e) {
            //devGrid.UpdateEdit();
            //devGrid1.PerformCallback('clear');
        }

        function fn_Check() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('좌측 설비내역을 선택하세요!!');
                return false;
            }
            if (selectedKeys.length > 80) {
                alert('마스터는 최대 80개까지 선택 가능합니다!!');
                return false;
            }
            if (schF_INSPCD.GetValue() == "" || schF_INSPCD.GetValue() == null) {
                alert('검사항목을 선택하세요.');
                schF_INSPCD.Focus();
                return false;
            }
            devGrid1.PerformCallback(selectedKeys);
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
        function SetChartSize(w, h) {
            hidChartWidth.SetValue(w);
            hidChartHeight.SetValue(h);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div id="divTop" class="form-horizontal">
            <div id="divMiddle" style="float: left; margin-bottom: 2px; width: 100%;">
                <table class="InputTable" style="width: 100%;">
                    <colgroup>
                        <col style="width: 10%" />
                        <col style="width: 18%" />
                        <col style="width: 10%" />
                        <col style="width: 21%" />
                        <col style="width: 10%" />
                        <col style="width: 21%" />
                    </colgroup>
                    <tr>
                        <td class="tdTitle">작업일자</td>
                        <td class="tdContent">
                            <ucCTF:Date ID="ucDate" runat="server" Changed="fn_schF_FROMYMD_ValueChanged" />
                        </td>
                        <td class="tdTitle">마스터ID</td>
                        <td class="tdContentR">
                            <dx:ASPxComboBox runat="server" ID="schF_MASTERID" ClientInstanceName="schF_MASTERID" IncrementalFilteringMode="None" ValueType="System.String" OnCallback="schF_MASTERID_Callback">
                                <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                <ClientSideEvents ValueChanged="fn_schF_MASTERID_ValueChanged" />
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdContentL" colspan="2" />
                    </tr>
                </table>
            </div>
        </div>
        <div style="clear: both; height: 2px;"></div>
        <div id="divLeft" style="width: 30%; float: left;">
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="tDate;sTime;tRecipeID2;tMasterID" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnDataBinding="devGrid_DataBinding">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Hidden" />
                <SettingsBehavior AllowSort="false" ColumnResizeMode="NextColumn" AllowDragDrop="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <ClientSideEvents Init="fn_OnGridInit"
                    EndCallback="fn_OnGridEndCallback"
                    CallbackError="fn_OnCallbackError"
                    RowDblClick="devGrid_RowDblClick" />
                <Templates>
                    <StatusBar>
                        <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                        <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount" runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="No" Caption="No" Width="30px" />
                    <dx:GridViewDataTextColumn FieldName="tDate" Width="80px" Caption="작업일자" />
                    <dx:GridViewDataTextColumn FieldName="sTime" Width="60px" Caption="시간" />
                    <dx:GridViewDataTextColumn FieldName="tRecipeID2" Width="0px" Caption="레시피ID2" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="tRecipeID" Caption="레시피" CellStyle-HorizontalAlign="Left" />
                    <dx:GridViewDataTextColumn FieldName="tMasterID" Width="105" Caption="마스터ID" CellStyle-HorizontalAlign="Left" />
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="35px">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div id="divSpace" style="width: 1%; float: left;">&nbsp;</div>
        <div id="divRight" style="width: 69%; float: left;" class="container1">
            <table class="InputTable" style="width: 100%;">
                <colgroup>
                    <col style="width: 8%" />
                    <col style="width: 17%" />
                    <col style="width: 8%" />
                    <col style="width: 17%" />
                    <col style="width: 8%" />
                    <col style="width: 17%" />
                    <col style="width: 8%" />
                    <col style="width: 16%" />
                </colgroup>
                <tr>
                    <td class="tdTitle">검사항목</td>
                    <td class="tdContentR">
                        <dx:ASPxComboBox runat="server" ID="schF_INSPCD" ClientInstanceName="schF_INSPCD" IncrementalFilteringMode="None" ValueType="System.String" OnCallback="schF_INSPCD_Callback">
                            <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                            <ClientSideEvents ValueChanged="fn_schF_INSPCD_ValueChanged" />
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdContentL" colspan="8" />
                </tr>
            </table>
            <div id="divRightBody" style="width: 100%;">
                <div id="div3" style="width: 100%;">
                    <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server" OnCustomCallback="devChart1_CustomCallback"
                        ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="500px">
                        <clientsideevents endcallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" begincallback="function(s, e) { chartResized1 = true; }" />
                    </dx:WebChartControl>
                </div>
                <div id="div1" style="width: 69%; float: left;">
                    <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="false" Width="100%"
                        KeyFieldName="TIME" EnableViewState="false" EnableRowsCache="false"
                        OnCustomCallback="devGrid1_CustomCallback"
                        OnDataBinding="devGrid1_DataBinding">
                        <Styles>
                            <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                            <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                            <EditForm CssClass="bg-default"></EditForm>
                        </Styles>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Hidden" />
                        <SettingsBehavior AllowSort="false" ColumnResizeMode="NextColumn" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" AllowDragDrop="false" />
                        <SettingsPager Mode="ShowPager" PageSize="2000" />
                        <ClientSideEvents Init="fn_OnGridInit"
                            EndCallback="fn_OnGridEndCallback"
                            CallbackError="fn_OnCallbackError" />
                        <Templates>
                            <StatusBar>
                                <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                                <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount" runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                            </StatusBar>
                        </Templates>
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="F_NO" Width="40px" Caption="NO" />
                            <dx:GridViewDataTextColumn FieldName="tRecipeID" Width="50%" Caption="레시피" CellStyle-HorizontalAlign="Left" />
                            <dx:GridViewDataTextColumn FieldName="tMasterID" Width="50%" Caption="마스터ID" CellStyle-HorizontalAlign="Left" />
                            <dx:GridViewDataTextColumn FieldName="F_DATA" Width="65px" Caption="측정치" CellStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataTextColumn FieldName="F_AVG" Width="65px" Caption="평균" CellStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataTextColumn FieldName="F_STV" Width="65px" Caption="표준편차" CellStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataTextColumn FieldName="F_MAX" Width="65px" Caption="상한" CellStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataTextColumn FieldName="F_MIN" Width="65px" Caption="하한" CellStyle-HorizontalAlign="Right" />
                        </Columns>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="devGrid1Exporter" runat="server" GridViewID="devGrid1" OnRenderBrick="devGrid1Exporter_RenderBrick"></dx:ASPxGridViewExporter>
                </div>
                <div id="div2" style="width: 30%; float: right;">
                    <table class="InputTable" style="width: 100%;">
                        <colgroup>
                            <col style="width: 25%" />
                            <col style="width: 25%" />
                            <col style="width: 25%" />
                            <col style="width: 25%" />
                        </colgroup>
                        <tr>
                            <td class="tdTitle" colspan="4" style="text-align: center">분석결과</td>
                        </tr>
                        <tr>
                            <td class="tdTitle">최대치</td>
                            <td class="tdContent" id="tdResult01"></td>
                            <td class="tdTitle">최소치</td>
                            <td class="tdContent" id="tdResult02"></td>
                        </tr>
                        <tr>
                            <td class="tdTitle">x평균</td>
                            <td class="tdContent" id="tdResult03"></td>
                            <td class="tdTitle">범위</td>
                            <td class="tdContent" id="tdResult04"></td>
                        </tr>
                        <tr>
                            <td class="tdTitle">Cp</td>
                            <td class="tdContent" id="tdResult05"></td>
                            <td class="tdTitle">Cpk</td>
                            <td class="tdContent" id="tdResult06"></td>
                        </tr>
                        <tr>
                            <td class="tdTitle">표준편차</td>
                            <td class="tdContent" id="tdResult07"></td>
                            <td class="tdContent" colspan="2"></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div style="clear: both;"></div>
    </div>
    <div id="divHidden" style="display: none;">
        <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click" CausesValidation="false" />
        <dx:ASPxTextBox runat="server" ID="srcF_COMPCD" ClientInstanceName="srcF_COMPCD" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxTextBox runat="server" ID="srcF_FACTCD" ClientInstanceName="srcF_FACTCD" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxTextBox runat="server" ID="hidPageMode" ClientInstanceName="hidPageMode" ClientVisible="false">
        </dx:ASPxTextBox>
        <dx:ASPxSpinEdit runat="server" ID="hidChartWidth" ClientInstanceName="hidChartWidth" ClientVisible="false" NumberType="Integer" Number="1024">
        </dx:ASPxSpinEdit>
        <dx:ASPxSpinEdit runat="server" ID="hidChartHeight" ClientInstanceName="hidChartHeight" ClientVisible="false" NumberType="Integer" Number="768">
        </dx:ASPxSpinEdit>
    </div>
</asp:Content>
