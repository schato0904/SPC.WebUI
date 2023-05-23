<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/controls/MasterPage/spcMasterPage.Master" AutoEventWireup="true" CodeBehind="PFRC0202.aspx.cs" Inherits="SPC.WebUI.Pages.PFRC.PFRC0202" %>
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
                    fn_doSetGridEventAction('true');
                    // 페이저 Callback
                    fn_pagerPerformCallback(s.cpResultMsg);

                    fn_doSetGridEventAction('false');
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
            fn_OnBatchValidate("F_MODULE1", s, e);
            fn_OnBatchValidate("F_MODULE2", s, e);
            fn_OnBatchValidate("F_PGMID", s, e);
            if (parseInt(e.visibleIndex, 10) >= 0) {
                fn_OnBatchValidate("F_SEQNO", s, e);
            }
        }

        // 대분류 선택값이 바뀌는 경우
        function fn_OnModule1SelectedIndexChanged(s, e) {
            txtMODULE1.SetText(s.GetValue());
            ddlMODULE2.PerformCallback(s.GetValue());
        }

        // 중분류 선택값이 바뀌는 경우
        function fn_OnModule2SelectedIndexChanged(s, e) {
            txtMODULE2.SetText(s.GetValue());
        }

        // Edit Form 의 대분류 선택값이 바뀌는 경우
        function fn_OnMODULE1EditorSelectedIndexChanged(s, e) {
            ddlMODULE2Edit.PerformCallback(s.GetValue() + '|');
        }

        // BatchEditStartEditing
        function fn_OnBatchEditStartEditing(s, e) {
            if (parseInt(e.visibleIndex, 10) >= 0) {
                var P_MODULE1 = devGrid.batchEditApi.GetCellValue(e.visibleIndex, 'F_MODULE1');
                var P_MODULE2 = devGrid.batchEditApi.GetCellValue(e.visibleIndex, 'F_MODULE2');
                var oParam = P_MODULE1 + '|' + P_MODULE2;
                ddlMODULE2Edit.PerformCallback(oParam);

                var editor = s.GetEditor('F_SEQNO');
                fn_OnControlEnableBox(editor, null);
            } else {
                var editor = s.GetEditor('F_SEQNO');
                fn_OnControlDisableBox(editor, null);
            }
        }

        // BatchEditEndEditing
        function fn_OnBatchEditEndEditing(s, e) {
            var subColumn = s.GetColumnByField("F_MODULE2");
            if (!e.rowValues.hasOwnProperty(subColumn.index))
                return;
            var cellInfo = e.rowValues[subColumn.index];
            if (ddlMODULE2Edit.GetSelectedIndex() > -1 || cellInfo.text != ddlMODULE2Edit.GetText()) {
                cellInfo.value = ddlMODULE2Edit.GetValue();
                cellInfo.text = ddlMODULE2Edit.GetText();
                ddlMODULE2Edit.SetValue(null);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container">
        <div class="search">
            <div class="form-horizontal bg-white fa-border" style="margin-bottom: 10px; border: 1px solid #9F9F9F;">
                <div class="form-group">
                    <label class="col-sm-2 control-label">대분류</label>
                    <div class="col-sm-3 control-label">
                        <dx:ASPxTextBox ID="txtMODULE1" ClientInstanceName="txtMODULE1" runat="server" ClientVisible ="false" />
                        <dx:ASPxComboBox ID="ddlMODULE1" ClientInstanceName="ddlMODULE1" runat="server" Width="100%"
                            OnDataBound="ddlComboBox_DataBound"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <ClientSideEvents Init="fn_OnControlDisable" SelectedIndexChanged="fn_OnModule1SelectedIndexChanged" />
                        </dx:ASPxComboBox>
                    </div>
                    <label class="col-sm-2 control-label">중분류</label>
                    <div class="col-sm-3 control-label">
                        <dx:ASPxTextBox ID="txtMODULE2" ClientInstanceName="txtMODULE2" runat="server" ClientVisible="false" />
                        <dx:ASPxComboBox ID="ddlMODULE2" ClientInstanceName="ddlMODULE2" runat="server" Width="100%"
                            OnDataBound="ddlComboBox_DataBound" OnCallback="ddlMODULE2_Callback"
                            IncrementalFilteringMode="None" CssClass="NoXButton">
                            <ClientSideEvents Init="fn_OnControlDisable" SelectedIndexChanged="fn_OnModule2SelectedIndexChanged" />
                        </dx:ASPxComboBox>
                    </div>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxCheckBox ID="chkISADMIN" ClientInstanceName="chkISADMIN" runat="server" Text="관리자전용"></dx:ASPxCheckBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">프로그램ID</label>
                    <div class="col-sm-3 control-label">
                        <dx:ASPxTextBox ID="txtPGMID" ClientInstanceName="txtPGMID" runat="server" Width="100%"></dx:ASPxTextBox>
                    </div>
                    <label class="col-sm-2 control-label">사용여부</label>
                    <div class="col-sm-3">
                        <dx:ASPxRadioButtonList ID="rdoSTATUS" ClientInstanceName="rdoSTATUS" runat="server"
                            RepeatDirection="Horizontal" Paddings-Padding="0px" Border-BorderStyle="None" ValueField="CODE" TextField="TEXT">
                            <Items>
                                <dx:ListEditItem Text="전체" Value="" Selected="true" />
                                <dx:ListEditItem Text="사용함" Value="1" />
                                <dx:ListEditItem Text="사용안함" Value="0" />
                            </Items>
                        </dx:ASPxRadioButtonList>
                    </div>
                    <div class="col-sm-2 control-label">
                        <dx:ASPxCheckBox ID="chkISDEV" ClientInstanceName="chkISDEV" runat="server" Text="개발자전용"></dx:ASPxCheckBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="content">
            <dx:ASPxTextBox ID="hidGridAction" ClientInstanceName="hidGridAction" runat="server" ClientVisible="false" Text="false" />
            <dx:ASPxTextBox ID="hidGridColumnsWidth" ClientInstanceName="hidGridColumnsWidth" runat="server" ClientVisible="false" />
            <dx:ASPxGridView ID="devGrid" ClientInstanceName="devGrid" runat="server" AutoGenerateColumns="false" Width="100%"
                KeyFieldName="F_MODULE1;F_MODULE2;F_PGMID" EnableViewState="false" EnableRowsCache="false"
                OnBatchUpdate="devGrid_BatchUpdate" OnInitNewRow="devGrid_InitNewRow" OnCustomCallback="devGrid_CustomCallback"
                OnCustomColumnDisplayText="devGrid_CustomColumnDisplayText" OnCellEditorInitialize="devGrid_CellEditorInitialize">
                <Styles>
                    <Header Font-Bold="true" HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center" Font-Size="9pt" Wrap="False" CssClass="truncated" Cursor="pointer"></Cell>
                    <EditForm CssClass="bg-default"></EditForm>
                </Styles>
                <Settings VerticalScrollBarMode="Visible" ShowStatusBar="Visible" />
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
                    <dx:GridViewBandColumn Name="F_MODULE1BAND" Caption="대메뉴">
                        <Columns>
                            <dx:GridViewDataComboBoxColumn FieldName="F_MODULE1" Name="F_MODULE1" Caption="코드" Width="80px">
                                <PropertiesComboBox>
                                    <ClientSideEvents SelectedIndexChanged="fn_OnMODULE1EditorSelectedIndexChanged" />
                                </PropertiesComboBox>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="F_MODULENM1" Caption="명칭" Width="120px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="F_STATUS1" Caption="사용" Width="40px">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Name="F_MODULE1BAND" Caption="중메뉴">
                        <Columns>
                            <dx:GridViewDataComboBoxColumn FieldName="F_MODULE2" Name="F_MODULE2" Caption="중메뉴" Width="100px">
                                <EditItemTemplate>
                                    <dx:ASPxComboBox ID="ddlMODULE2Edit" ClientInstanceName="ddlMODULE2Edit" runat="server" Width="100%"
                                        OnCallback="ddlMODULE2Edit_Callback" OnDataBound="ddlMODULE2Edit_DataBound"
                                        EnableCallbackMode="true" IncrementalFilteringMode="None" CssClass="NoXButton">
                                        <ClientSideEvents Init="fn_OnControlDisable" />
                                    </dx:ASPxComboBox>
                                </EditItemTemplate>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="F_MODULENM2" Caption="명칭" Width="120px">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="F_STATUS2" Caption="사용" Width="40px">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataCheckColumn FieldName="F_STATUS" Caption="메뉴<br />사용" Width="60px" />
                    <dx:GridViewDataCheckColumn FieldName="F_ISADMIN" Caption="관리자<br />전용" Width="60px" />
                    <dx:GridViewDataCheckColumn FieldName="F_ISDEV" Caption="개발자<br />전용" Width="60px" />
                    <dx:GridViewDataSpinEditColumn FieldName="F_SEQNO" Caption="순번" Name="F_SEQNO" Width="40px">
                        <PropertiesSpinEdit NumberType="Integer" MinValue="0" MaxValue="99999" SpinButtons-ShowIncrementButtons="false" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewBandColumn Name="F_PGM" Caption="프로그램">
                        <Columns>
                            <dx:GridViewDataComboBoxColumn FieldName="F_PGMID" Name="F_PGMID" Caption="ID" Width="100px">
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="F_PGMNM" Caption="명칭" Width="100%">
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="F_LINK" Caption="경로" Width="100px">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="F_STATUS3" Caption="사용" Width="70px">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="F_TOOLBAR" Caption="기능" Width="100px">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="devGridExporter" runat="server" GridViewID="devGrid" OnRenderBrick="devGridExporter_RenderBrick"></dx:ASPxGridViewExporter>
            <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExport" runat="server" ClientVisible="false" OnClick="btnExport_Click"></dx:ASPxButton>
        </div>
        <div class="paging"></div>
    </div>
</asp:Content>
