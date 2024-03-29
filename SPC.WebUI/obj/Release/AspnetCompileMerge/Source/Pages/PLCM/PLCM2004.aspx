﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="PLCM2004.aspx.cs" Inherits="SPC.WebUI.Pages.PLCM.PLCM2004" %>
<%-- 작업지시서 조회 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var totngcnt = 0;
        var _fieldList = "";
        var key;
        var chartWidth = 0;
        var chartHeight1 = 0;
        var chartResized1 = false;

        $(document).ready(function () {
            SetClear();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            chartWidth = parseInt($(".container1").width(), 10) - 20;

            if (this.resizeTO) clearTimeout(this.resizeTO);
            this.resizeTO = setTimeout(function () {
                $(this).trigger('resizeEnd');
            }, 500);

            fnASPxGridView_ReHeight(devGrid);
            //fnASPxGridView_ReHeight(devGrid1);
            devGrid1.SetHeight(400);
            document.getElementById("divRight").style.height = devGrid.GetHeight()+'px';
            
        }

        $(window).bind('resizeEnd', function () {
            fn_OnChartResized(devChart1, chartResized1, chartWidth, 120);
            fn_OnChartResized(devChart2, chartResized1, chartWidth, 120);
            fn_OnChartResized(devChart3, chartResized1, chartWidth, 120);
            fn_OnChartResized(devChart4, chartResized1, chartWidth, 120);
            fn_OnChartResized(devChart5, chartResized1, chartWidth, 120);
            fn_OnChartResized(devChart6, chartResized1, chartWidth, 120);
            fn_OnChartResized(devChart7, chartResized1, chartWidth, 120);
            fn_OnChartResized(devChart8, chartResized1, chartWidth, 120);
            fn_OnChartResized(devChart9, chartResized1, chartWidth, 120);
            fn_OnChartResized(devChart10, chartResized1, chartWidth, 120);
        });

        // 조회
        function fn_OnSearchClick() {

            if (schF_MASTERID.GetValue() == "" || schF_MASTERID.GetValue() == null) {
                alert("마스터ID를 선택하세요!!");
                return false;
            }
            var s = 0;
            for (var i = 0; i < devGrid.GetVisibleRowsOnPage() ; i++) {
                s += devGrid.batchEditApi.GetCellValue(i, 'F_USEYN');
            }
            if (s == 0) {
                alert("검사항목을 한개이상 선택하세요.");
                return false;
            }

            devGrid1.PerformCallback('GET');
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
            schF_FROMYMD.SetValue(new Date());
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

        // 선택항목 조회(좌측 키로 우측 조회)
        function ViewSelectedItem(pkey) {
            // 콜백으로 우측영역값 json으로 조회
            // 우측영역 세팅
            //pkey = pkey.split('|');
            //var param = {
            //    'action': 'VIEW',
            //    'F_COMPCD': pkey[0],
            //    'F_FACTCD': pkey[1],
            //    'F_WORKNO': pkey[2]
            //};
            //devCallback.PerformCallback(encodeURIComponent(JSON.stringify(param)));
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
                case devGrid:
                    break;
                case devGrid1:
                    var oParams = chartWidth + '|' + 120;
                    devChart1.PerformCallback(oParams);
                    devChart2.PerformCallback(oParams);
                    devChart3.PerformCallback(oParams);
                    devChart4.PerformCallback(oParams);
                    devChart5.PerformCallback(oParams);
                    devChart6.PerformCallback(oParams);
                    devChart7.PerformCallback(oParams);
                    devChart8.PerformCallback(oParams);
                    devChart9.PerformCallback(oParams);
                    devChart10.PerformCallback(oParams);
                    SetChartSize(chartWidth, 120);
                    break;
            }
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
                                result.data = JSON.parse(decodeURIComponent(result.data||'{}'));
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
         
        function devGrid1_RowDblClick(s, e) {
            //var key = s.GetRowKey(e.visibleIndex);
            //ViewSelectedItem(key);
        }

        function fn_schF_MACHCD_ValueChanged(s, e)
        {
            if (s.GetValue() != null) {
                schF_RECIPID.PerformCallback();
                schF_MASTERID.PerformCallback();
                //devGrid1.PerformCallback('clear');
                //devGrid.PerformCallback();
                schF_STEP.PerformCallback();
                schF_STEP2.PerformCallback();
            }
        }

        function fn_schF_FROMYMD_ValueChanged(s, e) {
            if (schF_MACHCD.GetValue() != null) {
                schF_RECIPID.PerformCallback();
                schF_MASTERID.PerformCallback();
            }
        }

        function fn_schF_STEP_ValueChanged(s, e) {
            devGrid.PerformCallback();
        }

        function fn_F_USEYN_changed(s, e) {
            //alert(s.GetValue());
            devGrid.UpdateEdit();
            devGrid1.PerformCallback('clear');
        }

        function fn_schF_RECIPID_ValueChanged(s, e) {
            schF_MASTERID.PerformCallback();
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
            <div id="divMiddle" style="float:left;margin-bottom:2px;width:100%;">
                <table class="InputTable" style="width:100%;">
                    <colgroup>
                        <col style="width: 5%" />
                        <col style="width: 10%" />
                        <col style="width: 6%" />
                        <col style="width: 10%" />
                        <col style="width: 5%" />
                        <col style="width: 14%" />
                        <col style="width: 7%" />
                        <col style="width: 18%" />
                        <col style="width: 7%" />
                        <col style="width: 18%" />
                       <%-- <col style="width: 4%" />
                        <col style="width: 8%" />
                        <col style="width: 6%" />
                        <col style="width: 10%" />
                        <col style="width: 5%" />
                        <col style="width: 12%" />
                        <col style="width: 7%" />
                        <col style="width: 15%" />
                        <col style="width: 7%" />
                        <col style="width: 15%" />
                        <col style="width: 4%" />
                        <col style="width: 7%" />--%>
                    </colgroup>
                    <tr>
                        <td class="tdTitle">설비</td>
                        <td class="tdContent">
                            <div style="width:100%;float:left;">
                                <dx:ASPxComboBox runat="server" ID="schF_MACHCD" ClientInstanceName="schF_MACHCD" IncrementalFilteringMode="None" ValueType="System.String" Width="100%" >
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                    <ClientSideEvents ValueChanged="fn_schF_MACHCD_ValueChanged" />
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                        <td class="tdTitle">작업일자</td>
                        <td class="tdContent">
                            <div style="width:70%;float:left;">
                                <dx:ASPxDateEdit runat="server" ID="schF_FROMYMD" ClientInstanceName="schF_FROMYMD" UseMaskBehavior="true" 
                                    EditFormat="Custom" CaptionSettings-ShowColon="false" CaptionStyle-ForeColor="White" 
                                    Width="100%" AllowNull="true" >
                                    <ClientSideEvents DateChanged="fn_schF_FROMYMD_ValueChanged" />
                                </dx:ASPxDateEdit>
                            </div>
                        </td>
                        <td class="tdTitle">레시피</td>
                        <td class="tdContent">
                            <dx:ASPxComboBox runat="server" ID="schF_RECIPID" ClientInstanceName="schF_RECIPID" IncrementalFilteringMode="None" ValueType="System.String" OnCallback="schF_RECIPID_Callback" Width="100%" >
                                <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                <ClientSideEvents ValueChanged="fn_schF_RECIPID_ValueChanged" />
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdTitle">마스터ID</td>
                        <td class="tdContent">
                            <dx:ASPxComboBox runat="server" ID="schF_MASTERID" ClientInstanceName="schF_MASTERID" IncrementalFilteringMode="None" ValueType="System.String"  OnCallback="schF_MASTERID_Callback" Width="100%">
                                <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdTitle">STEP</td>
                        <td class="tdContent">
                            <div style="width:49%;float:left;">
                                <dx:ASPxComboBox runat="server" ID="schF_STEP" ClientInstanceName="schF_STEP" IncrementalFilteringMode="None" ValueType="System.String"  OnCallback="schF_STEP_Callback" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                    <ClientSideEvents ValueChanged="fn_schF_STEP_ValueChanged" />
                                </dx:ASPxComboBox>
                            </div>
                            <div style="width:2%;float:left;">
                             ~
                           </div>
                            <div style="width:49%;float:left;">
                                <dx:ASPxComboBox runat="server" ID="schF_STEP2" ClientInstanceName="schF_STEP2" IncrementalFilteringMode="None" ValueType="System.String"  OnCallback="schF_STEP2_Callback" Width="100%">
                                    <DisabledStyle BackColor="#EEEEEE" ForeColor="#222222"></DisabledStyle>
                                    <ClientSideEvents ValueChanged="fn_schF_STEP_ValueChanged" />
                                </dx:ASPxComboBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
         </div>
        <div style="clear:both;height:2px;"></div>
        <div id="divLeft" style="width:20%;float:left;">
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                    KeyFieldName="F_MACHCD;F_MEAINSPCD" EnableViewState="false" EnableRowsCache="false"
                    OnCustomCallback="devGrid_CustomCallback" OnBatchUpdate="devGrid_BatchUpdate"
                    OnDataBinding="devGrid_DataBinding">
                    <Styles>
                        <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated"></Cell>
                        <EditForm CssClass="bg-default"></EditForm>
                    </Styles>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Hidden" />
                    <SettingsBehavior AllowSort="false" ColumnResizeMode="NextColumn" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" AllowDragDrop="false" />
                    <SettingsPager Mode="ShowAllRecords"/>
                    <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                    <ClientSideEvents Init="fn_OnGridInit"
                        EndCallback="fn_OnGridEndCallback"
                        CallbackError="fn_OnCallbackError"
                        RowDblClick="devGrid1_RowDblClick"/>
                    <Templates>
                        <StatusBar>
                            <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                            <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount"  runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                        </StatusBar>
                    </Templates>
                    <Columns>
                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="40px" EditFormSettings-Visible="False"/>
                        <dx:GridViewDataTextColumn FieldName="F_INSPDETAIL" Caption="검사항목명" EditFormSettings-Visible="False" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataCheckColumn FieldName="F_USEYN" Caption="선택" Width="60px" PropertiesCheckEdit-ClientSideEvents-ValueChanged="fn_F_USEYN_changed">
                        </dx:GridViewDataCheckColumn>
                    </Columns>
                </dx:ASPxGridView>
        </div>
        <div id="divSpace" style="width:1%;float:left;">&nbsp;</div>
        <div id="divRight" style="width:79%;float:left;overflow-y:auto;" class="container1">
            <div id="divRightBody" style="width:100%;">
                <dx:WebChartControl ID="devChart1" ClientInstanceName="devChart1" runat="server" OnCustomCallback="devChart1_CustomCallback"
                    ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="120px">
                    <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" />
                </dx:WebChartControl>
                <dx:WebChartControl ID="devChart2" ClientInstanceName="devChart2" runat="server" OnCustomCallback="devChart2_CustomCallback"
                    ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="120px">
                    <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" />
                </dx:WebChartControl>
                <dx:WebChartControl ID="devChart3" ClientInstanceName="devChart3" runat="server" OnCustomCallback="devChart3_CustomCallback"
                    ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="120px">
                    <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" />
                </dx:WebChartControl>
                <dx:WebChartControl ID="devChart4" ClientInstanceName="devChart4" runat="server" OnCustomCallback="devChart4_CustomCallback"
                    ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="120px">
                    <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" />
                </dx:WebChartControl>
                <dx:WebChartControl ID="devChart5" ClientInstanceName="devChart5" runat="server" OnCustomCallback="devChart5_CustomCallback"
                    ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="120px">
                    <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" />
                </dx:WebChartControl>
                <dx:WebChartControl ID="devChart6" ClientInstanceName="devChart6" runat="server" OnCustomCallback="devChart6_CustomCallback"
                    ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="120px">
                    <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" />
                </dx:WebChartControl>
                <dx:WebChartControl ID="devChart7" ClientInstanceName="devChart7" runat="server" OnCustomCallback="devChart7_CustomCallback"
                    ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="120px">
                    <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" />
                </dx:WebChartControl>
                <dx:WebChartControl ID="devChart8" ClientInstanceName="devChart8" runat="server" OnCustomCallback="devChart8_CustomCallback"
                    ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="120px">
                    <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" />
                </dx:WebChartControl>
                <dx:WebChartControl ID="devChart9" ClientInstanceName="devChart9" runat="server" OnCustomCallback="devChart9_CustomCallback"
                    ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="120px">
                    <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" />
                </dx:WebChartControl>
                <dx:WebChartControl ID="devChart10" ClientInstanceName="devChart10" runat="server" OnCustomCallback="devChart10_CustomCallback"
                    ToolTipEnabled="False" ViewStateMode="Disabled" EnableViewState="false" Height="120px">
                    <ClientSideEvents EndCallback="function(s, e) { chartResized1 = false; fn_OnChartEndCallback(s, e); }" />
                </dx:WebChartControl>
                <div id="divgrid">
                    <dx:ASPxGridView ID="devGrid1" ClientInstanceName="devGrid1" runat="server" AutoGenerateColumns="true" Width="100%"
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
                            CallbackError="fn_OnCallbackError"
                            RowDblClick="devGrid1_RowDblClick"/>
                        <Templates>
                            <StatusBar>
                                <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# string.Format("{0} : ", "조회 건 수") %>'></dx:ASPxLabel>
                                <dx:ASPxLabel ID="lblCount" ClientInstanceName="lblCount"  runat="server" Text='<%# (Container as DevExpress.Web.GridViewStatusBarTemplateContainer).Grid.VisibleRowCount.ToString("#,0") %>'></dx:ASPxLabel>
                            </StatusBar>
                        </Templates>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="devGrid1Exporter" runat="server" GridViewID="devGrid1" OnRenderBrick="devGrid1Exporter_RenderBrick"></dx:ASPxGridViewExporter>
                  </div>
            </div>
        </div>
        <div style="clear:both;"></div>
    </div>
    <div id="divHidden" style="display:none;">
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