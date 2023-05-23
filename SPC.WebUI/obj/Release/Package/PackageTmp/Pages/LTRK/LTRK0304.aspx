<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="LTRK0304.aspx.cs" Inherits="SPC.WebUI.Pages.LTRK.LTRK0304" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var isChanged = false;

        $(document).ready(function () {
            fn_OnSearchClick();
            fn_RendorTotalCount();
        });

        // 동적으로 높이값을 계산한다
        function fn_AdjustSize() {
            var top = $(".container").offset().top;
            var searchHeight = $(".search").height() > 0 ? $(".search").height() + 5 : 0;
            var pagingHeight = $(".paging").height() > 0 ? $(".paging").height() - 20 : 0;
            var height = Math.max(0, parseInt($(document).height() - _hMargin - top - searchHeight - pagingHeight - scrollbarWidth(), 10));
            devGrid.SetHeight(height);
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
            if (!devGrid.batchEditApi.HasChanges())
                alert('변경된 사항이 없습니다');
            else {
                if (!confirm('실사조정 내역을 저장하겠습니까?\r계속 진행하려면 확인을 누르세요!'))
                    fn_OnCancelClick();
                else
                    devGrid.UpdateEdit();
            }
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

        // Grid Callback Error
        function fn_OnCallbackError(s, e) {
            e.handled = true; if (e.message == 'isLogOut') { top.location.href = '<%=Page.ResolveClientUrl("~/Pages/ERROR/401.aspx")%>'; } else { alert(e.message); }
        }

        // Validate
        function fn_OnValidate(s, e) {
        }

        function fn_OnCloseUp(s, e) {

        }

        function fn_OnKeyUp(s, e) {
            s.GetInputElement().blur();
        }

        function fn_OnDateChanged(s, e) {
            if (!isChanged) {
                var data = {
                    'ACTION': 'CLOSECHK',
                    'F_DATE': s.GetText()
                };

                fn_devCallback_PerformCallback(data);
            }

            if (isChanged) isChanged = false;
        }

        // CallbackPanel Event Handler 시작
        var devCallback_parameter = null;   // 대기작업 변수
        // 대기작업 처리를 위해 PerformCallback을 별도로 처리
        function fn_devCallback_PerformCallback(parameter) {
            // devCallback이 실행 중일 경우, EndCallback까지 대기
            if (devCallback.InCallback()) {
                devCallback_parameter = parameter;
            } else {
                devCallback.PerformCallback(JSON.stringify(parameter));
            }
        }

        // 콜백 성공시 처리 : 성공시 PKEY에 JSON 구조로 키값을 받는다.
        function fn_devCallback_CallbackComplete(s, e) {
            var result = JSON.parse(e.result);
            var parameter = JSON.parse(e.parameter);
            var isOK = typeof (result.ISOK) != 'undefined' ? result.ISOK : null;
            var msg = typeof (result.MSG) != 'undefined' ? result.MSG : null;
            if (isOK) {
                var pkey = typeof (result.PKEY) != 'undefined' ? result.PKEY : null;
                var data = {};
                if (typeof (result.PAGEDATA) != 'undefined') {
                    data = result.PAGEDATA;
                } else {
                    for (var key in pkey) { data[key] = pkey[key]; }
                }
                var action = typeof (parameter.ACTION) != 'undefined' ? parameter.ACTION : null;

                switch (action) {
                    case "CLOSECHK":
                        if (msg != '') {
                            alert(msg);
                            isChanged = true;
                            fn_SetDate('srcF_DATE', new Date());
                        } else {
                            fn_OnSearchClick();
                        }
                        break;
                }
            } else {
                alert(msg);
                isChanged = true;
                fn_SetDate('srcF_DATE', new Date());
            }
        }

        // 콜백 종료시 처리
        function fn_devCallback_EndCallback(s, e) {
            // 대기중인 작업이 있을경우, 콜백 실행
            if (devCallback_parameter) {
                devCallback.PerformCallback(JSON.stringify(devCallback_parameter));
                devCallback_parameter = null;
            }
        }
        // CallbackPanel Event Handler   끝
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <table class="contentTable">
                <colgroup>
                    <col style="width:80px;" />
                    <col style="width:120px;" />
                    <col style="width:80px;" />
                    <col style="width:150px;" />
                    <col style="width:80px;" />
                    <col style="width:180px;" />
                    <col style="width:80px;" />
                    <col />
                    <col style="width:300px;" />
                </colgroup>
                <tr>
                    <td class="tdLabel">일자</td>
                    <td class="tdInput">
                        <dx:ASPxDateEdit ID="srcF_DATE" ClientInstanceName="srcF_DATE" runat="server" Width="100%" Theme="MetropolisBlue"
                            DisplayFormatString="yyyy-MM-dd" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="yyyy-MM-dd" AllowMouseWheel="false"
                            class="form-control input-sm">
                            <ClientSideEvents CloseUp="fn_OnCloseUp" DateChanged="fn_OnDateChanged" KeyUp="fn_OnKeyUp" />
                        </dx:ASPxDateEdit>
                    </td>
                    <td class="tdLabel">품목분류</td>
                    <td class="tdInput">
                        <dx:ASPxComboBox ID="ddlF_GUBN" ClientInstanceName="ddlF_GUBN" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton" />
                    </td>
                    <td class="tdLabel">재고유무</td>
                    <td class="tdInput">
                        <dx:ASPxComboBox ID="ddlF_REAMIN" ClientInstanceName="ddlF_REAMIN" runat="server" Width="100%"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <Items>
                                <dx:ListEditItem Text="전체" Value="" Selected="true" />
                                <dx:ListEditItem Text="재고있음(전체)" Value="1" />
                                <dx:ListEditItem Text="재고있음(안정재고이상)" Value="2" />
                                <dx:ListEditItem Text="재고있음(안정재고미달)" Value="3" />
                                <dx:ListEditItem Text="재고없음" Value="0" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdLabel">품목</td>
                    <td class="tdInput tdContentR">
                        <ucCTF:Item ID="ucItem" runat="server" />
                    </td>
                    <td class="tdInput tdContentL"></td>
                </tr>
            </table>
            <div class="divPadding"></div>
            <div style="color:black;background-color:white;border:1px solid #808080;padding:5px;">
                1. 실사조정할 품목의 조정수량을 입력 후 저장을 누르면 실사조정내역이 저장됩니다.<br />
                2. <span style="font-weight:bold;">실사수량은 재고수량의 <span style="color:blue;font-weight:bold;">실 수량</span>을 적으세요.(예: 재고수량이 100 이고 실재고수량 130 인 경우 실사수량에 130 입력)</span><br />
                <span style="color:blue;font-weight:bold;">* 이미 마감된 월의 실사조정은 마감취소 후 가능합니다</span>
            </div>
            <div class="divPadding"></div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_ITEMCD;F_REMAINCNT" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnHtmlDataCellPrepared="devGrid_HtmlDataCellPrepared" OnBatchUpdate="devGrid_BatchUpdate">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Caption="품번" Width="150px" EditFormSettings-Visible="False" />
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Caption="품명" EditFormSettings-Visible="False">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_GUBNNM" Caption="품목분류" Width="150px" EditFormSettings-Visible="False" />
                    <dx:GridViewDataSpinEditColumn FieldName="F_DANGA" Caption="안정재고" Width="100px" EditFormSettings-Visible="False">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_BASECNT" Caption="이월수량" Width="100px" EditFormSettings-Visible="False">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_INCNT" Caption="입고수량" Width="100px" EditFormSettings-Visible="False">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_OUTCNT" Caption="출고수량" Width="100px" EditFormSettings-Visible="False">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_REMAINCNT" Caption="재고수량" Width="120px" EditFormSettings-Visible="False">
                        <CellStyle HorizontalAlign="Right" Font-Bold="true"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="F_CHANGECNT" Caption="실사수량" Width="120px">
                        <CellStyle HorizontalAlign="Right" Font-Bold="true"></CellStyle>
                        <PropertiesSpinEdit DisplayFormatString="#,##0.#" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataColumn FieldName="F_UNITNM" Caption="단위" Width="120px" EditFormSettings-Visible="False" />
                    <dx:GridViewDataTextColumn Caption="수불현황" UnboundType="String" Width="120px" EditFormSettings-Visible="False">
                        <DataItemTemplate>
                            <dx:ASPxButton AutoPostBack="false" ID="btnLink" runat="server" Text="수불현황" OnInit="btnLink_Init" />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging"></div>
    </div>
    <dx:ASPxCallback ID="devCallback" ClientInstanceName="devCallback" runat="server"
        OnCallback="devCallback_Callback">
        <ClientSideEvents 
            CallbackComplete="fn_devCallback_CallbackComplete" 
            EndCallback="fn_devCallback_EndCallback" 
            CallbackError="fn_OnCallbackError" />
    </dx:ASPxCallback>
</asp:Content>