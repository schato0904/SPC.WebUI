<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="WERD0102.aspx.cs" Inherits="SPC.WebUI.Pages.WERD.WERD0102" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .WORKGRID {
            display:none;
        }
    </style>
    <script type="text/javascript">
        var controlID = 'cphBody_devGrid_efnew_';
        var editRowIndex;

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
            devGrid.AddNewRow();
        }

        // 수정
        function fn_OnModifyClick() {
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('수정할 데이타를 선택하세요!!');
                return false;
            }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.StartEditRowByKey(selectedKeys[i]);
            }
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
            selectedKeys = devGrid.GetSelectedKeysOnPage();

            // 1 Row 반드시 선택
            if (selectedKeys.length <= 0) {
                alert('삭제할 데이타를 선택하세요!!');
                return false;
            }

            if (!confirm('선택한 데이타를 삭제하시겠습니까?')) { return false; }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.DeleteRowByKey(selectedKeys[i]);
            }

            devGrid.UpdateEdit();
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
                } else if (s.cpResultCode == '2') {
                    fn_doSetGridEventAction('true');
                    alert(s.cpResultMsg);

                    s.cpResultCode = "";
                    s.cpResultMsg = "";
                    devGrid.PerformCallback();
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

        // 검색된 품목 세팅
        function fn_OnSettingItem(CODE, TEXT, MODEL) {
            devGrid.StartEditRow(editRowIndex);
            devGrid.GetEditor('F_ITEMCD').SetText(CODE);
            devGrid.GetEditor('F_ITEMNM').SetText(TEXT);
        }

        // Validate
        function fn_OnValidate(s, e) {
            fn_OnBatchValidate("F_WORKCD", s, e);
            fn_OnBatchValidate("F_ITEMCD", s, e);
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            //editRowIndex = parseInt(e.visibleIndex, 10);
            //if (editRowIndex >= 0) {
            //    fn_OnControlEnableComboBox(s.GetEditor('F_WORKCD'), false);
            //    fn_OnControlDisableBox(s.GetEditor('F_WORKNM'), null);
            //    fn_OnControlDisableBox(s.GetEditor('F_ITEMCD'), null);
            //    fn_OnControlDisableBox(s.GetEditor('F_ITEMNM'), null);

            //} else {
            //    fn_OnControlEnableComboBox(s.GetEditor('F_WORKCD'), true);
            //    fn_OnControlEnableBox(s.GetEditor('F_WORKNM'), null);
            //    fn_OnControlEnableBox(s.GetEditor('F_ITEMCD'), null);
            //    fn_OnControlEnableBox(s.GetEditor('F_ITEMNM'), null);
            //}
            fn_OnControlEnableComboBox(s.GetEditor('F_WORKCD'), false);
            fn_OnControlDisableBox(s.GetEditor('F_WORKNM'), null);
            fn_OnControlDisableBox(s.GetEditor('F_ITEMCD'), null);
            fn_OnControlDisableBox(s.GetEditor('F_ITEMNM'), null);
        }

        // BatchEditEndEditing
        function fn_OnBatchEditEndEditing(s, e) {
            //var subColumn = s.GetColumnByField("F_LINECD");
            //if (!e.rowValues.hasOwnProperty(subColumn.index))
            //    return;
            //var cellInfo = e.rowValues[subColumn.index];
            //if (ddlLINEEdit.GetSelectedIndex() > -1 || cellInfo.text != ddlLINEEdit.GetText()) {
            //    cellInfo.value = ddlLINEEdit.GetValue();
            //    cellInfo.text = ddlLINEEdit.GetText();
            //    ddlLINEEdit.SetValue(null);
            //}
        }

        function fn_OnWORKCDEditorSelectedIndexChanged(s, e) {
            devGrid.GetEditor("F_WORKNM").SetText(s.GetText());
        }
        // Edit Form 의 반 선택값이 바뀌는 경우
        function fn_OnBANCDEditorSelectedIndexChanged(s, e) {
            ddlLINEEdit.PerformCallback(s.GetValue() + '|');
            devGrid.GetEditor("F_BANNM").SetText(s.GetText());
        }

        function fn_OnLINECDEditorSelectedIndexChanged(s, e) {
            devGrid.GetEditor("F_LINENM").SetText(s.GetText());

            var P_BANCD = IsNull(devGrid.GetEditor("F_BANCD").GetValue(), '');
            var P_LINECD = IsNull(ddlLINEEdit.GetValue(), '');
        }

        function ISNULL(val, val2) {
            var returnval;
            if (val == null) {
                returnval = val2
            }
            return returnval;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-1 control-label">반</label>
                    <div class="col-sm-1 control-label">
                        <ucCTF:Ban ID="ucBan" runat="server" targetCtrls="ddlLINE" />
                    </div>
                    <label class="col-sm-1 control-label">라인</label>
                    <div class="col-sm-1 control-label">
                        <ucCTF:Line ID="ucLine" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="False" 
                KeyFieldName="F_WORKCD;F_ITEMCD" EnableViewState="False" EnableRowsCache="False"
                OnBatchUpdate="devGrid_BatchUpdate" OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCellEditorInitialize="devGrid_CellEditorInitialize" OnDataBound="devGrid_DataBound" >
                
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" ShowStatusBar="Visible" />
                <SettingsBehavior AllowSort="false" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="DblClick" />
                <ClientSideEvents Init="fn_OnGridInit" EndCallback="fn_OnGridEndCallback" CallbackError="fn_OnCallbackError"
                    BatchEditRowValidating="fn_OnValidate" BatchEditStartEditing="fn_OnBatchEditStartEditing" BatchEditEndEditing="fn_OnBatchEditEndEditing" />
                <Templates>
                    <StatusBar>
                        <ucCTF:Count id="ucCount" runat="server" targetCtrl="devGrid" />
                    </StatusBar>
                </Templates>
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                ClientSideEvents-CheckedChanged="function(s, e) { devGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_WORKCD" Name="F_WORKCD" Caption="공정코드" Width="100px">
                        <Settings AllowSort="True" />
                        <PropertiesComboBox>
                            <ClientSideEvents SelectedIndexChanged="fn_OnWORKCDEditorSelectedIndexChanged"  />
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="F_WORKNM" Name="F_WORKNM" Caption="공정명" Width="150px" ReadOnly="true" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn FieldName="F_ITEMCD" Name="F_ITEMCD" Caption="품목코드" Width="150px">
                        <Settings AllowSort="True" />                        
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_ITEMNM" Name="F_ITEMNM" Caption="품목명" Width="200px">
                        <Settings AllowSort="True" />                        
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="F_LOSSAMT" Name="F_LOSSAMT" Caption="단가" Width="90px">
                        <Settings AllowSort="True" />                        
                        <CellStyle HorizontalAlign="Right" />
                    </dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
