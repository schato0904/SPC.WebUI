<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="BSIF0206.aspx.cs" Inherits="SPC.WebUI.Pages.BSIF.BSIF0206" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .WORKGRID {
            display:none;
        }
    </style>
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

            if (!confirm('선택한 데이타를 삭제하시겠습니까?\r삭제후 반드시 저장버튼을 눌러야 삭제가 완료됩니다.')) { return false; }

            for (var i = 0; i < selectedKeys.length ; i++) {
                devGrid.DeleteRowByKey(selectedKeys[i]);
            }
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
            fn_OnBatchValidate("F_BANCD", s, e);
            fn_OnBatchValidate("F_LINECD", s, e);
            fn_OnBatchValidate("F_NGCODE", s, e);
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                var P_BANCD = devGrid.batchEditApi.GetCellValue(e.visibleIndex, 'F_BANCD');
                var P_LINECD = devGrid.batchEditApi.GetCellValue(e.visibleIndex, 'F_LINECD');
                var oParam = P_BANCD + '|' + P_LINECD;
                ddlLINEEdit.PerformCallback(oParam);

                fn_OnControlEnableComboBox(s.GetEditor('F_BANCD'), false);
                fn_OnControlDisableBox(s.GetEditor('F_BANNM'), null);
                fn_OnControlEnableComboBox(ddlLINEEdit, false);
                fn_OnControlDisableBox(s.GetEditor('F_LINENM'), null);
                fn_OnControlEnableComboBox(s.GetEditor('F_NGCODE'), false);

            } else {
                fn_OnControlEnableComboBox(s.GetEditor('F_BANCD'), true);
                fn_OnControlEnableBox(s.GetEditor('F_BANNM'), null);
                fn_OnControlEnableComboBox(ddlLINEEdit, true);
                fn_OnControlEnableBox(s.GetEditor('F_LINENM'), null);
                fn_OnControlEnableComboBox(s.GetEditor('F_NGCODE'), true);
            }
        }

        // BatchEditEndEditing
        function fn_OnBatchEditEndEditing(s, e) {
            var subColumn = s.GetColumnByField("F_LINECD");
            if (!e.rowValues.hasOwnProperty(subColumn.index))
                return;
            var cellInfo = e.rowValues[subColumn.index];
            if (ddlLINEEdit.GetSelectedIndex() > -1 || cellInfo.text != ddlLINEEdit.GetText()) {
                cellInfo.value = ddlLINEEdit.GetValue();
                cellInfo.text = ddlLINEEdit.GetText();
                ddlLINEEdit.SetValue(null);
            }
        }

        // Edit Form 의 반 선택값이 바뀌는 경우
        function fn_OnBANCDEditorSelectedIndexChanged(s, e) {
            ddlLINEEdit.PerformCallback(s.GetValue() + '|');
            //devGrid.GetEditor("F_BANNM").SetText(s.GetText());
        }

        function fn_OnLINECDEditorSelectedIndexChanged(s, e) {
            //devGrid.GetEditor("F_LINENM").SetText(s.GetText());
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
                KeyFieldName="F_BANCD;F_LINECD;F_NGCODE" EnableViewState="False" EnableRowsCache="False"
                OnBatchUpdate="devGrid_BatchUpdate" OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback" OnDataBound="devGrid_DataBound"
                OnHtmlRowPrepared="devGrid_HtmlRowPrepared"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCellEditorInitialize="devGrid_CellEditorInitialize" >
                
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
                    <dx:GridViewDataComboBoxColumn FieldName="F_BANCD" Name="F_BANCD" Caption="반코드" Width="70px">
                        <Settings AllowSort="True" />
                        <PropertiesComboBox>
                            <ClientSideEvents SelectedIndexChanged="fn_OnBANCDEditorSelectedIndexChanged"   />
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>                    
                    <dx:GridViewDataTextColumn FieldName="F_BANNM" Name="F_BANNM" Caption="반명" Width="120px" ReadOnly="true" >
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_LINECD" Name="F_LINECD" Caption="라인코드" Width="70px" >
                        <EditItemTemplate>
                            <dx:ASPxComboBox ID="ddlLINEEdit" ClientInstanceName="ddlLINEEdit" runat="server" Width="100%"
                                OnCallback="ddlLINEEdit_Callback" OnDataBound="ddlLINEEdit_DataBound"
                                EnableCallbackMode="true" IncrementalFilteringMode="None" CssClass="NoXButton">
                                <ClientSideEvents Init="fn_OnControlDisable" SelectedIndexChanged="fn_OnLINECDEditorSelectedIndexChanged" />
                            </dx:ASPxComboBox>
                        </EditItemTemplate>
                    </dx:GridViewDataComboBoxColumn>                    
                    <dx:GridViewDataColumn FieldName="F_LINENM" Caption="라인명" Width="120px"  ReadOnly="true">
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="F_NGCODE" Name="F_NGCODE" Caption="가공불량항목" Width="200px">
                        <Settings AllowSort="True" />             
                        <CellStyle HorizontalAlign="Left"            />
                    </dx:GridViewDataComboBoxColumn>                    
                    <dx:GridViewDataSpinEditColumn FieldName="F_SORTNO" Caption="순번" Name="F_SEQNO" Width="40px">
                        <PropertiesSpinEdit NumberType="Integer" MinValue="0" MaxValue="99999" SpinButtons-ShowIncrementButtons="false" >
                            <SpinButtons ShowIncrementButtons="False"></SpinButtons>
                        </PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>         
                    <dx:GridViewDataCheckColumn FieldName="F_STATUS" Caption="사용" Width="60px" />           
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
