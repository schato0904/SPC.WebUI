<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="FITM0101.aspx.cs" Inherits="SPC.WebUI.Pages.FITM.FITM0101" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';

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
            fn_doSetGridEventAction('true');

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
            else
                devGrid.UpdateEdit();
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-2 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" />
                    </div>
                    <div class="col-sm-9 control-label">
                        초,중,종 검사예약을 하려면 검사기준의 초중종관리의 사용여부를 [사용]으로 선택해야합니다.
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_BANCD;F_LINECD;F_WORKCD" EnableViewState="false" EnableRowsCache="false"
                OnCustomCallback="devGrid_CustomCallback" OnBatchUpdate="devGrid_BatchUpdate"
                OnCellEditorInitialize="devGrid_CellEditorInitialize" OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Cell" BatchEditSettings-StartEditAction="Click" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataColumn FieldName="F_BANCD" Caption="반코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_BANNM" Caption="반명" Width="120px">
                        <EditFormSettings Visible="False" />
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LINECD" Caption="라인코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="120px">
                        <EditFormSettings Visible="False" />
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_WORKCD" Caption="공정코드" Visible="false" />
                    <dx:GridViewDataColumn FieldName="F_WORKNM" Caption="공정명" Width="180px">
                        <EditFormSettings Visible="False" />
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewBandColumn Name="F_PGMNM" Caption="예약정보">
                        <Columns>
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE01" Caption="08" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE02" Caption="09" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE03" Caption="10" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE04" Caption="11" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE05" Caption="12" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE06" Caption="13" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE07" Caption="14" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE08" Caption="15" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE09" Caption="16" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE10" Caption="17" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE11" Caption="18" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE12" Caption="19" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE13" Caption="20" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE14" Caption="21" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE15" Caption="22" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE16" Caption="23" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE17" Caption="24" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE18" Caption="01" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE19" Caption="02" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE20" Caption="03" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE21" Caption="04" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE22" Caption="05" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE23" Caption="06" Width="30px" />
                            <dx:GridViewDataComboBoxColumn FieldName="F_TIMEZONE24" Caption="07" Width="30px" />
                        </Columns>
                    </dx:GridViewBandColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        <div class="paging"></div>
    </div>
    </div>
</asp:Content>
