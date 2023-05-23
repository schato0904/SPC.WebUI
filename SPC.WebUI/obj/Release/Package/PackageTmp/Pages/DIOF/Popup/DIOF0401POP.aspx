<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="DIOF0401POP.aspx.cs" Inherits="SPC.WebUI.Pages.DIOF.Popup.DIOF0401POP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var sSTATUS = '';

        $(document).ready(function () {
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
                if (!gridIsValid)
                    alert('입력값을 확인해보세요');
                else {
                }
            }
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
            gridValids = [];
            gridValidIdx = 0;
            gridIsValid = true;

            gridValids[gridValidIdx++] = fn_OnBatchValidate("F_STATUS", s, e);
            gridValids[gridValidIdx++] = fn_OnBatchValidate("F_NGREMK", s, e);

            if (sSTATUS == 'AAG902') {
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_RESPTYPE", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_RESPDT", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_RESPUSER", s, e);
                gridValids[gridValidIdx++] = fn_OnBatchValidate("F_RESPREMK", s, e);
            }

            gridValids.forEach(function (gridValid) {
                if (!gridValid) {
                    gridIsValid = false;
                }
            });

            return gridIsValid;
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            sSTATUS = '';
        }

        // 진행상태 변경 시
        function fn_STATUSSelectedIndexChanged(s, e) {
            sSTATUS = s.GetValue();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <dx:ASPxTextBox ID="srcF_MACHIDX" ClientInstanceName="srcF_MACHIDX" runat="server" ClientVisible="false" />
            <table class="contentTable">
                <colgroup>
                    <col style="width:70px;" />
                    <col style="width:130px;" />
                    <col style="width:70px;" />
                    <col style="width:130px;" />
                    <col style="width:70px;" />
                    <col />
                </colgroup>
                <tr>
                    <td class="tdLabel">점검일</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_MEASYMD" ClientInstanceName="srcF_MEASYMD" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">설비코드</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_MACHCD" ClientInstanceName="srcF_MACHCD" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdLabel">설비명</td>
                    <td class="tdInput">
                        <dx:ASPxTextBox ID="srcF_MACHNM" ClientInstanceName="srcF_MACHNM" runat="server" Width="100%">
                            <ClientSideEvents Init="fn_OnControlToLabel" GotFocus="fn_OnControlToLabel" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
            <div class="divPadding"></div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="srcF_ROWKEYS" ClientInstanceName="srcF_ROWKEYS" runat="server" ClientVisible="false" />
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_INSPIDX;F_NUMBER" EnableViewState="false" EnableRowsCache="false"
                OnCellEditorInitialize="devGrid_CellEditorInitialize">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" HorizontalScrollBarMode="Auto" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="Click" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                    BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_INSPNM" Caption="점검항목" Width="150px">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_NUMBER" Caption="차수" Width="40px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_STATUS" Caption="진행상태" Width="80px">
                        <PropertiesComboBox ConvertEmptyStringToNull="true">
                            <ClientSideEvents SelectedIndexChanged="fn_STATUSSelectedIndexChanged" />
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="F_NGREMK" Caption="이상내역" Width="250px" />
                    <dx:GridViewDataComboBoxColumn FieldName="F_RESPTYPE" Caption="조치유형" Width="80px">
                        <PropertiesComboBox ConvertEmptyStringToNull="true" />
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataDateColumn FieldName="F_RESPDT" Caption="조치일자" Width="90">
                        <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd" />
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="F_RESPUSER" Caption="조치자" Width="70px" />
                    <dx:GridViewDataTextColumn FieldName="F_RESPREMK" Caption="조치내역" Width="250px" />

                    <dx:GridViewDataColumn FieldName="F_INSPIDX" Visible="false" />
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid"></dx:ASPxGridViewExporter>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>