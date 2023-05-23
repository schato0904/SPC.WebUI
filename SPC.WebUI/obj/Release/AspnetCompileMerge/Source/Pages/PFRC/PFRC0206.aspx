<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="PFRC0206.aspx.cs" Inherits="SPC.WebUI.Pages.PFRC.PFRC0206" %>
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
            else if (confirm('저장하시겠습니까?')) {
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

        // 전체선택/해제
        function fn_AllCheckedChange(id, checked) {
            var rowCount = devGrid.GetVisibleRowsOnPage();

            for (i = 0; i < rowCount; i++) {
                devGrid.batchEditApi.SetCellValue(i, 'F_STATUS', checked);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search"></div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false"
                KeyFieldName="F_COMPCD;F_FACTCD;F_BANCD;F_LINECD" EnableViewState="false" EnableRowsCache="false"
                OnBatchUpdate="devGrid_BatchUpdate" OnCustomCallback="devGrid_CustomCallback">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="Click" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="F_BANCD" Caption="반코드" Width="80px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_BANNM" Caption="반명" Width="200px">
                        <EditFormSettings Visible="False" />
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_LINECD" Caption="라인코드" Width="80px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="F_LINENM" Caption="라인명" Width="250px">
                        <EditFormSettings Visible="False" />
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataCheckColumn FieldName="F_STATUS" Caption="사용여부" Width="100px">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="F_ALLCheckBox" ClientInstanceName="F_ALLCheckBox" runat="server"
                                ToolTip="Select/Unselect all rows on the page" Text="사용여부" Font-Bold="true"
                                ClientSideEvents-CheckedChanged="function(s, e) { fn_AllCheckedChange('chkALL', s.GetChecked()); }">
                            </dx:ASPxCheckBox>
                        </HeaderTemplate>
                    </dx:GridViewDataCheckColumn>
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>